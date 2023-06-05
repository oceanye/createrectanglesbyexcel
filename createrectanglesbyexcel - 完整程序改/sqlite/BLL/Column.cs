using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using createrectanglesbyexcel.sqlite.Model;
namespace createrectanglesbyexcel.sqlite.BLL
{
///<summary>
///Column数据访问静态类
///</summary>
[Serializable]
public partial class Columnbll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboColumn dal = new createrectanglesbyexcel.sqlite.DAL.dboColumn();
/// <summary>
/// 获取所有记录
/// </summary>
public static List<Column> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// 向数据库中插入一条新记录。
/// </summary>
public static int Insert(Column Column)
{
if (Column == null)
				return 0;
			return dal.Insert(Column);
}
/// <summary>
/// 向数据库中插入一条新记录。带事务
/// </summary>
public static int Insert(SQLiteTransaction sp,Column Column)
{
if (Column == null)
				return 0;
			return dal.Insert(sp,Column);
}
/// <summary>
/// 向数据表Column更新一条记录。
/// </summary>
public static int Update(Column Column)
{
if (Column == null)
				return 0;
			return dal.Update(Column);
}
/// <summary>
/// 向数据表Column更新一条记录。带事务。
/// </summary>
public static int Update(SQLiteTransaction sp,Column Column)
{
if (Column == null)
				return 0;
			return dal.Update(sp,Column);
}
/// <summary>
/// 删除数据表Column一条记录。
/// </summary>
public static int Delete(int ColumnID)
{
if (ColumnID < 0)
				return 0;
			return dal.Delete(ColumnID);
}
/// <summary>
/// 删除数据表Column更新一条记录。带事务。
/// </summary>
public static int Delete(SQLiteTransaction sp,int ColumnID)
{
if (ColumnID < 0)
				return 0;
			return dal.Delete(sp,ColumnID);
}
/// <summary>
/// 得到数据表Column一条记录。
/// </summary>
public static Column GetOnerecordbykey(int  ColumnID)
{
if (ColumnID < 0)
				return null;
			return dal.GetOnerecordbykey(ColumnID);
}
/// <summary>
/// 得到数据表Column所有记录。
/// </summary>
public static IList<Column> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// 检测是否存在,根据主键。
/// </summary>
public static bool IsExistColumn(int ColumnID)
{
return dal.IsExistColumn(ColumnID);
}
}
}
