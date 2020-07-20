using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using AgentlessIdpSample.Utils;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;


namespace AgentlessIdpSample.Controllers
{
    public class AppController : Controller
    {
        private IConfiguration _configuration;
        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            if (Request.Method == "POST")
            {
                String referenceId = Request.Form[IdpConstants.REF];
                HttpResponseMessage response = Utils.PickupUtils.pickUpAttributes(_configuration, referenceId);

                String responseBody = response.Content.ReadAsStringAsync().Result;
                dynamic responseBodyJson = JsonConvert.DeserializeObject<dynamic>(responseBody);

                ViewData["resumePath"] = Request.Form[IdpConstants.RESUME_PATH];
                ViewData["REF"] = referenceId;
                ViewData["responseBody"] = responseBodyJson;
                ViewData["pickupEndpoint"] = IdpConstants.PICKUP_ENDPOINT;
                ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                ViewData["loginUrl"] = UrlUtils.loginUrl(Request);
                ViewData["httpStatus"] = ReferenceIdAdapterUtil.httpStatus(response.StatusCode);
                ViewData["rawRequest"] = ReferenceIdAdapterUtil.pickupRequest(_configuration, referenceId);
                ViewData["rawResponse"] = ReferenceIdAdapterUtil.sessionResponse(response.Headers, responseBodyJson);
                ViewData["showData"] = true;

                return View();
            }
            else
            {
                return Redirect(UrlUtils.ssoUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID)));
            }
        }
    }
}
