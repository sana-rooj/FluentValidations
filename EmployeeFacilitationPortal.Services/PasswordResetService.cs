using EmployeeFacilitationPortal.DataRepository;
using EmployeeFacilitationPortal.DataRepository.Repository;
using EmployeeFacilitationPortal.DataRepository.UnitOfWork;
using EmployeeFacilitationPortal.Entities;
using EmployeeFacilitationPortal.Entities.Models;
using EmployeeFacilitationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.IdentityModel.Protocols;
using EmployeeFacilitationPortal.Entities.Common.Utility;
using Microsoft.Extensions.Configuration;

namespace EmployeeFacilitationPortal.Services
{
    public class PasswordResetService : Repository<PasswordReset>, IPasswordResetService
    {
        public DBContext _dbContext;
        public IUnitOfWork _unitOfWork;
        public bool check_timeout(string email)
        {
            var temp = _dbContext.PasswordResets.Where(p => string.Equals(p.UserEmail, email));
            PasswordReset u = temp.Cast<PasswordReset>().ToList()[0];
            double diff = (DateTime.Now - u.DateTime).TotalDays;
            if (diff > u.ExpiryInDays)
            {
                return false; // in case of first time registeration
            }
            else
            {
                return true;//valid
            }
            
        }
        public new IEnumerable<PasswordReset> Getall()
        {
            IEnumerable<PasswordReset> tempCollection = _dbContext.PasswordResets.AsEnumerable();
            List<PasswordReset> X = tempCollection.ToList();
            
            tempCollection = X;
            return tempCollection;
        }

        public IList<PasswordReset> GetallUnRegistered()
        {
            IList<PasswordReset> tempcollection = _dbContext.PasswordResets.Where(p => p.IsAlreadyRegistered.Equals(false) && ((System.DateTime.Now-p.DateCreated).Days>p.ExpiryInDays)).ToArray();
           
            return tempcollection;
        }

        public int GetUnRegisteredCount()
        {
            return _dbContext.PasswordResets.Where(p => p.IsAlreadyRegistered.Equals(false) && ((System.DateTime.Now - p.DateCreated).Days > p.ExpiryInDays)).Count();
        }
        public IConfiguration Configuration { get; }
        public void generate_link(string email)
        {
            string emailToSet = email;
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("AngularApp", "Angular505@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", emailToSet);
            message.To.Add(to);

            message.Subject = "Reset Password";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Please follow the link to Change password " + AppConfigurations.CurrentHost + AppConfigurations.ResetPasswordURL + emailToSet;
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("angular505@gmail.com", "Angular12*");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            //entering into db
            PasswordReset entry = new PasswordReset();
            entry.UserEmail = email;
            entry.DateTime = DateTime.Now;
            entry.DateCreated = System.DateTime.Now;
            entry.PasswordSetStatus = false;
            entry.ExpiryInDays = 1;
            entry.IsAlreadyRegistered = true;

            _dbContext.PasswordResets.Add(entry);
            _dbContext.SaveChanges();

        }

        private void GenerateEmail(string personalEmail, string ciklumEmail)
        {
            // An email will be generated to personal email of the newly hired employee
            // containing unique link to set password page
            // the link will expire in 72hrs 
            // if the link has expired and the password was not set, HR will be able to resend email will new and valid password

            string emailToSet = personalEmail;
            string ciklumProfileEmail = ciklumEmail;
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("AngularApp", "Angular505@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User", personalEmail);
            message.To.Add(to);

            message.Subject = "Set Password";
            BodyBuilder bodyBuilder = new BodyBuilder();



            bodyBuilder.TextBody = "Your Employee Facilitation Portal's user email is '" + ciklumProfileEmail + "'. Please follow the link to create password against this email: " + AppConfigurations.CurrentHost + AppConfigurations.ResetPasswordURL + ciklumEmail;
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("angular505@gmail.com", "Angular12*");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

          
        }
        public void GenerateAndSendEmail(string personalEmail, string ciklumEmail)
        {
            try
            {
                //entering into db
                PasswordReset entry = new PasswordReset();
                entry.UserEmail = ciklumEmail;
                entry.DateTime = DateTime.Now;
                entry.DateCreated = System.DateTime.Now;
                entry.PasswordSetStatus = false;
                entry.ExpiryInDays = 3;
                entry.IsAlreadyRegistered = false;
                _dbContext.PasswordResets.Add(entry);
                _dbContext.SaveChanges();
                Thread EmailThread = new Thread(() => GenerateEmail(personalEmail, ciklumEmail));
                EmailThread.Start();
            } catch (Exception E)
            {
                throw E;
            }
         
        }

        public bool PasswordReset(Login _login)
        {
            var isReset = false;
            var login = _dbContext.Logins.FirstOrDefault(emp => emp.Email.Equals(_login.Email));
            if (login != null)
            {
                login.EncryptedPassword = Encryptor.Encrypt( _login.Password);
                isReset = true;

                // also update reset password table. This link shouldn't work once the passsword has been set using this link
                PasswordReset entryToBeDeleted = _dbContext.PasswordResets.FirstOrDefault(entry => entry.UserEmail.Equals(login.Email));
                _dbContext.PasswordResets.Remove(entryToBeDeleted);
                _dbContext.SaveChanges();

                return isReset;
            }

            return isReset;
        }

        public PasswordReset GetObjUsingOfficialEmail(string userEmail)
        {
            return _dbContext.PasswordResets.FirstOrDefault(p => p.UserEmail == userEmail);
        }
        public PasswordResetService(DBContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _dbContext = context;
            _unitOfWork = unitOfWork;
        }
        
     
       
    }
}
