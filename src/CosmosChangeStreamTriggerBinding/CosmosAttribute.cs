namespace CosmosChangeStreamTriggerBinding
{
    using Microsoft.Azure.WebJobs.Description;
    using System;

    /// <summary>
    /// <c>Attribute</c> class for Trigger
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class CosmosTriggerAttribute : Attribute, ICosmosTriggerAttribute
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        internal string GetConnectionString()
        {
            return Environment.GetEnvironmentVariable(Connection);
        }
        internal string GetDatabaseName()
        {
            return Environment.GetEnvironmentVariable(DatabaseName);
        }
        internal string GetCollectionName()
        {
            return Environment.GetEnvironmentVariable(CollectionName);
        }
    }
}
