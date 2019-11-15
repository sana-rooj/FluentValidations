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

        public PermissionsAttribute(string pageUrl)
        {
            // get role id from token and check if the page url is present in the pages allowed to this role
        }

        // Get or set the Age property by manipulating the underlying Policy property
        public int Age
        {
            get
            {
                if (int.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var age))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }

}
