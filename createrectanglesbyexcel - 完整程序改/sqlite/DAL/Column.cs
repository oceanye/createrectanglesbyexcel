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
///Column数据访问类
///</summary>
[Serializable]
public partial class dboColumn
{
#region commandtext
private const string SQL_INSERT_COLUMN = @"INSERT INTO Column
(
point,
colx,
coly,
colb,
colh,
colmnname,
filename
)
VALUES
(
@point,
@colx,
@coly,
@colb,
@colh,
@colmnname,
@filename
);
SELECT last_insert_rowid() AS 'Identity'";
private const string SQL_UPDATE_COLUMN = @"UPDATE Column SET
point = @point,
colx = @colx,
coly = @coly,
colb = @colb,
colh = @colh,
colmnname = @colmnname,
filename = @filename
WHERE 
id = @id";
private const string SQL_DELETE_COLUMN = @"DELETE FROM Column WHERE id = @id";
private const string SQL_SELECT_ALLCOLUMNS = @"SELECT id,point,colx,coly,colb,colh,colmnname,filename FROM Column ";
private const string SQL_SELECT_ONECOLUMN = @"SELECT id,point,colx,coly,colb,colh,colmnname,filename FROM Column where id = @id limit 1";
private const string SQL_EXIST_COLUMN = @"SELECT COUNT(1) AS num FROM Column WHERE id = @id limit 1";

private const string PARM_ID = "@id";
private const string PARM_POINT = "@point";
private const string PARM_COLX = "@colx";
private const string PARM_COLY = "@coly";
private const string PARM_COLB = "@colb";
private const string PARM_COLH = "@colh";
private const string PARM_COLMNNAME = "@colmnname";
private const string PARM_FILENAME = "@filename";
#endregion

#region 构造函数
///<summary>
///Column构造函数
///</summary>
public dboColumn()
{}
#endregion
#region 公共方法
///<summary>
///向数据库插入一条记录
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(Column Column)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_COLUMN);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_POINT,DbType.String),
new SQLiteParameter(PARM_COLX,DbType.Double),
new SQLiteParameter(PARM_COLY,DbType.Double),
new SQLiteParameter(PARM_COLB,DbType.Double),
new SQLiteParameter(PARM_COLH,DbType.Double),
new SQLiteParameter(PARM_COLMNNAME,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_COLUMN,parms);
};
parms[0].Value = Column.id;
parms[1].Value = Column.point;
parms[2].Value = Column.colx;
parms[3].Value = Column.coly;
parms[4].Value = Column.colb;
parms[5].Value = Column.colh;
parms[6].Value = Column.colmnname;
parms[7].Value = Column.filename;
object obj=SQLiteHelper.ExecuteScalar(SQL_INSERT_COLUMN,SQLiteHelper.ConnectionString, CommandType.Text, parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据库插入一条记录，带事务
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(SQLiteTransaction sp,Column Column)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_COLUMN);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_POINT,DbType.String),
new SQLiteParameter(PARM_COLX,DbType.Double),
new SQLiteParameter(PARM_COLY,DbType.Double),
new SQLiteParameter(PARM_COLB,DbType.Double),
new SQLiteParameter(PARM_COLH,DbType.Double),
new SQLiteParameter(PARM_COLMNNAME,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_COLUMN,parms);
};
parms[0].Value = Column.id;
parms[1].Value = Column.point;
parms[2].Value = Column.colx;
parms[3].Value = Column.coly;
parms[4].Value = Column.colb;
parms[5].Value = Column.colh;
parms[6].Value = Column.colmnname;
parms[7].Value = Column.filename;
object obj=SQLiteHelper.ExecuteScalar(sp,SQL_INSERT_COLUMN, CommandType.Text,  parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据表更新一条记录。
///</summary>
/// <returns>影响的行数</returns>
public int Update(Column Column)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_COLUMN);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_POINT,DbType.String),
new SQLiteParameter(PARM_COLX,DbType.Double),
new SQLiteParameter(PARM_COLY,DbType.Double),
new SQLiteParameter(PARM_COLB,DbType.Double),
new SQLiteParameter(PARM_COLH,DbType.Double),
new SQLiteParameter(PARM_COLMNNAME,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_COLUMN,parms);
};
parms[0].Value = Column.id;
parms[1].Value = Column.point;
parms[2].Value = Column.colx;
parms[3].Value = Column.coly;
parms[4].Value = Column.colb;
parms[5].Value = Column.colh;
parms[6].Value = Column.colmnname;
parms[7].Value = Column.filename;
return SQLiteHelper.ExecuteNonQuery( SQL_UPDATE_COLUMN,SQLiteHelper.ConnectionString, CommandType.Text, parms);
}
///<summary>
///向数据表更新一条记录，带事务。
///</summary>
/// <returns>影响的行数</returns>
public int Update(SQLiteTransaction sp,Column Column)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_COLUMN);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_POINT,DbType.String),
new SQLiteParameter(PARM_COLX,DbType.Double),
new SQLiteParameter(PARM_COLY,DbType.Double),
new SQLiteParameter(PARM_COLB,DbType.Double),
new SQLiteParameter(PARM_COLH,DbType.Double),
new SQLiteParameter(PARM_COLMNNAME,DbType.String),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_COLUMN,parms);
};
parms[0].Value = Column.id;
parms[1].Value = Column.point;
parms[2].Value = Column.colx;
parms[3].Value = Column.coly;
parms[4].Value = Column.colb;
parms[5].Value = Column.colh;
parms[6].Value = Column.colmnname;
parms[7].Value = Column.filename;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_UPDATE_COLUMN, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(SQL_DELETE_COLUMN,SQLiteHelper.ConnectionString, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(sp,SQL_DELETE_COLUMN, CommandType.Text,  parms);
}
///<summary>
///得到Column所有数据实体
///</summary>
/// <returns>得到数据实体</returns>
public List<Column> PopulateAllrecordsFromDataRow(DataTable dt)
{
List<Column> returns=null;
if (dt != null)
{
returns = new List<Column>();
foreach (DataRow onerow in dt.Rows)
returns.Add(PopulateOnerecordFromDataRow(onerow));
}
return returns;
}

