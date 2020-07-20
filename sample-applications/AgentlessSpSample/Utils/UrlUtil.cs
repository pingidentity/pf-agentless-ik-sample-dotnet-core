using Microsoft.AspNetCore.Http;
using System.Net;


namespace AgentlessSpSample.Utils
{
    public class UrlUtils
    {
        public static string pickupUrl(string basePfUrl, string referenceId)
        {
            return basePfUrl + SpConstants.PICKUP_ENDPOINT + "?REF=" + referenceId;
        }

        public static string configureUrl(HttpRequest request)
        {
            return getHttp(request) + request.Host.ToString() + "/" + SpConstants.AGENTLESS_BASE + "/configuration"; 
        }

        public static string applicationSsoUrl(HttpRequest request)
        {
            return getHttp(request) + request.Host.ToString() + "/" + SpConstants.AGENTLESS_BASE;
        }

        public static string resumeLogoutUrl(string currentBaseUrl, string resumePath, string referenceId)
        {
            return currentBaseUrl + resumePath + "?REF=" + referenceId;
        }

        public static string ssoUrl(string basePfUrl, string parterId)
        {
            return basePfUrl + SpConstants.IDP_SSO_ENDPOINT + "?PartnerSpId=" + parterId;
        }

        public static string sloUrl(string basePfUrl, HttpRequest request)
        {
            return basePfUrl + SpConstants.SP_SLO_ENDPOINT + "?TargetResource=" 
            + WebUtility.UrlEncode(getHttp(request) + request.Host.ToString() + "/" + SpConstants.AGENTLESS_BASE + "/loggedout");
        }

        private static string getHttp(HttpRequest request)
        {
            return request.IsHttps ? "https://" : "http://";
        }
    }
}