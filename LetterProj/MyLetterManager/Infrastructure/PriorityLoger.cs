using Semaphore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLetterManager.Infrastructure
{
    public static class PriorityLoger
    {
        /// <summary>
        /// Method for adding text message to log file.
        /// </summary>
        /// <param name="message">This text will be add to log file</param>
        public static void AddRecordToLog(string message)
        {
            string path = AppSettings.PathToPriorityLog;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            string text = DateTime.Now + " " + Environment.UserName + Environment.NewLine + message + Environment.NewLine;                        
            File.AppendAllText(path, text);
        }
    }
}
