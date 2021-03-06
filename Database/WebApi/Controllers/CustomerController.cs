﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BarEvent;
using AutoMapper;
using Database;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.DTOs.Customers;
using WebApi.Utility;

namespace WebApi.Controllers
{
    /// <summary>
    /// Web Api Controller for Customers.<para/>
    /// Route: "api/Customers" <para></para>
    /// Returns BarEventDto objects
    /// </summary>
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Reference to unit of work, used for database access. 
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Field to store IMapper implementation.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller. <para/>
        /// Gets a IUnitOfWork by dependency injection (configured in Startup.cs)
        /// </summary>
        /// <param name="unitOfWork">
        /// UnitOfWork implementation used for database access. 
        /// </param>
        /// <param name="mapper">
        /// IMapper implementation used to map Dto object to model objects and vice versa. 
        /// </param>
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all Customers.
        /// Authorization: Admin
        /// </summary>
        /// <returns>
        /// Ok (200) and a List&lt;CustomerDto&gt; of all Customers<para></para>
        /// NotFound (404) if no Customers were found.<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult GetCustomers()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll();
            var customerDtoList = Converter.GenericListConvert
                <Customer, CustomerDto>(customers, _mapper);

            if (customerDtoList.Any())
            {
                return Ok(customerDtoList);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a CustomerDto found by username.
        /// Authorization: Admin
        /// </summary>
        /// <param name="username">
        /// is a string identifying the key of the Customer. 
        /// </param>
        /// <returns>
        /// Ok (200) and a CustomerDto object equivalent of the Customer saved in the database if found.<para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// NotFound (404) if the Customer was not found. <para></para>
        /// </returns>
        [HttpGet("{username}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
 
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult GetCustomer(string username)
        {
            var customer = _unitOfWork.CustomerRepository.Get(username);

            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        /// <summary>
        /// Adds a Customer to the database. 
        /// Authorization: Admin
        /// </summary>
        /// <param name="customerDto">
        /// is a CustomerDto object. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if Customer was added. <para></para>
        /// BadRequest (400) if model requirements weren't met. Body will contain string: "Duplicate Key"
        /// if request failed because of duplicate key sql exception<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpPost] 
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] CustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                _unitOfWork.CustomerRepository.Add(customer);
                _unitOfWork.Complete();
                return Created(string.Format($"api/Customer/{customer.Username}"), customerDto);
            }
            catch (Exception e)
            {
                if (e.InnerException is SqlException exception && exception.Number == 2627)
                {
                    return BadRequest("Duplicate Key");
                }
                return BadRequest();
            }
        }

        /// <summary>
        /// Edits an Customer.
        /// Authorization: Admin
        /// </summary>
        /// <param name="customerDto">
        /// is a CustomerDto which holds edited data. <para></para>
        /// Must hold a Username which can be found in the database.  <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// BadRequest (400) if edit was unsuccessful. See parameter requirements.<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult EditCustomer([FromBody] CustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                _unitOfWork.CustomerRepository.Edit(customer);
                _unitOfWork.Complete();
                return Created(string.Format($"api/customers/{customer.Username}"), customerDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes an Customer. 
        /// Authorization: Admin
        /// </summary>
        /// <param name="username">
        /// is a string holding the username
        /// </param>
        /// <returns>
        /// Ok (200) if deletion was successful. <para></para>
        /// BadRequest (400) if deletion was unsuccessful.<para></para>
        /// 401 or 403 if authorization is unsuccessful. <para></para>
        /// </returns>
        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(string username)
        {
            try
            {
                _unitOfWork.CustomerRepository.Delete(username);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}