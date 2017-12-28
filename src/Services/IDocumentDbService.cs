namespace loadtesting.Services
{
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public interface IDocumentDbService
    {
        Task AddItemAsync(JObject item);
        Task ReadItemAsync(string id);
    }
}
