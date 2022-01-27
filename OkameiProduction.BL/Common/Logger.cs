using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkameiProduction.BL
{
    public enum LogLevel : Int32
    {
        None,
        Debug,
        Info,
        Error
    }

    public class Logger
    {
        private static Logger _singleton = null;
        private readonly string _logFilePath = "";
        private readonly LogLevel _logLevel = LogLevel.Error;

        /// <summary>
        /// インスタンスを生成する
        /// </summary>
        public static Logger GetInstance()
        {
            if (_singleton == null)
            {
                _singleton = new Logger();
            }
            return _singleton;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Logger()
        {
            var ini = new ReadIni();
            _logFilePath = ini.GetValue("Log", "LOG_PATH").ToStringOrEmpty();
            _logLevel = (LogLevel)ini.GetValue("Log", "LOG_LEVEL").ToInt32(0);
        }

        public void Debug(string msg)
        {
            if (CheckLogLevel(LogLevel.Debug))
            {
                Write(msg, LogLevel.Debug);
            }
        }

        public void Info(string msg)
        {
            if (CheckLogLevel(LogLevel.Info))
            {
                Write(msg, LogLevel.Info);
            }
        }

        public void Error(string msg)
        {
            if (CheckLogLevel(LogLevel.Error))
            {
                Write(msg, LogLevel.Error);
            }
        }

        public void Error(Exception ex)
        {
            if (CheckLogLevel(LogLevel.Error))
            {
                Write(ex.Message + Environment.NewLine + ex.StackTrace, LogLevel.Error);
            }
        }

        public void Error(Exception ex, string additonal)
        {
            if (CheckLogLevel(LogLevel.Error))
            {
                Write(ex.Message + "\t" +additonal + Environment.NewLine + ex.StackTrace, LogLevel.Error);
            }
        }

        private void Write(string msg, LogLevel level)
        {
            var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            var absolutePath = Path.Combine(_logFilePath, fileName);

            if (!Directory.Exists(_logFilePath))
            {
                Directory.CreateDirectory(_logFilePath);
            }
            using (StreamWriter sw = new StreamWriter(absolutePath, true, Encoding.GetEncoding("Shift-JIS")))
            using (TextWriter syncWriter = TextWriter.Synchronized(sw))
            {
                syncWriter.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                syncWriter.Write("\t");
                switch (level)
                {
                    case LogLevel.Debug:
                        syncWriter.Write("[Debug]\t");
                        break;
                    case LogLevel.Info:
                        syncWriter.Write("[Info]\t");
                        break;
                    case LogLevel.Error:
                        syncWriter.Write("[Error]\t");
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                syncWriter.WriteLine(msg);
            }
        }

        private bool CheckLogLevel(LogLevel level)
        {
            if (_logFilePath == "")
            {
                return false;
            }
            if (level < _logLevel)
            {
                return false;
            }
            return true;
        }
    }
}
