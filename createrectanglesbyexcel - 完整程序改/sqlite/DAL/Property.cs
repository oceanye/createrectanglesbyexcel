using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using createrectanglesbyexcel.sqlite.Model;
using createrectanglesbyexcel.sqlite.DBUtility;
namespace createrectanglesbyexcel.sqlite.DAL
{
///<summary>
///Property���ݷ�����
///</summary>
[Serializable]
public partial class dboProperty
{
#region commandtext
private const string SQL_INSERT_PROPERTY = @"INSERT INTO Property
(
textheight,
textspace,
liangspace,
triangledi,
trianglegao
)
VALUES
(
@textheight,
@textspace,
@liangspace,
@triangledi,
@trianglegao
);
SELECT last_insert_rowid() AS 'Identity'";
private const string SQL_UPDATE_PROPERTY = @"UPDATE Property SET
textheight = @textheight,
textspace = @textspace,
liangspace = @liangspace,
triangledi = @triangledi,
trianglegao = @trianglegao
WHERE 
id = @id";
private const string SQL_DELETE_PROPERTY = @"DELETE FROM Property WHERE id = @id";
private const string SQL_SELECT_ALLPROPERTYS = @"SELECT id,textheight,textspace,liangspace,triangledi,trianglegao FROM Property ";
private const string SQL_SELECT_ONEPROPERTY = @"SELECT id,textheight,textspace,liangspace,triangledi,trianglegao FROM Property where id = @id limit 1";
private const string SQL_EXIST_PROPERTY = @"SELECT COUNT(1) AS num FROM Property WHERE id = @id limit 1";

private const string PARM_ID = "@id";
private const string PARM_TEXTHEIGHT = "@textheight";
private const string PARM_TEXTSPACE = "@textspace";
private const string PARM_LIANGSPACE = "@liangspace";
private const string PARM_TRIANGLEDI = "@triangledi";
private const string PARM_TRIANGLEGAO = "@trianglegao";
#endregion

#region ���캯��
///<summary>
///Property���캯��
///</summary>
public dboProperty()
{}
#endregion
#region ��������
///<summary>
///�����ݿ����һ����¼
///</summary>
/// <returns>�²����¼�ı��</returns>
public int Insert(Property Property)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_PROPERTY);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_TEXTHEIGHT,DbType.Double),
new SQLiteParameter(PARM_TEXTSPACE,DbType.Double),
new SQLiteParameter(PARM_LIANGSPACE,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEDI,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEGAO,DbType.Double)
};
SQLiteHelper.CacheParameters(SQL_INSERT_PROPERTY,parms);
};
parms[0].Value = Property.id;
parms[1].Value = Property.textheight;
parms[2].Value = Property.textspace;
parms[3].Value = Property.liangspace;
parms[4].Value = Property.triangledi;
parms[5].Value = Property.trianglegao;
object obj=SQLiteHelper.ExecuteScalar(SQL_INSERT_PROPERTY,SQLiteHelper.ConnectionString, CommandType.Text, parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///�����ݿ����һ����¼��������
///</summary>
/// <returns>�²����¼�ı��</returns>
public int Insert(SQLiteTransaction sp,Property Property)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_PROPERTY);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_TEXTHEIGHT,DbType.Double),
new SQLiteParameter(PARM_TEXTSPACE,DbType.Double),
new SQLiteParameter(PARM_LIANGSPACE,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEDI,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEGAO,DbType.Double)
};
SQLiteHelper.CacheParameters(SQL_INSERT_PROPERTY,parms);
};
parms[0].Value = Property.id;
parms[1].Value = Property.textheight;
parms[2].Value = Property.textspace;
parms[3].Value = Property.liangspace;
parms[4].Value = Property.triangledi;
parms[5].Value = Property.trianglegao;
object obj=SQLiteHelper.ExecuteScalar(sp,SQL_INSERT_PROPERTY, CommandType.Text,  parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///�����ݱ����һ����¼��
///</summary>
/// <returns>Ӱ�������</returns>
public int Update(Property Property)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_PROPERTY);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_TEXTHEIGHT,DbType.Double),
new SQLiteParameter(PARM_TEXTSPACE,DbType.Double),
new SQLiteParameter(PARM_LIANGSPACE,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEDI,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEGAO,DbType.Double)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_PROPERTY,parms);
};
parms[0].Value = Property.id;
parms[1].Value = Property.textheight;
parms[2].Value = Property.textspace;
parms[3].Value = Property.liangspace;
parms[4].Value = Property.triangledi;
parms[5].Value = Property.trianglegao;
return SQLiteHelper.ExecuteNonQuery( SQL_UPDATE_PROPERTY,SQLiteHelper.ConnectionString, CommandType.Text, parms);
}
///<summary>
///�����ݱ����һ����¼��������
///</summary>
/// <returns>Ӱ�������</returns>
public int Update(SQLiteTransaction sp,Property Property)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_PROPERTY);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_TEXTHEIGHT,DbType.Double),
new SQLiteParameter(PARM_TEXTSPACE,DbType.Double),
new SQLiteParameter(PARM_LIANGSPACE,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEDI,DbType.Double),
new SQLiteParameter(PARM_TRIANGLEGAO,DbType.Double)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_PROPERTY,parms);
};
parms[0].Value = Property.id;
parms[1].Value = Property.textheight;
parms[2].Value = Property.textspace;
parms[3].Value = Property.liangspace;
parms[4].Value = Property.triangledi;
parms[5].Value = Property.trianglegao;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_UPDATE_PROPERTY, CommandType.Text,  parms);
}
///<summary>
///ɾ�����ݱ��һ����¼��
///</summary>
/// <returns>Ӱ�������</returns>
public int Delete(int id)
{
SQLiteParameter[] parms = {
new SQLiteParameter(PARM_ID,DbType.Int32)
};
parms[0].Value = id;
return SQLiteHelper.ExecuteNonQuery(SQL_DELETE_PROPERTY,SQLiteHelper.ConnectionString, CommandType.Text,  parms);
}
///<summary>
///ɾ�����ݱ��һ����¼,������
///</summary>
/// <returns>Ӱ�������</returns>
public int Delete(SQLiteTransaction sp,int id)
{
SQLiteParameter[] parms = {
new SQLiteParameter(PARM_ID,DbType.Int32)
};
parms[0].Value = id;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_DELETE_PROPERTY, CommandType.Text,  parms);
}
///<summary>
///�õ�Property��������ʵ��
///</summary>
/// <returns>�õ�����ʵ��</returns>
public List<Property> PopulateAllrecordsFromDataRow(DataTable dt)
{
List<Property> returns=null;
if (dt != null)
{
returns = new List<Property>();
foreach (DataRow onerow in dt.Rows)
returns.Add(PopulateOnerecordFromDataRow(onerow));
}
return returns;
}

