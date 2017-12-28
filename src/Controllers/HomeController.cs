namespace loadtesting.Controllers
{
    using loadtesting.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly ITestDocumentService _testDocumentService;
        public HomeController(ITestDocumentService testDocumentService)
        {
            this._testDocumentService = testDocumentService;
        }

        public ActionResult Index()
        {

            return View("Index", _testDocumentService.GetDocument().ToString(Newtonsoft.Json.Formatting.Indented));
        }
    }
}