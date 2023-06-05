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
///Beam数据访问类
///</summary>
[Serializable]
public partial class dboBeam
{
#region commandtext
private const string SQL_INSERT_BEAM = @"INSERT INTO Beam
(
line,
p_start,
p_end,
width,
txt,
istriangle,
startpy,
endpy,
filename
)
VALUES
(
@line,
@p_start,
@p_end,
@width,
@txt,
@istriangle,
@startpy,
@endpy,
@filename
);
SELECT last_insert_rowid() AS 'Identity'";
private const string SQL_UPDATE_BEAM = @"UPDATE Beam SET
line = @line,
p_start = @p_start,
p_end = @p_end,
width = @width,
txt = @txt,
istriangle = @istriangle,
startpy = @startpy,
endpy = @endpy,
filename = @filename
WHERE 
id = @id";
private const string SQL_DELETE_BEAM = @"DELETE FROM Beam WHERE id = @id";
private const string SQL_SELECT_ALLBEAMS = @"SELECT id,line,p_start,p_end,width,txt,istriangle,startpy,endpy,filename FROM Beam ";
private const string SQL_SELECT_ONEBEAM = @"SELECT id,line,p_start,p_end,width,txt,istriangle,startpy,endpy,filename FROM Beam where id = @id limit 1";
private const string SQL_EXIST_BEAM = @"SELECT COUNT(1) AS num FROM Beam WHERE id = @id limit 1";

private const string PARM_ID = "@id";
private const string PARM_LINE = "@line";
private const string PARM_P_START = "@p_start";
private const string PARM_P_END = "@p_end";
private const string PARM_WIDTH = "@width";
private const string PARM_TXT = "@txt";
private const string PARM_ISTRIANGLE = "@istriangle";
private const string PARM_STARTPY = "@startpy";
private const string PARM_ENDPY = "@endpy";
private const string PARM_FILENAME = "@filename";
#endregion

#region 构造函数
///<summary>
///Beam构造函数
///</summary>
public dboBeam()
{}
#endregion
#region 公共方法
///<summary>
///向数据库插入一条记录
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(Beam Beam)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_BEAM);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LINE,DbType.String),
new SQLiteParameter(PARM_P_START,DbType.String),
new SQLiteParameter(PARM_P_END,DbType.String),
new SQLiteParameter(PARM_WIDTH,DbType.Double),
new SQLiteParameter(PARM_TXT,DbType.String),
new SQLiteParameter(PARM_ISTRIANGLE,DbType.String),
new SQLiteParameter(PARM_STARTPY,DbType.Double),
new SQLiteParameter(PARM_ENDPY,DbType.Double),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_BEAM,parms);
};
parms[0].Value = Beam.id;
parms[1].Value = Beam.line;
parms[2].Value = Beam.p_start;
parms[3].Value = Beam.p_end;
parms[4].Value = Beam.width;
parms[5].Value = Beam.txt;
parms[6].Value = Beam.istriangle;
parms[7].Value = Beam.startpy;
parms[8].Value = Beam.endpy;
parms[9].Value = Beam.filename;
object obj=SQLiteHelper.ExecuteScalar(SQL_INSERT_BEAM,SQLiteHelper.ConnectionString, CommandType.Text, parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据库插入一条记录，带事务
///</summary>
/// <returns>新插入记录的编号</returns>
public int Insert(SQLiteTransaction sp,Beam Beam)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_INSERT_BEAM);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LINE,DbType.String),
new SQLiteParameter(PARM_P_START,DbType.String),
new SQLiteParameter(PARM_P_END,DbType.String),
new SQLiteParameter(PARM_WIDTH,DbType.Double),
new SQLiteParameter(PARM_TXT,DbType.String),
new SQLiteParameter(PARM_ISTRIANGLE,DbType.String),
new SQLiteParameter(PARM_STARTPY,DbType.Double),
new SQLiteParameter(PARM_ENDPY,DbType.Double),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_INSERT_BEAM,parms);
};
parms[0].Value = Beam.id;
parms[1].Value = Beam.line;
parms[2].Value = Beam.p_start;
parms[3].Value = Beam.p_end;
parms[4].Value = Beam.width;
parms[5].Value = Beam.txt;
parms[6].Value = Beam.istriangle;
parms[7].Value = Beam.startpy;
parms[8].Value = Beam.endpy;
parms[9].Value = Beam.filename;
object obj=SQLiteHelper.ExecuteScalar(sp,SQL_INSERT_BEAM, CommandType.Text,  parms);
return obj == null ? 0 : int.Parse(obj.ToString());
}

