using Microsoft.AspNetCore.Http;
using System.Net;

namespace AgentlessIdpSample.Utils
{
    public class UrlUtils
    {
        public static string pickupUrl(string basePfUrl, string referenceId)
        {
            return basePfUrl + IdpConstants.PICKUP_ENDPOINT + "?REF=" + referenceId;
        }

        public static string dropoffUrl(string basePfUrl)
        {
            return basePfUrl + IdpConstants.DROPOFF_ENDPOINT;
        }

        public static string ssoUrl(string basePfUrl, string partnerId)
        {
            return basePfUrl + IdpConstants.START_SP_SSO + "?PartnerIdpId=" + partnerId;
        }

        public static string resumeToPf(string basePfUrl, string resumePath, string referenceId, string targetUrl)
        {
            return basePfUrl + resumePath + "?REF=" + referenceId + "&TargetResource=" + WebUtility.UrlEncode(targetUrl);
        }

        public static string loginUrl(HttpRequest request)
        {
            return getHttp(request) + request.Host.ToString() + "/" + IdpConstants.AGENTLESS_BASE + "/login";
        }

        public static string configureUrl(HttpRequest request)
        {
            return getHttp(request) + request.Host.ToString() + "/" + IdpConstants.AGENTLESS_BASE + "/configuration"; 
        }

        public static string resumeUrl(HttpRequest request)
        {
            return getHttp(request) + request.Host.ToString() + "/" + IdpConstants.AGENTLESS_BASE + "/resume";
        }

        public static string resumeLogoutUrl(string baseUrl, string resumePath, string referenceId)
        {
            return baseUrl + resumePath + "?REF=" + referenceId;
        }

        private static string getHttp(HttpRequest request)
        {
            return request.IsHttps ? "https://" : "http://";
        }
    }
}