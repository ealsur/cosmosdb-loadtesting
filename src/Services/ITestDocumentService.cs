namespace loadtesting.Services
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Service to map a dynamic document and provide an instance to load test
    /// </summary>
    public interface ITestDocumentService
    {
        /// <summary>
        /// Returns the parsed sample document
        /// </summary>
        /// <returns>Parsed <see cref="JObject"/> document</returns>
        JObject GetDocument();
    }
}