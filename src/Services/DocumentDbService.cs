namespace loadtesting.Services
{
    using System;
    using System.Threading.Tasks;
    using loadtesting.Models;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    public class DocumentDbService : IDocumentDbService
    {
        private readonly CosmosDbSettings _settings;
        private readonly Uri _collectionUri;
        private DocumentClient _dbClient;

        public DocumentDbService(IConfigurationSection configuration)
        {
            _settings = new CosmosDbSettings(configuration);
            _collectionUri = GetCollectionLink();
            //See https://docs.microsoft.com/en-us/azure/cosmos-db/performance-tips for performance tips
            _dbClient = new DocumentClient(_settings.DatabaseUri, _settings.DatabaseKey, new ConnectionPolicy() {
                MaxConnectionLimit = 1000,
                RetryOptions = new RetryOptions()
                {
                    MaxRetryAttemptsOnThrottledRequests = 0
                }
            });
        }
        
        public async Task AddItemAsync(JObject item)
        {
            await _dbClient.CreateDocumentAsync(_collectionUri, item);
        }

        public async Task ReadItemAsync(string id)
        {
            await _dbClient.ReadDocumentAsync(GetDocumentLink(id));
        }

        public async Task UpsertItemAsync(JObject item)
        {
            await _dbClient.UpsertDocumentAsync(_collectionUri, item);
        }

        private Uri GetCollectionLink()
        {
            return UriFactory.CreateDocumentCollectionUri(_settings.DatabaseName, _settings.CollectionName);
        }

        private Uri GetDocumentLink(string id)
        {
            return UriFactory.CreateDocumentUri(_settings.DatabaseName, _settings.CollectionName, id);
        }
    }
}
