using System;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class UserBL
    {
        public bool SelectForLogin(UserModel userModel)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = (int)EMultiPorpose.LoginUser };
            sqlParams[1] = new SqlParameter("@Key", SqlDbType.VarChar) { Value = userModel.UserID };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_MultiPorpose_SelectByIDKey", sqlParams);

            //未登録
            if (dt.Rows.Count == 0)
            {
                return false;
            }

            var dr = dt.Rows[0];

            //パスワード不一致
            if (dr["Char2"].ToStringOrEmpty() != userModel.Password)
            {
                return false;
            }

            userModel.UserName = dr["Char1"].ToStringOrEmpty();
            return true;
        }
    }
}
