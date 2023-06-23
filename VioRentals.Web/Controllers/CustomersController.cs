using Microsoft.AspNetCore.Mvc;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.Models;
using VioRentals.Core.Models;

namespace VioRentals.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerRepository;
        public CustomersController(ICustomerService customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
    }
}
