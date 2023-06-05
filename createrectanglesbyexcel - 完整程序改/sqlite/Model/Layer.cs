using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Layer数据类
///</summary>
[Serializable]
public partial class Layer
{
#region 自定义变量
///<summary>
///0
///</summary>
private int _id = (int)0;
///<summary>
///0
///</summary>
private string _layerflag = string.Empty;
///<summary>
///0
///</summary>
private string _layername = string.Empty;
///<summary>
///0
///</summary>
private string _linetype = string.Empty;
#endregion
#region 定义构造函数
public Layer()
{}
public Layer
(
int id,
string layerflag,
string layername,
string linetype
)
{
this._id = id;
this._layerflag = layerflag;
this._layername = layername;
this._linetype = linetype;
}
#endregion
#region 公共属性
public int id
{
get{return this._id;}
set{this._id = value;}
}
public string layerflag
{
get{return this._layerflag;}
set{this._layerflag = value;}
}
public string layername
{
get{return this._layername;}
set{this._layername = value;}
}
public string linetype
{
get{return this._linetype;}
set{this._linetype = value;}
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
Layer Layer = (Layer)obj;
return
(this.id == Layer.id)&&(this.layerflag == Layer.layerflag)&&(this.layername == Layer.layername)&&(this.linetype == Layer.linetype);}
///<summary>
///用作特定类型的哈希函数。GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
///</summary>
/// <returns>当前 Object 的哈希代码。 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.layerflag.GetHashCode()^this.layername.GetHashCode()^this.linetype.GetHashCode();
}
///<summary>
///克隆一个当前对象。
///</summary>
/// <returns>克隆的 AssociationInfo 对象</returns>
public object Clone()
{
Layer obj = new Layer();
obj.id = this.id;
obj.layerflag = this.layerflag;
obj.layername = this.layername;
obj.linetype = this.linetype;
return obj;
}
#endregion
}
}
