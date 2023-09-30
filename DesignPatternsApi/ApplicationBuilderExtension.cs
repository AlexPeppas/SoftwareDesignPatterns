namespace DesignPatternsApi
{
    public static class ApplicationBuilderExtension
    {
        public static WebApplication UseExperimentRouteBuilder(this WebApplication application)
        {
            ExperimentRouteBuilder.Create(application)
                .WithHealthCheck()
                .Build();

            return application;
        }
    }
}
