using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.Services
{
    public class CompanyInformationService : Repository<CompanyInformation>, ICompanyInformation
    {
        private DBContext _dbContext;
        private new IUnitOfWork _unitOfWork;

        public CompanyInformationService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }

        private bool UploadFile(string base64, string filePath)
        {
            try
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(base64));
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        private bool UploadPolicy(string base64)
        {
            try
            {
                var filePath = GetPolicyPath();
                return UploadFile(base64, filePath);
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private bool UploadHandbook(string base64)
        {
            try
            {
                var filePath = GetHandbookPath();
                return UploadFile(base64, filePath);
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private string GetHandbookPath()
        {
            var count = _dbContext.CompanyInformation.Count(i => i.FileType.Equals("Handbook"));
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Handbooks",
                "Handbook" + "_v" + count + ".pdf");
        }

        private bool UploadOrientation(string base64)
        {
            try
            {
                var filePath = GetOrientationPath();
                return UploadFile(base64, filePath);
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private string GetOrientationPath()
        {
            var count = _dbContext.CompanyInformation.Count(i => i.FileType.Equals("Orientation"));
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Orientations",
                "Orientation" + "_v" + count + ".pdf");
        }

        private string GetPolicyPath()
        {
            var count = _dbContext.CompanyInformation.Count(i => i.FileType.Equals("Policy"));
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Policies",
                "Policy" + "_v" + count + ".pdf");
        }


        public bool AddPolicy(CompanyInformation companyInformation)
        {
            try
            {
                var count = _dbContext.CompanyInformation.Count(i => i.FileType.Equals("Policy"));
                this.UploadPolicy(companyInformation.FilePath);
                companyInformation.FilePath = GetPolicyPath();
                companyInformation.FileType = "Policy";
                companyInformation.Filename = "Policy" + "_v" + count + ".pdf";
                companyInformation.IsActive = true;
                //companyInformation.WhatsNew = "";

                companyInformation.Date = DateTime.Now;
                _dbContext.CompanyInformation.Add(companyInformation);
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        public bool AddHandbook(CompanyInformation companyInformation)
        {
            try
            {
                var count = _dbContext.CompanyInformation.Count(i => i.FileType.Equals("Handbook"));
                this.UploadHandbook(companyInformation.FilePath);
                companyInformation.FilePath = GetHandbookPath();
                companyInformation.FileType = "Handbook";
                companyInformation.Filename = "Handbook" + "_v" + count + ".pdf";
                companyInformation.IsActive = true;
                //companyInformation.WhatsNew = "";

                companyInformation.Date = DateTime.Now;
                _dbContext.CompanyInformation.Add(companyInformation);
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        public bool AddOrientation(CompanyInformation companyInformation)
        {
            try
            {
                var count = _dbContext.CompanyInformation.Count(i => i.FileType.Equals("Orientation"));
                this.UploadOrientation(companyInformation.FilePath);
                companyInformation.FilePath = GetOrientationPath();
                companyInformation.FileType = "Orientation";
                companyInformation.Filename = "Orientation" + "_v" + count + ".ppt";
                companyInformation.IsActive = true;
                //companyInformation.WhatsNew = "";

                companyInformation.Date = DateTime.Now;

                _dbContext.CompanyInformation.Add(companyInformation);
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }


        public long FileLength(string path)
        {
            return new FileInfo(path).Length;
        }

        public bool delete(int id)
        {
            var companyInformation = _dbContext.CompanyInformation.Find(id);
            if (companyInformation == null)
            {
                return false;
            }

            companyInformation.IsActive = false;
            _dbContext.CompanyInformation.Update(companyInformation);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<CompanyInformation> GetActiveCompanyInformations()
        {
            var query = _dbContext.CompanyInformation.Where(c => c.IsActive == true).AsEnumerable()
                .OrderByDescending(policy => policy.Id);
            return query;
        }

        public IEnumerable<CompanyInformation> GetPolicies()
        {
            var query = _dbContext.CompanyInformation.Where(c => c.IsActive == true && c.FileType.Equals("Policy"))
                .AsEnumerable().OrderByDescending(policy => policy.Id);
            return query;
        }

        public IEnumerable<CompanyInformation> GetHandbooks()
        {
            var query = _dbContext.CompanyInformation.Where(c => c.IsActive == true && c.FileType.Equals("Handbook"))
                .AsEnumerable().OrderByDescending(policy => policy.Id);
            return query;
        }

        public IEnumerable<CompanyInformation> GetOrientations()
        {
            var query = _dbContext.CompanyInformation.Where(c => c.IsActive == true && c.FileType.Equals("Orientation"))
                .AsEnumerable().OrderByDescending(policy => policy.Id);
            return query;
        }


        //public string DownloadFile(int id)
        //{
        //    var companyInformation = _dbContext.CompanyInformation.Find(id);

        //    if (companyInformation != null)
        //    {
        //        try
        //        {
        //            var bytes = File.ReadAllBytes(companyInformation.FilePath);
        //            string file = Convert.ToBase64String(bytes);
        //            return file;
        //        }
        //        catch (Exception E)
        //        {
        //            Console.WriteLine("FileNotFoundException", E);
        //            return "";
        //        }
        //    }

        //    return "";
        //}
        public FileDownload DownloadFile(int id)
        {
            var companyInformation = _dbContext.CompanyInformation.Find(id);

            if (companyInformation != null)
            {
                try
                {
                    var bytes = File.ReadAllBytes(companyInformation.FilePath);
                    string file = Convert.ToBase64String(bytes);
                    if(companyInformation.FileType.Equals("Orientation"))
                        return new FileDownload { file = file, contentType = "application/vnd.ms-powerpoint", fileName = companyInformation.Filename };
                    return new FileDownload { file = file, contentType = "application/pdf", fileName = companyInformation.Filename };

                }
                catch (Exception E)
                {
                    Console.WriteLine("FileNotFoundException", E);
                    return new FileDownload();
                }
            }

            return new FileDownload();
        }
    }
}