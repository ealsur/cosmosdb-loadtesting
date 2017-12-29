namespace loadtesting.Controllers
{
    using System.Threading.Tasks;
    using loadtesting.Services;
    using Microsoft.AspNetCore.Mvc;

    public class LoadController : Controller
    {
        private readonly IDocumentDbService _documentDbService;
        private readonly ITestDocumentService _testDocumentService;

        public LoadController(IDocumentDbService documentDbService, ITestDocumentService testDocumentService)
        {
            this._documentDbService = documentDbService;
            this._testDocumentService = testDocumentService;

        }

        [ActionName("Write")]
        public async Task<ActionResult> CreateAsync()
        {
            await this._documentDbService.AddItemAsync(this._testDocumentService.GetDocument());
            return Ok();
        }

        [ActionName("Read")]
        public async Task<ActionResult> ReadAsync()
        {
            await this._documentDbService.ReadItemAsync(Constants.IdForReadTesting);
            return Ok();
        }
    }
}