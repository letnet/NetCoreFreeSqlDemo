using Microsoft.AspNetCore.Mvc;
using NetCoreFreeSqlDemo.Application;
using NetCoreFreeSqlDemo.Application.Application;
using NetCoreFreeSqlDemo.Web.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetCoreFreeSqlDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        TestService _testService { get; set; }
        public HomeController(TestService testService)
        {
            this._testService = testService;
        }

        public async Task<IActionResult> Index()
        {
           var testDto = await  _testService.Get("68f4469d-1222-4414-bf73-6823815cdb15");
            return View(testDto);
        }

        public IActionResult Privacy()
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


