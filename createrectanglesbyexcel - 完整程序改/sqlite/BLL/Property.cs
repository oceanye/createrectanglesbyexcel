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
///Property数据访问静态类
///</summary>
[Serializable]
public partial class Propertybll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboProperty dal = new createrectanglesbyexcel.sqlite.DAL.dboProperty();
/// <summary>
/// 获取所有记录
/// </summary>
public static List<Property> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// 向数据库中插入一条新记录。
/// </summary>
public static int Insert(Property Property)
{
if (Property == null)
				return 0;
			return dal.Insert(Property);
}
/// <summary>
/// 向数据库中插入一条新记录。带事务
/// </summary>
public static int Insert(SQLiteTransaction sp,Property Property)
{
if (Property == null)
				return 0;
			return dal.Insert(sp,Property);
}
/// <summary>
/// 向数据表Property更新一条记录。
/// </summary>
public static int Update(Property Property)
{
if (Property == null)
				return 0;
			return dal.Update(Property);
}
/// <summary>
/// 向数据表Property更新一条记录。带事务。
/// </summary>
public static int Update(SQLiteTransaction sp,Property Property)
{
if (Property == null)
				return 0;
			return dal.Update(sp,Property);
}
/// <summary>
/// 删除数据表Property一条记录。
/// </summary>
public static int Delete(int PropertyID)
{
if (PropertyID < 0)
				return 0;
			return dal.Delete(PropertyID);
}
/// <summary>
/// 删除数据表Property更新一条记录。带事务。
/// </summary>
public static int Delete(SQLiteTransaction sp,int PropertyID)
{
if (PropertyID < 0)
				return 0;
			return dal.Delete(sp,PropertyID);
}
/// <summary>
/// 得到数据表Property一条记录。
/// </summary>
public static Property GetOnerecordbykey(int  PropertyID)
{
if (PropertyID < 0)
				return null;
			return dal.GetOnerecordbykey(PropertyID);
}
/// <summary>
/// 得到数据表Property所有记录。
/// </summary>
public static IList<Property> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// 检测是否存在,根据主键。
/// </summary>
public static bool IsExistProperty(int PropertyID)
{
return dal.IsExistProperty(PropertyID);
}
}
}
