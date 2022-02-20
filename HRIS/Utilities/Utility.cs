using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.Utilities
{
    public class Utility
    {
        public string GenerateNo(string prefix, string recentNo)
        {
            var no = $"{prefix}001";
            if (string.IsNullOrEmpty(recentNo))
                return no;
            int.TryParse(recentNo.Substring(prefix.Length), out var number);
            if (number < 100)
                no = $"{prefix}0{number + 1}";
            if (number < 10)
                no = $"{prefix}00{number + 1}";
            else
                no = $"{prefix}{number + 1}";

            return no;
        }
    }
}
