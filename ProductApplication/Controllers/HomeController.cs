using Microsoft.AspNetCore.Mvc;

namespace ProductApplication.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
