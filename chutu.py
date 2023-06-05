# coding:utf-8

import sqlite3
import math

#定义单层函数
def danceng(jtcol1, sectcol, bstart, bend, sectb, binfo, startdev, enddev, clevel11, clevel22, nn=None):
    #将梁端点写入柱表
    if nn is None:
        nn = []
    jtcol=[]

    for i in range(len(binfo)):
            jtcol.append(bstart[i])
            jtcol.append(bend[i])

    jtcol=list(set(jtcol))
    colx=[]
    coly=[]
    collevel1=[]
    collevel2=[]
    m=0
    for i in range(len(jtcol)):
        m=m+1
        temp1=jtcol[i]
        colx.append(temp1[1:(temp1.find(","))])
        coly.append(temp1[temp1.find(",")+1:(temp1.find(",",(temp1.find(",")+1)))])
        collevel1.append(clevel11[0])
        collevel2.append(clevel22[0])
        colnum.append(nn[0]*1000+m)

    #假定v柱100x100
    colb=[]
    colh=[]
    for i in range(len(jtcol)):
        colb.append(100)
        colh.append(100)
    for i in range(len(jtcol)):
        for j in range(len(jtcol1)):
            if str(jtcol[i])==str(jtcol1[j]):
                temp1=sectcol[j]
                colb[i]=temp1[temp1.find("B",(temp1.find("B")+1))+1:(temp1.find("X"))]
                colh[i]=temp1[temp1.find("X")+1:temp1.find("X",(temp1.find("X")+1))]

    colname=[]
    v=1+nn[0]*1000
    k=1+nn[0]*1000
    for i in range(len(jtcol)):
        tag=0
        for j in range(len(jtcol1)):
            if str(jtcol[i])==str(jtcol1[j]):
                tag=tag+1
        if tag==0:
            colname.append("VZ"+str(v))
            v=v+1
        else:
            colname.append("KZ"+str(k))
            k=k+1

    #读取梁信息，写入梁表
    pstart=[]
    pend=[]
    for i in range(len(bstart)):
        for j in range(len(jtcol)):
            if jtcol[j]==bstart[i]:
                pstart.append(nn[0]*1000+j+1)
            if bend[i]==jtcol[j]:
                pend.append(nn[0]*1000+j+1)

    bwidth=[]
    for i in range(len(sectb)):
        temp1=sectb[i]
        bwidth.append(temp1[temp1.find("X")+1:temp1.find("X",(temp1.find("X")+1))])

    #偏心
    distmin=[]
    Vnum=[]

    sbx1=[]
    sby1=[]
    sbx2=[]
    sby2=[]
    for i in range(len(colx)):
        temp1=colname[i]
        temp2=temp1[0:1]
        if temp2=="V":
            dist = []
            num=[]
            #找出所有次梁
            for j in range(len(bstart)):
                temp3 = bstart[j]
                temp4 = bend[j]
                temp5 = float(temp3[1:(temp3.find(","))])
                temp6 = float(temp3[temp3.find(",") + 1:(temp3.find(",", (temp3.find(",") + 1)))])
                temp7 = float(temp4[1:(temp4.find(","))])
                temp8 = float(temp4[temp4.find(",") + 1:(temp4.find(",", (temp4.find(",") + 1)))])
                if temp5 == float(colx[i]) and temp6 == float(coly[i]):
                    sbx1.append(temp5)
                    sby1.append(temp6)
                    sbx1.append(temp7)
                    sby1.append(temp8)
                    sbx2.append(temp7)
                    sby2.append(temp8)
                    sbx2.append(temp5)
                    sby2.append(temp6)


    #每个次梁端点与所有梁计算，找到最近梁
    for i in range(len(sbx1)):
        for j in range(len(bstart)):
            temp3=bstart[j]
            temp4=bend[j]
            temp5=float(temp3[1:(temp3.find(","))])
            temp6=float(temp3[temp3.find(",")+1:(temp3.find(",",(temp3.find(",")+1)))])
            temp7=float(temp4[1:(temp4.find(","))])
            temp8=float(temp4[temp4.find(",")+1:(temp4.find(",",(temp4.find(",")+1)))])
            temp11=float(sbx1[i])
            temp12=float(sby1[i])
            temp16=float(sbx2[i])
            temp17 = float(sby2[i])
            temp13 = float(math.sqrt((temp7 - temp5) ** 2 + (temp8 - temp6) ** 2))
            temp14 = float(math.sqrt((temp11 - temp5) ** 2 + (temp12 - temp6) ** 2))
            temp15 = float(math.sqrt((temp11 - temp7) ** 2 + (temp12 - temp8) ** 2))
            #排除与自己计算
            if (temp5 == temp11 and temp6 == temp12) and (temp7 == temp16 and temp8 == temp17):
                temp9=float(99999)
            else:
                #海伦公式
                temp9=float(temp7*temp6-temp5*temp8+temp11*temp8-temp7*temp12+temp5*temp12-temp11*temp6)/float(math.sqrt((temp7-temp5)**2+(temp8-temp6)**2))
                # 判断点在线段上
                if temp9==0 and max(temp14,temp15)>temp13:
                    temp9=float(99999)
                dist.append(abs(temp9))
                num.append(j)

        distmin.append(num[dist.index(min(dist))])
        Vnum.append(i)

    #偏心方向
    for i in range(len(distmin)):
        temp1=bstart[distmin[i]]
        temp2=bend[distmin[i]]
        temp3=float(temp1[1:(temp1.find(","))])
        temp4=float(temp1[temp1.find(",")+1:(temp1.find(",",(temp1.find(",")+1)))])
        temp5 = float(temp2[1:(temp2.find(","))])
        temp6 = float(temp2[temp2.find(",") + 1:(temp2.find(",", (temp2.find(",") + 1)))])
        temp7=temp6-temp4
        temp8=temp5-temp3
        if temp8>temp7:
            temp4=temp4+startdev[distmin[i]]
            temp6=temp6+enddev[distmin[i]]
        else:
            temp3=temp3-startdev[distmin[i]]
            temp5=temp5-enddev[distmin[i]]

        if (temp5-temp3)==0:
            k1=None
            b1=0
        else:
            k1=(temp6-temp4)/(temp5-temp3)
            b1=temp4-temp3*k1

        for j in range(len(bstart)):
            temp7=bstart[j]
            temp8=float(temp7[1:(temp7.find(","))])
            temp9=float(temp7[temp7.find(",")+1:(temp7.find(",",(temp7.find(",")+1)))])
            temp10=bend[j]
            temp11 = float(temp10[1:(temp10.find(","))])
            temp12 = float(temp10[temp10.find(",") + 1:(temp10.find(",", (temp10.find(",") + 1)))])

            if (float(sbx1[Vnum[i]])==temp8 and float(sby1[Vnum[i]])==temp9) and (float(sbx2[Vnum[i]])==temp11 and float(sby2[Vnum[i]])==temp12):
                if (temp11-temp8)==0:
                    k2=None
                    b2=0
                else:
                    k2=(temp12-temp9)/(temp11-temp8)
                    b2=temp9-temp8*k2
                for k in range(len(colx)):
                    if colx[k] == sbx1[Vnum[i]] and coly[k] == sby1[Vnum[i]]:
                        print(k1)
                        print(b1)
                        if k1==None:
                            colx[k]=temp3
                            coly[k] = k2 * colx[k] + b2
                        else:
                            if k2==None:
                                colx[k]=temp8
                                coly[k]=k1*colx[k]+b1
                            else:
                                colx[k] = (b2-b1)/(k1-k2)
                                coly[k]=k1*colx[k]+b1

    print(colx)
    #v柱截面改
    n=0
    for i in range(len(colb)):
        if colb[i]==100:
            temp1 = sectb[int(distmin[n])]
            colb[i] = temp1[temp1.find("X")+1:temp1.find("X",(temp1.find("X")+1))]
            colh[i] = temp1[temp1.find("X")+1:temp1.find("X",(temp1.find("X")+1))]
            n=n+1

    #截面
    section=[]
    elementtype=[]
    for i in range(len(bsectname)):
        temp1=bsectname[i]
        temp2=temp1.split(" ")
        section.append("'" + "GKL"+temp1[16:temp1.find("X") - 1] + temp1[temp1.find("X") + 1:temp1.find("X") + 3] + "'")
        elementtype.append(temp2[2])
    for i in range(len(csectname)):
        temp1=csectname[i]
        temp2 = temp1.split(" ")
        section.append("'" + "GKZ"+temp1[20:temp1.find("X") - 1] + temp1[temp1.find("X") + 1:temp1.find("X") + 3] + "'")
        elementtype.append(temp2[2])

    shuchu=[colx,coly,colb,colh,colname,pstart,pend,bwidth,section,collevel1,collevel2,colnum]
    return shuchu

