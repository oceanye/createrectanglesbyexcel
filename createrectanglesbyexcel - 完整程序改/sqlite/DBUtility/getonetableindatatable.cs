using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace createrectanglesbyexcel.sqlite.DBUtility
{
    public abstract class getonetableindatatable
    {
        private static string getselectstring(string tables, string fields = "*", string wheres = "")
        {
            string selectstrings = string.Empty;
            if (!string.IsNullOrEmpty(wheres))
            {
                selectstrings = " where " + wheres;
                return string.Format("select {0} from {1} {2}", new object[] { fields, tables, wheres });
            }
            return string.Format("select {0} from {1} ", new object[] { fields, tables });
        }

        public static DataTable Gettableinfosbycmd(string commands = "", string tablename = "")
        {
            if (!string.IsNullOrEmpty(commands))
            {
                if (string.IsNullOrEmpty(tablename))
                    if (commands.ToLower().IndexOf("where") == -1)
                    {
                        tablename = commands.ToLower().Substring(commands.ToLower().IndexOf("from") + "from".Length).Trim();
                    }
                    else
                    {
                        int wherepos = commands.ToLower().IndexOf("where");
                        int frompos = commands.ToLower().IndexOf("from") + "from".Length;
                        tablename = commands.ToLower().Substring(frompos, wherepos - frompos).Trim();
                    }
                else
                    commands = getselectstring(tablename, "*", " where " + commands);
            }
            else
            {
                if (string.IsNullOrEmpty(tablename))
                    return null;
                else
                    commands = getselectstring(tablename);
            }


            return SQLiteHelper.Fill(commands, tablename);
        }

        public static int Operatetableinfos(string commands)
        {
            return SQLiteHelper.ExecuteNonQuery(commands);
        }
    }
}
