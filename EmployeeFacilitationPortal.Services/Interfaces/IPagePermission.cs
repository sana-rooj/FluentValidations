using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Services.Interfaces
{
    public interface IPagePermission : IRepository<PagePermission>
    {
        List<String> GetALLPermittedPages(int roleId);

        bool IsPermittedToAccessPage(int roleId, string pageUrlToCheck);
    }
}
