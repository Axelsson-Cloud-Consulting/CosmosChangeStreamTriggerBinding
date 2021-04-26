namespace CosmosChangeStreamTriggerBinding
{
    using Microsoft.Azure.WebJobs.Host.Executors;
    using Microsoft.Azure.WebJobs.Host.Listeners;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CosmosListener : IListener
    {
        private readonly CosmosTriggerContext _context;
        private readonly ITriggeredFunctionExecutor _executor;
        private System.Collections.Generic.IEnumerator<BsonDocument> _enumerator;

        public CosmosListener(CosmosTriggerContext context, ITriggeredFunctionExecutor executor)
        {
            _context = context;
            _executor = executor;

            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<BsonDocument>>()
                .Match(change => change.OperationType == ChangeStreamOperationType.Insert || change.OperationType == ChangeStreamOperationType.Update || change.OperationType == ChangeStreamOperationType.Replace)
                .AppendStage<ChangeStreamDocument<BsonDocument>, ChangeStreamDocument<BsonDocument>, BsonDocument>(
                "{ $project: { '_id': 1, 'fullDocument': 1, 'ns': 1, 'documentKey': 1 }}");

            var options = new ChangeStreamOptions
            {
                FullDocument = ChangeStreamFullDocumentOption.UpdateLookup
            };

            var client = new MongoClient(_context.TriggerAttribute.GetConnectionString());
            var db = client.GetDatabase(_context.TriggerAttribute.GetDatabaseName());
            var coll = db.GetCollection<BsonDocument>(_context.TriggerAttribute.GetCollectionName());
            _enumerator = coll.Watch(pipeline, options).ToEnumerable().GetEnumerator();
        }

        /// <summary>
        ///  Dispose method
        /// </summary>
        public void Dispose()
        {
            _enumerator.Dispose();
        }

        /// <summary>
        /// Start the listener for insert, update or replace events and wait for event.
        /// When a message is received, execute the function.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A Task returned from the stream feed</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            var task = Task.Run(async () =>
            {
                await ProcessMethod(ct);
            }, tokenSource2.Token);

            return task;
        }

        /// <summary>
        /// Stop current operation
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken ct)
        {
            var tokenSource2 = new CancellationTokenSource();
            ct = tokenSource2.Token;
            tokenSource2.Cancel();
            return Task.Run(async () => {
                await ProcessMethod(ct);
            });
        }

        public async Task ProcessMethod(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            while (true)
            {
                try
                {
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }

                    while (_enumerator.MoveNext())
                    {
                        var dotNetObj = BsonTypeMapper.MapToDotNetValue(_enumerator.Current);
                        var triggerData = new TriggeredFunctionData
                        {
                            TriggerValue = JsonConvert.SerializeObject(dotNetObj)
                        };

                        var task = _executor.TryExecuteAsync(triggerData, CancellationToken.None);
                        task.Wait();
                    }

                    _enumerator.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        void IListener.Cancel()
        {
            _enumerator.Reset();
        }
    }
}
