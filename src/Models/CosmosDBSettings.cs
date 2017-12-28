namespace loadtesting.Models
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbSettings
    {
        public CosmosDbSettings(IConfiguration configuration)
        {
            try
            {
                DatabaseName = configuration.GetSection("DatabaseName").Value;
                CollectionName = configuration.GetSection("CollectionName").Value;
                DatabaseUri = new Uri($"https://{configuration.GetSection("Account").Value}.documents.azure.com:443/");
                DatabaseKey = configuration.GetSection("Key").Value;
            }
            catch
            {
                throw new MissingFieldException("IConfiguration missing a valid Azure Cosmos DB field appsettings.json");
            }
        }
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }
        public Uri DatabaseUri { get; private set; }
        public string DatabaseKey { get; private set; }
    }
}
