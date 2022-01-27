namespace OkameiProduction.BL
{
    public class ReadIni
    {
        private static string InifilePath = @"C:\Okamei\Okamei.ini";

        public string GetValue(string category, string key)
        {
            IniFileReader ifr = new IniFileReader(InifilePath);
            return ifr.IniReadValue(category, key);
        }
        public DBInfoEntity GetDBInfo()
        {
            string[] iniString = GetValue("Database", "Okamei").Split(',');
            if (iniString.Length < 4)
            {
                throw new System.Exception("INIファイルが正しく設定されていません。");
            }

            DBInfoEntity dbInfo = new DBInfoEntity(iniString[0], iniString[1], iniString[2], iniString[3]);

            //未指定時、SqlConnectionのデフォルト値
            dbInfo.ConnectionTimeout = GetValue("Database", "ConnectionTimeout").ToInt32(15);
            //未指定時、SqlCommandのデフォルト値
            dbInfo.CommandTimeout = GetValue("Database", "CommandTimeout").ToInt32(30);

            return dbInfo;
        }
    }
}
