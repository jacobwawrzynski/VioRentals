using Microsoft.AspNetCore.Mvc;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Core.Entities;
using VioRentals.Web.Models;

namespace VioRentals.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerRepository;
        public CustomersController(ICustomerService customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return View("CustomerForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] CustomerEntity customer)
        {
            bool isSaved = false;
            if (ModelState.IsValid)
            {
                customer.MembershipDetailsFK = (int)customer.MembershipType;
                isSaved = await _customerRepository.SaveCustomerAsync(customer);
            }

            return isSaved
                    ? View("CustomerForm", customer)
                    : BadRequest(ModelState);
        }

        [HttpPatch]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, [FromForm] CustomerEntity customerForm)
        {
            var customer = await _customerRepository.FindByIdAsync(id);
            bool isUpdated = false;
            if (customer is null) 
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                customer = customerForm;
                isUpdated = await _customerRepository.UpdateCustomerAsync(customer);
            }

            return isUpdated
                ? View("CustomerForm", customer)
                : BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var customer = await _customerRepository.FindByIdAsync(id);

            return customer is null 
                ? NotFound() 
                : View(customer);
        }

        public async Task<JsonResult> SearchAsync(string searchTerm)
        {
            var customers = await _customerRepository.FindByTermAsync(searchTerm);
            var result = customers.Select(c => new
            {
                c.Id,
                c.Forename,
                c.Surname,
                c.DateOfBirth,
                c.MembershipType,
                c.IsSubscribingToNewsletter
            });
            return Json(result);
        }

        public async Task<ViewResult> Index(int page = 1, int pageSize = 10)
        {
            var totalPages = (int)Math.Ceiling((double) await _customerRepository.CountCustomersAsync() / pageSize);
            //check if user enters value higher than totalpages and set the value to the hightes pagenumber availabe
            if (page > totalPages)
            {
                page = totalPages;
                //optional
                Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            }
            else if (page < 1)
            {
                page = 1;
                Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            }

            if (pageSize < 1)
            {
                pageSize = 10;
                page = 1;
                Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            }
            else if (pageSize > 100)
            {
                pageSize = 100;
                Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            }

            var getCustomers = await _customerRepository.FindAllAsync();
            var customers = getCustomers
                .OrderBy(c => c.Forename)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //pass to view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(customers);
        }
    }
}
