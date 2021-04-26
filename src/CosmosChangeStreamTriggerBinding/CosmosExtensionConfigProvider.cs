namespace CosmosChangeStreamTriggerBinding
{
    using Microsoft.Azure.WebJobs.Description;
    using Microsoft.Azure.WebJobs.Host.Config;

    [Extension("CosmosChangeStreamTriggerBinding")]
    public class CosmosExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            // Add trigger binding rule
            var triggerRule = context.AddBindingRule<CosmosTriggerAttribute>();
            triggerRule.BindToTrigger(new CosmosTriggerBindingProvider(this));
        }
    }
}