#按楼层循环，导出每层梁柱信息
cnR = sqlite3.connect(r'Y:\数字化课题\数据库\RevitData.db')
print("Opened database successfully")

tjtcol=[]
tjtcol1=[]
tsectcol=[]
tbstart=[]
tbend=[]
tsectb=[]
tbinfo=[]
tstartdev=[]
tenddev=[]
gridid=[]
gridname=[]
gridpoint=[]
bsectname=[]
csectname=[]
clevel1=[]
clevel2=[]

c = cnR.cursor()
cursor1 = c.execute("SELECT Jt2Coord,SectionInfo,BottomLevel,TopLevel from MemberColumn")
for row in cursor1:
    tjtcol1.append(row[0])
    tsectcol.append(row[1])
    clevel1.append(row[2])
    clevel2.append(row[3])
cnR.commit()

blevel=[]
c = cnR.cursor()
cursor2 = c.execute("SELECT Jt1Coord,Jt2Coord,SectionInfo,DivisionBeam,YStartDeviation,YEndDeviation,ReferenceLevel from MemberBeam")
for row in cursor2:
    tbstart.append(row[0])
    tbend.append(row[1])
    tsectb.append(row[2])
    tbinfo.append(row[3])
    tstartdev.append(row[4])
    tenddev.append(row[5])
    blevel.append(row[6])
