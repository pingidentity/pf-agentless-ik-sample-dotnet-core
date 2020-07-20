using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

public class IdpSample
{
    public static void Main(string[] args)
    {
        string userAttributes ="{\"attribute1\": \"value1\"}";

        HttpResponseMessage response = null;
        using (HttpClientHandler httpClientHandler = new HttpClientHandler())
        {
            // For simplicity, trust any certificate. Do not use in production.
            httpClientHandler.ServerCertificateCustomValidationCallback = (message,cert, chain, errors) => { return true; };

            dropoffLocation = "https://localhost:9031/ext/ref/dropoff";

            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("ping.uname","changeme");
                    client.DefaultRequestHeaders.Add("ping.pwd","please change me before you go into production!");
                    client.DefaultRequestHeaders.Add("ping.instanceId","idpadapter");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(userAttributes, Encoding.UTF8, "application/json");

                    // Drop the attributes into PingFederate
                    response = client.PostAsync(dropoffLocation,content).Result;
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    // proper logging here is omitted for simplicity
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }

        string responseBody = response.Content.ReadAsStringAsync().Result;
    }
}
