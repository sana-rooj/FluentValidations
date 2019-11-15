using EmployeeFacilitationPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Services.Interfaces
{

    public interface IGenerateWebToken
    {
        string GenerateJSONWebToken(Employee userInfo);
    }
}
