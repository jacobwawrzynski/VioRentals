using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.DTOs;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Web.Controllers
{
    public class AuthenticateController : Controller
    {
        public async Task<ActionResult> Login([FromForm] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7071/api/");
                    JsonContent content = JsonContent.Create(loginDto);
                    await client.PostAsync("Users/login", content);
                }

                return RedirectToAction("Index", "Customers");
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> Register([FromForm] RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7071/api/");
                    JsonContent content = JsonContent.Create(registerDto);
                    await client.PostAsync("Users/register", content);
                }

                return RedirectToAction("Index", "Customers");
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                HttpResponseMessage response = await client.GetAsync("Users/all");
                return Ok(response.Content);
            }
        }
    }
}
