using Microsoft.AspNetCore.Mvc;

namespace AgentlessIdpSample.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
