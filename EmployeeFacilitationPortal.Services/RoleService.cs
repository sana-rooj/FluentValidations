using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeFacilitationPortal.Entities.Models;
using System.Linq;

namespace EmployeeFacilitationPortal.Services
{
    public class RoleService : Repository<Role>, IRole
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;
        public RoleService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
        public bool PageAccessAllowed(string role, string pagename)
        {
            var roleIdQuery = _dbContext.Roles.Where(p => p.Name == role);
            Role roleObj = roleIdQuery.Cast<Role>().ToList()[0];
            int roleId = roleObj.Id;
            var PageNameQuery = _dbContext.Pages.Where(p => p.Name.Equals(pagename));
            Page pageObj = PageNameQuery.Cast<Page>().ToList()[0];
            int pageId = pageObj.Id;
            var PagePermissionQuery = _dbContext.PagePermissions.Where(p => p.RoleId==roleId && p.PageId == pageId);
            PagePermission pagePermission =  PagePermissionQuery.Cast<PagePermission>().ToList()[0];
            return pagePermission.IsAllowed;

        }


        public List<FieldPermission> GetFieldPermissions(string role, string pagename)
        {
            var roleIdQuery = _dbContext.Roles.Where(p => p.Name == role);
            Role roleObj = roleIdQuery.Cast<Role>().ToList()[0];
            int roleId = roleObj.Id;
            var PageNameQuery = _dbContext.Pages.Where(p => p.Name.Equals(pagename));
            Page pageObj = PageNameQuery.Cast<Page>().ToList()[0];
            int pageId = pageObj.Id;

      
            var fieldData = _dbContext.FieldPermissions.Where(p => p.RoleId == roleId && p.PageId == pageId);
            List<FieldPermission> fieldPermissionList = new List<FieldPermission>();
            fieldPermissionList= fieldData.Cast<FieldPermission>().ToList();
   
            return fieldPermissionList;
        }
    }
}
