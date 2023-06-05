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
///SectionTable数据访问类
///</summary>
[Serializable]
public partial class dboSectionTable
{
#region commandtext
private const string SQL_INSERT_SECTIONTABLE = @"INSERT INTO SectionTable
(
elementname,
elementtype,
elementmaterial,
description,
filename
)
VALUES
(
@elementname,
@elementtype,
@elementmaterial,
@description,
@filename
);
SELECT last_insert_rowid() AS 'Identity'";
private const string SQL_UPDATE_SECTIONTABLE = @"UPDATE SectionTable SET
elementname = @elementname,
elementtype = @elementtype,
elementmaterial = @elementmaterial,
description = @description,
filename = @filename
WHERE 
id = @id";
private const string SQL_DELETE_SECTIONTABLE = @"DELETE FROM SectionTable WHERE id = @id";
private const string SQL_SELECT_ALLSECTIONTABLES = @"SELECT id,elementname,elementtype,elementmaterial,description,filename FROM SectionTable ";
private const string SQL_SELECT_ONESECTIONTABLE = @"SELECT id,elementname,elementtype,elementmaterial,description,filename FROM SectionTable where id = @id limit 1";
private const string SQL_EXIST_SECTIONTABLE = @"SELECT COUNT(1) AS num FROM SectionTable WHERE id = @id limit 1";

private const string PARM_ID = "@id";
private const string PARM_ELEMENTNAME = "@elementname";
private const string PARM_ELEMENTTYPE = "@elementtype";
private const string PARM_ELEMENTMATERIAL = "@elementmaterial";
private const string PARM_DESCRIPTION = "@description";
private const string PARM_FILENAME = "@filename";
#endregion

#region 构造函数
///<summary>
///SectionTable构造函数
///</summary>
public dboSectionTable()
{}
#endregion
#region 公共方法
///<summary>
///向数据库插入一条记录
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(SectionTable SectionTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_SECTIONTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_ELEMENTNAME,DbType.String),
new SQLiteParameter(PARM_ELEMENTTYPE,DbType.String),
new SQLiteParameter(PARM_ELEMENTMATERIAL,DbType.String),
new SQLiteParameter(PARM_DESCRIPTION,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_SECTIONTABLE,parms);
};
parms[0].Value = SectionTable.id;
parms[1].Value = SectionTable.elementname;
parms[2].Value = SectionTable.elementtype;
parms[3].Value = SectionTable.elementmaterial;
parms[4].Value = SectionTable.description;
parms[5].Value = SectionTable.filename;
object obj=SQLiteHelper.ExecuteScalar(SQL_INSERT_SECTIONTABLE,SQLiteHelper.ConnectionString, CommandType.Text, parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据库插入一条记录，带事务
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(SQLiteTransaction sp,SectionTable SectionTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_SECTIONTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_ELEMENTNAME,DbType.String),
new SQLiteParameter(PARM_ELEMENTTYPE,DbType.String),
new SQLiteParameter(PARM_ELEMENTMATERIAL,DbType.String),
new SQLiteParameter(PARM_DESCRIPTION,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_SECTIONTABLE,parms);
};
parms[0].Value = SectionTable.id;
parms[1].Value = SectionTable.elementname;
parms[2].Value = SectionTable.elementtype;
parms[3].Value = SectionTable.elementmaterial;
parms[4].Value = SectionTable.description;
parms[5].Value = SectionTable.filename;
object obj=SQLiteHelper.ExecuteScalar(sp,SQL_INSERT_SECTIONTABLE, CommandType.Text,  parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据表更新一条记录。
///</summary>
/// <returns>影响的行数</returns>
public int Update(SectionTable SectionTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_SECTIONTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_ELEMENTNAME,DbType.String),
new SQLiteParameter(PARM_ELEMENTTYPE,DbType.String),
new SQLiteParameter(PARM_ELEMENTMATERIAL,DbType.String),
new SQLiteParameter(PARM_DESCRIPTION,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_SECTIONTABLE,parms);
};
parms[0].Value = SectionTable.id;
parms[1].Value = SectionTable.elementname;
parms[2].Value = SectionTable.elementtype;
parms[3].Value = SectionTable.elementmaterial;
parms[4].Value = SectionTable.description;
parms[5].Value = SectionTable.filename;
return SQLiteHelper.ExecuteNonQuery( SQL_UPDATE_SECTIONTABLE,SQLiteHelper.ConnectionString, CommandType.Text, parms);
}
///<summary>
///向数据表更新一条记录，带事务。
///</summary>
/// <returns>影响的行数</returns>
public int Update(SQLiteTransaction sp,SectionTable SectionTable)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_SECTIONTABLE);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_ELEMENTNAME,DbType.String),
new SQLiteParameter(PARM_ELEMENTTYPE,DbType.String),
new SQLiteParameter(PARM_ELEMENTMATERIAL,DbType.String),
new SQLiteParameter(PARM_DESCRIPTION,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_SECTIONTABLE,parms);
};
parms[0].Value = SectionTable.id;
parms[1].Value = SectionTable.elementname;
parms[2].Value = SectionTable.elementtype;
parms[3].Value = SectionTable.elementmaterial;
parms[4].Value = SectionTable.description;
parms[5].Value = SectionTable.filename;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_UPDATE_SECTIONTABLE, CommandType.Text,  parms);
}
///<summary>
///删除数据表的一条记录。
///</summary>
/// <returns>影响的行数</returns>
public int Delete(int id)
{
SQLiteParameter[] parms = {
new SQLiteParameter(PARM_ID,DbType.Int32)
};
parms[0].Value = id;
return SQLiteHelper.ExecuteNonQuery(SQL_DELETE_SECTIONTABLE,SQLiteHelper.ConnectionString, CommandType.Text,  parms);
}
///<summary>
///删除数据表的一条记录,带事务
///</summary>
/// <returns>影响的行数</returns>
public int Delete(SQLiteTransaction sp,int id)
{
SQLiteParameter[] parms = {
new SQLiteParameter(PARM_ID,DbType.Int32)
};
parms[0].Value = id;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_DELETE_SECTIONTABLE, CommandType.Text,  parms);
}
///<summary>
///得到SectionTable所有数据实体
///</summary>
/// <returns>得到数据实体</returns>
public List<SectionTable> PopulateAllrecordsFromDataRow(DataTable dt)
{
List<SectionTable> returns=null;
if (dt != null)
{
returns = new List<SectionTable>();
foreach (DataRow onerow in dt.Rows)
returns.Add(PopulateOnerecordFromDataRow(onerow));
}
return returns;
}

