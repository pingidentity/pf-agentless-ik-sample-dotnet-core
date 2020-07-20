using Microsoft.AspNetCore.Mvc;
using AgentlessIdpSample.Utils;
using Microsoft.Extensions.Configuration;


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
            if(Request.Method == "POST")
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
    }
}
