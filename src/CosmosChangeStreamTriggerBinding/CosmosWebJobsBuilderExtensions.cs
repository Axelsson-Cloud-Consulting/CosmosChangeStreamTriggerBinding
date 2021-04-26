namespace CosmosChangeStreamTriggerBinding
{
    using Microsoft.Azure.WebJobs;
    using System;

    /// <summary>
    /// WebJobBuilder extension to add extensions
    /// </summary>
    public static class CosmosWebJobsBuilderExtensions
    {
        /// <summary>
        /// Extension method to add our custom extension
        /// </summary>
        /// <param name="builder"><c>IWebJobsBuilder</c> instance</param>
        /// <returns><c>IWebJobsBuilder</c> instance</returns>
        /// <exception>Throws ArgumentNullException if builder is null</exception>
        public static IWebJobsBuilder AddCosmosStreamFeedExtension(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<CosmosExtensionConfigProvider>();

            return builder;
        }
    }
}
