using FlightReservationsApplication.Context;
using FlightReservationsApplication.Interfaces;
using FlightReservationsApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightReservationsApplication.Attributes
{
    [AllowAnonymous]
    [Route("Accounts/[controller]")]
    public class LoginController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public LoginController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] Account account)
        {
            var dbAccount = await _accountRepository.GetByEmailAsync(account.Email);
            if (dbAccount == null)
            {
                return NotFound();
            }

            if (account.Password != dbAccount.Password)
            {
                return Unauthorized();
            }

            if (dbAccount.EmployeeID != null) {
                dbAccount.Employee = await _accountRepository.GetEmployeeByIdAsync(dbAccount.EmployeeID.Value);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbAccount.Email),
                new Claim(ClaimTypes.Role, dbAccount.IsEmployee ? (dbAccount.Employee.IsAdmin ? "Admin" : "Employee") : "Customer")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(600),
                    IsPersistent = true,
                });

            return Ok();
        }
    }
}
