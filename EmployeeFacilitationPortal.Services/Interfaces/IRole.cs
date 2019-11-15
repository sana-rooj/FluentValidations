using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IRole:IRepository<Role>
    {
        List<FieldPermission> GetFieldPermissions(string role, string pageName);

        bool PageAccessAllowed(string role, string pageName);


    }
}
