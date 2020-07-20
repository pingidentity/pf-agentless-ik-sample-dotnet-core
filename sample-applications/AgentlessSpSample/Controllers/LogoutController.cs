using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using AgentlessSpSample.Utils;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace AgentlessSpSample.Controllers
{
    public class LogoutController : Controller
    {
        private IConfiguration _configuration;
        public LogoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            if (Request.Method == "POST")
            {
                String referenceId = Request.Form[SpConstants.REF];
                HttpResponseMessage response = Utils.PickupUtils.pickUpAttributes(_configuration, referenceId);

                String responseBody = response.Content.ReadAsStringAsync().Result;
                dynamic responseBodyJson = JsonConvert.DeserializeObject<dynamic>(responseBody);

                ViewData["resumePath"] = responseBodyJson[SpConstants.RESUME_PATH];
                ViewData["REF"] = referenceId;
                ViewData["responseBody"] = responseBodyJson;
                ViewData["pickupEndpoint"] = SpConstants.PICKUP_ENDPOINT;
                ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                ViewData["ssoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL),
                                                        _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.PARTNER_ENTITY_ID));
                ViewData["redirectUrl"] = UrlUtils.resumeLogoutUrl(_configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL), 
                                                                    (string) responseBodyJson[SpConstants.RESUME_PATH], referenceId);
                ViewData["httpStatus"] = ReferenceIdAdapterUtil.httpStatus(response.StatusCode);
                ViewData["rawRequest"] = ReferenceIdAdapterUtil.pickupRequest(_configuration,referenceId);
                ViewData["rawResponse"] = ReferenceIdAdapterUtil.sessionResponse(response.Headers, responseBodyJson);
                ViewData["showData"] = true;

                return View();
            }
            else
            {
                ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                ViewData["ssoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL),
                                                        _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.PARTNER_ENTITY_ID));
                return View("~/View/Error/Index.cshtml");
            }

        }
    }
}
