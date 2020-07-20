using Microsoft.AspNetCore.Mvc;
using AgentlessIdpSample.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using AgentlessIdpSample.Configuration;
using System;


namespace AgentlessIdpSample.Controllers
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
                    ConfigurationManager.saveConfigurations(_configuration.GetValue<string>(WebHostDefaults.ContentRootKey), Request);
                    return Redirect(UrlUtils.ssoUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID)));
                }
                catch (Exception e)
                {
                    ViewData["errorMessage"] = e.Message;
                    ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                    ViewData["basePfUrlName"] = IdpConstants.BASE_PF_URL;
                    ViewData["basePfUrl"] = Request.Form[IdpConstants.BASE_PF_URL];
                    ViewData["adapterUsernameName"] = IdpConstants.ADAPTER_USERNAME;
                    ViewData["adapterUsername"] = Request.Form[IdpConstants.ADAPTER_USERNAME];
                    ViewData["adapterPassphraseName"] = IdpConstants.ADAPTER_PASSWORD;
                    ViewData["adapterPassphrase"] = Request.Form[IdpConstants.ADAPTER_PASSWORD];
                    ViewData["adapterIdName"] = IdpConstants.ADAPTER_ID;
                    ViewData["adapterId"] = Request.Form[IdpConstants.ADAPTER_ID];
                    ViewData["targetUrlName"] = IdpConstants.TARGET_URL;
                    ViewData["targetUrl"] = Request.Form[IdpConstants.TARGET_URL];
                    ViewData["partnerIdName"] = IdpConstants.PARTNER_ENTITY_ID;
                    ViewData["partnerId"] = Request.Form[IdpConstants.PARTNER_ENTITY_ID];

                return View();
                }
            }
            else
            {
                ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                ViewData["basePfUrlName"] = IdpConstants.BASE_PF_URL;
                ViewData["basePfUrl"] = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL);
                ViewData["adapterUsernameName"] = IdpConstants.ADAPTER_USERNAME;
                ViewData["adapterUsername"] = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_USERNAME);
                ViewData["adapterPassphraseName"] = IdpConstants.ADAPTER_PASSWORD;
                ViewData["adapterPassphrase"] = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_PASSWORD);
                ViewData["adapterIdName"] = IdpConstants.ADAPTER_ID;
                ViewData["adapterId"] = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_ID);
                ViewData["targetUrlName"] = IdpConstants.TARGET_URL;
                ViewData["targetUrl"] = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.TARGET_URL);
                ViewData["partnerIdName"] = IdpConstants.PARTNER_ENTITY_ID;
                ViewData["partnerId"] = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID);

                return View();
            }
        }
    }
}
