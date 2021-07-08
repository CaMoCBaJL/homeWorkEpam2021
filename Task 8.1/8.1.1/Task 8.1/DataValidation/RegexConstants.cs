using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataValidation
{
    public class RegexConstants
    {
        public const string birthDateRegexPattern = "\\d{1,2}(\\.\\d{1,2}){2}";

        public const string ageRegexPattern = "\\d{1,2}";
    }
}
