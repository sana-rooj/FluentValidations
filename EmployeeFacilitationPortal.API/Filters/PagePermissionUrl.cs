using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Services.Filters
{
    internal class PagePermissionUrl : IAuthorizationRequirement
    {
        public string pageUrl { get; private set; }

        public PagePermissionUrl(string url) { pageUrl = url; }
    }
    
}
