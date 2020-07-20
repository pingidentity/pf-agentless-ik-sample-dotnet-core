using Microsoft.AspNetCore.Mvc;
using AgentlessSpSample.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System;


namespace AgentlessSpSample.Controllers
{
    public class ConfigurationController : Controller
    {
        private IConfiguration _configuration;
        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            if (Request.Method == "POST")
            {
                try
                {
                    Configuration.ConfigurationManager.saveConfigurations(_configuration.GetValue<string>(WebHostDefaults.ContentRootKey), Request);
                    return Redirect(UrlUtils.ssoUrl(_configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL),
                                                    _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.PARTNER_ENTITY_ID)));
                }
                catch (Exception e)
                {
                    ViewData["errorMessage"] = e.Message;
                    ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                    ViewData["basePfUrlName"] = SpConstants.BASE_PF_URL;
                    ViewData["basePfUrl"] = Request.Form[SpConstants.BASE_PF_URL];
                    ViewData["adapterUsernameName"] = SpConstants.ADAPTER_USERNAME;
                    ViewData["adapterUsername"] = Request.Form[SpConstants.ADAPTER_USERNAME];
                    ViewData["adapterPassphraseName"] = SpConstants.ADAPTER_PASSWORD;
                    ViewData["adapterPassphrase"] = Request.Form[SpConstants.ADAPTER_PASSWORD];
                    ViewData["adapterIdName"] = SpConstants.ADAPTER_ID;
                    ViewData["adapterId"] = Request.Form[SpConstants.ADAPTER_ID];
                    ViewData["targetUrlName"] = SpConstants.TARGET_URL;
                    ViewData["targetUrl"] = Request.Form[SpConstants.TARGET_URL];
                    ViewData["partnerIdName"] = SpConstants.PARTNER_ENTITY_ID;
                    ViewData["partnerId"] = Request.Form[SpConstants.PARTNER_ENTITY_ID];

                    return View();
                }
            }
            else
            {
                ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                ViewData["basePfUrlName"] = SpConstants.BASE_PF_URL;
                ViewData["basePfUrl"] = _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL);
                ViewData["adapterUsernameName"] = SpConstants.ADAPTER_USERNAME;
                ViewData["adapterUsername"] = _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_USERNAME);
                ViewData["adapterPassphraseName"] = SpConstants.ADAPTER_PASSWORD;
                ViewData["adapterPassphrase"] = _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_PASSWORD);
                ViewData["adapterIdName"] = SpConstants.ADAPTER_ID;
                ViewData["adapterId"] = _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.ADAPTER_ID);
                ViewData["targetUrlName"] = SpConstants.TARGET_URL;
                ViewData["targetUrl"] = _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.TARGET_URL);
                ViewData["partnerIdName"] = SpConstants.PARTNER_ENTITY_ID;
                ViewData["partnerId"] = _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.PARTNER_ENTITY_ID);

                return View();
            }
        }
    }
}
