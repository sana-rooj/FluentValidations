using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeFacilitationPortal.Entities.Common.Utility
{
    public class AppConfigurations
    {
        public static string CurrentHost { get; set; }
        public static string ResetPasswordURL { get; set; }

        public static string EncryptionIV { get; set; }
        public static string EncryptionKey { get; set; }
        public static int TokenTimeOutInMinutes { get; set; }

    }
}