///<summary>
///得到Column数据实体
///</summary>
/// <returns>得到数据实体</returns>
public Column PopulateOnerecordFromDataRow(DataRow row)
{
Column obj=new Column();
if (row!=null)
{
obj.id = (row["id"]==DBNull.Value)?(int)0:int.Parse(row["id"].ToString());
obj.point = (row["point"]==DBNull.Value)?string.Empty:row["point"].ToString();
obj.colx = (row["colx"]==DBNull.Value)?0:double.Parse(row["colx"].ToString());
obj.coly = (row["coly"]==DBNull.Value)?0:double.Parse(row["coly"].ToString());
obj.colb = (row["colb"]==DBNull.Value)?0:double.Parse(row["colb"].ToString());
obj.colh = (row["colh"]==DBNull.Value)?0:double.Parse(row["colh"].ToString());
obj.colmnname = (row["colmnname"]==DBNull.Value)?string.Empty:row["colmnname"].ToString();
obj.filename = (row["filename"]==DBNull.Value)?string.Empty:row["filename"].ToString();
}
else
{
return null;
}
return obj;
}
///<summary>
///得到Column数据实体
///</summary>
/// <returns>得到数据实体</returns>
public Column PopulateOnerecordFromDataRow(IDataReader dr)
{
Column obj=new Column();
if (dr!=null)
{
obj.id = (dr["id"]==DBNull.Value)?(int)0:int.Parse(dr["id"].ToString());
obj.point = (dr["point"]==DBNull.Value)?string.Empty:dr["point"].ToString();
obj.colx = (dr["colx"]==DBNull.Value)?0:double.Parse(dr["colx"].ToString());
obj.coly = (dr["coly"]==DBNull.Value)?0:double.Parse(dr["coly"].ToString());
obj.colb = (dr["colb"]==DBNull.Value)?0:double.Parse(dr["colb"].ToString());
obj.colh = (dr["colh"]==DBNull.Value)?0:double.Parse(dr["colh"].ToString());
obj.colmnname = (dr["colmnname"]==DBNull.Value)?string.Empty:dr["colmnname"].ToString();
obj.filename = (dr["filename"]==DBNull.Value)?string.Empty:dr["filename"].ToString();
}
return obj;
}
///<summary>
///根据主键，返回一个Column数据对象
///</summary>
/// <returns>得到数据对象</returns>
public Column GetOnerecordbykey(int id)
{
Column obj=null;
SQLiteParameter[] param={ 
new SQLiteParameter(PARM_ID, DbType.Int32)
};
param[0].Value=id;
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ONECOLUMN,SQLiteHelper.ConnectionString, CommandType.Text,param)){
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
///得到数据表Column所有记录
///</summary>
/// <returns>得到所有数据对象</returns>
public IList<Column> GetAllrecords()
{
IList<Column> obj=new List<Column>(); 
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ALLCOLUMNS,SQLiteHelper.ConnectionString, CommandType.Text))
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
public bool IsExistColumn(int id)
{
SQLiteParameter[] param={
            new SQLiteParameter(PARM_ID, DbType.Int32)};
param[0].Value=id;
int count = 0;
object obj = SQLiteHelper.ExecuteScalar(SQL_EXIST_COLUMN,SQLiteHelper.ConnectionString, CommandType.Text, param);
 if (obj != null) count = int.Parse(obj.ToString());
return count>0;
}
#endregion
}
}
