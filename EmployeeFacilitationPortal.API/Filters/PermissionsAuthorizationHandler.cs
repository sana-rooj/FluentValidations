using EmployeeFacilitationPortal.Services.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EmployeeFacilitationPortal.Services.Interfaces;

namespace EmployeeFacilitationPortal.API.Filters
{
    internal class PermissionsAuthorizationHandler: AuthorizationHandler<PagePermissionUrl>
    {
        private readonly ILogger<PermissionsAuthorizationHandler> _logger;
        private readonly IPagePermission _pagePermissionService;

        public PermissionsAuthorizationHandler(ILogger<PermissionsAuthorizationHandler> logger, IPagePermission pagePermissionService)
        {
            _logger = logger;
            _pagePermissionService = pagePermissionService;


        }

        // Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PagePermissionUrl requirement)
        {
            // Check the user's age
            var roleId = context.User.FindFirst(c => c.Type == ClaimTypes.Role);
            int convertedRoleId;
            if (roleId != null)
            { 
                try
                {
                    convertedRoleId = Convert.ToInt32(roleId.Value);
                    if (_pagePermissionService.IsPermittedToAccessPage(convertedRoleId, requirement.pageUrl))
                    {
                        _logger.LogInformation("Is permitted to access", requirement.pageUrl);
                        context.Succeed(requirement);
                    }

                }
                catch (FormatException)
                {
                    // the FormatException is thrown when the string text does 
                    // not represent a valid integer.
                    _logger.LogInformation("Invalid user ID", roleId.Value);
                }
            }
            else
            {
                _logger.LogInformation("No role Claim present");
            }

            return Task.CompletedTask;
        }
        
    }
}
