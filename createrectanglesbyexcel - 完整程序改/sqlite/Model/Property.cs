using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Property数据类
///</summary>
[Serializable]
public partial class Property
{
#region 自定义变量
///<summary>
///0
///</summary>
private int _id = (int)0;
///<summary>
///0
///</summary>
private double _textheight = 0;
///<summary>
///0
///</summary>
private double _textspace = 0;
///<summary>
///0
///</summary>
private double _liangspace = 0;
///<summary>
///0
///</summary>
private double _triangledi = 0;
///<summary>
///0
///</summary>
private double _trianglegao = 0;
#endregion
#region 定义构造函数
public Property()
{}
public Property
(
int id,
double textheight,
double textspace,
double liangspace,
double triangledi,
double trianglegao
)
{
this._id = id;
this._textheight = textheight;
this._textspace = textspace;
this._liangspace = liangspace;
this._triangledi = triangledi;
this._trianglegao = trianglegao;
}
#endregion
#region 公共属性
public int id
{
get{return this._id;}
set{this._id = value;}
}
public double textheight
{
get{return this._textheight;}
set{this._textheight = value;}
}
public double textspace
{
get{return this._textspace;}
set{this._textspace = value;}
}
public double liangspace
{
get{return this._liangspace;}
set{this._liangspace = value;}
}
public double triangledi
{
get{return this._triangledi;}
set{this._triangledi = value;}
}
public double trianglegao
{
get{return this._trianglegao;}
set{this._trianglegao = value;}
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
Property Property = (Property)obj;
return
(this.id == Property.id)&&(this.textheight == Property.textheight)&&(this.textspace == Property.textspace)&&(this.liangspace == Property.liangspace)&&(this.triangledi == Property.triangledi)&&(this.trianglegao == Property.trianglegao);}
///<summary>
///用作特定类型的哈希函数。GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
///</summary>
/// <returns>当前 Object 的哈希代码。 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.textheight.GetHashCode()^this.textspace.GetHashCode()^this.liangspace.GetHashCode()^this.triangledi.GetHashCode()^this.trianglegao.GetHashCode();
}
///<summary>
///克隆一个当前对象。
///</summary>
/// <returns>克隆的 AssociationInfo 对象</returns>
public object Clone()
{
Property obj = new Property();
obj.id = this.id;
obj.textheight = this.textheight;
obj.textspace = this.textspace;
obj.liangspace = this.liangspace;
obj.triangledi = this.triangledi;
obj.trianglegao = this.trianglegao;
return obj;
}
#endregion
}
}
