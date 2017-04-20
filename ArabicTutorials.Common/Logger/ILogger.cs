using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicTutorials.Common
{
    public interface ILogger
    {
        void LogError(Exception ex);
        void LogError(string exMessage);
        void LogInfo(string message);
    }
}
