using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Areas.User.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the page could not be found";
                    break;
            }
            return View("NotFound");
        }
    }
}
