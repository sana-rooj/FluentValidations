using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Services
{
    public class GrievanceTypeService : Repository<GrievanceTypes>,IGrievanceTypes
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;

        public GrievanceTypeService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
    }
}
