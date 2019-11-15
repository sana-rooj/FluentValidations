using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.IQueryableExtensions;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.Services
{
    public class EmployeeService : Repository<Employee>, IEmployee
    {
        private DBContext _dbContext;
        private new IUnitOfWork _unitOfWork;
        // private IPersonalInfo _personalInfoService;
        private readonly IUploadernDownloader _uploadndownloadService;
        private IPasswordResetService _passwordResetService;
        private readonly IQueryableExtensions<Employee> _queryableExtensions;
        public EmployeeService(DBContext context, IUnitOfWork unitOfWork, IQueryableExtensions<Employee> query,
            IPasswordResetService PS, IUploadernDownloader UnDService) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
            _passwordResetService = PS;
            _uploadndownloadService = UnDService;
            _queryableExtensions = query;
        }


        private Employee AttachAdditionalData(Employee E)  //AttachAdditionalData()
        {
            if (E != null)
            {
                E.PersonalInfo = _dbContext.PersonalInfos.AsNoTracking().FirstOrDefault(pinfo => pinfo.EmployeeId.Equals(E.Id));
                E.ProfessionalReferences = _dbContext.ProfessionalReferences.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.Letters = _dbContext.Letters.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.EducationalRecords = _dbContext.EducationalRecords.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.Dependents = _dbContext.Dependents.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.WorkHistories = _dbContext.WorkHistories.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.Certifications = _dbContext.Certifications.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.TrainingRequests = _dbContext.TrainingRequests.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.LanguageSkills = _dbContext.LanguageSkills.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
                E.AllAttachments = _dbContext.AllAttachments.AsNoTracking().Where(val => val.EmployeeId.Equals(E.Id)).ToList();
            }
            return E;
        }
        private bool ToBool(string val)
        {
            return (val == "1" || val == "true" || val == "TRUE") ? true : false;
        }

        
        public new async Task<IEnumerable<Employee>> GetAll()
        {
            var query = this.GetQueryable();
            return await query.ToListAsync();
        }
        public async Task<Employee> GetById(int id)
        {
            var query = await this.GetQueryable().Where(employee => employee.Id.Equals(id)).SingleOrDefaultAsync();
            
            return query;

        }

        public async Task<Employee> GetByUsername(string username)
        {
            var query = await this.GetQueryable().Where(employee => employee.Username.Equals(username)).SingleOrDefaultAsync();
            return query;
        }
       
        public int GetCount()
        {
            return _dbContext.Employees.Count();
        }
        public int GetUnRegisteredAccountCount()
        {
            return _passwordResetService.GetUnRegisteredCount();
        }
        public int GetInvalidatedAccountCount()
        {
            return _dbContext.Employees.Count(p => p.IsValidated == false);
        }
        public int GetSearchedAccountsCount(string search)
        {
            return _dbContext.Employees.Count(p => p.EmpId.ToString().Contains(search) || p.PersonalInfo.FullName.Contains(search) || p.Role.Name.Contains(search) || p.Username.Contains(search) || p.Designation.Contains(search));
        }
        //***********************FUNCTION FOR PAGINATION,SORTING AND SEARCHING*************************

        public IList<Employee> GetPaginatedEmployees(IQueryable<Employee> empList, int page = 1, string sort = "EmpId", bool sortOrder = false, string search = "", int limit = 10, bool isActive = true, int roleId = 0)
        {
            var skip = (page - 1) * limit;
            HashSet<int> IDCollection = new HashSet<int>(_dbContext.PersonalInfos.Select(s => s.EmployeeId));

            empList.Include(e => e.PersonalInfo).Where(p => IDCollection.Contains(p.Id)).Load();

            if (roleId == _dbContext.Roles.FirstOrDefault(r => r.Name.Equals("HR")).Id) //Only HR needs to select on basis of IsActive
                empList = empList.Where(p => p.IsActive.Equals(isActive));

            if (search != "")
                empList = empList.Where(p => p.EmpId.ToString().Contains(search) || p.PersonalInfo.FullName.Contains(search) || p.Username.Contains(search)
                || p.Designation.Contains(search) || p.Role.Name.Contains(search));

            if (sortOrder == false)
            {
                if (sort == "FullName")
                    empList = empList.OrderBy(p => EF.Property<object>(p.PersonalInfo, "FullName"));
                else if (sort == "Role")
                    empList = empList.OrderBy(p => EF.Property<object>(p.Role, "Name"));
                else
                    empList = empList.OrderBy(p => EF.Property<object>(p, sort));
            }
            else
            {
                if (sort == "FullName")
                    empList = empList.OrderByDescending(p => EF.Property<object>(p.PersonalInfo, sort));
                else if (sort == "Role")
                    empList = empList.OrderByDescending(p => EF.Property<object>(p.Role, "Name"));
                else
                    empList = empList.OrderByDescending(p => EF.Property<object>(p, sort));
            }

            IList<Employee> tempList = empList.Skip(skip).Take(limit).ToList();
            for (int i = 0; i < tempList.Count;)
                tempList[i] = AttachAdditionalData(tempList[i++]);

            return tempList;
        }
        public IList<Employee> GetAllEmployees(int page = 1, string sort = "EmpId", bool sortOrder = false, string search = "", int limit = 10, bool isActive = true, int roleId = 0)
        {
            IQueryable<Employee> empList = _dbContext.Employees;
            return GetPaginatedEmployees(empList, page, sort, sortOrder, search, limit, isActive, roleId);

        }

        public IList<Employee> GetEmployeesOnValidation(int page = 1, string sort = "IsValidated", bool sortOrder = false, string search = "", int limit = 10, bool isActive = true, int roleId = 0)
        {
            IQueryable<Employee> empList = _dbContext.Employees.Where(p => p.IsValidated.Equals(ToBool(search)));
            //Search here will be blank because it was used to set true/false value of IsValidated.
            return GetPaginatedEmployees(empList, page, sort, sortOrder, "", limit, isActive, roleId);
        }
        public IList<Employee> GetPasswordResetList(int page = 1, string sort = "EmpId", bool sortOrder = false, string search = "", int limit = 10)
        {
            IList<PasswordReset> tempCollection = _passwordResetService.GetallUnRegistered();
            // Filter employee records which have pasword link expired and haven't yet reset their password 
            HashSet<string> PasswordCollection = new HashSet<string>(tempCollection.Where(s => (System.DateTime.Now).Subtract(s.DateCreated).Days > s.ExpiryInDays).Select(s => s.UserEmail));
            IQueryable<Employee> empList = _dbContext.Employees.Where(m => PasswordCollection.Contains(m.Username));


            return GetPaginatedEmployees(empList, page, sort, sortOrder, search, limit);
        }

        public int GetRole(string email)
        {
            var employee = _dbContext.Employees.FirstOrDefault(info => info.Username.Equals(email));
            return employee != null ? employee.RoleId : 1;
        }

        public void EmpUpdate(Employee employee, bool deleteAssociatedObjects = false)
        {
            // UploadFileAsReference ( Base64, Email, FullName )
            // Delete all associated data and re-enter everything in order to update deleted objects as well
            
                if (deleteAssociatedObjects == true)
                {

                    var employeeToBeDeleted = _dbContext.Employees.AsNoTracking().Include(p => p.AllAttachments)
                                                                  .Include(p => p.Grievances)
                                                                  .Include(p => p.LanguageSkills)
                                                                  .Include(p => p.Letters)
                                                                  .Include(p => p.ProfessionalReferences)
                                                                  .Include(p => p.EmployeeCandidatesReferred)
                                                                  .Include(p => p.EducationalRecords)
                                                                  .Include(p => p.Dependents)
                                                                  .Include(p => p.WorkHistories)
                                                                  //.Include(p => p.Claims)
                                                                  .Include(p => p.TrainingRequests)
                                                                  .Include(p => p.Certifications)
                                                                  .Include(p => p.Queries)
                                                                  .Include(p => p.Role)
                                                                  .SingleOrDefault(p => p.Id == employee.Id);
                    // removing attachments
                    if (employeeToBeDeleted.AllAttachments.ToArray() != null && employeeToBeDeleted.AllAttachments.ToArray().Count() > 0)
                        _dbContext.AllAttachments.RemoveRange(employeeToBeDeleted.AllAttachments.ToArray());

                    // removing referred candidates
                    if (employeeToBeDeleted.EmployeeCandidatesReferred.ToArray() != null && employeeToBeDeleted.EmployeeCandidatesReferred.ToArray().Count() > 0)
                        _dbContext.EmployeeCandidatesReferred.RemoveRange(employeeToBeDeleted.EmployeeCandidatesReferred.ToArray());

                    //removing Professional references
                    if (employeeToBeDeleted.ProfessionalReferences.ToArray() != null && employeeToBeDeleted.ProfessionalReferences.ToArray().Count() > 0)
                        _dbContext.ProfessionalReferences.RemoveRange(employeeToBeDeleted.ProfessionalReferences.ToArray());

                    //removing educational records
                    if (employeeToBeDeleted.EducationalRecords.ToArray() != null && employeeToBeDeleted.EducationalRecords.ToArray().Count() > 0)
                        _dbContext.EducationalRecords.RemoveRange(employeeToBeDeleted.EducationalRecords.ToArray());

                    //removing Dependents
                    if (employeeToBeDeleted.Dependents.ToArray() != null && employeeToBeDeleted.Dependents.ToArray().Count() > 0)
                        _dbContext.Dependents.RemoveRange(employeeToBeDeleted.Dependents.ToArray());

                    //removing work histories
                    if (employeeToBeDeleted.WorkHistories.ToArray() != null && employeeToBeDeleted.WorkHistories.ToArray().Count() > 0)
                        _dbContext.WorkHistories.RemoveRange(employeeToBeDeleted.WorkHistories.ToArray());

                    //removing claims
                    //if (employeeToBeDeleted.Claims.ToArray() != null && employeeToBeDeleted.Claims.ToArray().Count() > 0)
                    //    _dbContext.Claims.RemoveRange(employeeToBeDeleted.Claims.ToArray());

                    // removing Grievances
                    if (employeeToBeDeleted.Grievances.ToArray() != null && employeeToBeDeleted.Grievances.ToArray().Count() > 0)
                        _dbContext.Grievances.RemoveRange(employeeToBeDeleted.Grievances.ToArray());

                    // removing TrainingRequests
                    if (employeeToBeDeleted.TrainingRequests.ToArray() != null && employeeToBeDeleted.TrainingRequests.ToArray().Count() > 0)
                        _dbContext.TrainingRequests.RemoveRange(employeeToBeDeleted.TrainingRequests.ToArray());

                    // removing Letters
                    if (employeeToBeDeleted.Letters.ToArray() != null && employeeToBeDeleted.Letters.ToArray().Count() > 0)
                        _dbContext.Letters.RemoveRange(employeeToBeDeleted.Letters.ToArray());

                    // removing Queries
                    if (employeeToBeDeleted.Queries.ToArray() != null && employeeToBeDeleted.Queries.ToArray().Count() > 0)
                        _dbContext.Queries.RemoveRange(employeeToBeDeleted.Queries.ToArray());

                    // removing ForgotEmails 
                    // _dbContext.ForgotEmails.RemoveRange(employeeToBeDeleted.Grievances.ToArray());

                    // removing Certifications
                    if (employeeToBeDeleted.Certifications.ToArray() != null && employeeToBeDeleted.Certifications.ToArray().Count() > 0)
                        _dbContext.Certifications.RemoveRange(employeeToBeDeleted.Certifications.ToArray());

                    // LanguageSkills
                    if (employeeToBeDeleted.LanguageSkills.ToArray() != null && employeeToBeDeleted.LanguageSkills.ToArray().Count() > 0)
                        _dbContext.LanguageSkills.RemoveRange(employeeToBeDeleted.LanguageSkills.ToArray());

                }
                employee.PersonalInfo.Picture = _uploadndownloadService.UploadFileAsReference(employee.PersonalInfo.Picture, employee.Username, employee.PersonalInfo.FullName);
                //update login object
                var oldEmployee = _dbContext.Employees.Where(s => s.Id == employee.Id).FirstOrDefault();
                if (oldEmployee != null && oldEmployee.Username!=null)
                {
                    _dbContext.Entry<Employee>(oldEmployee).State = EntityState.Detached;
                    string officialEmail = oldEmployee.Username;
                    var loginObj = _dbContext.Logins.Where(s => s.Email.Equals(officialEmail)).FirstOrDefault();
                    if(loginObj != null && loginObj.Email!=null)
                    {
                        loginObj.Email = employee.Username;
                        _dbContext.Logins.Update(loginObj);
                    }
                    else
                    {
                        throw new NullReferenceException("No login object to update");
                    }
                }
                else
                {
                    throw new NullReferenceException("Original employee doesn't exist");
                }

                //
                _dbContext.Employees.Update(employee);

                _dbContext.SaveChanges();
            
            
            
        }

        public async void GenerateLink(string email, int empId, string personalEmail)
        {
            PasswordReset passReset;
            passReset = _passwordResetService.GetObjUsingOfficialEmail(email);
            _passwordResetService.Remove(passReset);
            await _unitOfWork.Complete();
            //_passwordResetService.Update(passReset); We do not need this line because while generating a new email, a new record is entered
            _passwordResetService.GenerateAndSendEmail(personalEmail, email);
        }
        private IQueryable<Employee> GetQueryable()
        {
            return _dbContext.Employees.Include(employee => employee.PersonalInfo)
                .Include(employee => employee.ProfessionalReferences)
                .Include(employee => employee.Letters)
                .Include(employee => employee.EducationalRecords)
                .Include(employee => employee.Dependents)
                .Include(employee => employee.WorkHistories)
                .Include(employee => employee.Certifications)
                .Include(employee => employee.TrainingRequests)
                .Include(employee => employee.LanguageSkills)
                .Include(employee => employee.AllAttachments)
                .Include(employee => employee.Role);
        }

        private IQueryable<Employee> ApplyFiltering(IQueryable<Employee> query, EmployeeQuery queryObj)
        {
            // Wild search
            if (!string.IsNullOrWhiteSpace(queryObj.Search))
                query = query.Where(a =>    a.EmpId.ToString().Contains(queryObj.Search) 
                                         || a.Username.Contains(queryObj.Search)
                                         || a.PersonalInfo.FullName.Contains(queryObj.Search)
                                         || a.Designation.Contains(queryObj.Search)
                                         || a.Role.Name.Contains(queryObj.Search));
            if (queryObj.IsValidated.HasValue)
                query = query.Where(v => v.IsValidated.Equals(queryObj.IsValidated.Value) );
            if (queryObj.IsActive.HasValue)
                query = query.Where(v => v.IsActive.Equals(queryObj.IsActive.Value));
            if (queryObj.EmpId.HasValue)
                query = query.Where(v => v.EmpId.Equals(queryObj.EmpId.Value));
            if (!string.IsNullOrWhiteSpace(queryObj.Username))
                query = query.Where(v => v.Username.Contains(queryObj.Username));
            if (!string.IsNullOrWhiteSpace(queryObj.Fullname))
                query = query.Where(v => v.PersonalInfo.FullName.Contains(queryObj.Fullname));

            if (!string.IsNullOrWhiteSpace(queryObj.Designation))
                query = query.Where(v => v.Designation.Contains(queryObj.Designation));



            return query;
        }

        public async Task<QueryResult<Employee>> GetEmployees(EmployeeQuery queryObj)
        {
            var result = new QueryResult<Employee>();
            var query = GetQueryable();
            query = ApplyFiltering(query, queryObj);
            var columnsMap = new Dictionary<string, Expression<Func<Employee, object>>>()
            {
                ["username"] = employee=>employee.Username,
                ["fullname"] = employee => employee.PersonalInfo.FullName,
                ["empId"] = employee => employee.EmpId,
                ["designation"] = employee => employee.Designation,
                ["role"] = employee => employee.Role.Name,
                ["isActive"] = employee => employee.IsActive




            };
            query = _queryableExtensions.ApplyOrderingColumnMap(query, queryObj,columnsMap);
            result.TotalItems = await query.CountAsync();
            query = _queryableExtensions.ApplyPaging(query, queryObj);
            result.Items = await query.ToListAsync();
            return result;
        }

 

    }
}