using Microsoft.AspNetCore.Mvc;

namespace FlightReservationsApplication.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Errors/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Pagina nu exista";
                    ViewBag.ErrorContent = "Ne pare rau, nu am gasit pagina pe care o cauti.";
                    ViewBag.ErrorCode = "404";

                    break;
            }
            return View("NotFound");
        }
    }
}
