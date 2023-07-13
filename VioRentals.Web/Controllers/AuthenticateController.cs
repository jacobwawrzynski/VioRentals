using AutoMapper;
using Microsoft.AspNetCore.Mvc;
//using VioRentals.Core.DTOs;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly HttpClient _httpClient;
        public AuthenticateController(HttpClient httpClient) 
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7071/");
            _httpClient = httpClient;
        }

        // TESTING PURPOSES
        public ActionResult Index()
        {
            return Ok("API call works");
        }

        public async Task<ActionResult> Login([FromForm] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("https://localhost:7071");
                //    JsonContent content = JsonContent.Create(loginDto);
                //    await client.PostAsync("https://localhost:7071/api/Users/login", content);
                //}
                
                JsonContent content = JsonContent.Create(loginDto);
                await _httpClient.PostAsync("api/Users/login", content);

                return RedirectToAction("Index");
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> Register([FromForm] RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("https://localhost:7071");
                //    JsonContent content = JsonContent.Create(registerDto);
                //    await client.PostAsync("https://localhost:7071/api/Users/register", content);
                //}

                JsonContent content = JsonContent.Create(registerDto);
                await _httpClient.PostAsync("api/Users/register", content);

                return RedirectToAction("Index");
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> GetAll()
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:7071");
            //    HttpResponseMessage response = await client.GetAsync("https://localhost:7071/api/Users/all");
            //}

            var response = await _httpClient.GetAsync("api/Users/all");

            return RedirectToAction("Index");

        }
    }
}
