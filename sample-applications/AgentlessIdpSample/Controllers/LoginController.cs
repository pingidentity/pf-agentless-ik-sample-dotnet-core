using Microsoft.AspNetCore.Mvc;
using AgentlessIdpSample.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using AgentlessIdpSample.Utils;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Primitives;

namespace AgentlessIdpSample.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            if (Request.Method == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form[IdpConstants.RESUME_PATH]))
                {
                    if (!StringValues.IsNullOrEmpty(Request.Form[IdpConstants.USERNAME]) 
                    && !StringValues.IsNullOrEmpty( Request.Form[IdpConstants.PASSWORD]))
                    {
                        JObject userAttributes = Authenticator.authenticate(_configuration.GetValue<string>(WebHostDefaults.ContentRootKey),
                                                                            Request.Form[IdpConstants.USERNAME],
                                                                            Request.Form[IdpConstants.PASSWORD]);
                        if (userAttributes != null)
                        {
                            return new DropoffController(_configuration).Index(Request, userAttributes);
                        }
                    }

                    ViewData["resumePath"] = Request.Form[IdpConstants.RESUME_PATH];
                    ViewData["showData"] = false;
                    ViewData["loginError"] = "Invalid Login.";

                    return View("~/Views/App/Index.cshtml");
                }
                else
                {
                    return new AppController(_configuration).Index();
                }
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
