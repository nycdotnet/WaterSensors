using Microsoft.AspNetCore.Mvc;

namespace WaterSensors.Controllers
{
    [Route("[controller]")]
    public class MapController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
