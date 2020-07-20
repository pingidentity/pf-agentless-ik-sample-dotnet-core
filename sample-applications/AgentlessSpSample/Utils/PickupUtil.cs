using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace AgentlessSpSample.Utils
{
    public class PickupUtils
    {
        public static HttpResponseMessage pickUpAttributes(IConfiguration configuration, String referenceId)
        {
            HttpResponseMessage response = null;
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                // For simplicity, trust any certificate. Do not use in production.
                httpClientHandler.ServerCertificateCustomValidationCallback = (message,cert, chain, errors) => { return true; };

                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        string authenticationString = configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_USERNAME)
                                                        + ":" + configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_PASSWORD);
                        string base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
                        client.DefaultRequestHeaders.Add(SpConstants.PING_ADAPTER_HEADER,
                                                        configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_ID));

                        // Pick up the Attributes from PickFederate
                        response = client.GetAsync(UrlUtils.pickupUrl(configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL),
                                                                        referenceId)).Result;
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
            return response;
        }
    }
}
