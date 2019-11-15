using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.DataRepository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public DBContext _context;

        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
