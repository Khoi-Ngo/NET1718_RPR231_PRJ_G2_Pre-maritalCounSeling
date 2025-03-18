using Microsoft.AspNetCore.Mvc;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class ERRController : Controller
    {
        [Route("error/403")]
        public IActionResult Forbidden()
        {
            return View("~/Views/ERR_PAGE/403page.cshtml");
        }
        [Route("error/404")]
        public IActionResult NotFound()
        {
            return View("~/Views/ERR_PAGE/404page.cshtml");
        }
    }
}