///<summary>
///得到SectionTable数据实体
///</summary>
/// <returns>得到数据实体</returns>
public SectionTable PopulateOnerecordFromDataRow(DataRow row)
{
SectionTable obj=new SectionTable();
if (row!=null)
{
obj.id = (row["id"]==DBNull.Value)?(int)0:int.Parse(row["id"].ToString());
obj.elementname = (row["elementname"]==DBNull.Value)?string.Empty:row["elementname"].ToString();
obj.elementtype = (row["elementtype"]==DBNull.Value)?string.Empty:row["elementtype"].ToString();
obj.elementmaterial = (row["elementmaterial"]==DBNull.Value)?string.Empty:row["elementmaterial"].ToString();
obj.description = (row["description"]==DBNull.Value)?string.Empty:row["description"].ToString();
obj.filename = (row["filename"]==DBNull.Value)?string.Empty:row["filename"].ToString();
}
else
{
return null;
}
return obj;
}
///<summary>
///得到SectionTable数据实体
///</summary>
/// <returns>得到数据实体</returns>
public SectionTable PopulateOnerecordFromDataRow(IDataReader dr)
{
SectionTable obj=new SectionTable();
if (dr!=null)
{
obj.id = (dr["id"]==DBNull.Value)?(int)0:int.Parse(dr["id"].ToString());
obj.elementname = (dr["elementname"]==DBNull.Value)?string.Empty:dr["elementname"].ToString();
obj.elementtype = (dr["elementtype"]==DBNull.Value)?string.Empty:dr["elementtype"].ToString();
obj.elementmaterial = (dr["elementmaterial"]==DBNull.Value)?string.Empty:dr["elementmaterial"].ToString();
obj.description = (dr["description"]==DBNull.Value)?string.Empty:dr["description"].ToString();
obj.filename = (dr["filename"]==DBNull.Value)?string.Empty:dr["filename"].ToString();
}
return obj;
}
///<summary>
///根据主键，返回一个SectionTable数据对象
///</summary>
/// <returns>得到数据对象</returns>
public SectionTable GetOnerecordbykey(int id)
{
SectionTable obj=null;
SQLiteParameter[] param={ 
new SQLiteParameter(PARM_ID, DbType.Int32)
};
param[0].Value=id;
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ONESECTIONTABLE,SQLiteHelper.ConnectionString, CommandType.Text,param)){
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
///得到数据表SectionTable所有记录
///</summary>
/// <returns>得到所有数据对象</returns>
public IList<SectionTable> GetAllrecords()
{
IList<SectionTable> obj=new List<SectionTable>(); 
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ALLSECTIONTABLES,SQLiteHelper.ConnectionString, CommandType.Text))
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
///检测是否存在，根据主键
///</summary>
/// <returns>是/否</returns>
public bool IsExistSectionTable(int id)
{
SQLiteParameter[] param={
            new SQLiteParameter(PARM_ID, DbType.Int32)};
param[0].Value=id;
int count = 0;
object obj = SQLiteHelper.ExecuteScalar(SQL_EXIST_SECTIONTABLE,SQLiteHelper.ConnectionString, CommandType.Text, param);
 if (obj != null) count = int.Parse(obj.ToString());
return count>0;
}
#endregion
}
}
