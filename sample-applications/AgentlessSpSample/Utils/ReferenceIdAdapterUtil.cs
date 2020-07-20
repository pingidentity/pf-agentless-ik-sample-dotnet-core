using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace AgentlessSpSample.Utils
{

    public static class ReferenceIdAdapterUtil
    {
        public static string sessionResponse(System.Net.Http.Headers.HttpHeaders header, object response)
        {
            return header.ToString() + "\n" + response;
        }

        public static string httpStatus(HttpStatusCode status)
        {
            if (status == HttpStatusCode.OK)
            {
                return "HTTP Status: 200 OK";
            }
            else
            {
                return "HTTP Status: " + status.ToString();
            }
        }

        public static string pickupRequest(IConfiguration configuration, string referenceId)
        {
            string authenticationString = configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_USERNAME)
                                                        + ":" + configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_PASSWORD);
            return "GET " + UrlUtils.pickupUrl(configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL), referenceId) + "\n"
                    + "ping.instanceId: spAdapter" + "\n"
                    + "Authorization " + Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
        }
    }
}