cnR.commit()

c = cnR.cursor()
cursor3 = c.execute("SELECT ID,GridName,GridPoint from GridTable")
for row in cursor3:
    gridid.append(row[0])
    gridname.append(row[1])
    gridpoint.append(row[2])
cnR.commit()

c = cnR.cursor()
cursor4 = c.execute("SELECT Name from SectPropertyBeam")
for row in cursor4:
    bsectname.append(row[0])
cnR.commit()

c = cnR.cursor()
cursor5 = c.execute("SELECT Name from SectPropertyColumn")
for row in cursor5:
    csectname.append(row[0])
cnR.commit()

clevel=[]
for i in range(len(clevel2)):
    clevel.append(clevel2[i])
for i in range(len(clevel1)):
    clevel.append(clevel1[i])

clevel=list(set(clevel))
clevel=sorted(clevel)

colx = []
coly = []
colb = []
colh = []
colname = []
pstart = []
pend = []
bwidth = []
section = []
collevel11=[]
collevel22=[]
colnum=[]

sectb1 = []
binfo1 = []
startdev1=[]
enddev1=[]
blevel11 = []
n=0

#根据楼层信息循环
for p in range(len(clevel)):
    nn=[]
    n=n+1
    nn.append(n)
    jtcol1 = []
    sectcol = []
    bstart = []
    bend = []
    sectb = []
    binfo = []
    startdev = []
    enddev = []
    clevel11 = []
    clevel22 = []

    for j in range(len(clevel2)):
        if clevel[p]==clevel2[j]:
            jtcol1.append(tjtcol1[j])
            sectcol.append(tsectcol[j])
            clevel11.append(clevel1[j])
            clevel22.append(clevel2[j])

    for k in range(len(blevel)):
        if clevel[p]==blevel[k]:
            bstart.append(tbstart[k])
            bend.append(tbend[k])
            sectb.append(tsectb[k])
            sectb1.append(tsectb[k])
            binfo.append(tbinfo[k])
            binfo1.append(tbinfo[k])
            startdev.append(tstartdev[k])
            startdev1.append(tstartdev[k])
            enddev.append(tenddev[k])
            enddev1.append(tenddev[k])
            blevel11.append(blevel[k])
    shuchu=danceng(jtcol1,sectcol,bstart,bend,sectb,binfo,startdev,enddev,clevel11,clevel22,nn)

    tcolx=shuchu[0]
    tcoly=shuchu[1]
    tcolb=shuchu[2]
    tcolh=shuchu[3]
    tcolname=shuchu[4]
    tpstart=shuchu[5]
    tpend=shuchu[6]
    tbwidth=shuchu[7]
    tsection=shuchu[8]
    collevel1=shuchu[9]
    collevel2=shuchu[10]
    colnum1=shuchu[11]

    #整合楼层信息
    for i in range(len(tcolx)):
        colx.append(tcolx[i])
    for i in range(len(tcoly)):
        coly.append(tcoly[i])
    for i in range(len(tcolb)):
        colb.append(tcolb[i])
    for i in range(len(tcolh)):
        colh.append(tcolh[i])
    for i in range(len(tcolname)):
        colname.append(tcolname[i])
    for i in range(len(tpstart)):
        pstart.append(tpstart[i])
    for i in range(len(tpend)):
        pend.append(tpend[i])
    for i in range(len(tbwidth)):
        bwidth.append(tbwidth[i])
    for i in range(len(tsection)):
        section.append(tsection[i])
    for i in range(len(collevel1)):
        collevel11.append(collevel1[i])
    for i in range(len(collevel2)):
        collevel22.append(collevel2[i])

