using CosmosChangeStreamTriggerBinding;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(CosmosBinding.Startup))]
namespace CosmosChangeStreamTriggerBinding
{
    /// <summary>
    /// Startup object
    /// </summary>
    public class CosmosBinding
    {
        /// <summary>
        /// IWebJobsStartup startup class
        /// </summary>
        public class Startup : IWebJobsStartup
        {
            public void Configure(IWebJobsBuilder builder)
            {
                builder.AddCosmosStreamFeedExtension();
            }
        }
    }
}
