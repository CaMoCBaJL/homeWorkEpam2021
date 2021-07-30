namespace DataValidation
{
    public static class StringConstants
    {
        public const string emptyStringValue = " отсутствуют.";

        public const string successfullOperationResult = "Операция успешно завершена.";

        public const string unsuccessfullOperationResult = "Не удалось завешрить операцию.";

        public const string birthDateRegexPattern = "\\d{1,2}(\\.\\d{1,2}){2}";

        public const string ageRegexPattern = "\\d{1,2}";

        public const string allOk = "All is ok";

        public const string tooLargeAwardTitle = "Award title is too large. (100 symbols - maximum length).";

        public const string tooLargeUsername = "Username is too large. (100 symbols - maximum length).";

        public const string wrongBithDate = "Wrong birth date. (format: 22.22.22)";

        public const string wrongAge = "Wrong age. (format 0-99)";
    }
}
