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
///SectionTable数据访问静态类
///</summary>
[Serializable]
public partial class SectionTablebll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboSectionTable dal = new createrectanglesbyexcel.sqlite.DAL.dboSectionTable();
/// <summary>
/// 获取所有记录
/// </summary>
public static List<SectionTable> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// 向数据库中插入一条新记录。
/// </summary>
public static int Insert(SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Insert(SectionTable);
}
/// <summary>
/// 向数据库中插入一条新记录。带事务
/// </summary>
public static int Insert(SQLiteTransaction sp,SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Insert(sp,SectionTable);
}
/// <summary>
/// 向数据表SectionTable更新一条记录。
/// </summary>
public static int Update(SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Update(SectionTable);
}
/// <summary>
/// 向数据表SectionTable更新一条记录。带事务。
/// </summary>
public static int Update(SQLiteTransaction sp,SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Update(sp,SectionTable);
}
/// <summary>
/// 删除数据表SectionTable一条记录。
/// </summary>
public static int Delete(int SectionTableID)
{
if (SectionTableID < 0)
				return 0;
			return dal.Delete(SectionTableID);
}
/// <summary>
/// 删除数据表SectionTable更新一条记录。带事务。
/// </summary>
public static int Delete(SQLiteTransaction sp,int SectionTableID)
{
if (SectionTableID < 0)
				return 0;
			return dal.Delete(sp,SectionTableID);
}
/// <summary>
/// 得到数据表SectionTable一条记录。
/// </summary>
public static SectionTable GetOnerecordbykey(int  SectionTableID)
{
if (SectionTableID < 0)
				return null;
			return dal.GetOnerecordbykey(SectionTableID);
}
/// <summary>
/// 得到数据表SectionTable所有记录。
/// </summary>
public static IList<SectionTable> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// 检测是否存在,根据主键。
/// </summary>
public static bool IsExistSectionTable(int SectionTableID)
{
return dal.IsExistSectionTable(SectionTableID);
}
}
}
