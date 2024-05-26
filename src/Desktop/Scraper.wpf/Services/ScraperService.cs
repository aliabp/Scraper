using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Scraper.wpf.Models;
using System.Configuration;
using Newtonsoft.Json;

namespace Scraper.wpf.Services;

/*
 * Call Scraper API
 * Manage API response
 */
public class ScraperService
{
    public ScraperService()
    {
    }

    public async Task<string> SendRequestToApi(ScraperModel model)
    {
        try
        {
            HttpClientHandler handler = GetHttpClientHandler();

            using (HttpClient client = new HttpClient(handler))
            {
                // retrive API base URL from config file
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // serialize model object as Json string
                string jsonString = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Call scraper API
                HttpResponseMessage response = await client.PostAsync("api/v1/Scraper", content);

                // in case of unsuccessful response code, handle response
                if (!response.IsSuccessStatusCode)
                    return "URL not found!";

                // return response as string
                return await response.Content.ReadAsStringAsync();
            }
        }
        catch (HttpRequestException hre)
        {
            throw new Exception($"Request error: {hre.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    private static HttpClientHandler GetHttpClientHandler()
    {
        // ignore ssl in development. only should use in development
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };
    }
}