///<summary>
///向数据表更新一条记录。
///</summary>
/// <returns>影响的行数</returns>
public int Update(Beam Beam)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_BEAM);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LINE,DbType.String),
new SQLiteParameter(PARM_P_START,DbType.String),
new SQLiteParameter(PARM_P_END,DbType.String),
new SQLiteParameter(PARM_WIDTH,DbType.Double),
new SQLiteParameter(PARM_TXT,DbType.String),
new SQLiteParameter(PARM_ISTRIANGLE,DbType.String),
new SQLiteParameter(PARM_STARTPY,DbType.Double),
new SQLiteParameter(PARM_ENDPY,DbType.Double),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_BEAM,parms);
};
parms[0].Value = Beam.id;
parms[1].Value = Beam.line;
parms[2].Value = Beam.p_start;
parms[3].Value = Beam.p_end;
parms[4].Value = Beam.width;
parms[5].Value = Beam.txt;
parms[6].Value = Beam.istriangle;
parms[7].Value = Beam.startpy;
parms[8].Value = Beam.endpy;
parms[9].Value = Beam.filename;
return SQLiteHelper.ExecuteNonQuery( SQL_UPDATE_BEAM,SQLiteHelper.ConnectionString, CommandType.Text, parms);
}
///<summary>
///向数据表更新一条记录，带事务。
///</summary>
/// <returns>影响的行数</returns>
public int Update(SQLiteTransaction sp,Beam Beam)
{
SQLiteParameter[] parms = SQLiteHelper.GetCachedParameters(SQL_UPDATE_BEAM);
if (parms == null)
{
parms = new SQLiteParameter[]{
new SQLiteParameter(PARM_ID,DbType.Int32),
new SQLiteParameter(PARM_LINE,DbType.String),
new SQLiteParameter(PARM_P_START,DbType.String),
new SQLiteParameter(PARM_P_END,DbType.String),
new SQLiteParameter(PARM_WIDTH,DbType.Double),
new SQLiteParameter(PARM_TXT,DbType.String),
new SQLiteParameter(PARM_ISTRIANGLE,DbType.String),
new SQLiteParameter(PARM_STARTPY,DbType.Double),
new SQLiteParameter(PARM_ENDPY,DbType.Double),
new SQLiteParameter(PARM_FILENAME,DbType.String)
};
SQLiteHelper.CacheParameters(SQL_UPDATE_BEAM,parms);
};
parms[0].Value = Beam.id;
parms[1].Value = Beam.line;
parms[2].Value = Beam.p_start;
parms[3].Value = Beam.p_end;
parms[4].Value = Beam.width;
parms[5].Value = Beam.txt;
parms[6].Value = Beam.istriangle;
parms[7].Value = Beam.startpy;
parms[8].Value = Beam.endpy;
parms[9].Value = Beam.filename;
return SQLiteHelper.ExecuteNonQuery(sp,SQL_UPDATE_BEAM, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(SQL_DELETE_BEAM,SQLiteHelper.ConnectionString, CommandType.Text,  parms);
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
return SQLiteHelper.ExecuteNonQuery(sp,SQL_DELETE_BEAM, CommandType.Text,  parms);
}
///<summary>
///得到Beam所有数据实体
///</summary>
/// <returns>得到数据实体</returns>
public List<Beam> PopulateAllrecordsFromDataRow(DataTable dt)
{
List<Beam> returns=null;
if (dt != null)
{
returns = new List<Beam>();
foreach (DataRow onerow in dt.Rows)
returns.Add(PopulateOnerecordFromDataRow(onerow));
}
return returns;
}

///<summary>
///得到Beam数据实体
///</summary>
/// <returns>得到数据实体</returns>
public Beam PopulateOnerecordFromDataRow(DataRow row)
{
Beam obj=new Beam();
if (row!=null)
{
obj.id = (row["id"]==DBNull.Value)?(int)0:int.Parse(row["id"].ToString());
obj.line = (row["line"]==DBNull.Value)?string.Empty:row["line"].ToString();
obj.p_start = (row["p_start"]==DBNull.Value)?string.Empty:row["p_start"].ToString();
obj.p_end = (row["p_end"]==DBNull.Value)?string.Empty:row["p_end"].ToString();
obj.width = (row["width"]==DBNull.Value)?0:double.Parse(row["width"].ToString());
obj.txt = (row["txt"]==DBNull.Value)?string.Empty:row["txt"].ToString();
obj.istriangle = (row["istriangle"]==DBNull.Value)?string.Empty:row["istriangle"].ToString();
obj.startpy = (row["startpy"]==DBNull.Value)?0:double.Parse(row["startpy"].ToString());
obj.endpy = (row["endpy"]==DBNull.Value)?0:double.Parse(row["endpy"].ToString());
obj.filename = (row["filename"]==DBNull.Value)?string.Empty:row["filename"].ToString();
}
else
{
return null;
}
return obj;
}
///<summary>
///得到Beam数据实体
///</summary>
/// <returns>得到数据实体</returns>
public Beam PopulateOnerecordFromDataRow(IDataReader dr)
{
Beam obj=new Beam();
if (dr!=null)
{
obj.id = (dr["id"]==DBNull.Value)?(int)0:int.Parse(dr["id"].ToString());
obj.line = (dr["line"]==DBNull.Value)?string.Empty:dr["line"].ToString();
obj.p_start = (dr["p_start"]==DBNull.Value)?string.Empty:dr["p_start"].ToString();
obj.p_end = (dr["p_end"]==DBNull.Value)?string.Empty:dr["p_end"].ToString();
obj.width = (dr["width"]==DBNull.Value)?0:double.Parse(dr["width"].ToString());
obj.txt = (dr["txt"]==DBNull.Value)?string.Empty:dr["txt"].ToString();
obj.istriangle = (dr["istriangle"]==DBNull.Value)?string.Empty:dr["istriangle"].ToString();
obj.startpy = (dr["startpy"]==DBNull.Value)?0:double.Parse(dr["startpy"].ToString());
obj.endpy = (dr["endpy"]==DBNull.Value)?0:double.Parse(dr["endpy"].ToString());
obj.filename = (dr["filename"]==DBNull.Value)?string.Empty:dr["filename"].ToString();
}
return obj;
}
///<summary>
///根据主键，返回一个Beam数据对象
///</summary>
/// <returns>得到数据对象</returns>
public Beam GetOnerecordbykey(int id)
{
Beam obj=null;
SQLiteParameter[] param={ 
new SQLiteParameter(PARM_ID, DbType.Int32)
};
param[0].Value=id;
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ONEBEAM,SQLiteHelper.ConnectionString, CommandType.Text,param)){
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
///得到数据表Beam所有记录
///</summary>
/// <returns>得到所有数据对象</returns>
public IList<Beam> GetAllrecords()
{
IList<Beam> obj=new List<Beam>(); 
using(SQLiteDataReader dr=SQLiteHelper.ExecuteReader(SQL_SELECT_ALLBEAMS,SQLiteHelper.ConnectionString, CommandType.Text))
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
public bool IsExistBeam(int id)
{
SQLiteParameter[] param={
            new SQLiteParameter(PARM_ID, DbType.Int32)};
param[0].Value=id;
int count = 0;
object obj = SQLiteHelper.ExecuteScalar(SQL_EXIST_BEAM,SQLiteHelper.ConnectionString, CommandType.Text, param);
 if (obj != null) count = int.Parse(obj.ToString());
return count>0;
}
#endregion
}
}
