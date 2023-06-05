using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.Serialization.Formatters.Binary;
using Autodesk.AutoCAD.Geometry;


namespace createrectanglesbyexcel.Commons
{
    public abstract class Globals
    {
        public static bool issameline(Point3d onep, Point3d twop, Point3d judgep,double jiangdu=StaticValues.comparejingdu)
        {
            return Math.Abs((twop.X - onep.X) * (judgep.Y - twop.Y) - (twop.Y - onep.Y) * (judgep.X - twop.X)) <= jiangdu ? true : false;
        }
        /// <summary>
        /// 获取垂线另一个点
        /// </summary>
        /// <param name="startp"></param>
        /// <param name="endp"></param>
        /// <returns></returns>
        public static Point3d getchuixianos(Point3d startp, Point3d endp,bool isstart=true)
        {
            double newx = 0, newy = 0, newz = 0;
            lineparams oneparms = getonelineparams(startp, endp);
            if (isstart)
            {
                lineparams chuixianparms = getlinechuixian(oneparms, startp);
                if (chuixianparms.B == 0)
                {
                    newx = startp.X;
                    newy = startp.Y + 10;                    
                }
                else
                {
                    newx = startp.X + 10;
                    newy = -(chuixianparms.A * newx + chuixianparms.C) / chuixianparms.B;
                }
            }
            else
            {
                lineparams chuixianparms = getlinechuixian(oneparms, endp);
                if (chuixianparms.B == 0)
                {
                    newx = endp.X;
                    newy = endp.Y + 10;
                }
                else
                {
                    newx = endp.X + 10;
                    newy = -(chuixianparms.A * newx + chuixianparms.C) / chuixianparms.B;
                }
            }

            return new Point3d(newx, newy, newz);
        }
        /// <summary>
        /// 用一点和直线参数计算垂线方程
        /// </summary>
        /// <param name="lineparms"></param>
        /// <param name="endp"></param>
        /// <returns></returns>
        public static lineparams getlinechuixian(lineparams lineparms, Point3d onep)
        {
            double C = -(lineparms.B * onep.X - lineparms.A * onep.Y);
            return new lineparams(lineparms.B, -lineparms.A, C);
        }
        /// <summary>
        /// 按线参数计算值
        /// </summary>
        /// <param name="lineparms"></param>
        /// <param name="endp"></param>
        /// <returns></returns>
        public static double getparamsvalues(lineparams lineparms, Point3d endp)
        {
            return lineparms.A * endp.X + lineparms.B * endp.Y + lineparms.C;
        }
        /// <summary>
        /// 获取直线的系数
        /// </summary>
        /// <param name="oneline"></param>
        /// <returns></returns>
        public static lineparams getonelineparams(Point3d startp, Point3d endp)
        {
            double A = endp.Y - startp.Y, B = startp.X - endp.X, C = endp.X * startp.Y - startp.X * endp.Y;
            return new lineparams(A, B, C);
        }

        #region
        /// <summary>
        /// 使用反射方法加载所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> Getallrecordsfromdt<T>(DataTable dt = default(DataTable))
        {
            if (dt.IsDefault())
                dt = Getdts<T>();

            string fullname = typeof(T).FullName.Replace("Model", "BLL").Trim() + "bll";
            Type type = Type.GetType(fullname);
            dynamic newobj = type.Assembly.CreateInstance(fullname);

            MethodInfo mi = newobj.GetType().GetMethod("PopulateAllrecordsFromDataRow");
            object returnvalues = mi.Invoke(null, new object[] { dt });

            return (List<T>)returnvalues;
        }

        public static DataTable Getcompares<T>(string inputstring)
        {
            System.Data.DataTable clones = default(DataTable);
            System.Data.DataTable newdt = Getdts<T>();
            ///设置默认
            if (!newdt.IsDefault())
            {
                clones = newdt.Clone();
                List<DataRow> getalls = new List<DataRow>();
                if (!string.IsNullOrEmpty(inputstring))
                    getalls = newdt.Select(inputstring).ToList();
                else
                    getalls = newdt.Select().ToList();
                getalls.ForEach(onerow =>
                {
                    System.Data.DataRow newrow = clones.NewRow();
                    newrow.ItemArray = onerow.ItemArray;

                    clones.Rows.Add(newrow);
                });
            }

            return clones;
        }

        public static T Getonerd<T>(string inputstring)
        {
            System.Data.DataTable newdt = Getcompares<T>(inputstring);
            ///设置默认
            if (!newdt.IsDefault() && newdt.Rows.Count > 0)
            {
                List<T> getthis = Globals.Getallrecordsfromdt<T>(newdt);
                return getthis[0];
            }

            return default(T);
        }

        /// <summary>
        /// 获取数据,并缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable Getdts<T>()
        {
            string tablename = typeof(T).Name;

            DataTable newdt = Gettableinfosbycmd<T>();
            return newdt;
        }
        public static DataTable Gettableinfosbycmd<T>(string condition = default(string), string orderstring = default(string))
        {
            string tablename = typeof(T).Name;

            string commandstring = string.Format("select {0} from {1} ", new object[] { "*", tablename });

            if (!string.IsNullOrEmpty(condition))
                commandstring = commandstring + " where " + condition;

            if (!string.IsNullOrEmpty(orderstring))
                commandstring = commandstring + " order by " + orderstring;


            return sqlite.DBUtility.SQLiteHelper.Fill(commandstring, tablename);
        }
        #endregion
        /// <summary>
        /// dataparse方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static T DataParse<T>(string text = default(string), T defaultvalue = default(T))
        {
            if (!string.IsNullOrEmpty(text))
                try
                {
                    switch (Type.GetTypeCode(typeof(T)))
                    {
                        case TypeCode.Int32:
                            return (T)(int.Parse(text) as object);
                        case TypeCode.Double:
                            {
                                if (text.Trim().IndexOf('%') == text.Trim().Length - 1)
                                {
                                    text = text.Trim().Substring(0, text.Trim().Length - 1);
                                    return (T)(double.Parse(text) / 100 as object);
                                }
                                return (T)(double.Parse(text) as object);
                            }
                        case TypeCode.Single:
                            return (T)(float.Parse(text) as object);
                        case TypeCode.Int64:
                            return (T)(long.Parse(text) as object);
                        case TypeCode.Boolean:
                            return (T)(bool.Parse(text) as object);
                        case TypeCode.DateTime:
                            return (T)(DateTime.Parse(text) as object);
                        case TypeCode.Char:
                            return (T)(char.Parse(text) as object);
                        default:
                            return (T)(text as object);
                    }
                }
                catch { }

            return defaultvalue;
        }
    }

    public static class Extends
    {
        public static bool IsDefault<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default(T));
        }
    }
}
