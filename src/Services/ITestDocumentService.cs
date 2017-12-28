using Newtonsoft.Json.Linq;

namespace loadtesting.Services
{
    public interface ITestDocumentService
    {
        JObject GetDocument();
    }
}