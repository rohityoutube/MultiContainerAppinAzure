using Microsoft.AspNetCore.Mvc;
using MVCUI.Models;
using System.Diagnostics;

namespace MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,  IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string APIURL = _configuration["APIURL"];
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIURL);

                    var reponse = await client.GetAsync("/weatherforecast");

                    if (reponse.IsSuccessStatusCode)
                    {
                        string resbody = await reponse.Content.ReadAsStringAsync();

                        ViewBag.Message = resbody;
                    }
                    else
                    {
                        ViewBag.Messsage = "Error in project";
                    }
                }
            }
            catch (Exception ex) { }
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}