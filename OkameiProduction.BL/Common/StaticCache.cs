using System;
using System.Collections.Generic;
using Models;

namespace OkameiProduction.BL
{
    public static class StaticCache
    {
        public static DBInfoEntity DBInfo { get; set; }

        public static Dictionary<string, MessageInfo> SystemMessages { get; set; }

        private static readonly object _lockObject = new object();

        public static void SetIniInfo()
        {
            ReadIni ini = new ReadIni();
            DBInfo = ini.GetDBInfo();
        }
        public static void SetMessageCache()
        {
            MessageBL dl = new MessageBL();
            SystemMessages = dl.SelecetAll();
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
                    MessageBL dl = new MessageBL();
                    message = dl.SelectMessage(id);
                    if (message.MessageID.Length > 0)
                    {
                        SystemMessages.Add(message.MessageID, message);
                    }
                    return message;
                }
            }
        }
    }
}
