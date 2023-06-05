using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Column数据类
///</summary>
[Serializable]
public partial class Column
{
#region 自定义变量
///<summary>
///0
///</summary>
private int _id = (int)0;
///<summary>
///0
///</summary>
private string _point = string.Empty;
///<summary>
///0
///</summary>
private double _colx = 0;
///<summary>
///0
///</summary>
private double _coly = 0;
///<summary>
///0
///</summary>
private double _colb = 0;
///<summary>
///0
///</summary>
private double _colh = 0;
///<summary>
///0
///</summary>
private string _colmnname = string.Empty;
///<summary>
///0
///</summary>
private string _filename = string.Empty;
#endregion
#region 定义构造函数
public Column()
{}
public Column
(
int id,
string point,
double colx,
double coly,
double colb,
double colh,
string colmnname,
string filename
)
{
this._id = id;
this._point = point;
this._colx = colx;
this._coly = coly;
this._colb = colb;
this._colh = colh;
this._colmnname = colmnname;
this._filename = filename;
}
#endregion
#region 公共属性
public int id
{
get{return this._id;}
set{this._id = value;}
}
public string point
{
get{return this._point;}
set{this._point = value;}
}
public double colx
{
get{return this._colx;}
set{this._colx = value;}
}
public double coly
{
get{return this._coly;}
set{this._coly = value;}
}
public double colb
{
get{return this._colb;}
set{this._colb = value;}
}
public double colh
{
get{return this._colh;}
set{this._colh = value;}
}
public string colmnname
{
get{return this._colmnname;}
set{this._colmnname = value;}
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
Column Column = (Column)obj;
return
(this.id == Column.id)&&(this.point == Column.point)&&(this.colx == Column.colx)&&(this.coly == Column.coly)&&(this.colb == Column.colb)&&(this.colh == Column.colh)&&(this.colmnname == Column.colmnname)&&(this.filename == Column.filename);}
///<summary>
///用作特定类型的哈希函数。GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
///</summary>
/// <returns>当前 Object 的哈希代码。 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.point.GetHashCode()^this.colx.GetHashCode()^this.coly.GetHashCode()^this.colb.GetHashCode()^this.colh.GetHashCode()^this.colmnname.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///克隆一个当前对象。
///</summary>
/// <returns>克隆的 AssociationInfo 对象</returns>
public object Clone()
{
Column obj = new Column();
obj.id = this.id;
obj.point = this.point;
obj.colx = this.colx;
obj.coly = this.coly;
obj.colb = this.colb;
obj.colh = this.colh;
obj.colmnname = this.colmnname;
obj.filename = this.filename;
return obj;
}
#endregion
}
}
