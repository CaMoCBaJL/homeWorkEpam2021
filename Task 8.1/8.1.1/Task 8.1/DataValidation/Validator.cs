using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataValidation
{
    public class Validator
    {
        public string ValidateAward(List<string> awardData)
        {
            if (awardData[1].Count() > 100)
                return "Award title is too large. (100 symbols - maximum length).";

            return "All is ok";
        }

        public string ValidateUser(List<string> userData)
        {
            if (userData[1].Count() > 100)
                return "Username is too large. (100 symbols - maximum length).";

            if (!ValidateParameter(userData[2], Constants.birthDateRegexPattern))
                return "Wrong birth date. (format: 22.22.22)";

            else if (!ValidateParameter(userData[3], Constants.ageRegexPattern))
                return "Wrong age. (format 0-99)";

            return "All is ok)";
        }
        bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);

    }
}
