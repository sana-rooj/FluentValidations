using EmployeeFacilitationPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeFacilitationPortal.Services
{
   public class UploadnDownloadService : IUploadernDownloader
    {
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".cs","text/plain" }
            };
        }
        public string CleanFile(string File) //Ensure that this method is needed or not.
        {
            if (File.Contains("base64"))
            {
                File = File.Substring(File.IndexOf(',') + 1);
                return File;
            }
            return "";
        }
        public UploadnDownloadService()
        {
          
        }
        public string  UploadFileAsReference(string FileBase64, string EmployeeEmail,string FileName  /*An algo will be used to get this value*/)
        {     
            string FilePath = EmployeeEmail;
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            FileName += "_" + GuidString; //Add Random String 

            var path = Path.Combine(Directory.GetCurrentDirectory(),"EmployeeFiles", FilePath);
            System.IO.Directory.CreateDirectory(path); //To create a specified directory
            path = Path.Combine(path, FileName);
            //  var path = Path.Combine( tempFile.FilePath, tempFile.Filename);
            if (FileBase64!="" && FileBase64 != null)
            {
                try
                {
                    System.IO.File.WriteAllBytes(path, Convert.FromBase64String(CleanFile(FileBase64)));                                 
                    // FileWritten = true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return FileName; //Can be changed to just file name.

        }

        public string DownloadFileFromReference(string Filename,string Filepath) //Gets file from Local Storage on the basis of path+filename
        {         
            Byte[] bytes = null;
            var path =
               Path.Combine(Directory.GetCurrentDirectory(), "EmployeeFiles",Filepath,Filename);      
            try
            {
                bytes = System.IO.File.ReadAllBytes(path);               
            }
            catch (Exception E)
            {
                Console.WriteLine("FileNotFoundException", E);
                throw E;
            }
             string File = Convert.ToBase64String(bytes);
            return File;
        }
    }
}
