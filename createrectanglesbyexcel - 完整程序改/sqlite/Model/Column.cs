using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///Column������
///</summary>
[Serializable]
public partial class Column
{
#region �Զ������
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
#region ���幹�캯��
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
#region ��������
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
#region ��������
///<summary>
///ȷ������ Object ʵ���Ƿ���ȡ�
///</summary>
///<param name="obj">�뵱ǰ�� Object ���бȽϵ� Object��</param>
///<returns>���ָ���� Object ���ڵ�ǰ�� Object����Ϊ true������Ϊ false��</returns>
public override bool Equals(object obj)
{
if (obj == null || this.GetType() != obj.GetType()) return false;
Column Column = (Column)obj;
return
(this.id == Column.id)&&(this.point == Column.point)&&(this.colx == Column.colx)&&(this.coly == Column.coly)&&(this.colb == Column.colb)&&(this.colh == Column.colh)&&(this.colmnname == Column.colmnname)&&(this.filename == Column.filename);}
///<summary>
///�����ض����͵Ĺ�ϣ������GetHashCode �ʺ��ڹ�ϣ�㷨�����ݽṹ�����ϣ����ʹ�á�
///</summary>
/// <returns>��ǰ Object �Ĺ�ϣ���롣 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.point.GetHashCode()^this.colx.GetHashCode()^this.coly.GetHashCode()^this.colb.GetHashCode()^this.colh.GetHashCode()^this.colmnname.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///��¡һ����ǰ����
///</summary>
/// <returns>��¡�� AssociationInfo ����</returns>
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
