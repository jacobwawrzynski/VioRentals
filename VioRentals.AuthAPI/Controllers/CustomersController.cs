﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.DTOs;
using VioRentals.Infrastructure.Repositories;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMembershipService _membershipService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService,
            IMapper mapper,
            IMembershipService membershipService)
        {
            _customerService = customerService;
            _mapper = mapper;
            _membershipService = membershipService;
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.FindByIdAsync(id);
            
            if (customer is not null)
            {
                var customerDto = _mapper.Map<CustomerDto>(customer);
                return Ok(customerDto);
            }
            return NotFound();
        }

        [HttpGet("all")]
        [VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.FindAllAsync();

            // PROBLEM WITH CIRCLE REFERENCE
            var memberships = await _membershipService.FindAllAsync();
            foreach (var cus in customers)
            {
                cus._MembershipDetails = memberships
                    .Where(m => m.Id == cus.MembershipDetailsFK)
                    .First();
            }

            return Ok(customers);
        }
    }
}
