using System.Text.RegularExpressions;

namespace Utils
{
    public static class CPFUtils
    {
        public static bool ValidateFormat(string cpf)
        {
            return Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
        }
    }
}
