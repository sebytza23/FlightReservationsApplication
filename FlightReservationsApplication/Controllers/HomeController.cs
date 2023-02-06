using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FlightReservationsApplication.Controllers
{
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

        public async Task<IActionResult> Index()
        {
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
            return View(await _flightRepository.GetFlightsByLocationAsync(FromLocation, ToLocation, DepartureDate, pageNumber ?? 1, pageSize ?? 50));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}