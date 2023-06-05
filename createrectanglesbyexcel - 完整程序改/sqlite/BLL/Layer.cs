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
///Layer数据访问静态类
///</summary>
[Serializable]
public partial class Layerbll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboLayer dal = new createrectanglesbyexcel.sqlite.DAL.dboLayer();
/// <summary>
/// 获取所有记录
/// </summary>
public static List<Layer> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// 向数据库中插入一条新记录。
/// </summary>
public static int Insert(Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Insert(Layer);
}
/// <summary>
/// 向数据库中插入一条新记录。带事务
/// </summary>
public static int Insert(SQLiteTransaction sp,Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Insert(sp,Layer);
}
/// <summary>
/// 向数据表Layer更新一条记录。
/// </summary>
public static int Update(Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Update(Layer);
}
/// <summary>
/// 向数据表Layer更新一条记录。带事务。
/// </summary>
public static int Update(SQLiteTransaction sp,Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Update(sp,Layer);
}
/// <summary>
/// 删除数据表Layer一条记录。
/// </summary>
public static int Delete(int LayerID)
{
if (LayerID < 0)
				return 0;
			return dal.Delete(LayerID);
}
/// <summary>
/// 删除数据表Layer更新一条记录。带事务。
/// </summary>
public static int Delete(SQLiteTransaction sp,int LayerID)
{
if (LayerID < 0)
				return 0;
			return dal.Delete(sp,LayerID);
}
/// <summary>
/// 得到数据表Layer一条记录。
/// </summary>
public static Layer GetOnerecordbykey(int  LayerID)
{
if (LayerID < 0)
				return null;
			return dal.GetOnerecordbykey(LayerID);
}
/// <summary>
/// 得到数据表Layer所有记录。
/// </summary>
public static IList<Layer> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// 检测是否存在,根据主键。
/// </summary>
public static bool IsExistLayer(int LayerID)
{
return dal.IsExistLayer(LayerID);
}
}
}
