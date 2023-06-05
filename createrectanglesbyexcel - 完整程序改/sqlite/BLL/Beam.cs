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
///Beam���ݷ��ʾ�̬��
///</summary>
[Serializable]
public partial class Beambll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboBeam dal = new createrectanglesbyexcel.sqlite.DAL.dboBeam();
/// <summary>
/// ��ȡ���м�¼
/// </summary>
public static List<Beam> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// �����ݿ��в���һ���¼�¼��
/// </summary>
public static int Insert(Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Insert(Beam);
}
/// <summary>
/// �����ݿ��в���һ���¼�¼��������
/// </summary>
public static int Insert(SQLiteTransaction sp,Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Insert(sp,Beam);
}
/// <summary>
/// �����ݱ�Beam����һ����¼��
/// </summary>
public static int Update(Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Update(Beam);
}
/// <summary>
/// �����ݱ�Beam����һ����¼��������
/// </summary>
public static int Update(SQLiteTransaction sp,Beam Beam)
{
if (Beam == null)
				return 0;
			return dal.Update(sp,Beam);
}
/// <summary>
/// ɾ�����ݱ�Beamһ����¼��
/// </summary>
public static int Delete(int BeamID)
{
if (BeamID < 0)
				return 0;
			return dal.Delete(BeamID);
}
/// <summary>
/// ɾ�����ݱ�Beam����һ����¼��������
/// </summary>
public static int Delete(SQLiteTransaction sp,int BeamID)
{
if (BeamID < 0)
				return 0;
			return dal.Delete(sp,BeamID);
}
/// <summary>
/// �õ����ݱ�Beamһ����¼��
/// </summary>
public static Beam GetOnerecordbykey(int  BeamID)
{
if (BeamID < 0)
				return null;
			return dal.GetOnerecordbykey(BeamID);
}
/// <summary>
/// �õ����ݱ�Beam���м�¼��
/// </summary>
public static IList<Beam> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// ����Ƿ����,����������
/// </summary>
public static bool IsExistBeam(int BeamID)
{
return dal.IsExistBeam(BeamID);
}
}
}
