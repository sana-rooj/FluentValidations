using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _roleService;
        private readonly ILogService _logger;

        public RoleController(IRole roleService, ILogService log)
        {
            _roleService = roleService;
            _logger = log;
        }
        //for getting page permissions 
        [HttpGet("PagePermission")]
        public IActionResult GetPagePermission(string role, string page)
        {
            bool result;

            try
            {
                result = _roleService.PageAccessAllowed(role, page);
               
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }

            return Ok(result);
        }
        // for getting list of fields allowed 
        [HttpGet("FieldPermission")]
        public IActionResult GetFieldPermission(string role, string page)
        {
            List<FieldPermission> fieldList = new List<FieldPermission>();

            try
            {
                fieldList = _roleService.GetFieldPermissions(role, page);
                if (fieldList.Count == 0)
                {
                    return NotFound("Record Not Found.");
                }
            }
            catch (Exception E)
            {
                _logger.AddLog(E.ToString(), E.GetType().ToString());
                return NotFound("Exception Occured.");
            }

            return Ok(fieldList);
        }



        //GET: api/Role
        [HttpGet]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _roleService.GetAll();
        }




    }
}