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
///GridTable数据访问静态类
///</summary>
[Serializable]
public partial class GridTablebll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboGridTable dal = new createrectanglesbyexcel.sqlite.DAL.dboGridTable();
/// <summary>
/// 获取所有记录
/// </summary>
public static List<GridTable> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// 向数据库中插入一条新记录。
/// </summary>
public static int Insert(GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Insert(GridTable);
}
/// <summary>
/// 向数据库中插入一条新记录。带事务
/// </summary>
public static int Insert(SQLiteTransaction sp,GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Insert(sp,GridTable);
}
/// <summary>
/// 向数据表GridTable更新一条记录。
/// </summary>
public static int Update(GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Update(GridTable);
}
/// <summary>
/// 向数据表GridTable更新一条记录。带事务。
/// </summary>
public static int Update(SQLiteTransaction sp,GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Update(sp,GridTable);
}
/// <summary>
/// 删除数据表GridTable一条记录。
/// </summary>
public static int Delete(int GridTableID)
{
if (GridTableID < 0)
				return 0;
			return dal.Delete(GridTableID);
}
/// <summary>
/// 删除数据表GridTable更新一条记录。带事务。
/// </summary>
public static int Delete(SQLiteTransaction sp,int GridTableID)
{
if (GridTableID < 0)
				return 0;
			return dal.Delete(sp,GridTableID);
}
/// <summary>
/// 得到数据表GridTable一条记录。
/// </summary>
public static GridTable GetOnerecordbykey(int  GridTableID)
{
if (GridTableID < 0)
				return null;
			return dal.GetOnerecordbykey(GridTableID);
}
/// <summary>
/// 得到数据表GridTable所有记录。
/// </summary>
public static IList<GridTable> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// 检测是否存在,根据主键。
/// </summary>
public static bool IsExistGridTable(int GridTableID)
{
return dal.IsExistGridTable(GridTableID);
}
}
}
