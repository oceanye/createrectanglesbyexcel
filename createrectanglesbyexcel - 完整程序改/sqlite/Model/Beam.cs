using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Beam数据类
///</summary>
[Serializable]
public partial class Beam
{
#region 自定义变量
///<summary>
///0
///</summary>
private int _id = (int)0;
///<summary>
///0
///</summary>
private string _line = string.Empty;
///<summary>
///0
///</summary>
private string _p_start = string.Empty;
///<summary>
///0
///</summary>
private string _p_end = string.Empty;
///<summary>
///0
///</summary>
private double _width = 0;
///<summary>
///0
///</summary>
private string _txt = string.Empty;
///<summary>
///0
///</summary>
private string _istriangle = string.Empty;
///<summary>
///0
///</summary>
private double _startpy = 0;
///<summary>
///0
///</summary>
private double _endpy = 0;
///<summary>
///0
///</summary>
private string _filename = string.Empty;
#endregion
#region 定义构造函数
public Beam()
{}
public Beam
(
int id,
string line,
string p_start,
string p_end,
double width,
string txt,
string istriangle,
double startpy,
double endpy,
string filename
)
{
this._id = id;
this._line = line;
this._p_start = p_start;
this._p_end = p_end;
this._width = width;
this._txt = txt;
this._istriangle = istriangle;
this._startpy = startpy;
this._endpy = endpy;
this._filename = filename;
}
#endregion
#region 公共属性
public int id
{
get{return this._id;}
set{this._id = value;}
}
public string line
{
get{return this._line;}
set{this._line = value;}
}
public string p_start
{
get{return this._p_start;}
set{this._p_start = value;}
}
public string p_end
{
get{return this._p_end;}
set{this._p_end = value;}
}
public double width
{
get{return this._width;}
set{this._width = value;}
}
public string txt
{
get{return this._txt;}
set{this._txt = value;}
}
public string istriangle
{
get{return this._istriangle;}
set{this._istriangle = value;}
}
public double startpy
{
get{return this._startpy;}
set{this._startpy = value;}
}
public double endpy
{
get{return this._endpy;}
set{this._endpy = value;}
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
Beam Beam = (Beam)obj;
return
(this.id == Beam.id)&&(this.line == Beam.line)&&(this.p_start == Beam.p_start)&&(this.p_end == Beam.p_end)&&(this.width == Beam.width)&&(this.txt == Beam.txt)&&(this.istriangle == Beam.istriangle)&&(this.startpy == Beam.startpy)&&(this.endpy == Beam.endpy)&&(this.filename == Beam.filename);}
///<summary>
///用作特定类型的哈希函数。GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
///</summary>
/// <returns>当前 Object 的哈希代码。 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.line.GetHashCode()^this.p_start.GetHashCode()^this.p_end.GetHashCode()^this.width.GetHashCode()^this.txt.GetHashCode()^this.istriangle.GetHashCode()^this.startpy.GetHashCode()^this.endpy.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///克隆一个当前对象。
///</summary>
/// <returns>克隆的 AssociationInfo 对象</returns>
public object Clone()
{
Beam obj = new Beam();
obj.id = this.id;
obj.line = this.line;
obj.p_start = this.p_start;
obj.p_end = this.p_end;
obj.width = this.width;
obj.txt = this.txt;
obj.istriangle = this.istriangle;
obj.startpy = this.startpy;
obj.endpy = this.endpy;
obj.filename = this.filename;
return obj;
}
#endregion
}
}
