using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmployeeFacilitationPortal.DataRepository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
    }
}