///<summary>
///�õ�Property����ʵ��
///</summary>
/// <returns>�õ�����ʵ��</returns>
public Property PopulateOnerecordFromDataRow(DataRow row)
{
Property obj=new Property();
if (row!=null)
{
obj.id = (row["id"]==DBNull.Value)?(int)0:int.Parse(row["id"].ToString());
obj.textheight = (row["textheight"]==DBNull.Value)?0:double.Parse(row["textheight"].ToString());
obj.textspace = (row["textspace"]==DBNull.Value)?0:double.Parse(row["textspace"].ToString());
obj.liangspace = (row["liangspace"]==DBNull.Value)?0:double.Parse(row["liangspace"].ToString());
obj.triangledi = (row["triangledi"]==DBNull.Value)?0:double.Parse(row["triangledi"].ToString());
obj.trianglegao = (row["trianglegao"]==DBNull.Value)?0:double.Parse(row["trianglegao"].ToString());
}
else
{
return null;
}
return obj;
}
///<summary>
///�õ�Property����ʵ��
///</summary>
/// <returns>�õ�����ʵ��</returns>
public Property PopulateOnerecordFromDataRow(IDataReader dr)
{
Property obj=new Property();
if (dr!=null)
{
obj.id = (dr["id"]==DBNull.Value)?(int)0:int.Parse(dr["id"].ToString());
obj.textheight = (dr["textheight"]==DBNull.Value)?0:double.Parse(dr["textheight"].ToString());
obj.textspace = (dr["textspace"]==DBNull.Value)?0:double.Parse(dr["textspace"].ToString());
obj.liangspace = (dr["liangspace"]==DBNull.Value)?0:double.Parse(dr["liangspace"].ToString());
obj.triangledi = (dr["triangledi"]==DBNull.Value)?0:double.Parse(dr["triangledi"].ToString());
obj.trianglegao = (dr["trianglegao"]==DBNull.Value)?0:double.Parse(dr["trianglegao"].ToString());
}
return obj;
}
///<summary>
///��������������һ��Property���ݶ���
///</summary>
/// <returns>�õ����ݶ���</returns>
public Property GetOnerecordbykey(int id)
{
Property obj=null;
SQLiteParameter[] param={ 
new SQLiteParameter(PARM_ID, DbType.Int32)
};
param[0].Value=id;
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ONEPROPERTY,SQLiteHelper.ConnectionString, CommandType.Text,param)){
if(dr!=null)
				{
while(dr.Read())
				{
obj=PopulateOnerecordFromDataRow(dr);
}
}
}
return obj;
}
///<summary>
///�õ����ݱ�Property���м�¼
///</summary>
/// <returns>�õ��������ݶ���</returns>
public IList<Property> GetAllrecords()
{
IList<Property> obj=new List<Property>(); 
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ALLPROPERTYS,SQLiteHelper.ConnectionString, CommandType.Text))
{
if(dr!=null)
				{
while(dr.Read())
{
					obj.Add(PopulateOnerecordFromDataRow(dr));
				}
}
}
return obj;
}
///<summary>
///����Ƿ���ڣ���������
///</summary>
/// <returns>��/��</returns>
public bool IsExistProperty(int id)
{
SQLiteParameter[] param={
            new SQLiteParameter(PARM_ID, DbType.Int32)};
param[0].Value=id;
int count = 0;
object obj = SQLiteHelper.ExecuteScalar(SQL_EXIST_PROPERTY,SQLiteHelper.ConnectionString, CommandType.Text, param);
 if (obj != null) count = int.Parse(obj.ToString());
return count>0;
}
#endregion
}
}
