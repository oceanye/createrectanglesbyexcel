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
///GridTable���ݷ�����
///</summary>
[Serializable]
public partial class dboGridTable
{
#region commandtext
private const string SQL_INSERT_GRIDTABLE = @"INSERT INTO GridTable
(
thisid,
gridname,
gridpoint,
gridgroup,
gridtablepos,
filename
)
VALUES
(
@thisid,
@gridname,
@gridpoint,
@gridgroup,
@gridtablepos,
@filename
);
SELECT last_insert_rowid() AS 'Identity'";
private const string SQL_UPDATE_GRIDTABLE = @"UPDATE GridTable SET
thisid = @thisid,
gridname = @gridname,
gridpoint = @gridpoint,
gridgroup = @gridgroup,
gridtablepos = @gridtablepos,
filename = @filename
WHERE 
id = @id";
private const string SQL_DELETE_GRIDTABLE = @"DELETE FROM GridTable WHERE id = @id";
private const string SQL_SELECT_ALLGRIDTABLES = @"SELECT id,thisid,gridname,gridpoint,gridgroup,gridtablepos,filename FROM GridTable ";
private const string SQL_SELECT_ONEGRIDTABLE = @"SELECT id,thisid,gridname,gridpoint,gridgroup,gridtablepos,filename FROM GridTable where id = @id limit 1";
private const string SQL_EXIST_GRIDTABLE = @"SELECT COUNT(1) AS num FROM GridTable WHERE id = @id limit 1";

private const string PARM_ID = "@id";
private const string PARM_THISID = "@thisid";
private const string PARM_GRIDNAME = "@gridname";
private const string PARM_GRIDPOINT = "@gridpoint";
private const string PARM_GRIDGROUP = "@gridgroup";
private const string PARM_GRIDTABLEPOS = "@gridtablepos";
private const string PARM_FILENAME = "@filename";
#endregion

#region ���캯��
///<summary>
///GridTable���캯��
///</summary>
public dboGridTable()
{}
#endregion
#region ��������
///<summary>
///�����ݿ����һ����¼
///</summary>
/// <returns>�²����¼�ı��</returns>
public int Insert(GridTable GridTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_GRIDTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_THISID,DbType.Int32),
new SQLiteParameter(PARM_GRIDNAME,DbType.String),
new SQLiteParameter(PARM_GRIDPOINT,DbType.String),
new SQLiteParameter(PARM_GRIDGROUP,DbType.String),
new SQLiteParameter(PARM_GRIDTABLEPOS,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_GRIDTABLE,parms);
};
parms[0].Value = GridTable.id;
parms[1].Value = GridTable.thisid;
parms[2].Value = GridTable.gridname;
parms[3].Value = GridTable.gridpoint;
parms[4].Value = GridTable.gridgroup;
parms[5].Value = GridTable.gridtablepos;
parms[6].Value = GridTable.filename;
object obj=SQLiteHelper.ExecuteScalar(SQL_INSERT_GRIDTABLE,SQLiteHelper.ConnectionString, CommandType.Text, parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///�����ݿ����һ����¼��������
///</summary>
/// <returns>�²����¼�ı��</returns>
public int Insert(SQLiteTransaction sp,GridTable GridTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_GRIDTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_THISID,DbType.Int32),
new SQLiteParameter(PARM_GRIDNAME,DbType.String),
new SQLiteParameter(PARM_GRIDPOINT,DbType.String),
new SQLiteParameter(PARM_GRIDGROUP,DbType.String),
new SQLiteParameter(PARM_GRIDTABLEPOS,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_GRIDTABLE,parms);
};
parms[0].Value = GridTable.id;
parms[1].Value = GridTable.thisid;
parms[2].Value = GridTable.gridname;
parms[3].Value = GridTable.gridpoint;
parms[4].Value = GridTable.gridgroup;
parms[5].Value = GridTable.gridtablepos;
parms[6].Value = GridTable.filename;
object obj=SQLiteHelper.ExecuteScalar(sp,SQL_INSERT_GRIDTABLE, CommandType.Text,  parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///�����ݱ����һ����¼��
///</summary>
/// <returns>Ӱ�������</returns>
public int Update(GridTable GridTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_GRIDTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_THISID,DbType.Int32),
new SQLiteParameter(PARM_GRIDNAME,DbType.String),
new SQLiteParameter(PARM_GRIDPOINT,DbType.String),
new SQLiteParameter(PARM_GRIDGROUP,DbType.String),
new SQLiteParameter(PARM_GRIDTABLEPOS,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_GRIDTABLE,parms);
};
parms[0].Value = GridTable.id;
parms[1].Value = GridTable.thisid;
parms[2].Value = GridTable.gridname;
parms[3].Value = GridTable.gridpoint;
parms[4].Value = GridTable.gridgroup;
parms[5].Value = GridTable.gridtablepos;
parms[6].Value = GridTable.filename;
return SQLiteHelper.ExecuteNonQuery( SQL_UPDATE_GRIDTABLE,SQLiteHelper.ConnectionString, CommandType.Text, parms);
}
///<summary>
///�����ݱ����һ����¼��������
///</summary>
/// <returns>Ӱ�������</returns>
public int Update(SQLiteTransaction sp,GridTable GridTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_GRIDTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_THISID,DbType.Int32),
new SQLiteParameter(PARM_GRIDNAME,DbType.String),
new SQLiteParameter(PARM_GRIDPOINT,DbType.String),
new SQLiteParameter(PARM_GRIDGROUP,DbType.String),
new SQLiteParameter(PARM_GRIDTABLEPOS,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_GRIDTABLE,parms);
};
parms[0].Value = GridTable.id;
parms[1].Value = GridTable.thisid;
parms[2].Value = GridTable.gridname;
parms[3].Value = GridTable.gridpoint;
parms[4].Value = GridTable.gridgroup;
parms[5].Value = GridTable.gridtablepos;
parms[6].Value = GridTable.filename;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_UPDATE_GRIDTABLE, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(SQL_DELETE_GRIDTABLE,SQLiteHelper.ConnectionString, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(sp,SQL_DELETE_GRIDTABLE, CommandType.Text,  parms);
}
///<summary>
///�õ�GridTable��������ʵ��
///</summary>
/// <returns>�õ�����ʵ��</returns>
public List<GridTable> PopulateAllrecordsFromDataRow(DataTable dt)
{
List<GridTable> returns=null;
if (dt != null)
{
returns = new List<GridTable>();
foreach (DataRow onerow in dt.Rows)
returns.Add(PopulateOnerecordFromDataRow(onerow));
}
return returns;
}

