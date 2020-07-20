using Microsoft.AspNetCore.Mvc;
using AgentlessSpSample.Utils;
using Microsoft.Extensions.Configuration;

namespace AgentlessSpSample.Controllers
{
    public class LoggedoutController : Controller
    {
        
        private IConfiguration _configuration;
        public LoggedoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {

            ViewData["IdpSsoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.BASE_PF_URL),
                                                    _configuration.GetValue<string>(SpConstants.ADAPTER_CONFIG_SECTION + ":" + SpConstants.PARTNER_ENTITY_ID));
            ViewData["SpSsoUrl"] = UrlUtils.applicationSsoUrl(Request);

            return View();
        }
    }
}
