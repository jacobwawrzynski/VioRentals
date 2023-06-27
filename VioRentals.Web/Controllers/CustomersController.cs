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
        public async Task<ActionResult> CreateAsync()
        {
            return View("Create");
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateAsync(CustomerEntity customer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        if (customer.MembershipType == MembershipTypeEnum.)
        //        {

        //        }
        //    }
        //}
    }
}
