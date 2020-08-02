using System.Text.RegularExpressions;

namespace Utils
{
    /// <summary>
    /// Class containing static methods to provide CPF utilities
    /// </summary>
    public static class CPFUtils
    {
        /// <summary>
        /// Verifies if the provided CPF is correctly formated. There are two possible formats
        /// The first is a string containing 11 digits. Example: 01254398710
        /// The second is a 14 caracteres string containig the digits and the CPF separators. Example: 012.543.987-10
        /// </summary>
        /// <param name="cpf"> The CPF as string </param>
        /// <returns>Boolean value representing whether the CPF is correctly formatted</returns>
        public static bool ValidateFormat(string cpf)
        {
            return Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
        }
    }
}
