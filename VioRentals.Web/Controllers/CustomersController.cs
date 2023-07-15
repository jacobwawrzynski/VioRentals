using Microsoft.AspNetCore.Mvc;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using VioRentals.Core.DTOs;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace VioRentals.Web.Controllers
{
    //[Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMembershipService _membershipService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService,
            IMembershipService membershipService,
            IMapper mapper)
        {
            _customerService = customerService;
            _membershipService = membershipService;
            _mapper = mapper;
        }

        public async Task<ActionResult> GetCreate()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.GetAsync("Customers/create");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                return View("Create");
            }
        }

        public async Task<ActionResult> GetEditAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.GetAsync($"Customers/edit/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<CustomerDto>(responseContent);
                    return View("Edit", customer);
                }

                return BadRequest(response);
            }
        }

        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);

            if (customer is not null)
            {
                return View("Details", customer);
            }
            return NotFound();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] CustomerDto customerDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var content = JsonContent.Create(customerDto);
                var response = await client.PostAsync("Customers/create", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([FromForm] CustomerDto customerDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var content = JsonContent.Create(customerDto);
                var response = await client.PatchAsync("Customers/edit", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.DeleteAsync($"Customers/delete/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
        }

        public async Task<JsonResult> SearchAsync(string searchTerm)
        {
            var customers = await _customerService.FindAllAsync();
            var result = customers.Where(c => c.Forename.Contains(searchTerm) || c.Surname.Contains(searchTerm))
                .Select(c => new
                {
                    c.Id,
                    c.Forename,
                    c.Surname,
                    c.DateOfBirth,
                    c.MembershipType,
                    c.IsSubscribingToNewsletter
                }).ToList();
            return Json(result);

            // USING SERVICE
            //var customers = await _customerService.FindByTermAsync(searchTerm);
            //var result = customers.Select(c => new
            //{
            //    c.Id,
            //    c.Forename,
            //    c.Surname,
            //    c.DateOfBirth,
            //    c.MembershipType,
            //    c.IsSubscribingToNewsletter
            //});
            //return Json(result);
        }

        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            
            var totalPages = (int)Math.Ceiling((double)await _customerService.CountCustomersAsync() / pageSize);
            //check if user enters value higher than totalpages and set the value to the hightes pagenumber availabe
            if (page > totalPages)
            {
                RedirectToAction("Index", new { page = totalPages, pageSize });
            }
            else if (page < 1)
            {
                RedirectToAction("Index", new { page = 1, pageSize });
            }

            if (pageSize < 1)
            {
                RedirectToAction("Index", new { page = 1, pageSize = 10 });
            }
            else if (pageSize > 100)
            {
                RedirectToAction("Index", new { page, pageSize = 100 });
            }

            //pass to view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.GetAsync("Customers/all");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerEntity>>(responseContent);
                    return View(customers);
                }
            }
            return BadRequest();
        }
    }
}
