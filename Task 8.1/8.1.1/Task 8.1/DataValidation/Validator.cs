using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataValidation
{
    public class Validator
    {
        public string ValidateAward(List<string> awardData)
        {
            if (awardData[1].Count() > 100)
                return StringConstants.tooLargeAwardTitle;

            return StringConstants.allOk;
        }

        public string ValidateUser(List<string> userData)
        {
            if (userData[1].Count() > 100)
                return StringConstants.tooLargeAwardTitle;

            if (!ValidateParameter(userData[2], StringConstants.birthDateRegexPattern))
                return StringConstants.wrongBithDate;

            else if (!ValidateParameter(userData[3], StringConstants.ageRegexPattern))
                return StringConstants.wrongAge;

            return StringConstants.allOk;
        }

        public static bool ValidateParameter(string parameter, string regexExpression) => new Regex(regexExpression).IsMatch(parameter);

        public static bool DoesStringContainsCommonParts(string entity) => entity.EndsWith(StringConstants.emptyStringValue) || entity.StartsWith("Список ");
    }
}
