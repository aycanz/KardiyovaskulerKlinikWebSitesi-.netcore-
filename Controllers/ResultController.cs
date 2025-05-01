using Microsoft.AspNetCore.Mvc;

namespace KVH.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Result()
        {
            return View();
        }
    }
}
