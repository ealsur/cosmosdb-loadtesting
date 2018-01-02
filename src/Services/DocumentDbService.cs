namespace loadtesting.Services
{
    using System;
    using System.Threading.Tasks;
    using loadtesting.Models;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Singleton document service
    /// </summary>
    public class DocumentDbService : IDocumentDbService
    {
        private readonly CosmosDbSettings _settings;
        private readonly Uri _collectionUri;
        private DocumentClient _dbClient;

        /// <summary>
        /// Initializes a <see cref="DocumentClient"/> with certain <see cref="ConnectionPolicy"/> settings for load testing
        /// </summary>
        /// <param name="configuration"></param>
        public DocumentDbService(IConfigurationSection configuration)
        {
            _settings = new CosmosDbSettings(configuration);
            _collectionUri = GetCollectionLink();
            //See https://docs.microsoft.com/en-us/azure/cosmos-db/performance-tips for performance tips
            _dbClient = new DocumentClient(_settings.DatabaseUri, _settings.DatabaseKey, new ConnectionPolicy() {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp,
                RetryOptions = new RetryOptions()
                {
                    // Forces errors when reaching Throughput limit to visualize in tests
                    MaxRetryAttemptsOnThrottledRequests = 0
                }
            });
        }
        
        /// <summary>
        /// Adds a document to the collection
        /// </summary>
        /// <param name="item">Document to store</param>
        /// <returns></returns>
        public async Task AddItemAsync(JObject item)
        {
            await _dbClient.CreateDocumentAsync(_collectionUri, item);
        }

        /// <summary>
        /// Reads a document from the collection
        /// </summary>
        /// <param name="id">Id of the document</param>
        /// <returns></returns>
        public async Task ReadItemAsync(string id)
        {
            await _dbClient.ReadDocumentAsync(GetDocumentLink(id));
        }

        /// <summary>
        /// Upserts a document to the collection
        /// </summary>
        /// <param name="item">Document to insert or update</param>
        /// <returns></returns>
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
