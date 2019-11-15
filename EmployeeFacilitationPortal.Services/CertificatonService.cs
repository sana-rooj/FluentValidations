using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFacilitationPortal.Services
{
    public class CertificationService : Repository<Certification>, ICertification
    {
        private DBContext _dbContext;
        private new IUnitOfWork _unitOfWork;
      //  private UploadnDownloadService _UploadnDownload;
        public CertificationService(DBContext context, IUnitOfWork unitOfWork/*,UploadnDownloadService uNd*/) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
           // _UploadnDownload = uNd;
        }

        public IEnumerable<Certification> GetAll(int EmpId)
        {
            IEnumerable<Certification> _certificationList = _dbContext.Certifications.Where(p => p.Id == EmpId).AsEnumerable();
            return _certificationList;
        }

        public Certification GetCertification(int id)
        {
            return _dbContext.Certifications.FirstOrDefault(cert => cert.Id == id);
        }
        public void PostCertifcation(Certification certification) //I would expect an EmpId in it .
        {

        //    _UploadnDownload.UploadFileAsReference(new FileBase() { file = certification.CertificateScannedCopy, filename = certification.CertificateName });

            _dbContext.Certifications.Add(certification);
        }

        bool ICertification.CertificationExists(int id)
        {
            return _dbContext.Certifications.Any(e => e.Id == id);
        }


        async Task<bool> ICertification.Remove(int id)
        {
            var certificate = await _dbContext.Certifications.FindAsync(id);
            if (certificate == null)
            {
                return false;
            }

            _dbContext.Certifications.Remove(certificate);
            await _dbContext.SaveChangesAsync();

          
            return _dbContext.Certifications.Any(e => e.Id == id);
        }


        async Task<bool> ICertification.RemoveAllAgainst(int empId)
        {
            var certifications = _dbContext.Certifications.Where(certificate => certificate.EmployeeId == empId);
            if (certifications == null)
            {
                return false;
            }

            foreach (Certification current in certifications)
            {
                _dbContext.Certifications.Remove(current);
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
} 