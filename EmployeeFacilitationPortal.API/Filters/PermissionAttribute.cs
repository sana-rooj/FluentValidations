using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EmployeeFacilitationPortal.Services.Filters
{
    internal class PermissionsAttribute : AuthorizeAttribute
    {
       const string POLICY_PREFIX = "Permissions";

        public PermissionsAttribute(string url) => currentUrl = url;

        // Get or set the Age property by manipulating the underlying Policy property
        public string currentUrl
        {
            get
            {
                var age = Policy.Substring(POLICY_PREFIX.Length);
                if (age != null)
                {
                    return age;
                }
                return default(string);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }

}
