using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Property������
///</summary>
[Serializable]
public partial class Property
{
#region �Զ������
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
#region ���幹�캯��
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
#region ��������
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
#region ��������
///<summary>
///ȷ������ Object ʵ���Ƿ���ȡ�
///</summary>
///<param name="obj">�뵱ǰ�� Object ���бȽϵ� Object��</param>
///<returns>���ָ���� Object ���ڵ�ǰ�� Object����Ϊ true������Ϊ false��</returns>
public override bool Equals(object obj)
{
if (obj == null || this.GetType() != obj.GetType()) return false;
Property Property = (Property)obj;
return
(this.id == Property.id)&&(this.textheight == Property.textheight)&&(this.textspace == Property.textspace)&&(this.liangspace == Property.liangspace)&&(this.triangledi == Property.triangledi)&&(this.trianglegao == Property.trianglegao);}
///<summary>
///�����ض����͵Ĺ�ϣ������GetHashCode �ʺ��ڹ�ϣ�㷨�����ݽṹ�����ϣ����ʹ�á�
///</summary>
/// <returns>��ǰ Object �Ĺ�ϣ���롣 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.textheight.GetHashCode()^this.textspace.GetHashCode()^this.liangspace.GetHashCode()^this.triangledi.GetHashCode()^this.trianglegao.GetHashCode();
}
///<summary>
///��¡һ����ǰ����
///</summary>
/// <returns>��¡�� AssociationInfo ����</returns>
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
