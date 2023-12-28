using CandidateCodeTest.Common.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace CandidateCodeTest.Common
{
    public class LogWriter : ILogWriter
    {
        private string m_exePath = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); //storing log file here only for now
            try
            {
                using StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"); //storing all logs like details, exceptions here in one file only for now
                Log(logMessage, w);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
