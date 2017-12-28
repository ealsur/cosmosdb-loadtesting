namespace loadtesting.Services
{
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Singleton document service
    /// </summary>
    public interface IDocumentDbService
    {
        /// <summary>
        /// Adds a document to the collection
        /// </summary>
        /// <param name="item">Document to store</param>
        /// <returns></returns>
        Task AddItemAsync(JObject item);

        /// <summary>
        /// Reads a document from the collection
        /// </summary>
        /// <param name="id">Id of the document</param>
        /// <returns></returns>
        Task ReadItemAsync(string id);
    }
}
