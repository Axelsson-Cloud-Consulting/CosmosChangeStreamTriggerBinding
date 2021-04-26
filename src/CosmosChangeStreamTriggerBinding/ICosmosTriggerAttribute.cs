namespace CosmosChangeStreamTriggerBinding
{
    public interface ICosmosTriggerAttribute
    {
        string CollectionName { get; set; }
        string Connection { get; set; }
        string DatabaseName { get; set; }
    }
}