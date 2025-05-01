using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    public class ClinicController : Controller
    {
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View();
        }
    }
}
