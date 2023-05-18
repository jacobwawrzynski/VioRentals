//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using VioRentals.Data;
//using VioRentals.Models;
//using VioRentals.ViewModels;

//namespace VioRentals.Controllers;

//[Authorize(Roles = "Admin, Employee")]
//public class CustomersController : Controller
//{
//    private readonly ApplicationDbContext _context;
//    private bool HasErrors = false;

//    public CustomersController()
//    {
//        _context = new ApplicationDbContext();
//    }

//    protected override void Dispose(bool disposing)
//    {
//        _context.Dispose();
//    }

//    public ActionResult New()
//    {
//        var membershipTypes = _context.MembershipTypes.ToList();
//        var viewModel = new NewCustomerViewModel
//        {
//            Customer = new Customer(),
//            MembershipTypes = membershipTypes
//        };

//        return View("CustomerForm", viewModel);
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public ActionResult Save(Customer customer)
//    {
//        if (!ModelState.IsValid)
//        {
//            var viewModel = new NewCustomerViewModel
//            {
//                Customer = customer,
//                MembershipTypes = _context.MembershipTypes.ToList(),
//                //show the error message
//                HasErrors = true
//            };

//            return View("CustomerForm", viewModel);
//        }

//        if (customer.Id == 0)
//        {
//            _context.Customers.Add(customer);
//        }
//        else
//        {
//            var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
//            customerInDb.Name = customer.Name;
//            customerInDb.Surname = customer.Surname;
//            customerInDb.DateOfBirth = customer.DateOfBirth;
//            customerInDb.MembershipTypeId = customer.MembershipTypeId;
//            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
//        }

//        _context.SaveChanges();

//        return RedirectToAction("Index", "Customers");
//    }

//    public ViewResult Index(int page = 1, int pageSize = 10)
//    {
//        var totalPages = (int)Math.Ceiling((double)_context.Customers.Count() / pageSize);
//        //check if user enters value higher than totalpages and set the value to the hightes pagenumber availabe
//        if (page > totalPages)
//        {
//            page = totalPages;
//            //optional
//            Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
//        }
//        else if (page < 1)
//        {
//            page = 1;
//            Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
//        }

//        if (pageSize < 1)
//        {
//            pageSize = 10;
//            page = 1;
//            Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
//        }
//        else if (pageSize > 100)
//        {
//            pageSize = 100;
//            Response.Redirect("/Customers/Index?page=" + page + "&pageSize=" + pageSize);
//        }

//        var customers = _context.Customers
//            .Include(c => c.MembershipType)
//            .OrderBy(c => c.Name)
//            .Skip((page - 1) * pageSize)
//            .Take(pageSize)
//            .ToList();
//        //pass to view
//        ViewBag.TotalPages = totalPages;
//        ViewBag.CurrentPage = page;
//        ViewBag.PageSize = pageSize;

//        return View(customers);
//    }

//    public JsonResult Search(string searchTerm)
//    {
//        var result = _context.Customers.Where(c => c.Name.Contains(searchTerm) || c.Surname.Contains(searchTerm))
//            .Select(c => new
//            {
//                c.Id,
//                c.Name,
//                c.Surname,
//                c.DateOfBirth,
//                c.MembershipType,
//                c.IsSubscribedToNewsletter
//            }).ToList();
//        return Json(result);
//    }


//    public ActionResult Details(int id)
//    {
//        var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

//        if (customer == null)
//            return NotFound();

//        return View(customer);
//    }

//    public ActionResult Edit(int id)
//    {
//        var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

//        if (customer == null)
//            return NotFound();

//        var viewModel = new NewCustomerViewModel
//        {
//            Customer = customer,
//            MembershipTypes = _context.MembershipTypes.ToList()
//        };

//        return View("CustomerForm", viewModel);
//    }
//}