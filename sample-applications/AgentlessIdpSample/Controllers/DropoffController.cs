using System;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using AgentlessIdpSample.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace AgentlessIdpSample.Controllers
{
    public class DropoffController : Controller
    {
        private IConfiguration _configuration;
        public DropoffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index(Microsoft.AspNetCore.Http.HttpRequest request, JObject userAttributes)
        {
            if (request != null)
            {
                try
                {
                    userAttributes.Add(IdpConstants.SUBJECT, new Newtonsoft.Json.Linq.JValue(request.Form[IdpConstants.USERNAME]));
                    userAttributes.Add(IdpConstants.AUTH_INST, new Newtonsoft.Json.Linq.JValue(DateTime.UtcNow));
                }
                catch (ArgumentException)
                {
                    userAttributes[IdpConstants.SUBJECT] = new Newtonsoft.Json.Linq.JValue(request.Form[IdpConstants.USERNAME]);
                    userAttributes[IdpConstants.AUTH_INST] = new Newtonsoft.Json.Linq.JValue(DateTime.UtcNow);
                }
                
                HttpResponseMessage response = null;
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    // For simplicity, trust any certificate. Do not use in production.
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        try
                        {
                            string authenticationString = _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_USERNAME)
                                                        + ":" + _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_PASSWORD);
                            string base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
                            client.DefaultRequestHeaders.Add(IdpConstants.PING_ADAPTER_HEADER,
                                                            _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.ADAPTER_ID));
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            StringContent content = new StringContent(Convert.ToString(userAttributes), Encoding.UTF8, "application/json");

                            // Drop the attributes into PingFederate
                            response = client.PostAsync(UrlUtils.dropoffUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL)),
                                                        content).Result;
                            response.EnsureSuccessStatusCode();
                        }
                        catch (HttpRequestException e)
                        {
                            // proper logging here is omitted for simplicity
                            Console.WriteLine("\nException Caught!");
                            Console.WriteLine("Message :{0} ", e.Message);
                        }
                    }
                }

                String responseBody = response.Content.ReadAsStringAsync().Result;
                dynamic responseBodyJson = JsonConvert.DeserializeObject<dynamic>(responseBody);

                ViewData["resumePath"] = request.Form[IdpConstants.RESUME_PATH];
                ViewData["resumeUrl"] = UrlUtils.resumeUrl(request);
                ViewData["REF"] = responseBodyJson[IdpConstants.REF];
                ViewData["responseBody"] = responseBodyJson;
                ViewData["dropoffEndpoint"] = IdpConstants.DROPOFF_ENDPOINT;
                ViewData["configureUrl"] = UrlUtils.configureUrl(request);
                ViewData["httpStatus"] = ReferenceIdAdapterUtil.httpStatus(response.StatusCode);
                ViewData["rawRequest"] = ReferenceIdAdapterUtil.dropoffPost(_configuration, Convert.ToString(userAttributes));
                ViewData["rawResponse"] = ReferenceIdAdapterUtil.sessionResponse(response.Headers, responseBodyJson);
                ViewData["showData"] = true;
                ViewData["userAttributes"] = userAttributes;
                ViewData["ssoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                    _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID));

                return View();
            }
            else
            {
                ViewData["configureUrl"] = UrlUtils.configureUrl(request);
                ViewData["ssoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                    _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID));
                return View("~/Views/Error/Index.cshtml");
            }
        }

        public IActionResult Index()
        {
            ViewData["configureUrl"] = UrlUtils.configureUrl(Request);
            ViewData["ssoUrl"] = UrlUtils.ssoUrl(_configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.BASE_PF_URL),
                                                    _configuration.GetValue<string>(IdpConstants.ADAPTER_CONFIG_SECTION + ":" + IdpConstants.PARTNER_ENTITY_ID));
            return View("~/Views/Error/Index.cshtml");
        }
    }
}
