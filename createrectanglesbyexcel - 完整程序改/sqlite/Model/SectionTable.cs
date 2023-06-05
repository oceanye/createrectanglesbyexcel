using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///SectionTable������
///</summary>
[Serializable]
public partial class SectionTable
{
#region �Զ������
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
#region ���幹�캯��
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
#region ��������
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
#region ��������
///<summary>
///ȷ������ Object ʵ���Ƿ���ȡ�
///</summary>
///<param name="obj">�뵱ǰ�� Object ���бȽϵ� Object��</param>
///<returns>���ָ���� Object ���ڵ�ǰ�� Object����Ϊ true������Ϊ false��</returns>
public override bool Equals(object obj)
{
if (obj == null || this.GetType() != obj.GetType()) return false;
SectionTable SectionTable = (SectionTable)obj;
return
(this.id == SectionTable.id)&&(this.elementname == SectionTable.elementname)&&(this.elementtype == SectionTable.elementtype)&&(this.elementmaterial == SectionTable.elementmaterial)&&(this.description == SectionTable.description)&&(this.filename == SectionTable.filename);}
///<summary>
///�����ض����͵Ĺ�ϣ������GetHashCode �ʺ��ڹ�ϣ�㷨�����ݽṹ�����ϣ����ʹ�á�
///</summary>
/// <returns>��ǰ Object �Ĺ�ϣ���롣 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.elementname.GetHashCode()^this.elementtype.GetHashCode()^this.elementmaterial.GetHashCode()^this.description.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///��¡һ����ǰ����
///</summary>
/// <returns>��¡�� AssociationInfo ����</returns>
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
