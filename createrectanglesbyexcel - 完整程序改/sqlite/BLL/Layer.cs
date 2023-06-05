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
///Layer���ݷ��ʾ�̬��
///</summary>
[Serializable]
public partial class Layerbll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboLayer dal = new createrectanglesbyexcel.sqlite.DAL.dboLayer();
/// <summary>
/// ��ȡ���м�¼
/// </summary>
public static List<Layer> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// �����ݿ��в���һ���¼�¼��
/// </summary>
public static int Insert(Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Insert(Layer);
}
/// <summary>
/// �����ݿ��в���һ���¼�¼��������
/// </summary>
public static int Insert(SQLiteTransaction sp,Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Insert(sp,Layer);
}
/// <summary>
/// �����ݱ�Layer����һ����¼��
/// </summary>
public static int Update(Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Update(Layer);
}
/// <summary>
/// �����ݱ�Layer����һ����¼��������
/// </summary>
public static int Update(SQLiteTransaction sp,Layer Layer)
{
if (Layer == null)
				return 0;
			return dal.Update(sp,Layer);
}
/// <summary>
/// ɾ�����ݱ�Layerһ����¼��
/// </summary>
public static int Delete(int LayerID)
{
if (LayerID < 0)
				return 0;
			return dal.Delete(LayerID);
}
/// <summary>
/// ɾ�����ݱ�Layer����һ����¼��������
/// </summary>
public static int Delete(SQLiteTransaction sp,int LayerID)
{
if (LayerID < 0)
				return 0;
			return dal.Delete(sp,LayerID);
}
/// <summary>
/// �õ����ݱ�Layerһ����¼��
/// </summary>
public static Layer GetOnerecordbykey(int  LayerID)
{
if (LayerID < 0)
				return null;
			return dal.GetOnerecordbykey(LayerID);
}
/// <summary>
/// �õ����ݱ�Layer���м�¼��
/// </summary>
public static IList<Layer> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// ����Ƿ����,����������
/// </summary>
public static bool IsExistLayer(int LayerID)
{
return dal.IsExistLayer(LayerID);
}
}
}
