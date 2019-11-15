using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Common.Utility
{
    public class RandomNumberGenerator
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
