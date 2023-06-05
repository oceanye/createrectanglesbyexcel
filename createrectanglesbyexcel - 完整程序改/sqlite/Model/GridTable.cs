using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace createrectanglesbyexcel.sqlite.Model
{
///<summary>
///GridTable������
///</summary>
[Serializable]
public partial class GridTable
{
#region �Զ������
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
#region ���幹�캯��
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
#region ��������
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
#region ��������
///<summary>
///ȷ������ Object ʵ���Ƿ���ȡ�
///</summary>
///<param name="obj">�뵱ǰ�� Object ���бȽϵ� Object��</param>
///<returns>���ָ���� Object ���ڵ�ǰ�� Object����Ϊ true������Ϊ false��</returns>
public override bool Equals(object obj)
{
if (obj == null || this.GetType() != obj.GetType()) return false;
GridTable GridTable = (GridTable)obj;
return
(this.id == GridTable.id)&&(this.thisid == GridTable.thisid)&&(this.gridname == GridTable.gridname)&&(this.gridpoint == GridTable.gridpoint)&&(this.gridgroup == GridTable.gridgroup)&&(this.gridtablepos == GridTable.gridtablepos)&&(this.filename == GridTable.filename);}
///<summary>
///�����ض����͵Ĺ�ϣ������GetHashCode �ʺ��ڹ�ϣ�㷨�����ݽṹ�����ϣ����ʹ�á�
///</summary>
/// <returns>��ǰ Object �Ĺ�ϣ���롣 </returns>
public override int GetHashCode()
{
return
this.id.GetHashCode()^this.thisid.GetHashCode()^this.gridname.GetHashCode()^this.gridpoint.GetHashCode()^this.gridgroup.GetHashCode()^this.gridtablepos.GetHashCode()^this.filename.GetHashCode();
}
///<summary>
///��¡һ����ǰ����
///</summary>
/// <returns>��¡�� AssociationInfo ����</returns>
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
