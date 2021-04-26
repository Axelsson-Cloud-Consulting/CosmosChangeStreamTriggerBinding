The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

### <a name="1.0.0"/> [1.0.0](https://www.nuget.org/packages/CosmosChangeStreamTriggerBinding/1.0.0) - 2021-04-26

- General availability of [Version 1.0.0](https://www.nuget.org/packages/CosmosChangeStreamTriggerBinding/1.0.0) of the Azure Function trigger binding for Azure Cosmos DB API for MongoDB.
- Implements [this example](https://docs.microsoft.com/en-us/azure/cosmos-db/mongodb-change-streams?tabs=csharp#examples) as a custom Azure Function trigger binding for ease of use.
- Takes connection string, collection name and database name as environment variables in the trigger attribute.
- Targets .NET Standard 2.1, which supports .NET Core 3.0+ and .NET 5+.