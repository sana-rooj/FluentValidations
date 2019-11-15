using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;

namespace EmployeeFacilitationPortal.Services
{
    public class PagePermissionService: Repository<PagePermission>, IPagePermission
    {
        private DBContext _dbContext;
        private new IUnitOfWork _unitOfWork;

        public PagePermissionService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
        
        public List<String> GetALLPermittedPages(int roleId)
        {
            var permittedPageIds = (_dbContext.PagePermissions.Where(p => p.RoleId == roleId).Select(p => p.PageId)).ToList();
            if (permittedPageIds!= null)
            {
                var pageUrls = _dbContext.Pages.Where(p => permittedPageIds.Contains(p.Id)).Select(p => p.PageUrl);
                return pageUrls.ToList();
            }
            return null;
        }

        public bool IsPermittedToAccessPage(int roleId, string pageUrlToCheck)
        {
            var pages = GetALLPermittedPages(roleId);
            return pages.Contains(pageUrlToCheck);
            
        }
    }
}
