using Microsoft.AspNetCore.Mvc;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Core.Entities;
using AutoMapper;
using VioRentals.Web.DTOs;
using Microsoft.AspNetCore.Authorization;

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
                var customerDto = _mapper.Map<CustomerDto>(customer);
                return View("Edit", customerDto);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);

            if (customer is not null)
            {
                return View("Details", customer);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<CustomerEntity>(customerDto);
                await _customerService.UpdateCustomerAsync(customer);
                return RedirectToAction("Index");
            }
                
            return RedirectToAction("GetEditAsync", id);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);
            await _customerService.DeleteCustomerAsync(customer);
            return RedirectToAction("Index");
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
        //[VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<ViewResult> Index(int page = 1, int pageSize = 10)
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

            // FIND BETTER SOLUTION (THIS WORKS BUT UGLY)
            var customers = await _customerService.FindAllAsync();
            var memberships = await _membershipService.FindAllAsync();

            foreach (var cus in customers)
            {
                cus._MembershipDetails = memberships
                    .Where(m => m.Id == cus.MembershipDetailsFK)
                    .First();
            }
            //pass to view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(customers);
        }
    }
}
