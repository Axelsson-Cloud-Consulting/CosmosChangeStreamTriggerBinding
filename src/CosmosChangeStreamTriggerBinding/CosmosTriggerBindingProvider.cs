namespace CosmosChangeStreamTriggerBinding
{
    using Microsoft.Azure.WebJobs.Host.Triggers;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    internal sealed class CosmosTriggerBindingProvider : ITriggerBindingProvider
    {

        /// <summary>
        /// CosmosDbExtensionConfigProvider instance variable. Used to create the context.
        /// </summary>
        private CosmosExtensionConfigProvider _provider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="provider"><c>CosmosDbExtensionConfigProvider</c> instance</param>
        public CosmosTriggerBindingProvider(CosmosExtensionConfigProvider provider)
        {
            _provider = provider;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            var parameter = context.Parameter;
            var attribute = parameter.GetCustomAttribute<CosmosTriggerAttribute>(false);

            if (attribute == null) return Task.FromResult<ITriggerBinding>(null);
            if (parameter.ParameterType != typeof(string)) throw new InvalidOperationException("Invalid parameter type");

            var triggerContext = new CosmosTriggerContext(attribute);

            var triggerBinding = new CosmosTriggerBinding(triggerContext);

            return Task.FromResult<ITriggerBinding>(triggerBinding);
        }
    }
}
