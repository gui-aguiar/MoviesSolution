using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ExceptionUtils
    {
        public static string GetErrorMessages(Exception pException)
        {
            string vMessages = string.Empty;
            Exception vException = pException;

            while (vException != null)
            {
                vMessages += vException.Message.Replace(Environment.NewLine, "¬") + "; ";
                vException = vException.InnerException;
            }
            return vMessages;
        }
    }
}
