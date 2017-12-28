using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;

namespace loadtesting.Services
{
    public class TestDocumentService : ITestDocumentService
    {
        private readonly JObject _baseObject;
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

        public JObject GetDocument()
        {
            return this._baseObject;
        }
    }
}
