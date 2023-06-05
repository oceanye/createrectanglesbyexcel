using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Layer������
///</summary>
[Serializable]
public partial class Layer
{
#region �Զ������
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
#region ���幹�캯��
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
#region ��������
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
#region ��������
///<summary>
///ȷ������ Object ʵ���Ƿ���ȡ�
///</summary>
///<param name="obj">�뵱ǰ�� Object ���бȽϵ� Object��</param>
///<returns>���ָ���� Object ���ڵ�ǰ�� Object����Ϊ true������Ϊ false��</returns>
public override bool Equals(object obj)
{
if (obj == null || this.GetType() != obj.GetType()) return false;
Layer Layer = (Layer)obj;
return
(this.id == Layer.id)&&(this.layerflag == Layer.layerflag)&&(this.layername == Layer.layername)&&(this.linetype == Layer.linetype);}
///<summary>
///�����ض����͵Ĺ�ϣ������GetHashCode �ʺ��ڹ�ϣ�㷨�����ݽṹ�����ϣ����ʹ�á�
///</summary>
/// <returns>��ǰ Object �Ĺ�ϣ���롣 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.layerflag.GetHashCode()^this.layername.GetHashCode()^this.linetype.GetHashCode();
}
///<summary>
///��¡һ����ǰ����
///</summary>
/// <returns>��¡�� AssociationInfo ����</returns>
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
