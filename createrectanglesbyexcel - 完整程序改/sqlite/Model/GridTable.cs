using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///GridTable数据类
///</summary>
[Serializable]
public partial class GridTable
{
#region 自定义变量
///<summary>
///0
///</summary>
private int _id = (int)0;
///<summary>
///0
///</summary>
private int _thisid = (int)0;
///<summary>
///0
///</summary>
private string _gridname = string.Empty;
///<summary>
///0
///</summary>
private string _gridpoint = string.Empty;
///<summary>
///0
///</summary>
private string _gridgroup = string.Empty;
///<summary>
///0
///</summary>
private string _gridtablepos = string.Empty;
///<summary>
///0
///</summary>
private string _filename = string.Empty;
#endregion
#region 定义构造函数
public GridTable()
{}
public GridTable
(
int id,
int thisid,
string gridname,
string gridpoint,
string gridgroup,
string gridtablepos,
string filename
)
{
this._id = id;
this._thisid = thisid;
this._gridname = gridname;
this._gridpoint = gridpoint;
this._gridgroup = gridgroup;
this._gridtablepos = gridtablepos;
this._filename = filename;
}
#endregion
#region 公共属性
public int id
{
get{return this._id;}
set{this._id = value;}
}
public int thisid
{
get{return this._thisid;}
set{this._thisid = value;}
}
public string gridname
{
get{return this._gridname;}
set{this._gridname = value;}
}
public string gridpoint
{
get{return this._gridpoint;}
set{this._gridpoint = value;}
}
public string gridgroup
{
get{return this._gridgroup;}
set{this._gridgroup = value;}
}
public string gridtablepos
{
get{return this._gridtablepos;}
set{this._gridtablepos = value;}
}
public string filename
{
get{return this._filename;}
set{this._filename = value;}
}
#endregion
#region 公共方法
///<summary>
///确定两个 Object 实例是否相等。
///</summary>
///<param name="obj">与当前的 Object 进行比较的 Object。</param>
///<returns>如果指定的 Object 等于当前的 Object，则为 true；否则为 false。</returns>
public override bool Equals(object obj)
{
if (obj == null || this.GetType() != obj.GetType()) return false;
GridTable GridTable = (GridTable)obj;
return
(this.id == GridTable.id)&&(this.thisid == GridTable.thisid)&&(this.gridname == GridTable.gridname)&&(this.gridpoint == GridTable.gridpoint)&&(this.gridgroup == GridTable.gridgroup)&&(this.gridtablepos == GridTable.gridtablepos)&&(this.filename == GridTable.filename);}
///<summary>
///用作特定类型的哈希函数。GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
///</summary>
/// <returns>当前 Object 的哈希代码。 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.thisid.GetHashCode()^this.gridname.GetHashCode()^this.gridpoint.GetHashCode()^this.gridgroup.GetHashCode()^this.gridtablepos.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///克隆一个当前对象。
///</summary>
/// <returns>克隆的 AssociationInfo 对象</returns>
public object Clone()
{
GridTable obj = new GridTable();
obj.id = this.id;
obj.thisid = this.thisid;
obj.gridname = this.gridname;
obj.gridpoint = this.gridpoint;
obj.gridgroup = this.gridgroup;
obj.gridtablepos = this.gridtablepos;
obj.filename = this.filename;
return obj;
}
#endregion
}
}