///<summary>
///�õ�GridTable����ʵ��
///</summary>
/// <returns>�õ�����ʵ��</returns>
public GridTable PopulateOnerecordFromDataRow(DataRow row)
{
GridTable obj=new GridTable();
if (row!=null)
{
obj.id = (row["id"]==DBNull.Value)?(int)0:int.Parse(row["id"].ToString());
obj.thisid = (row["thisid"]==DBNull.Value)?(int)0:int.Parse(row["thisid"].ToString());
obj.gridname = (row["gridname"]==DBNull.Value)?string.Empty:row["gridname"].ToString();
obj.gridpoint = (row["gridpoint"]==DBNull.Value)?string.Empty:row["gridpoint"].ToString();
obj.gridgroup = (row["gridgroup"]==DBNull.Value)?string.Empty:row["gridgroup"].ToString();
obj.gridtablepos = (row["gridtablepos"]==DBNull.Value)?string.Empty:row["gridtablepos"].ToString();
obj.filename = (row["filename"]==DBNull.Value)?string.Empty:row["filename"].ToString();
}
else
{
return null;
}
return obj;
}
///<summary>
///�õ�GridTable����ʵ��
///</summary>
/// <returns>�õ�����ʵ��</returns>
public GridTable PopulateOnerecordFromDataRow(IDataReader dr)
{
GridTable obj=new GridTable();
if (dr!=null)
{
obj.id = (dr["id"]==DBNull.Value)?(int)0:int.Parse(dr["id"].ToString());
obj.thisid = (dr["thisid"]==DBNull.Value)?(int)0:int.Parse(dr["thisid"].ToString());
obj.gridname = (dr["gridname"]==DBNull.Value)?string.Empty:dr["gridname"].ToString();
obj.gridpoint = (dr["gridpoint"]==DBNull.Value)?string.Empty:dr["gridpoint"].ToString();
obj.gridgroup = (dr["gridgroup"]==DBNull.Value)?string.Empty:dr["gridgroup"].ToString();
obj.gridtablepos = (dr["gridtablepos"]==DBNull.Value)?string.Empty:dr["gridtablepos"].ToString();
obj.filename = (dr["filename"]==DBNull.Value)?string.Empty:dr["filename"].ToString();
}
return obj;
}
///<summary>
///��������������һ��GridTable���ݶ���
///</summary>
/// <returns>�õ����ݶ���</returns>
public GridTable GetOnerecordbykey(int id)
{
GridTable obj=null;
SQLiteParameter[] param={ 
new SQLiteParameter(PARM_ID, DbType.Int32)
};
param[0].Value=id;
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ONEGRIDTABLE,SQLiteHelper.ConnectionString, CommandType.Text,param)){
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
///�õ����ݱ�GridTable���м�¼
///</summary>
/// <returns>�õ��������ݶ���</returns>
public IList<GridTable> GetAllrecords()
{
IList<GridTable> obj=new List<GridTable>(); 
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ALLGRIDTABLES,SQLiteHelper.ConnectionString, CommandType.Text))
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
public bool IsExistGridTable(int id)
{
SQLiteParameter[] param={
            new SQLiteParameter(PARM_ID, DbType.Int32)};
param[0].Value=id;
int count = 0;
object obj = SQLiteHelper.ExecuteScalar(SQL_EXIST_GRIDTABLE,SQLiteHelper.ConnectionString, CommandType.Text, param);
 if (obj != null) count = int.Parse(obj.ToString());
return count>0;
}
#endregion
}
}
