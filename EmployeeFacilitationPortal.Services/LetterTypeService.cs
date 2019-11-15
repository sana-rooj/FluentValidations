using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Services
{
    public class LetterTypeService : Repository<LetterTypes>, ILetterTypes
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;

        public LetterTypeService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
    }
}
