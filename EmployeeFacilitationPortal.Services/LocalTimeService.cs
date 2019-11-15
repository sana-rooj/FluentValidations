using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Services
{
    public static class LocalTimeService
    {
       
        public static DateTime ToLocalTime()
        {
           
                var localTimeZoneId = "Pakistan Standard Time";
                var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZoneId);
                var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);
                return localTime;
  
        }
    }
}