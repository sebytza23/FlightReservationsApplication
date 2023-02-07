using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using FlightReservationsApplication.Attributes;
using Microsoft.AspNetCore.Http;


namespace FlightReservationsApplication.Controllers
{

    public class ConnectedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Accounts");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAirportRepository _airportRepository;
        private readonly IFlightRepository _flightRepository;

        public HomeController(ILogger<HomeController> logger, IAirportRepository airportRepository, IFlightRepository flightRepository)
        {
            _logger = logger;
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;

        }
        [Connected]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            var homeModelIndex = new HomeIndexModel
            {
                ToLocation = "",
                FromLocation = "",
                CityLocation = await _airportRepository.GetAirportsLocations()
            };
            return View(homeModelIndex);

        }

        public async Task<IActionResult> Search(string? FromLocation, string? ToLocation, DateTime? DepartureDate, int? pageSize, int? pageNumber)
        {
            ViewData["Seats"] = await _flightRepository.GetSeats();
            ViewData["Classes"] = await _flightRepository.GetClasses();
            ViewData["FromLocation"] = FromLocation;
            ViewData["ToLocation"] = ToLocation;
            ViewData["DepartureDate"] = DepartureDate;
            return View(await _flightRepository.GetFlightsByLocationAsync(FromLocation, ToLocation, DepartureDate, pageNumber ?? 1, pageSize ?? 50));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}