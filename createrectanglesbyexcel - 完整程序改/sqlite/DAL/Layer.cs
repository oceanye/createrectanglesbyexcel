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
///Layer数据访问类
///</summary>
[Serializable]
public partial class dboLayer
{
#region commandtext
private const string SQL_INSERT_LAYER = @"INSERT INTO Layer
(
layerflag,
layername,
linetype
)
VALUES
(
@layerflag,
@layername,
@linetype
);
SELECT last_insert_rowid() AS 'Identity'";
private const string SQL_UPDATE_LAYER = @"UPDATE Layer SET
layerflag = @layerflag,
layername = @layername,
linetype = @linetype
WHERE 
id = @id";
private const string SQL_DELETE_LAYER = @"DELETE FROM Layer WHERE id = @id";
private const string SQL_SELECT_ALLLAYERS = @"SELECT id,layerflag,layername,linetype FROM Layer ";
private const string SQL_SELECT_ONELAYER = @"SELECT id,layerflag,layername,linetype FROM Layer where id = @id limit 1";
private const string SQL_EXIST_LAYER = @"SELECT COUNT(1) AS num FROM Layer WHERE id = @id limit 1";

private const string PARM_ID = "@id";
private const string PARM_LAYERFLAG = "@layerflag";
private const string PARM_LAYERNAME = "@layername";
private const string PARM_LINETYPE = "@linetype";
#endregion

#region 构造函数
///<summary>
///Layer构造函数
///</summary>
public dboLayer()
{}
#endregion
#region 公共方法
///<summary>
///向数据库插入一条记录
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(Layer Layer)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_LAYER);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LAYERFLAG,DbType.String),
new SQLiteParameter(PARM_LAYERNAME,DbType.String),
new SQLiteParameter(PARM_LINETYPE,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_LAYER,parms);
};
parms[0].Value = Layer.id;
parms[1].Value = Layer.layerflag;
parms[2].Value = Layer.layername;
parms[3].Value = Layer.linetype;
object obj=SQLiteHelper.ExecuteScalar(SQL_INSERT_LAYER,SQLiteHelper.ConnectionString, CommandType.Text, parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据库插入一条记录，带事务
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(SQLiteTransaction sp,Layer Layer)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_LAYER);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LAYERFLAG,DbType.String),
new SQLiteParameter(PARM_LAYERNAME,DbType.String),
new SQLiteParameter(PARM_LINETYPE,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_LAYER,parms);
};
parms[0].Value = Layer.id;
parms[1].Value = Layer.layerflag;
parms[2].Value = Layer.layername;
parms[3].Value = Layer.linetype;
object obj=SQLiteHelper.ExecuteScalar(sp,SQL_INSERT_LAYER, CommandType.Text,  parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据表更新一条记录。
///</summary>
/// <returns>影响的行数</returns>
public int Update(Layer Layer)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_LAYER);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LAYERFLAG,DbType.String),
new SQLiteParameter(PARM_LAYERNAME,DbType.String),
new SQLiteParameter(PARM_LINETYPE,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_LAYER,parms);
};
parms[0].Value = Layer.id;
parms[1].Value = Layer.layerflag;
parms[2].Value = Layer.layername;
parms[3].Value = Layer.linetype;
return SQLiteHelper.ExecuteNonQuery( SQL_UPDATE_LAYER,SQLiteHelper.ConnectionString, CommandType.Text, parms);
}
///<summary>
///向数据表更新一条记录，带事务。
///</summary>
/// <returns>影响的行数</returns>
public int Update(SQLiteTransaction sp,Layer Layer)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_LAYER);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LAYERFLAG,DbType.String),
new SQLiteParameter(PARM_LAYERNAME,DbType.String),
new SQLiteParameter(PARM_LINETYPE,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_LAYER,parms);
};
parms[0].Value = Layer.id;
parms[1].Value = Layer.layerflag;
parms[2].Value = Layer.layername;
parms[3].Value = Layer.linetype;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_UPDATE_LAYER, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(SQL_DELETE_LAYER,SQLiteHelper.ConnectionString, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(sp,SQL_DELETE_LAYER, CommandType.Text,  parms);
}
///<summary>
///得到Layer所有数据实体
///</summary>
/// <returns>得到数据实体</returns>
public List<Layer> PopulateAllrecordsFromDataRow(DataTable dt)
{
List<Layer> returns=null;
if (dt != null)
{
returns = new List<Layer>();
foreach (DataRow onerow in dt.Rows)
returns.Add(PopulateOnerecordFromDataRow(onerow));
}
return returns;
}

///<summary>
///得到Layer数据实体
///</summary>
/// <returns>得到数据实体</returns>
public Layer PopulateOnerecordFromDataRow(DataRow row)
{
Layer obj=new Layer();
if (row!=null)
{
obj.id = (row["id"]==DBNull.Value)?(int)0:int.Parse(row["id"].ToString());
obj.layerflag = (row["layerflag"]==DBNull.Value)?string.Empty:row["layerflag"].ToString();
obj.layername = (row["layername"]==DBNull.Value)?string.Empty:row["layername"].ToString();
obj.linetype = (row["linetype"]==DBNull.Value)?string.Empty:row["linetype"].ToString();
}
else
{
return null;
}
return obj;
}
///<summary>
///得到Layer数据实体
///</summary>
/// <returns>得到数据实体</returns>
public Layer PopulateOnerecordFromDataRow(IDataReader dr)
{
Layer obj=new Layer();
if (dr!=null)
{
obj.id = (dr["id"]==DBNull.Value)?(int)0:int.Parse(dr["id"].ToString());
obj.layerflag = (dr["layerflag"]==DBNull.Value)?string.Empty:dr["layerflag"].ToString();
obj.layername = (dr["layername"]==DBNull.Value)?string.Empty:dr["layername"].ToString();
obj.linetype = (dr["linetype"]==DBNull.Value)?string.Empty:dr["linetype"].ToString();
}
return obj;
}
///<summary>
///根据主键，返回一个Layer数据对象
///</summary>
/// <returns>得到数据对象</returns>
public Layer GetOnerecordbykey(int id)
{
Layer obj=null;
SQLiteParameter[] param={ 
new SQLiteParameter(PARM_ID, DbType.Int32)
};
param[0].Value=id;
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ONELAYER,SQLiteHelper.ConnectionString, CommandType.Text,param)){
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
///得到数据表Layer所有记录
///</summary>
/// <returns>得到所有数据对象</returns>
public IList<Layer> GetAllrecords()
{
IList<Layer> obj=new List<Layer>(); 
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ALLLAYERS,SQLiteHelper.ConnectionString, CommandType.Text))
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
public bool IsExistLayer(int id)
{
SQLiteParameter[] param={
            new SQLiteParameter(PARM_ID, DbType.Int32)};
param[0].Value=id;
int count = 0;
object obj = SQLiteHelper.ExecuteScalar(SQL_EXIST_LAYER,SQLiteHelper.ConnectionString, CommandType.Text, param);
 if (obj != null) count = int.Parse(obj.ToString());
return count>0;
}
#endregion
}
}
