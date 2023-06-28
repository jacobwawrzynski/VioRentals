using Microsoft.AspNetCore.Mvc;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Core.Entities;
using AutoMapper;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public ViewResult GetCreate()
        {
            return View("Create");
        }

        [HttpGet]
        public async Task<ActionResult> GetEditAsync(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);
            
            if (customer is not null)
            {
                return View("Edit", customer);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<CustomerEntity>(customerDto);
                await _customerService.SaveCustomerAsync(customer);
                return RedirectToAction("Index");
            }

            return RedirectToAction("GetCreateAsync", customerDto);
        }

        [HttpPatch]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([FromForm] CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<CustomerEntity>(customerDto);
                await _customerService.UpdateCustomerAsync(customer);
                return RedirectToAction("Index");
            }

            return RedirectToAction("GetEditAsync", customerDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);

            if (customer is not null)
            {
                return View(customer);
            }

            return NotFound();

        }

        [HttpGet]
        public async Task<JsonResult> SearchAsync(string searchTerm)
        {
            var customers = await _customerService.FindByTermAsync(searchTerm);
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

        [HttpGet]
        public async Task<ViewResult> Index(int page = 1, int pageSize = 10)
        {
            //var totalPages = (int)Math.Ceiling((double) await _customerService.CountCustomersAsync() / pageSize);
            ////check if user enters value higher than totalpages and set the value to the hightes pagenumber availabe
            //if (page > totalPages)
            //{
            //    page = totalPages;
            //    //optional
            //    Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            //}
            //else if (page < 1)
            //{
            //    page = 1;
            //    Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            //}

            //if (pageSize < 1)
            //{
            //    pageSize = 10;
            //    page = 1;
            //    Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            //}
            //else if (pageSize > 100)
            //{
            //    pageSize = 100;
            //    Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
            //}

            var getCustomers = await _customerService.FindAllAsync();
            var customers = getCustomers
                .ToList();

            //pass to view
            //ViewBag.TotalPages = totalPages;
            //ViewBag.CurrentPage = page;
            //ViewBag.PageSize = pageSize;

            return View(customers);
        }
    }
}
