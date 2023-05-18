//using System.Net;
//using System.Web.Http;
//using AutoMapper;
//using Microsoft.AspNetCore.Http.Extensions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using VioRentals.Data;
//using VioRentals.Dtos;
//using VioRentals.Models;

//namespace VioRentals.Controllers.Api;

//[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
//[ApiController]
//[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, Employee")]
//public class CustomersController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public CustomersController(ApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }


//    //GET /api/customers
//    [Microsoft.AspNetCore.Mvc.HttpGet]
//    public IEnumerable<CustomerDto> GetCustomers()
//    {
//        return _mapper.Map<IEnumerable<CustomerDto>>(_context.Customers.Include(c => c.MembershipType));
//    }

//    //GET /api/customers/1
//    [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
//    public ActionResult<CustomerDto> GetCustomer(int id)
//    {
//        var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
//        if (customer == null)
//            return NotFound();

//        return Ok(_mapper.Map<Customer, CustomerDto>(customer));
//    }


//    //POST /api/customers
//    //201 request (created) 
//    [Microsoft.AspNetCore.Mvc.HttpPost]
//    public CreatedResult CreateCustomer(CustomerDto customerDto)
//    {
//        if (!ModelState.IsValid)
//            throw new HttpResponseException(HttpStatusCode.BadRequest);
//        var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
//        _context.Customers.Add(customer);
//        _context.SaveChanges();

//        customerDto.Id = customer.Id;

//        return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id), customerDto);
//    }

//    //PUT /api/customers/1
//    [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
//    public void UpdateCustomer(int id, CustomerDto customerDto)
//    {
//        if (!ModelState.IsValid)
//        {
//            //return badrequest
//            BadRequest(ModelState);
//            return;
//        }

//        var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
//        if (customerInDb == null)
//            throw new HttpResponseException(HttpStatusCode.NotFound);

//        _mapper.Map(customerDto, customerInDb);
//        _context.SaveChanges();
//    }

//    //DELETE /api/customers/1
//    [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
//    public ActionResult<Customer> DeleteCustomer(int id)
//    {
//        var customer = _context.Customers.Find(id);
//        if (customer == null) return NotFound();

//        _context.Customers.Remove(customer);
//        _context.SaveChanges();
//        return customer;
//    }
//}