#斜率判断轴网
kkk=[]
gp1=[]
gp2=[]
gp3=[]
gp4=[]
for i in range(len(gridpoint)):
    temp1 = gridpoint[i]
    temp2 = temp1.replace("(", "").replace(")", "")
    temp3 = temp2.split(",")
    gp1.append(temp3[0])
    gp2.append(temp3[1])
    gp3.append(temp3[3])
    gp4.append(temp3[4])

for i in range(len(gp4)):
    if gp3[i]==gp1[i]:
        kkk.append("a")
    else:
        if gp2[i] == gp4[i]:
            kkk.append("b")
        else:
            kkk.append((float(gp4[i]) - float(gp2[i])) / (float(gp3[i]) - float(gp1[i])))


gridtable=[]
kk=list(set(kkk))

for i in range(len(kkk)):
    for j in range(len(kk)):
        if kkk[i]==kk[j]:
            gridtable.append(str(j+1))

#截面
section=[]
elementtype=[]
for i in range(len(bsectname)):
    temp1=bsectname[i]
    temp2=temp1.split(" ")
    section.append("'" + "GKL"+temp1[16:temp1.find("X") - 1] + temp1[temp1.find("X") + 1:temp1.find("X") + 3] + "'")
    elementtype.append(temp2[2])
for i in range(len(csectname)):
    temp1=csectname[i]
    temp2 = temp1.split(" ")
    section.append("'" + "GKZ"+temp1[20:temp1.find("X") - 1] + temp1[temp1.find("X") + 1:temp1.find("X") + 3] + "'")
    elementtype.append(temp2[2])

# 建表填数据
cnY = sqlite3.connect(r'Y:\数字化课题\数据库\CBGtest.db')
cuY = cnY.cursor()

tbl1 = []
for tt in range(8):
    tbl1.append([])
for i in range(0, len(colx)):
    tbl1[0].append(str(i + 1))
    tbl1[1].append(colnum[i])
    tbl1[2].append(colx[i])
    tbl1[3].append(coly[i])
    tbl1[4].append(colb[i])
    tbl1[5].append(colh[i])
    tbl1[6].append("'" + colname[i] + "'")
    tbl1[7].append("'"+collevel11[i]+","+collevel22[i]+"'")

tbl1_T = list(zip(*tbl1))

cuY.execute("drop table if exists Column;")
cuY.execute('''
 CREATE TABLE Column 
     (
     id             INTEGER   PRIMARY KEY,
     point            TEXT,
     colx           REAL,
     coly           REAL,
     colb           REAL,
     colh           REAL,
     colmnname      TEXT,
     filename       TEXT);''')
cnY.commit()
sql_insert = "INSERT INTO Column(id,point,colx,coly,colb,colh,colmnname,filename) VALUES"
sql_values = ""
sql_values1 = ""
for i in range(len(tbl1_T)):
    a = []
    List = tbl1_T[i]
    for j in range(len(List)):
        s = str(List[j])
        a.append(s)
    for k in range(0, len(a)):
        sql_values += a[k]
        sql_values += ","
    sql_values1 = "(" + sql_values.strip(',') + ")"
    sql_todo = sql_insert + sql_values1
    cuY = cnY.cursor()
    cuY.execute(sql_todo)
    sql_values = ""
    sql_value1 = ""
cnY.commit()

tbl2 = []
for tt in range(10):
    tbl2.append([])
for i in range(0, len(pstart)):
    tbl2[0].append(str(i + 1))
    tbl2[1].append(str(i + 1))
    tbl2[2].append(str(pstart[i]))
    tbl2[3].append(str(pend[i]))
    tbl2[4].append(bwidth[i])
    temp1 = sectb1[i]
    tbl2[5].append("'" + binfo1[i] + temp1[16:temp1.find("X") - 1] + temp1[temp1.find("X") + 1:temp1.find("X") + 3] + "'")
    if binfo1[i] == "GKL":
        tbl2[6].append("'" + str(pstart[i]) + "&" + str(pend[i]) + "'")
    else:
        tbl2[6].append("'" + "" + "'")
    tbl2[7].append(startdev1[i])
    tbl2[8].append(enddev1[i])
    tbl2[9].append("'"+blevel11[i]+"'")
