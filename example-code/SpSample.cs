using System;
using System.Net.Http;
public class IdpSample
{
    public static void Main(string[] args)
    {
        string referenceValue = "<INSERT REFERENCE VALUE HERE>";

        HttpResponseMessage response = null;
        using (HttpClientHandler httpClientHandler = new HttpClientHandler())
        {
            // For simplicity, trust any certificate. Do not use in production.
            httpClientHandler.ServerCertificateCustomValidationCallback = (message,cert, chain, errors) => { return true; };

            string pickupLocation = "https://localhost:9031/ext/ref/pickup?REF=" + referenceValue;

            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("ping.uname","changeme");
                    client.DefaultRequestHeaders.Add("ping.pwd","please change me before you go into production!");
                    client.DefaultRequestHeaders.Add("ping.instanceId","spadapter");

                    // Drop the attributes into PingFederate
                    response = client.GetAsync(pickupLocation ,content).Result;
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
