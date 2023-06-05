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
///Column���ݷ��ʾ�̬��
///</summary>
[Serializable]
public partial class Columnbll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboColumn dal = new createrectanglesbyexcel.sqlite.DAL.dboColumn();
/// <summary>
/// ��ȡ���м�¼
/// </summary>
public static List<Column> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// �����ݿ��в���һ���¼�¼��
/// </summary>
public static int Insert(Column Column)
{
if (Column == null)
				return 0;
			return dal.Insert(Column);
}
/// <summary>
/// �����ݿ��в���һ���¼�¼��������
/// </summary>
public static int Insert(SQLiteTransaction sp,Column Column)
{
if (Column == null)
				return 0;
			return dal.Insert(sp,Column);
}
/// <summary>
/// �����ݱ�Column����һ����¼��
/// </summary>
public static int Update(Column Column)
{
if (Column == null)
				return 0;
			return dal.Update(Column);
}
/// <summary>
/// �����ݱ�Column����һ����¼��������
/// </summary>
public static int Update(SQLiteTransaction sp,Column Column)
{
if (Column == null)
				return 0;
			return dal.Update(sp,Column);
}
/// <summary>
/// ɾ�����ݱ�Columnһ����¼��
/// </summary>
public static int Delete(int ColumnID)
{
if (ColumnID < 0)
				return 0;
			return dal.Delete(ColumnID);
}
/// <summary>
/// ɾ�����ݱ�Column����һ����¼��������
/// </summary>
public static int Delete(SQLiteTransaction sp,int ColumnID)
{
if (ColumnID < 0)
				return 0;
			return dal.Delete(sp,ColumnID);
}
/// <summary>
/// �õ����ݱ�Columnһ����¼��
/// </summary>
public static Column GetOnerecordbykey(int  ColumnID)
{
if (ColumnID < 0)
				return null;
			return dal.GetOnerecordbykey(ColumnID);
}
/// <summary>
/// �õ����ݱ�Column���м�¼��
/// </summary>
public static IList<Column> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// ����Ƿ����,����������
/// </summary>
public static bool IsExistColumn(int ColumnID)
{
return dal.IsExistColumn(ColumnID);
}
}
}