tbl2_T = list(zip(*tbl2))

cuY.execute("drop table if exists Beam;")
cuY.execute('''
 CREATE TABLE Beam 
     (id              INTEGER      PRIMARY KEY,
     line             TEXT,
     p_start           TEXT,
     p_end             TEXT,
     width               REAL,
     txt               TEXT,
     istriangle            TEXT,
     startpy            REAL,
     endpy            REAL,
     filename           TEXT);''')
cnY.commit()
sql_insert = "INSERT INTO Beam(id,line,p_start,p_end,width,txt,istriangle,startpy,endpy,filename) VALUES"
sql_values = ""
sql_values1 = ""
for i in range(len(tbl2_T)):
    a = []
    List = tbl2_T[i]
    for j in range(len(List)):
        s = str(List[j])
        a.append(s)
    for k in range(0, len(a)):
        sql_values += a[k]
        sql_values += ","
    sql_values1 = "(" + sql_values.strip(',') + ")"
    sql_todo = sql_insert + sql_values1
    cuY = cnY.cursor()
    cuY.execute(sql_todo)
    sql_values = ""
    sql_value1 = ""
cnY.commit()

tbl3 = []
for tt in range(4):
    tbl3.append([])
for i in range(20):
    tbl3[0].append(i + 1)

tbl3[1].append("'" + "P-图层名" + "'")
tbl3[1].append("'" + "L-图层名" + "'")
tbl3[1].append("'" + "其余标注图层名" + "'")
tbl3[1].append("'" + "P-图层名2" + "'")
tbl3[1].append("'" + "字高" + "'")
tbl3[1].append("'" + "文字间隔" + "'")
tbl3[1].append("'" + "梁端间距" + "'")
tbl3[1].append("'" + "三角底宽" + "'")
tbl3[1].append("'" + "三角高度" + "'")
tbl3[1].append("'" + "轴网尺寸1" + "'")
tbl3[1].append("'" + "第一道标注距离" + "'")
tbl3[1].append("'" + "第二道标注距离" + "'")
tbl3[1].append("'" + "编号文字大小" + "'")
tbl3[1].append("'" + "编号圆圈直径" + "'")
tbl3[1].append("'" + "标注样式" + "'")
tbl3[1].append("'" + "表头标题2" + "'")
tbl3[1].append("'" + "表格格式" + "'")
tbl3[1].append("'" + "G-图层名" + "'")
tbl3[1].append("'" + "T-图层名" + "'")
tbl3[1].append("'" + "表头标题1" + "'")

tbl3[2].append("'" + "S-column" + "'")
tbl3[2].append("'" + "S-Beam" + "'")
tbl3[2].append("'" + "S-Text" + "'")
tbl3[2].append("'" + "S-columnV" + "'")
tbl3[2].append(250)
tbl3[2].append(50)
tbl3[2].append(50)
tbl3[2].append(300)
tbl3[2].append(250)
tbl3[2].append(1000)
tbl3[2].append(500)
tbl3[2].append(750)
tbl3[2].append(300)
tbl3[2].append(400)
tbl3[2].append("'" + "TSSD_25_25" + "'")
tbl3[2].append("'" + "构件名称,构件规格,材质,备注" + "'")
tbl3[2].append("'" + "(@1500),(2500,3500,3000,2500@800)" + "'")
tbl3[2].append("'" + "S-Grid" + "'")
tbl3[2].append("'" + "S-Text" + "'")
tbl3[2].append("'" + "表头标题" + "'")

tbl3[3].append("'" + "Continous" + "'")
tbl3[3].append("'" + "ACAD_ISO02W100" + "'")
tbl3[3].append("'" + "Continous" + "'")
tbl3[3].append("'" + "Continous" + "'")
tbl3[3].append("'" + "'")
tbl3[3].append("'" + "'")
tbl3[3].append("'" + "'")
tbl3[3].append("'" + "'")
tbl3[3].append("'" + "'")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("NULL")
tbl3[3].append("'" + "Continous" + "'")
tbl3[3].append("'" + "Continous" + "'")
tbl3[3].append("NULL")
tbl3_T = list(zip(*tbl3))

cuY.execute("drop table if exists Layer;")
cuY.execute('''
 CREATE TABLE Layer 
     (id             INTEGER      PRIMARY KEY,
     layerflag           TEXT,
     layername            TEXT,
     linetype              TEXT);''')
