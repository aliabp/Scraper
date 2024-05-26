using Microsoft.AspNetCore.Mvc.Testing;
using Scraper.api.Controllers;
using Scraper.api.Services;

namespace Scraper.api.test;

/*
 * mock scraper service inject by ScraperControllerFixture class
 * ScrapeAndParseAsync mocked by moq for integration tests
 */
public class ScraperControllerFixture : WebApplicationFactory<ScraperController>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Add mock services for scraper service
            var scraperServiceMock = new Mock<IScraperService>();

            scraperServiceMock.Setup(service => service.ScrapeAndParseAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<int> { 1, 2, 3 });

            // Remove all registrations of IScraperService.
            var descriptors = services.Where(d => d.ServiceType == typeof(IScraperService)).ToList();
            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            // Add the mock ScraperService registration.
            services.AddTransient(_ => scraperServiceMock.Object);
        });
    }
}

