using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers
{
    public class AuthenticateController : Controller
    {
        // TESTING PURPOSES
        public ActionResult Index()
        {
            return Ok("API call works");
        }

        public async Task<ActionResult> Login([FromForm] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7071");
                    JsonContent content = JsonContent.Create(loginDto);
                    await client.PostAsync("https://localhost:7071/api/Users/login", content);
                }
                return RedirectToAction("Index");
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> Register([FromForm] RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7071");
                    JsonContent content = JsonContent.Create(registerDto);
                    await client.PostAsync("https://localhost:7071/api/Users/register", content);
                }
                return RedirectToAction("Index");
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071");
                HttpResponseMessage response = await client.GetAsync("https://localhost:7071/api/Users/all");
            }
            return RedirectToAction("Index");

        }
    }
}