cnY.commit()
sql_insert = "INSERT INTO Layer(id,layerflag,layername,linetype) VALUES"
sql_values = ""
sql_values1 = ""
for i in range(len(tbl3_T)):
    a = []
    List = tbl3_T[i]
    for j in range(len(List)):
        s = str(List[j])
        a.append(s)
    for k in range(0, len(a)):
        sql_values += a[k]
        sql_values += ","
    sql_values1 = "(" + sql_values.strip(',') + ")"
    sql_todo = sql_insert + sql_values1
    cuY = cnY.cursor()
    cuY.execute(sql_todo)
    sql_values = ""
    sql_value1 = ""
cnY.commit()

cuY.execute("drop table if exists Property;")
cuY.execute('''
 CREATE TABLE Property 
     (id              INTEGER      PRIMARY KEY,
     textheight             REAL,
     textspace           REAL,
     liangspace           REAL,
     triangledi           REAL,
     trianglegao           REAL);''')
cnY.commit()
sql_insert = "INSERT INTO Property(id,textheight,textspace,liangspace,triangledi,trianglegao) VALUES(1,250,50,50,300,250)"
cuY.execute(sql_insert)
cnY.commit()

tbl4 = []
n=''
for i in range(len(clevel)):
    n=n+clevel[i]+","
nn=n[0:-1]
for tt in range(7):
    tbl4.append([])
for i in range(0, len(gridid)):
    tbl4[0].append(str(i + 1))
    tbl4[1].append(gridid[i])
    tbl4[2].append("'" + gridname[i] + "'")
    tbl4[3].append("'" + gridpoint[i] + "'")
    tbl4[4].append("'" + gridtable[i] + "'")
    tbl4[5].append("'" + "Both" + "'")
    tbl4[6].append("'"+nn+"'")
tbl4_T = list(zip(*tbl4))

cuY.execute("drop table if exists GridTable;")
cuY.execute('''
 CREATE TABLE GridTable 
     (id                INTEGER      PRIMARY KEY,
     thisid             INTEGER,
     gridname             TEXT,
     gridpoint               TEXT,
     gridgroup               TEXT,
     gridtablepos            TEXT,
     filename            TEXT);''')
cnY.commit()
sql_insert = "INSERT INTO GridTable(id,thisid,gridname,gridpoint,gridgroup,gridtablepos,filename) VALUES"
sql_values = ""
sql_values1 = ""
for i in range(len(tbl4_T)):
    a = []
    List = tbl4_T[i]
    for j in range(len(List)):
        s = str(List[j])
        a.append(s)
    for k in range(0, len(a)):
        sql_values += a[k]
        sql_values += ","
    sql_values1 = "(" + sql_values.strip(',') + ")"
    sql_todo = sql_insert + sql_values1
    cuY = cnY.cursor()
    cuY.execute(sql_todo)
    sql_values = ""
    sql_value1 = ""
cnY.commit()

tbl5 = []
n=''
for i in range(len(clevel)):
    n=n+clevel[i]+","
nn=n[0:-1]
for tt in range(6):
    tbl5.append([])
for i in range(len(section)):
    tbl5[0].append(str(i + 1))
    tbl5[1].append(section[i])
    tbl5[2].append("'" + elementtype[i] + "'")
    tbl5[3].append("'" + "Q355B" + "'")
    tbl5[4].append("'" + "焊接" + "'")
    tbl5[5].append("'"+nn+"'")
tbl5_T = list(zip(*tbl5))

cuY.execute("drop table if exists SectionTable;")
cuY.execute('''
 CREATE TABLE SectionTable 
     (id                INTEGER      PRIMARY KEY,
     elementname             TEXT,
     elementtype             TEXT,
     elementmaterial               TEXT,
     description               TEXT  ,
     filename            TEXT);''')
cnY.commit()
sql_insert = "INSERT INTO SectionTable(id,elementname,elementtype,elementmaterial,description,filename) VALUES"
sql_values = ""
sql_values1 = ""
for i in range(len(tbl5_T)):
    a = []
    List = tbl5_T[i]
    for j in range(len(List)):
        s = str(List[j])
        a.append(s)
    for k in range(0, len(a)):
        sql_values += a[k]
        sql_values += ","
    sql_values1 = "(" + sql_values.strip(',') + ")"
    sql_todo = sql_insert + sql_values1
    cuY = cnY.cursor()
    cuY.execute(sql_todo)
    sql_values = ""
    sql_value1 = ""
cnY.commit()