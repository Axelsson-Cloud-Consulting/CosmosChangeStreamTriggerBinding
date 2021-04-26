namespace CosmosChangeStreamTriggerBinding.Model
{
    public class Id
    {
        public string _data { get; set; }
        public int _kind { get; set; }
    }

    public class Ns
    {
        public string db { get; set; }
        public string coll { get; set; }
    }

    public class DocumentKey
    {
        public string _id { get; set; }
    }

    public class Document
    {
        public Id _id { get; set; }
        public object fullDocument { get; set; }
        public Ns ns { get; set; }
        public DocumentKey documentKey { get; set; }
    }
}
