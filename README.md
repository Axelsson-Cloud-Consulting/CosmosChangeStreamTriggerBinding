# CosmosChangeStreamTriggerBinding
Azure Function trigger binding that listens to Azure Cosmos DB API for MongoDB's change stream API and triggers an execution on insert, update or replace events. (Requires Azure Cosmos DB's API for MongoDB ver. 3.6+)

## Releases
Azure Cosmos DB MongoDB API change stream trigger binding for Azure Functions is released as a NuGet package.
* Download the [NuGet package](https://www.nuget.org/packages/Cosmos.ChangeStream.TriggerBinding/).
* See [Release notes](https://raw.githubusercontent.com/Axelsson-Cloud-Consulting/CosmosChangeStreamTriggerBinding/main/changelog.md).

## Usage
### The workflow
- Install package using Install-Package Cosmos.ChangeStream.TriggerBinding -Version 1.0.0-beta
- Set environment variables and specify their keys in the trigger attribute, i.e. CosmosDBConnectionString and CosmosDBDatabase.

### Example
```csharp
using CosmosChangeStreamTriggerBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CosmosDbMongoDbAPITriggeredFunctionApp
{
    public static class ProcessDocument
    {
        public static async Task RunAsync(
            [CosmosDbCustomTrigger(
            Connection = "CosmosDBConnectionString",
            DatabaseName = "CosmosDBDatabase",
            CollectionName = "CosmosDBCollection")] string document,
            ILogger log)
        {
            try
            {
                var docObject = JsonConvert.DeserializeObject<CosmosChangeStreamTriggerBinding.Model.Document>(document);
            }
            catch (Exception ex)
            {
                log.LogCritical(ex.Message + Environment.NewLine + "Stack trace was:" + Environment.NewLine + ex.StackTrace.ToString() +
                    Environment.NewLine + Environment.NewLine + "Inner exception was:" + Environment.NewLine + ex.InnerException.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
```

