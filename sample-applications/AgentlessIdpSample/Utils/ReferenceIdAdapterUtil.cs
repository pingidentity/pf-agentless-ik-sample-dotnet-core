using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace AgentlessIdpSample.Utils
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
            string authenticationString = configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_USERNAME)
                                                        + ":" + configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_PASSWORD);
            return "GET " + UrlUtils.pickupUrl(configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL), referenceId) + "\n"
                    + IdpConstants.PING_ADAPTER_HEADER + ": " + configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_ID) + "\n"
                    + "Authorization " + Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
        }

        public static string dropoffPost(IConfiguration configuration, string userAttributes)
        {
            string authenticationString = configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_USERNAME)
                                                        + ":" + configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_PASSWORD);
            return "POST " + UrlUtils.dropoffUrl(configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL)) + "\n"
                    + "Content-Type: application/json" + "\n"
                    + "Content-Length: " + userAttributes.Length + "\n"
                    + IdpConstants.PING_ADAPTER_HEADER + ": " + configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_ID) + "\n"
                    + "Authorization " + Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString)) + "\n"
                    + userAttributes;
        }
    }
}