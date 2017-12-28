namespace loadtesting.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Service to map a dynamic document and provide an instance to load test
    /// </summary>
    public class TestDocumentService : ITestDocumentService
    {
        private readonly JObject _baseObject;

        /// <summary>
        /// Creates a service and tries to parse a sample document
        /// </summary>
        public TestDocumentService()
        {
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly.GetManifestResourceStream("loadtesting.testitem.json");
            if(resourceStream == null)
            {
                throw new KeyNotFoundException("Missing 'testitem.json' file.");
            }

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                string testItemContent = reader.ReadToEnd();
                try
                {
                    this._baseObject = JObject.Parse(testItemContent);
                    if (this._baseObject.TryGetValue("id", out JToken idValue))
                    {
                        this._baseObject.Remove("id");
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns the parsed sample document
        /// </summary>
        /// <returns>Parsed <see cref="JObject"/> document</returns>
        public JObject GetDocument()
        {
            return this._baseObject;
        }
    }
}
