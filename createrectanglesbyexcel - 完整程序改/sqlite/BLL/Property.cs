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
///Property���ݷ��ʾ�̬��
///</summary>
[Serializable]
public partial class Propertybll
{
///<summary>
///SqlServe DAL
///</summary>
private static readonly createrectanglesbyexcel.sqlite.DAL.dboProperty dal = new createrectanglesbyexcel.sqlite.DAL.dboProperty();
/// <summary>
/// ��ȡ���м�¼
/// </summary>
public static List<Property> PopulateAllrecordsFromDataRow(DataTable dt)
{
if (dt == null)
                        				return null;
                        			return dal.PopulateAllrecordsFromDataRow(dt);
}

/// <summary>
/// �����ݿ��в���һ���¼�¼��
/// </summary>
public static int Insert(Property Property)
{
if (Property == null)
				return 0;
			return dal.Insert(Property);
}
/// <summary>
/// �����ݿ��в���һ���¼�¼��������
/// </summary>
public static int Insert(SQLiteTransaction sp,Property Property)
{
if (Property == null)
				return 0;
			return dal.Insert(sp,Property);
}
/// <summary>
/// �����ݱ�Property����һ����¼��
/// </summary>
public static int Update(Property Property)
{
if (Property == null)
				return 0;
			return dal.Update(Property);
}
/// <summary>
/// �����ݱ�Property����һ����¼��������
/// </summary>
public static int Update(SQLiteTransaction sp,Property Property)
{
if (Property == null)
				return 0;
			return dal.Update(sp,Property);
}
/// <summary>
/// ɾ�����ݱ�Propertyһ����¼��
/// </summary>
public static int Delete(int PropertyID)
{
if (PropertyID < 0)
				return 0;
			return dal.Delete(PropertyID);
}
/// <summary>
/// ɾ�����ݱ�Property����һ����¼��������
/// </summary>
public static int Delete(SQLiteTransaction sp,int PropertyID)
{
if (PropertyID < 0)
				return 0;
			return dal.Delete(sp,PropertyID);
}
/// <summary>
/// �õ����ݱ�Propertyһ����¼��
/// </summary>
public static Property GetOnerecordbykey(int  PropertyID)
{
if (PropertyID < 0)
				return null;
			return dal.GetOnerecordbykey(PropertyID);
}
/// <summary>
/// �õ����ݱ�Property���м�¼��
/// </summary>
public static IList<Property> GetAllrecords()
{
return dal.GetAllrecords();
}
/// <summary>
/// ����Ƿ����,����������
/// </summary>
public static bool IsExistProperty(int PropertyID)
{
return dal.IsExistProperty(PropertyID);
}
}
}
