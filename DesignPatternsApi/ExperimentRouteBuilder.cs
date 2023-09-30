using System.Net.Mime;

namespace DesignPatternsApi
{
    public sealed class ExperimentRouteBuilder
    {
        private readonly IApplicationBuilder builder;
        private readonly ILogger<ExperimentRouteBuilder> logger;
        private readonly IRouteBuilder routeBuilder;

        private ExperimentRouteBuilder(WebApplication webApplication)
        {
            this.builder = webApplication;
            this.logger = webApplication.Services.GetRequiredService<ILogger<ExperimentRouteBuilder>>();
            this.routeBuilder = new RouteBuilder(webApplication);
        }

        public static ExperimentRouteBuilder Create(WebApplication webApplication) 
        {
            return new(webApplication);
        }

        public ExperimentRouteBuilder WithHealthCheck()
        {
            routeBuilder.MapGet("experiment/healthcheck/ping", async (context) =>
            {
                var request = context.Request;
                var response = context.Response;
                var aborted = context.RequestAborted;

                response.StatusCode = 200;
                response.ContentType = MediaTypeNames.Text.Plain;

                await response.WriteAsync(DateTime.UtcNow.ToString("o"));
            });
            return this;
        }

        public void Build()
        {
            var router = this.routeBuilder.Build();
            this.builder.UseRouter(router);
        }
    }
}
