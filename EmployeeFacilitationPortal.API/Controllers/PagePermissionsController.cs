using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using EmployeeFacilitationPortal.Services.Interfaces;

namespace EmployeeFacilitationPortal.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PagePermissionsController : ControllerBase
    {
        private readonly IPagePermission _pagePermissionService;
        private readonly ILogService _logger;

        public PagePermissionsController(IPagePermission pagePermissionService, ILogService logger)
        {
            _pagePermissionService = pagePermissionService;
            _logger = logger;
        }

        

        // GET: api/PagePermissions/5
        [HttpGet("{roleId}")]
        public async Task<ActionResult<List<String>>> GetPagePermission(int roleId)
        {
            var pagePermission = _pagePermissionService.GetALLPermittedPages(roleId);

            if (pagePermission == null)
            {
                return NotFound();
            }

            return pagePermission;
        }

        
    }
}
