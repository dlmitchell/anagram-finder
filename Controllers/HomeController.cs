using Microsoft.AspNetCore.Mvc;

namespace Ibotta.Controllers
{
    /// <summary>
    /// controller home
    /// </summary>
    [Route("")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Just redirects to the swagger docs
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
