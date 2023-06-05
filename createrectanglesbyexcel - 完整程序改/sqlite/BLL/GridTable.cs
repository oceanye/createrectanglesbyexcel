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
///GridTable���ݷ��ʾ�̬��
///</summary>
[Serializable]
public partial class GridTablebll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboGridTable dal = new createrectanglesbyexcel.sqlite.DAL.dboGridTable();
/// <summary>
/// ��ȡ���м�¼
/// </summary>
public static List<GridTable> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// �����ݿ��в���һ���¼�¼��
/// </summary>
public static int Insert(GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Insert(GridTable);
}
/// <summary>
/// �����ݿ��в���һ���¼�¼��������
/// </summary>
public static int Insert(SQLiteTransaction sp,GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Insert(sp,GridTable);
}
/// <summary>
/// �����ݱ�GridTable����һ����¼��
/// </summary>
public static int Update(GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Update(GridTable);
}
/// <summary>
/// �����ݱ�GridTable����һ����¼��������
/// </summary>
public static int Update(SQLiteTransaction sp,GridTable GridTable)
{
if (GridTable == null)
				return 0;
			return dal.Update(sp,GridTable);
}
/// <summary>
/// ɾ�����ݱ�GridTableһ����¼��
/// </summary>
public static int Delete(int GridTableID)
{
if (GridTableID < 0)
				return 0;
			return dal.Delete(GridTableID);
}
/// <summary>
/// ɾ�����ݱ�GridTable����һ����¼��������
/// </summary>
public static int Delete(SQLiteTransaction sp,int GridTableID)
{
if (GridTableID < 0)
				return 0;
			return dal.Delete(sp,GridTableID);
}
/// <summary>
/// �õ����ݱ�GridTableһ����¼��
/// </summary>
public static GridTable GetOnerecordbykey(int  GridTableID)
{
if (GridTableID < 0)
				return null;
			return dal.GetOnerecordbykey(GridTableID);
}
/// <summary>
/// �õ����ݱ�GridTable���м�¼��
/// </summary>
public static IList<GridTable> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// ����Ƿ����,����������
/// </summary>
public static bool IsExistGridTable(int GridTableID)
{
return dal.IsExistGridTable(GridTableID);
}
}
}
