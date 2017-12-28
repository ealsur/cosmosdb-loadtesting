namespace loadtesting
{
    using loadtesting.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Linq;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DocumentDbService documentDbService = new DocumentDbService(Configuration.GetSection("CosmosDb"));
            // Store a test document for read load testing
            TestDocumentService testDocumentService = new TestDocumentService();
            JObject toRead = (JObject)testDocumentService.GetDocument().DeepClone();
            toRead.Add("id", Constants.IdForReadTesting);
            documentDbService.UpsertItemAsync(toRead).Wait();
            services.AddSingleton<IDocumentDbService>(documentDbService);
            services.AddSingleton<ITestDocumentService>(testDocumentService);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
