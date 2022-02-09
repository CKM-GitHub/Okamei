using System;
using System.Collections.Generic;
using Models;

namespace OkameiProduction.BL
{
    public static class StaticCache
    {
        public static DBInfoEntity DBInfo { get; private set; }

        public static Dictionary<string, MessageInfo> SystemMessages { get; private set; } 
            = new Dictionary<string, MessageInfo>();

        public static string UploadedFilePath { get; private set; }

        private static readonly object _lockObject = new object();



        public static void SetIniInfo()
        {
            ReadIni ini = new ReadIni();
            DBInfo = ini.GetDBInfo();
        }
        public static void SetMessageCache()
        {
            MessageBL bl = new MessageBL();
            SystemMessages = bl.SelecetAll();
        }
        public static MessageInfo GetMessageInfo(string id)
        {
            lock (_lockObject)
            {
                if (SystemMessages.TryGetValue(id, out MessageInfo message))
                {
                    return message;
                }
                else
                {
                    MessageBL bl = new MessageBL();
                    message = bl.SelectMessage(id);
                    if (message.MessageID.Length > 0)
                    {
                        SystemMessages.Add(message.MessageID, message);
                    }
                    return message;
                }
            }
        }

        public static void SetMControl()
        {
            CommonBL bl = new CommonBL();
            var dt = bl.GetMControl();
            if (dt.Rows.Count > 0)
            {
                var control = dt.Rows[0].ToEntity<MControl>();
                UploadedFilePath = control.TenpuFilePass;
            }
        }
    }
}
