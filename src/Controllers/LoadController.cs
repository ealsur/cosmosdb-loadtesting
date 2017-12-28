namespace loadtesting.Controllers
{
    using System;
    using System.Threading.Tasks;
    using loadtesting.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class LoadController : Controller
    {
        //private static RootObject toStore = new RootObject()
        //{
        //    sensors = new Sensors(),
        //    devicestate = new Devicestate()
        //    {
        //        hvacstate = new Hvacstate(),
        //        indoorairquality = new Indoorairquality(),
        //        energysavings = new Energysavings()
        //    },
        //    createddate = DateTime.UtcNow,
        //    modifieddate = DateTime.UtcNow
        //};

        private readonly IDocumentDbService _documentDbService;
        private readonly ITestDocumentService _testDocumentService;
        public LoadController(IDocumentDbService documentDbService, ITestDocumentService testDocumentService)
        {
            _documentDbService = documentDbService;
            _testDocumentService = testDocumentService;

        }

        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            await _documentDbService.AddItemAsync(_testDocumentService.GetDocument());
            return Ok();
        }

        [ActionName("Read")]
        public async Task<ActionResult> ReadAsync()
        {
            await _documentDbService.ReadItemAsync(Constants.IdForReadTesting);
            return Ok();
        }
    }
}