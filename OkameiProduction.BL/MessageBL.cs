using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace OkameiProduction.BL
{
    public class MessageBL
    {
        public Dictionary<string, MessageInfo> SelecetAll()
        {
            var result = new Dictionary<string, MessageInfo>();

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_Message_SelectAll");

            //未登録
            if (dt.Rows.Count == 0)
            {
                return result;
            }

            foreach (DataRow dr in dt.Rows)
            {
                var msg = new MessageInfo()
                {
                    MessageID = dr["MessageID"].ToStringOrEmpty(),
                    MessageText1 = dr["MessageText1"].ToStringOrEmpty(),
                    MessageText2 = dr["MessageText2"].ToStringOrEmpty(),
                    MessageText3 = dr["MessageText3"].ToStringOrEmpty(),
                    MessageText4 = dr["MessageText4"].ToStringOrEmpty(),
                    MessageButton = dr["MessageButton"].ToInt32(0),
                    MessageMark = dr["MessageMark"].ToInt32(0),
                    MessageIcon = GetMessageIcon(dr["MessageMark"].ToInt32(0))
                };
                result.Add(msg.MessageID, msg);
            }

            return result;
        }

        public MessageInfo SelectMessage(string messageid)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@MessageID", SqlDbType.VarChar) { Value = messageid };

            DBAccess db = new DBAccess();
            var dt = db.SelectDatatable("M_Message_SelectByID", sqlParams);

            //未登録
            if (dt.Rows.Count == 0)
            {
                return new MessageInfo();
            }

            DataRow dr = dt.Rows[0];
            return new MessageInfo()
            {
                MessageID = dr["MessageID"].ToStringOrEmpty(),
                MessageText1 = dr["MessageText1"].ToStringOrEmpty(),
                MessageText2 = dr["MessageText2"].ToStringOrEmpty(),
                MessageText3 = dr["MessageText3"].ToStringOrEmpty(),
                MessageText4 = dr["MessageText4"].ToStringOrEmpty(),
                MessageButton = dr["MessageButton"].ToInt32(0),
                MessageMark = dr["MessageMark"].ToInt32(0),
                MessageIcon = GetMessageIcon(dr["MessageMark"].ToInt32(0))
            };
        }

        public string GetMessageIcon(int messageMark)
        {
            if (messageMark == 1)
            {
                return "success";
            }
            if (messageMark == 2)
            {
                return "error";
            }
            if (messageMark == 3)
            {
                return "warning";
            }
            if (messageMark == 4)
            {
                return "info";
            }

            return "";
        }
    }
}
