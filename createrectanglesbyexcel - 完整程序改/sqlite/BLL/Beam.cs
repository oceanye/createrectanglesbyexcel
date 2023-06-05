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
///Beam数据访问静态类
///</summary>
[Serializable]
public partial class Beambll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboBeam dal = new createrectanglesbyexcel.sqlite.DAL.dboBeam();
/// <summary>
/// 获取所有记录
/// </summary>
public static List<Beam> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// 向数据库中插入一条新记录。
/// </summary>
public static int Insert(Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Insert(Beam);
}
/// <summary>
/// 向数据库中插入一条新记录。带事务
/// </summary>
public static int Insert(SQLiteTransaction sp,Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Insert(sp,Beam);
}
/// <summary>
/// 向数据表Beam更新一条记录。
/// </summary>
public static int Update(Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Update(Beam);
}
/// <summary>
/// 向数据表Beam更新一条记录。带事务。
/// </summary>
public static int Update(SQLiteTransaction sp,Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Update(sp,Beam);
}
/// <summary>
/// 删除数据表Beam一条记录。
/// </summary>
public static int Delete(int BeamID)
{
if (BeamID < 0)
				return 0;
			return dal.Delete(BeamID);
}
/// <summary>
/// 删除数据表Beam更新一条记录。带事务。
/// </summary>
public static int Delete(SQLiteTransaction sp,int BeamID)
{
if (BeamID < 0)
				return 0;
			return dal.Delete(sp,BeamID);
}
/// <summary>
/// 得到数据表Beam一条记录。
/// </summary>
public static Beam GetOnerecordbykey(int  BeamID)
{
if (BeamID < 0)
				return null;
			return dal.GetOnerecordbykey(BeamID);
}
/// <summary>
/// 得到数据表Beam所有记录。
/// </summary>
public static IList<Beam> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// 检测是否存在,根据主键。
/// </summary>
public static bool IsExistBeam(int BeamID)
{
return dal.IsExistBeam(BeamID);
}
}
}
