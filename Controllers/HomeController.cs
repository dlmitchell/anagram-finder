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
        /// Just redirects to swagger docs
        /// </summary>   
        [HttpGet]
        public IActionResult Get() => Redirect("~/swagger");
    }
}
