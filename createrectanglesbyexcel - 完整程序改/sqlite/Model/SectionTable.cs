using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///SectionTable数据类
///</summary>
[Serializable]
public partial class SectionTable
{
#region 自定义变量
///<summary>
///0
///</summary>
private int _id = (int)0;
///<summary>
///0
///</summary>
private string _elementname = string.Empty;
///<summary>
///0
///</summary>
private string _elementtype = string.Empty;
///<summary>
///0
///</summary>
private string _elementmaterial = string.Empty;
///<summary>
///0
///</summary>
private string _description = string.Empty;
///<summary>
///0
///</summary>
private string _filename = string.Empty;
#endregion
#region 定义构造函数
public SectionTable()
{}
public SectionTable
(
int id,
string elementname,
string elementtype,
string elementmaterial,
string description,
string filename
)
{
this._id = id;
this._elementname = elementname;
this._elementtype = elementtype;
this._elementmaterial = elementmaterial;
this._description = description;
this._filename = filename;
}
#endregion
#region 公共属性
public int id
{
get{return this._id;}
set{this._id = value;}
}
public string elementname
{
get{return this._elementname;}
set{this._elementname = value;}
}
public string elementtype
{
get{return this._elementtype;}
set{this._elementtype = value;}
}
public string elementmaterial
{
get{return this._elementmaterial;}
set{this._elementmaterial = value;}
}
public string description
{
get{return this._description;}
set{this._description = value;}
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
SectionTable SectionTable = (SectionTable)obj;
return
(this.id == SectionTable.id)&&(this.elementname == SectionTable.elementname)&&(this.elementtype == SectionTable.elementtype)&&(this.elementmaterial == SectionTable.elementmaterial)&&(this.description == SectionTable.description)&&(this.filename == SectionTable.filename);}
///<summary>
///用作特定类型的哈希函数。GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
///</summary>
/// <returns>当前 Object 的哈希代码。 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.elementname.GetHashCode()^this.elementtype.GetHashCode()^this.elementmaterial.GetHashCode()^this.description.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///克隆一个当前对象。
///</summary>
/// <returns>克隆的 AssociationInfo 对象</returns>
public object Clone()
{
SectionTable obj = new SectionTable();
obj.id = this.id;
obj.elementname = this.elementname;
obj.elementtype = this.elementtype;
obj.elementmaterial = this.elementmaterial;
obj.description = this.description;
obj.filename = this.filename;
return obj;
}
#endregion
}
}
