using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.DataRepository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected IUnitOfWork _unitOfWork;


        public Repository(DbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _unitOfWork.Complete();
        }

        public async Task Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _unitOfWork.Complete();
        }
        public async Task Update(TEntity entity)
        {
            //_context.Entry(entity).State = EntityState.Detached;
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
            await _unitOfWork.Complete();
        }
        public async Task<TEntity> RemoveById(int id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return null;
            _context.Set<TEntity>().Remove(entity);
            await _unitOfWork.Complete();
            return entity;

        }

    }
}
