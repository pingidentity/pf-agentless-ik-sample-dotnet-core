using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;


namespace AgentlessIdpSample.Utils
{
    public class PickupUtils
    {
        public static HttpResponseMessage pickUpAttributes(IConfiguration configuration, string referenceId)
        {
            HttpResponseMessage response = null;
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                // For simplicity, trust any certificate. Do not use in production.
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        string authenticationString = configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_USERNAME)
                                                        + ":" + configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_PASSWORD);
                        string base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                        client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
                        client.DefaultRequestHeaders.Add(IdpConstants.PING_ADAPTER_HEADER,
                                                        configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_ID));

                        // Pick up the Attributes from PickFederate
                        response = client.GetAsync(UrlUtils.pickupUrl(configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
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
