using Microsoft.AspNetCore.Mvc;
using AgentlessIdpSample.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.RegularExpressions;

namespace AgentlessIdpSample.Controllers
{
    public class ResumeController : Controller
    {
        private IConfiguration _configuration;
        public ResumeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            if(Request.Method == "POST" 
                && isAlphaNumeric(Request.Form[IdpConstants.REF]) 
                && checkRelativeUrl(Request.Form[IdpConstants.RESUME_PATH]))
            {
                return Redirect(UrlUtils.resumeToPf(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                    Request.Form[IdpConstants.RESUME_PATH], 
                                                    Request.Form[IdpConstants.REF],
                                                    _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.TARGET_URL)));
            }
            else
            {
                ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
                ViewData["ssoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                    _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID));
                return View("~/Views/Error/Index.cshtml");
            }
        }
        private static Boolean isAlphaNumeric(String referenceId)
        {
            Regex alphaNumeric = new Regex(@"^[a-zA-Z0-9]*$");
            return alphaNumeric.IsMatch(referenceId);
        }

        private static Boolean checkRelativeUrl(String relativeUrl)
        {
            Regex resumePattern = new Regex(@"^(/idp/){1}[a-zA-Z0-9/]*(idp/){1}((SSO.ping)?(startSSO.ping)?){1}$");
            return resumePattern.IsMatch(relativeUrl) 
                    && Uri.IsWellFormedUriString(relativeUrl, UriKind.Relative);
        }
    }
}
