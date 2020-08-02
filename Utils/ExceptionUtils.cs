using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// Class containing static methods to provide Exception utilities
    /// </summary>
    public static class ExceptionUtils
    {
        /// <summary>
        /// Retrieves the Exception message and make it more readable
        /// </summary>
        /// <param name="exception">An exception to have its message extracted</param>
        /// <returns>String containing with the separator character '¬' replaced by breaklines</returns>
        public static string GetErrorMessages(Exception exception)
        {
            string vMessages = string.Empty;
            Exception vException = exception;

            while (vException != null)
            {
                vMessages += vException.Message.Replace(Environment.NewLine, "¬") + "; ";
                vException = vException.InnerException;
            }
            return vMessages;
        }
    }
}
