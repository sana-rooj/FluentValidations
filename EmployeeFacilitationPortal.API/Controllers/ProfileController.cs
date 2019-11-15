using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ProfileController : Controller
    {
        //private readonly IEmployee employee;

        //public ProfileController(IEmployee emp)
        //{
        //    employee = emp;
        //}

        //[HttpGet]
        //public  IActionResult GetEmployees()
        //{
        //    List<Employee> empList = employee.GetAll().ToList();

        //    return Ok(empList);

        //}
        //[HttpGet("{id}")]
        //public Employee Get(int id) //This function is redundant, the Employee Controller offers this function.
        //{
        //    return employee.Get(id);
        //}
    }
}