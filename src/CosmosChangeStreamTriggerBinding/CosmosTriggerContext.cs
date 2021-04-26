namespace CosmosChangeStreamTriggerBinding
{
    /// <summary>
    /// Trigger context class
    /// </summary>
    public class CosmosTriggerContext
    {
        /// <summary>
        /// <c>Attribute</c> instance
        /// </summary>
        public CosmosTriggerAttribute TriggerAttribute;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attribute">Attribute instnace</param>
        public CosmosTriggerContext(
            CosmosTriggerAttribute attribute
            )
        {
            TriggerAttribute = attribute;
        }
    }
}
