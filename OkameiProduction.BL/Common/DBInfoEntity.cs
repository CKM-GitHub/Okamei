using System;
using System.Collections.Generic;
using System.Text;

namespace OkameiProduction.BL
{
    public class DBInfoEntity
    {
        public DBInfoEntity(string server, string dbname, string userid, string password)
        {
            Server = server;
            DatabaseName = dbname;
            UserID = userid;
            Password = password;
        }
        public string Server { get; private set; }
        public string DatabaseName { get; private set; }
        public string UserID { get; private set; }
        public string Password { get; private set; }
        public int ConnectionTimeout { get; set; }
        public int CommandTimeout { get; set; }
        public string GetSQLConnString()
        {
            return "Data Source=" + Server +
                ";Initial Catalog=" + DatabaseName +
                ";Persist Security Info=True;User ID=" + UserID +
                ";Password=" + Password +
                ";Connection Timeout=" + ConnectionTimeout.ToInt32(15); //未指定時、SqlConnectionのデフォルト値
        }
    }
}
