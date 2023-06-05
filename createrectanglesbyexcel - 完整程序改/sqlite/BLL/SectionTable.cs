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
///SectionTable���ݷ��ʾ�̬��
///</summary>
[Serializable]
public partial class SectionTablebll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboSectionTable dal = new createrectanglesbyexcel.sqlite.DAL.dboSectionTable();
/// <summary>
/// ��ȡ���м�¼
/// </summary>
public static List<SectionTable> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// �����ݿ��в���һ���¼�¼��
/// </summary>
public static int Insert(SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Insert(SectionTable);
}
/// <summary>
/// �����ݿ��в���һ���¼�¼��������
/// </summary>
public static int Insert(SQLiteTransaction sp,SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Insert(sp,SectionTable);
}
/// <summary>
/// �����ݱ�SectionTable����һ����¼��
/// </summary>
public static int Update(SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Update(SectionTable);
}
/// <summary>
/// �����ݱ�SectionTable����һ����¼��������
/// </summary>
public static int Update(SQLiteTransaction sp,SectionTable SectionTable)
{
if (SectionTable == null)
				return 0;
			return dal.Update(sp,SectionTable);
}
/// <summary>
/// ɾ�����ݱ�SectionTableһ����¼��
/// </summary>
public static int Delete(int SectionTableID)
{
if (SectionTableID < 0)
				return 0;
			return dal.Delete(SectionTableID);
}
/// <summary>
/// ɾ�����ݱ�SectionTable����һ����¼��������
/// </summary>
public static int Delete(SQLiteTransaction sp,int SectionTableID)
{
if (SectionTableID < 0)
				return 0;
			return dal.Delete(sp,SectionTableID);
}
/// <summary>
/// �õ����ݱ�SectionTableһ����¼��
/// </summary>
public static SectionTable GetOnerecordbykey(int  SectionTableID)
{
if (SectionTableID < 0)
				return null;
			return dal.GetOnerecordbykey(SectionTableID);
}
/// <summary>
/// �õ����ݱ�SectionTable���м�¼��
/// </summary>
public static IList<SectionTable> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// ����Ƿ����,����������
/// </summary>
public static bool IsExistSectionTable(int SectionTableID)
{
return dal.IsExistSectionTable(SectionTableID);
}
}
}
