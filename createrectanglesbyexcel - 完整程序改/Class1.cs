using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.PlottingServices;

using System.IO;

[assembly: CommandClass(typeof(createrectanglesbyexcel.Class1))]
[assembly: ExtensionApplication(typeof(createrectanglesbyexcel.Class1))]
namespace createrectanglesbyexcel
{
    public class Class1 : IExtensionApplication
    {
        public void Initialize()
        {
            if (string.IsNullOrEmpty(Commons.StaticValues.assemblypath))
                Commons.StaticValues.assemblypath = path();

            if (string.IsNullOrEmpty(sqlite.DBUtility.SQLiteHelper.ConnectionString))
                sqlite.DBUtility.SQLiteHelper.ConnectionString = string.Format(Commons.StaticValues.connectstring, Commons.StaticValues.assemblypath + Commons.StaticValues.dbname);
        }

        public void Terminate()
        {
        }

        private string path()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path) + "\\";
        }

        [CommandMethod("test1")]
        public void mytest1()
        {
            Document mydoc = Application.DocumentManager.MdiActiveDocument;

            #region
            {
                using (Transaction tr = mydoc.Database.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(mydoc.Database.BlockTableId, OpenMode.ForWrite);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


                    Circle onecl = new Circle();
                    onecl.Center = Point3d.Origin;
                    onecl.Radius = 100 / 2;


                    double bianchang = onecl.Radius * Math.Sin(Math.PI / 4);//正方形边长
                    Point3d newposition = new Point3d(onecl.Center.X - bianchang / 2, onecl.Center.Y - bianchang / 2, onecl.Center.Z);//文字位置


                    DBText onetxt = new DBText();
                    onetxt.TextString = "ceshi";
                    onetxt.Position = newposition;
                    onetxt.Height = 20;

                    #region 修正位置
                    {
                        Extents3d thisextends = onetxt.GeometricExtents;
                        double width = Math.Abs(thisextends.MaxPoint.X - thisextends.MinPoint.X);
                        double height = Math.Abs(thisextends.MaxPoint.Y - thisextends.MinPoint.Y);

                        double pianyix = (bianchang - width) > 0 ? (bianchang - width) / 2 : 0;
                        double pianyiy = (bianchang - height) > 0 ? (bianchang - height) / 2 : 0;

                        onetxt.Position = new Point3d(newposition.X + pianyix, newposition.Y + pianyiy, newposition.Z);
                    }
                    #endregion

                    onetxt.Rotation = Math.PI / 4;


                    btr.AppendEntity(onecl);
                    tr.AddNewlyCreatedDBObject(onecl, true);

                    btr.AppendEntity(onetxt);
                    tr.AddNewlyCreatedDBObject(onetxt, true);


                    tr.Commit();
                }
            }
            #endregion
        }

        [CommandMethod("test2")]
        public void mytest2()
        {
            Document mydoc = Application.DocumentManager.MdiActiveDocument;

            #region
            {
                using (Transaction tr = mydoc.Database.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(mydoc.Database.BlockTableId, OpenMode.ForWrite);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


                    Circle onecl = new Circle();
                    onecl.Center = Point3d.Origin;
                    onecl.Radius = 100 / 2;


                    double bianchang = onecl.Radius * Math.Sin(Math.PI / 4);//正方形边长
                    Point3d newposition = new Point3d(onecl.Center.X - bianchang / 2, onecl.Center.Y - bianchang / 2, onecl.Center.Z);//文字位置


                    DBText onetxt = new DBText();
                    onetxt.TextString = "ceshi";
                    onetxt.Position = newposition;
                    onetxt.Height = 20;


                    #region 修正位置
                    {
                        //onetxt.Position = new Point3d(newposition.X + 5, newposition.Y + 5, newposition.Z);
                    }
                    #endregion

                    //onetxt.TransformBy(Matrix3d.Rotation(Math.PI / 4, Vector3d.ZAxis, onecl.Center));


                    btr.AppendEntity(onecl);
                    tr.AddNewlyCreatedDBObject(onecl, true);


                    btr.AppendEntity(onetxt);
                    tr.AddNewlyCreatedDBObject(onetxt, true);


                    tr.Commit();
                }
            }
            #endregion
        }

        [CommandMethod("test")]
        public void mytest()
        {
            Document mydoc = Application.DocumentManager.MdiActiveDocument;

            List<sqlite.Model.Beam> getalls = Commons.Globals.Getallrecordsfromdt<sqlite.Model.Beam>();
            mydoc.Editor.WriteMessage(getalls.Count.ToString());

            #region 测试创建带文字的圆形
            using (Transaction tr = mydoc.Database.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(mydoc.Database.BlockTableId, OpenMode.ForWrite);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                #region 创建带属性的块
                ObjectId newblkid = ObjectId.Null;
                {
                    BlockTableRecord blk = new BlockTableRecord();
                    blk.Name = "mydefine";

                    Circle newcircle = new Circle();
                    newcircle.Center = Point3d.Origin;
                    newcircle.Radius = 100;

                    double bianchang = newcircle.Radius * Math.Sin(Math.PI / 4);//正方形边长
                    Point3d newposition = new Point3d(newcircle.Center.X - bianchang/2, newcircle.Center.Y - bianchang/2, newcircle.Center.Z);//文字位置

                    AttributeDefinition newabr = new AttributeDefinition();
                    newabr.Tag = "A";
                    newabr.Visible = true;
                    newabr.IsMTextAttributeDefinition = false;
                    newabr.TextString = "11";
                    newabr.Position = newposition;
                    newabr.WidthFactor = 0.8;
                    newabr.HorizontalMode = TextHorizontalMode.TextFit;
                    newabr.Height = bianchang;
                    newabr.VerticalMode = TextVerticalMode.TextVerticalMid;
                    newabr.AlignmentPoint = newposition;
                    //newabr.LockPositionInBlock = true;
                    //newabr.Preset = true;
                    newabr.Constant = true;
                    //newabr.Invisible = true;
                    newabr.TextStyleId = mydoc.Database.Textstyle;


                    blk.AppendEntity(newabr);
                    blk.AppendEntity(newcircle);
                    blk.Origin = newcircle.Center;
                    
                    
                    newblkid = bt.Add(blk);
                    tr.AddNewlyCreatedDBObject(blk, true);   
                }


                BlockReference newblk = new BlockReference(Point3d.Origin, newblkid);

                btr.AppendEntity(newblk);
                tr.AddNewlyCreatedDBObject(newblk, true);
                #endregion

                tr.Commit();
            }
            #endregion

        }

        //public static void AddAttributeDefinitionToBlocks(this ObjectId blockId, AttributeDefinition atts)
        //{
        //    Database db = blockId.Database;//获取数据库对象
        //    BlockTableRecord btr = blockId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
        //    btr.AppendEntity(atts);
        //    db.TransactionManager.AddNewlyCreatedDBObject(atts, true);
        //    db.TransactionManager.Dispose();
        //    btr.DowngradeOpen();
        //}

        /// <summary>
        /// 按要求生成
        /// </summary>
        [CommandMethod("CR")]
        public void createpls()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string savepaths = fbd.SelectedPath;
                if (!Directory.Exists(savepaths))
                    Directory.CreateDirectory(savepaths);


                #region 读取所有信息
                List<sqlite.Model.Column> getallcols = Commons.Globals.Getallrecordsfromdt<sqlite.Model.Column>();//柱表
                List<sqlite.Model.Beam> getallbeams = Commons.Globals.Getallrecordsfromdt<sqlite.Model.Beam>();//梁表                
                List<sqlite.Model.GridTable> getallgrids = Commons.Globals.Getallrecordsfromdt<sqlite.Model.GridTable>();//网格表
                List<sqlite.Model.SectionTable> getalltbs = Commons.Globals.Getallrecordsfromdt<sqlite.Model.SectionTable>();//导入设置表

                #region 优化变量设置
                Dictionary<string, sqlite.Model.Layer> getallsets = new Dictionary<string, sqlite.Model.Layer>();
                {
                    List<sqlite.Model.Layer> getallsets1 = Commons.Globals.Getallrecordsfromdt<sqlite.Model.Layer>();//设置表
                    foreach (sqlite.Model.Layer onelayer in getallsets1)
                        if (!getallsets.Keys.Contains(onelayer.layerflag.Trim()))
                            getallsets.Add(onelayer.layerflag.Trim(), onelayer);
                }

                double liangduanjianju = Commons.StaticValues.juxingpy;
                double zigao = Commons.StaticValues.zigao;
                double zidijiange = Commons.StaticValues.wenzipy;
                double sanjiaodikuan = Commons.StaticValues.sanjiaokuandu;
                double sanjiaogaodu = Commons.StaticValues.sanjiaogaodu;

                double circlediameter = Commons.StaticValues.circlediameter;
                double circlezigao = Commons.StaticValues.circlezigao;
                double zhouwang = Commons.StaticValues.zhouwang;
                double zhouwang1 = Commons.StaticValues.zhouwang1;
                double zhouwang2 = Commons.StaticValues.zhouwang2;
                {
                    List<string> findlayernames = new List<string>() { "梁端间距", "字高", "文字间隔", "三角底宽", "三角高度", "编号圆圈直径", "编号文字大小", "轴网尺寸1", "第一道标注距离", "第二道标注距离" };

                    for (int i = 0; i < findlayernames.Count; i++)
                    {
                        #region
                        string findname = findlayernames[i].Trim();
                        if (getallsets.Keys.Contains(findname.Trim()))
                        {
                            switch (i)
                            {
                                case 0:
                                    liangduanjianju = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, liangduanjianju);
                                    break;
                                case 1:
                                    zigao = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, zigao);
                                    break;
                                case 2:
                                    zidijiange = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, zidijiange);
                                    break;
                                case 3:
                                    sanjiaodikuan = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, sanjiaodikuan);
                                    break;
                                case 4:
                                    sanjiaogaodu = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, sanjiaogaodu);
                                    break;
                                case 5:
                                    circlediameter = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, circlediameter);
                                    break;
                                case 6:
                                    circlezigao = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, circlezigao);
                                    break;
                                case 7:
                                    zhouwang = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, zhouwang);
                                    break;
                                case 8:
                                    zhouwang1 = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, zhouwang1);
                                    break;
                                case 9:
                                    zhouwang2 = Commons.Globals.DataParse(getallsets[findname.Trim()].layername, zhouwang2);
                                    break;
                            }
                        }

                        #endregion
                    }
                }

                string title = "";
                {
                    string findname = "表头标题1";
                    if (getallsets.Keys.Contains(findname.Trim()))
                        title = getallsets[findname.Trim()].layername.Trim();
                }


                List<string> heads = new List<string>();
                {
                    string findname = "表头标题2";
                    if (getallsets.Keys.Contains(findname.Trim()))
                        heads = getallsets[findname.Trim()].layername.Split(new char[] { Commons.StaticValues.newsplitchar, Commons.StaticValues.newsplitchar1 }).ToList();
                }
                List<double> colwidth = new List<double>();
                List<double> colheight = new List<double>();//table尺寸信息
                {
                    string findname = "表格格式";
                    if (getallsets.Keys.Contains(findname.Trim()))
                    {
                        string tempstring = getallsets[findname.Trim()].layername.Replace("(", string.Empty).Replace("（", string.Empty).Replace(")", string.Empty).Replace("）", string.Empty).Trim();
                        List<string> thisstring = tempstring.Split(new char[] { Commons.StaticValues.newsplitchar, Commons.StaticValues.newsplitchar1 }).ToList();
                        bool isfirstwidth = false;                      
                        #region
                        for (int i = 0; i < thisstring.Count; i++)
                        {
                            if (thisstring[i].IndexOf(Commons.StaticValues.newtbwidthchar) >= 0)
                            {
                                List<string> thissplitstring = thisstring[i].Split(new char[] { Commons.StaticValues.newtbwidthchar },StringSplitOptions.RemoveEmptyEntries).ToList();
                                if (thissplitstring.Count > 0)
                                    colheight.Add(Commons.Globals.DataParse(thissplitstring[thissplitstring.Count - 1], (double)Commons.StaticValues.errvalues));
                                  
                                if (thissplitstring.Count > 1)
                                {
                                    if (i == 0)
                                        isfirstwidth = true;

                                    colwidth.Add(Commons.Globals.DataParse(thissplitstring[thissplitstring.Count - 2], (double)Commons.StaticValues.errvalues));
                                }
                            }
                            else
                                colwidth.Add(Commons.Globals.DataParse(thisstring[i], (double)Commons.StaticValues.errvalues));
                        }
                        #endregion

                        if (!isfirstwidth)
                        {
                            double qiuhe = colwidth.Sum();
                            colwidth.Insert(0, qiuhe);
                            isfirstwidth = true;
                        }
                    }
                }
                #endregion

                #endregion
                List<string> allfiles = new List<string>();

                List<string> allfileSecond = new List<string>();
                #region
                allfiles.AddRange(getallcols.Select(oneobj => oneobj.filename.Trim()));
                allfiles.AddRange(getallbeams.Select(oneobj => oneobj.filename.Trim()));
                allfiles.AddRange(getallgrids.Select(oneobj => oneobj.filename.Trim()));
                allfiles.AddRange(getalltbs.Select(oneobj => oneobj.filename.Trim()));
                allfiles.RemoveAll(oneobj => string.IsNullOrEmpty(oneobj));
                allfiles = allfiles.Distinct().ToList();

                for(int i=0;i< allfiles.Count;i++)
                {
                    
                    if (allfiles[i].Contains(","))
                    {
                        for(int j=0; j< allfiles[i].Split(',').Length;j++)
                        {
                            allfileSecond.Add(allfiles[i].Split(',')[j].ToString());
                        }
                    }
                    else
                    {
                        allfileSecond.Add(allfiles[i]);
                    }

                }

                allfileSecond = allfileSecond.Distinct().ToList();
                #endregion

                #region
                foreach (string onefile in allfileSecond)
                {
                    List<sqlite.Model.Column> thisfilecols = getallcols.Where(oneobj => oneobj.filename.Trim().Contains(onefile.Trim())).ToList();
                    List<sqlite.Model.Beam> thisfilebeams = getallbeams.Where(oneobj => oneobj.filename.Trim().Contains(onefile.Trim())).ToList();
                    List<sqlite.Model.SectionTable> thisfiletbs = getalltbs.Where(oneobj => oneobj.filename.Trim().Contains(onefile.Trim())).ToList();

                    List<Commons.GridTableEx> thisfilegrids = new List<Commons.GridTableEx>();

                    foreach (sqlite.Model.GridTable onegrid in getallgrids.Where(oneobj => oneobj.filename.Trim().Contains(onefile.Trim())).ToList())
                        thisfilegrids.Add(new Commons.GridTableEx(onegrid));

                    if (thisfilecols.Count == 0 && thisfilebeams.Count == 0 && thisfilegrids.Count == 0 && thisfiletbs.Count == 0)
                        continue;

                    #region 作图
                    using (Database db = new Database(true, false))
                    {
                        ObjectIdCollection newids = new ObjectIdCollection();
                        #region 复制样式
                        using (Database sourcedb = new Database(false, true))
                        {
                            sourcedb.ReadDwgFile(Commons.StaticValues.assemblypath + "\\" + Commons.StaticValues.templatedwg, FileShare.Read, true, string.Empty);
                            sourcedb.CloseInput(true);

                            #region
                            ObjectIdCollection oldids = new ObjectIdCollection();
                            IdMapping mapper = new IdMapping();
                            using (Transaction tr = sourcedb.TransactionManager.StartTransaction())
                            {
                                BlockTable bt = (BlockTable)tr.GetObject(sourcedb.BlockTableId, OpenMode.ForWrite);
                                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                                foreach (ObjectId oneid in btr)
                                    oldids.Add(oneid);

                                tr.Commit();
                            }
                            sourcedb.WblockCloneObjects(oldids, db.CurrentSpaceId, mapper, DuplicateRecordCloning.Ignore, false);

                            foreach(ObjectId oneid in oldids)
                                try
                                {
                                    if (!newids.Contains(mapper[oneid].Value))
                                    {
                                        newids.Add(mapper[oneid].Value);
                                    }
                                }
                                catch { }
                            #endregion
                        }
                        #endregion

                        #region 图纸样式
                        ObjectId dimstyleid = ObjectId.Null;
                        using (Transaction tr = db.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                            #region 图纸样式
                            {
                                string findname = "标注样式";
                                if (getallsets.Keys.Contains(findname.Trim()))
                                {
                                    string dimstylename = getallsets[findname.Trim()].layername.Trim();
                                    DimStyleTable DimTabb = tr.GetObject(db.DimStyleTableId, OpenMode.ForWrite) as DimStyleTable;
                                    if (DimTabb.Has(dimstylename.Trim()))
                                        dimstyleid = DimTabb[dimstylename.Trim()];
                                }

                                foreach (ObjectId oneid in newids)
                                {
                                    DBObject getdbo = tr.GetObject(oneid, OpenMode.ForWrite);
                                    getdbo.Erase();
                                }
                                newids.Clear();
                            }
                            #endregion
                            tr.Commit();
                        }
                        #endregion

                        #region 图纸图层
                        ObjectId pxlayerid = ObjectId.Null;//平行线图层
                        ObjectId otherlayerid = ObjectId.Null;//平行线图层

                        ObjectId layerid1 = ObjectId.Null;
                        ObjectId layerid2 = ObjectId.Null;
                        ObjectId layerid3 = ObjectId.Null;
                        ObjectId layerid4 = ObjectId.Null;
                        {
                            Dictionary<string, ObjectId> lintypedictionays = new Dictionary<string, ObjectId>();
                            Dictionary<string, ObjectId> layerdictionays = new Dictionary<string, ObjectId>();
                            List<string> findlayernames = new List<string>() { "G-图层名", "T-图层名", "P-图层名", "L-图层名", "其余标注图层名", "P-图层名2" };

                            for (int i = 0; i < findlayernames.Count; i++)
                            {
                                ObjectId newid = ObjectId.Null;
                                #region
                                string findname = findlayernames[i].Trim();
                                if (getallsets.Keys.Contains(findname.Trim()))
                                {
                                    string onelinetype = getallsets[findname.Trim()].linetype.Trim();
                                    if (!lintypedictionays.Keys.Contains(onelinetype.Trim()))
                                    {
                                        ObjectId linetypeid1 = LoadLinetype(db, onelinetype.Trim());
                                        lintypedictionays.Add(onelinetype.Trim(), linetypeid1);
                                    }

                                    string onelayer = getallsets[findname.Trim()].layername.Trim();
                                    if (!layerdictionays.Keys.Contains(onelayer.Trim()))
                                    {
                                        ObjectId templayerid = GetLayid(db, onelayer, lintypedictionays[onelinetype.Trim()]);
                                        layerdictionays.Add(onelayer.Trim(), templayerid);
                                    }
                                    newid = layerdictionays[onelayer.Trim()];
                                }
                                #endregion
                                #region
                                switch (i)
                                {
                                    case 0:
                                        pxlayerid = newid;
                                        break;
                                    case 1:
                                        otherlayerid = newid;
                                        break;
                                    case 2:
                                        layerid1 = newid;
                                        break;
                                    case 3:
                                        layerid2 = newid;
                                        break;
                                    case 4:
                                        layerid3 = newid;
                                        break;
                                    case 5:
                                        layerid4= newid;
                                        break;
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region 生成原有的梁柱
                        using (Transaction tr = db.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                            #region 原有梁、柱
                            if (thisfilecols.Count > 0 && thisfilebeams.Count > 0)
                            {
                                Dictionary<string, Point3d> getppps = new Dictionary<string, Point3d>();//保存第一步生成的点
                                Dictionary<string, Polyline> getppls = new Dictionary<string, Polyline>();//保存第一步生成的矩形
                                #region 第一步生成P
                                {
                                    #region 生成多边形
                                    foreach (sqlite.Model.Column thiszhuzi in thisfilecols)
                                    {


                                        Polyline oneply = createplinebycenterp(new Point2d(thiszhuzi.colx, thiszhuzi.coly), thiszhuzi.colb, thiszhuzi.colh);
                                        


                                        if (thiszhuzi.colmnname.Contains("VZ"))
                                        {
                                            if (!layerid4.IsNull)
                                                oneply.LayerId = layerid4;

                                           
                                        }
                                        else
                                        {
                                            if (!layerid1.IsNull)
                                                oneply.LayerId = layerid1;



                                            Polyline lineText = createplinebycenterpSecond(new Point2d(thiszhuzi.colx, thiszhuzi.coly), thiszhuzi.colb, thiszhuzi.colh);

                                            Point2d rightps = new Point2d(new Point2d(thiszhuzi.colx, thiszhuzi.coly).X + thiszhuzi.colb / 2, new Point2d(thiszhuzi.colx, thiszhuzi.coly).Y + thiszhuzi.colh / 2);

                                            Point2d ptMiddle = new Point2d(rightps.X + Math.Cos(Math.PI / 4) * 400, rightps.Y + Math.Sin(Math.PI / 4) * 400);

                                            Point2d endPoint = new Point2d(ptMiddle.X + 600, ptMiddle.Y);

                                            if (!layerid3.IsNull)
                                                lineText.LayerId = layerid3;
                                            
                                            btr.AppendEntity(lineText);
                                            tr.AddNewlyCreatedDBObject(lineText, true);


                                            DBText onetxt = new DBText();
                                            if (!layerid3.IsNull)
                                                onetxt.LayerId = layerid3;
                                            onetxt.TextString = thiszhuzi.colmnname.Trim();
                                            onetxt.Position = new Point3d (endPoint.X, endPoint.Y-100,0);
                                            onetxt.Height = circlezigao;


                                            btr.AppendEntity(onetxt);
                                            tr.AddNewlyCreatedDBObject(onetxt, true);

                                        }
                                       
                                        oneply.Closed = true;
                                        btr.AppendEntity(oneply);

                                      
                                        tr.AddNewlyCreatedDBObject(oneply, true);
                                      


                                        getppps.Add(thiszhuzi.point.Trim(), new Point3d(thiszhuzi.colx, thiszhuzi.coly, Commons.StaticValues.errvalues));
                                        getppls.Add(thiszhuzi.point.Trim(), oneply);
                                    }
                                    #endregion
                                }
                                #endregion

                                #region 第二步生成PL
                                {
                                    #region
                                    {
                                        foreach (sqlite.Model.Beam thisliang in thisfilebeams)
                                        {
                                            Polyline getstartpl = getppls[thisliang.p_start];
                                            Polyline getendpl = getppls[thisliang.p_end];

                                            Point3d linestartp = getppps[thisliang.p_start];
                                            Point3d lineendp = getppps[thisliang.p_end];

                                            #region 变换点
                                            {
                                                Line oneline = new Line(linestartp, lineendp);

                                                foreach (DBObject onedbo in getstartpl.GetOffsetCurves(-liangduanjianju))
                                                {
                                                    if (onedbo is Polyline)
                                                    {
                                                        Point3dCollection getjiaodian = new Point3dCollection();
                                                        (onedbo as Polyline).IntersectWith(oneline, Intersect.OnBothOperands, getjiaodian, IntPtr.Zero, IntPtr.Zero);

                                                        if (getjiaodian.Count > 0)
                                                            linestartp = getjiaodian[0];

                                                        break;
                                                    }
                                                }
                                                foreach (DBObject onedbo in getendpl.GetOffsetCurves(-liangduanjianju))
                                                {
                                                    if (onedbo is Polyline)
                                                    {
                                                        Point3dCollection getjiaodian = new Point3dCollection();
                                                        (onedbo as Polyline).IntersectWith(oneline, Intersect.OnBothOperands, getjiaodian, IntPtr.Zero, IntPtr.Zero);

                                                        if (getjiaodian.Count > 0)
                                                            lineendp = getjiaodian[0];

                                                        break;
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 计算偏移后点位置
                                            {
                                                Line oneline = new Line(linestartp, lineendp);

                                                foreach (DBObject onedbo in (oneline.Clone() as Line).GetOffsetCurves(thisliang.startpy))//正负控制方向
                                                {
                                                    if (onedbo is Line)
                                                    {
                                                        linestartp = (onedbo as Line).StartPoint;
                                                        break;
                                                    }
                                                }

                                                foreach (DBObject onedbo in (oneline.Clone() as Line).GetOffsetCurves(thisliang.endpy))
                                                {
                                                    if (onedbo is Line)
                                                    {
                                                        lineendp = (onedbo as Line).EndPoint;
                                                        break;
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region

                                            #region 生成图形
                                            {
                                                double pianyiabs = thisliang.width / 2;

                                                Line oneline = new Line(linestartp, lineendp);
                                                Line leftline = new Line();
                                                foreach (DBObject onedbo in oneline.GetOffsetCurves(pianyiabs))
                                                {
                                                    if (onedbo is Line)
                                                    {
                                                        leftline = (onedbo as Line);
                                                        break;
                                                    }
                                                }

                                                Line rightline = new Line();
                                                foreach (DBObject onedbo in oneline.GetOffsetCurves(-pianyiabs))
                                                {
                                                    if (onedbo is Line)
                                                    {
                                                        rightline = (onedbo as Line);
                                                        break;
                                                    }
                                                }

                                                oneline.LayerId = layerid2;

                                                btr.AppendEntity(oneline);
                                                tr.AddNewlyCreatedDBObject(oneline, true);

                                                #region
                                                {
                                                    Polyline oneply = new Polyline();
                                                    oneply.AddVertexAt(0, new Point2d(leftline.StartPoint.X, leftline.StartPoint.Y), 0, 0, 0);
                                                    oneply.AddVertexAt(0, new Point2d(leftline.EndPoint.X, leftline.EndPoint.Y), 0, 0, 0);
                                                    oneply.AddVertexAt(0, new Point2d(rightline.EndPoint.X, rightline.EndPoint.Y), 0, 0, 0);
                                                    oneply.AddVertexAt(0, new Point2d(rightline.StartPoint.X, rightline.StartPoint.Y), 0, 0, 0);

                                                    oneply.Closed = true;
                                                    oneply.LayerId = layerid2;

                                                    btr.AppendEntity(oneply);
                                                    tr.AddNewlyCreatedDBObject(oneply, true);
                                                }
                                                #endregion

                                                #region 生成辅助图形
                                                {
                                                    #region 生成三角形
                                                    {
                                                        List<string> getsanjiaos = thisliang.istriangle.Split(new char[] { Commons.StaticValues.splitsanjiao }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                                        if (getsanjiaos.Contains(thisliang.p_start))
                                                        {
                                                            Polyline newply = new Polyline();
                                                            newply.AddVertexAt(0, new Point2d(oneline.StartPoint.X, oneline.StartPoint.Y), 0, sanjiaodikuan, 0);
                                                            newply.AddVertexAt(1, new Point2d(oneline.StartPoint.X + sanjiaogaodu * Math.Cos(oneline.Angle), oneline.StartPoint.Y + sanjiaogaodu * Math.Sin(oneline.Angle)), 0, 0, 0);
                                                            newply.LayerId = layerid3;

                                                            btr.AppendEntity(newply);
                                                            tr.AddNewlyCreatedDBObject(newply, true);
                                                        }

                                                        if (getsanjiaos.Contains(thisliang.p_end))
                                                        {
                                                            Polyline newply = new Polyline();
                                                            newply.AddVertexAt(0, new Point2d(oneline.EndPoint.X, oneline.EndPoint.Y), 0, sanjiaodikuan, 0);
                                                            newply.AddVertexAt(1, new Point2d(oneline.EndPoint.X - sanjiaogaodu * Math.Cos(oneline.Angle), oneline.EndPoint.Y - sanjiaogaodu * Math.Sin(oneline.Angle)), 0, 0, 0);
                                                            newply.LayerId = layerid3;

                                                            btr.AppendEntity(newply);
                                                            tr.AddNewlyCreatedDBObject(newply, true);
                                                        }

                                                    }
                                                    #endregion

                                                    #region 生成文本
                                                    {
                                                        double juli = zidijiange + pianyiabs + zigao;

                                                        Point3d centerp = new Point3d((rightline.StartPoint.X + rightline.EndPoint.X) / 2, (rightline.StartPoint.Y + rightline.EndPoint.Y) / 2, rightline.StartPoint.Z);
                                                        DBText newtext = new DBText();
                                                        newtext.LayerId = layerid3;
                                                        newtext.Height = zigao;
                                                        newtext.TextString = thisliang.txt;
                                                        newtext.Rotation = oneline.Angle;
                                                        newtext.Position = new Point3d(centerp.X + juli * Math.Abs(Math.Sin(oneline.Angle)), centerp.Y - juli * Math.Abs(Math.Cos(oneline.Angle)), centerp.Z);

                                                        btr.AppendEntity(newtext);
                                                        tr.AddNewlyCreatedDBObject(newtext, true);
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                            #endregion


                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion

                            tr.Commit();
                        }
                        #endregion

                        #region 生成平行线图
                        Point3d savemaxp = new Point3d(double.MinValue, double.MinValue, double.MinValue);//记录右侧最大点
                        using (Transaction tr = db.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                            #region 生成平行线图
                            if (thisfilegrids.Count > 0)
                            {
                                List<string> groups = thisfilegrids.Select(oneobj => oneobj.gridgroup.Trim()).Distinct().ToList();
                                List<Point3d> getallstartps = thisfilegrids.Select(oneobj => oneobj.startp).ToList();
                                List<Point3d> getallendps = thisfilegrids.Select(oneobj => oneobj.endp).ToList();
                                foreach (string onegroup in groups)
                                {
                                    List<Commons.GridTableEx> groupgrids = thisfilegrids.Where(oneobj => oneobj.gridgroup.Trim() == onegroup.Trim()).ToList();

                                    #region 计算直线
                                    foreach (Commons.GridTableEx onegtline in groupgrids)
                                    {
                                        #region  重新生成样式，默认是都生成                                     
                                        {
                                            var tt = getallendps.Where(oneobj => oneobj.IsEqualTo(onegtline.startp, new Tolerance(Commons.StaticValues.comparejingdu, Commons.StaticValues.comparejingdu))).ToList();
                                            var tt1 = getallstartps.Where(oneobj => oneobj.IsEqualTo(onegtline.endp, new Tolerance(Commons.StaticValues.comparejingdu, Commons.StaticValues.comparejingdu))).ToList();
                                            if (tt.Count > 0 && tt1.Count == 0)
                                                onegtline.thislinetype = Commons.linepostype.endpoint;
                                            if (tt1.Count > 0 && tt.Count == 0)
                                                onegtline.thislinetype = Commons.linepostype.startpoint;
                                            if (tt.Count > 0 && tt1.Count > 0)
                                                onegtline.thislinetype = Commons.linepostype.none;
                                            if (tt.Count == 0 && tt1.Count == 0)
                                                onegtline.thislinetype = Commons.linepostype.both;
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region 判断平行线是否起点和终点相同
                                    if (groupgrids.Count > 2)
                                    {
                                        Line3d startpline = new Line3d(groupgrids[0].startp, groupgrids[1].startp);
                                        Line3d endpline = new Line3d(groupgrids[0].endp, groupgrids[1].endp);

                                        for (int i = 2; i < groupgrids.Count; i++)
                                        {
                                            bool computestartp = Commons.Globals.issameline(groupgrids[0].startp, groupgrids[1].startp, groupgrids[i].startp);
                                            bool computeendp = Commons.Globals.issameline(groupgrids[0].endp, groupgrids[1].endp, groupgrids[i].endp);

                                            if (!computestartp || !computeendp)
                                                groupgrids[i].isdrawline = false;//改变不生成直线
                                        }
                                    }

                                    #endregion
                                    #region 生成直线和文本
                                    int startpxishu = 0;
                                    int endpxishu = 0;
                                    List<Commons.GridTableEx> groupgridsthisuse = groupgrids.Where(oneobj => oneobj.isdrawline).ToList();
                                    foreach (Commons.GridTableEx onegridtb in groupgridsthisuse)
                                    {
                                        #region 生成平行线
                                        Line thisline = new Line(onegridtb.startp, onegridtb.endp);
                                        thisline.LayerId = pxlayerid;

                                        savemaxp = getmaxp(savemaxp, thisline.StartPoint);
                                        savemaxp = getmaxp(savemaxp, thisline.EndPoint);
                                        btr.AppendEntity(thisline);
                                        tr.AddNewlyCreatedDBObject(thisline, true);
                                        #endregion

                                        #region 生成文本
                                        if (onegridtb.thislinetype == Commons.linepostype.both || onegridtb.thislinetype == Commons.linepostype.startpoint)
                                        {
                                            Point3d startpo = Commons.Globals.getchuixianos(thisline.StartPoint, thisline.EndPoint, true);
                                            Line newline = new Line(thisline.StartPoint, startpo);

                                            #region
                                            if (startpxishu == 0)
                                                startpxishu = judgejulioffsetfuhao(thisline, newline);
                                            Point3d centerp = getjiaodians(thisline, newline, circlediameter / 2 * startpxishu);
                                            double angle = newline.Angle % Math.PI;

                                            Point3d p1 = getjiaodians(thisline, newline, -zhouwang * startpxishu);
                                            Point3d p2 = getjiaodians(thisline, newline, -zhouwang1 * startpxishu);
                                            Point3d p3 = getjiaodians(thisline, newline, -zhouwang2 * startpxishu);

                                            onegridtb.getleftps.Add(zhouwang, p1);
                                            onegridtb.getleftps.Add(zhouwang1, p2);
                                            onegridtb.getleftps.Add(zhouwang2, p3);
                                            #endregion


                                            Circle onecl = new Circle();
                                            onecl.Center = Point3d.Origin;
                                            onecl.Radius = circlediameter / 2;

                                           
                                            double bianchang = onecl.Radius * Math.Sin(Math.PI / 4);//边长
                                                                                                    //Point3d newposition = getmaxps(onecl, thisline, bianchang);//文字位置,采用最低位置

                                            Point3d newposition = new Point3d(onecl.Center.X - bianchang / 2, onecl.Center.Y - bianchang / 2, onecl.Center.Z);//文字位置

                                            DBText onetxt = new DBText();
                                            onetxt.TextString = onegridtb.gridname.Trim();
                                            onetxt.Position = newposition;
                                            onetxt.Height = circlezigao;

                                            #region 修正位置
                                            {
                                                Extents3d thisextends = onetxt.GeometricExtents;

                                                double width = getwidthorheight(db, onetxt.TextString, onetxt.Height);
                                                double height = getwidthorheight(db, onetxt.TextString, onetxt.Height, false);

                                                double pianyix = (bianchang - width) > 0 ? (bianchang - width) / 2 : 0;
                                                double pianyiy = (bianchang - height) > 0 ? (bianchang - height) / 2 : 0;

                                                onetxt.Position = new Point3d(newposition.X + pianyix, newposition.Y + pianyiy, newposition.Z);
                                            }
                                            #endregion

                                            onetxt.LayerId = otherlayerid;

                                            onetxt.TransformBy(Matrix3d.Rotation(angle, Vector3d.ZAxis, onecl.Center));
                                            //onetxt.Rotation = angle;

                                            btr.AppendEntity(onecl);
                                            tr.AddNewlyCreatedDBObject(onecl, true);

                                            btr.AppendEntity(onetxt);
                                            tr.AddNewlyCreatedDBObject(onetxt, true);


                                            ///移动图形
                                            move(onecl, Point3d.Origin, centerp);
                                            move(onetxt, Point3d.Origin, centerp);

                                            try
                                            {
                                                savemaxp = getmaxp(savemaxp, onecl.GeometricExtents.MinPoint);
                                                savemaxp = getmaxp(savemaxp, onecl.GeometricExtents.MaxPoint);
                                            }
                                            catch { }
                                        }

                                        if (onegridtb.thislinetype == Commons.linepostype.both || onegridtb.thislinetype == Commons.linepostype.endpoint)
                                        {
                                            Point3d endpo = Commons.Globals.getchuixianos(thisline.StartPoint, thisline.EndPoint, false);
                                            Line newline = new Line(thisline.EndPoint, endpo);

                                            #region
                                            if (endpxishu == 0)
                                                endpxishu = judgejulioffsetfuhao(thisline, newline);
                                            Point3d centerp = getjiaodians(thisline, newline, circlediameter / 2 * endpxishu);
                                            double angle = newline.Angle % Math.PI;

                                            Point3d p1 = getjiaodians(thisline, newline, -zhouwang * endpxishu);
                                            Point3d p2 = getjiaodians(thisline, newline, -zhouwang1 * endpxishu);
                                            Point3d p3 = getjiaodians(thisline, newline, -zhouwang2 * endpxishu);

                                            onegridtb.getrightps.Add(zhouwang, p1);
                                            onegridtb.getrightps.Add(zhouwang1, p2);
                                            onegridtb.getrightps.Add(zhouwang2, p3);
                                            #endregion


                                            Circle onecl = new Circle();
                                            onecl.Center = Point3d.Origin;
                                            onecl.Radius = circlediameter / 2;
                                           
                                            double bianchang = onecl.Radius * Math.Sin(Math.PI / 4);//边长
                                            //Point3d newposition = getmaxps(onecl, thisline, bianchang);//文字位置,采用最低位置
                                            Point3d newposition = new Point3d(onecl.Center.X - bianchang / 2, onecl.Center.Y - bianchang / 2, onecl.Center.Z);//文字位置

                                            DBText onetxt = new DBText();
                                            onetxt.TextString = onegridtb.gridname.Trim();
                                            onetxt.Position = newposition;
                                            onetxt.Height = circlezigao;

                                            #region 修正位置
                                            {
                                                Extents3d thisextends = onetxt.GeometricExtents;

                                                double width = getwidthorheight(db, onetxt.TextString, onetxt.Height);
                                                double height = getwidthorheight(db, onetxt.TextString, onetxt.Height, false);

                                                double pianyix = (bianchang - width) > 0 ? (bianchang - width) / 2 : 0;
                                                double pianyiy = (bianchang - height) > 0 ? (bianchang - height) / 2 : 0;

                                                onetxt.Position = new Point3d(newposition.X + pianyix, newposition.Y + pianyiy, newposition.Z);
                                            }
                                            #endregion

                                            onetxt.LayerId = otherlayerid;
                                            onetxt.TransformBy(Matrix3d.Rotation(angle, Vector3d.ZAxis, onecl.Center));
                                            //onetxt.Rotation = angle;

                                            ///移动图形
                                            move(onecl, Point3d.Origin, centerp);
                                            move(onetxt, Point3d.Origin, centerp);

                                            try
                                            {
                                                savemaxp = getmaxp(savemaxp, onecl.GeometricExtents.MinPoint);
                                                savemaxp = getmaxp(savemaxp, onecl.GeometricExtents.MaxPoint);
                                            }
                                            catch { }


                                            btr.AppendEntity(onecl);
                                            tr.AddNewlyCreatedDBObject(onecl, true);

                                            btr.AppendEntity(onetxt);
                                            tr.AddNewlyCreatedDBObject(onetxt, true);
                                        }
                                        #endregion
                                    }
                                    #endregion
                                    #region 标注
                                    if (groupgridsthisuse.Count > 0)
                                    {
                                        List<Point3d> getp1s = new List<Point3d>();
                                        List<Point3d> getp2s = new List<Point3d>();
                                        List<Point3d> getp3s = new List<Point3d>();
                                        {
                                            var tt = groupgridsthisuse.Select(oneobj => oneobj.thislinetype).Distinct().OrderBy(oneobj => (int)oneobj).ToList();
                                            tt.RemoveAll(oneobj => oneobj == Commons.linepostype.both);
                                            if (tt.Count == 0 || tt[0] == Commons.linepostype.startpoint)
                                            {
                                                try
                                                {
                                                    getp1s = groupgridsthisuse.Select(oneobj => oneobj.getleftps[zhouwang]).ToList();
                                                    getp2s = groupgridsthisuse.Select(oneobj => oneobj.getleftps[zhouwang1]).ToList();
                                                    getp3s = groupgridsthisuse.Select(oneobj => oneobj.getleftps[zhouwang2]).ToList();
                                                }
                                                catch { }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    getp1s = groupgridsthisuse.Select(oneobj => oneobj.getrightps[zhouwang]).ToList();
                                                    getp2s = groupgridsthisuse.Select(oneobj => oneobj.getrightps[zhouwang1]).ToList();
                                                    getp3s = groupgridsthisuse.Select(oneobj => oneobj.getrightps[zhouwang2]).ToList();
                                                }
                                                catch { }
                                            }
                                        }


                                        for (int i = 0; i < getp1s.Count - 1; i++)
                                        {

                                            AlignedDimension onedimension = new AlignedDimension();
                                            onedimension.XLine1Point = getp1s[i];
                                            onedimension.XLine2Point = getp1s[i + 1];
                                            onedimension.DimLinePoint = new Point3d((getp3s[i].X + getp3s[i + 1].X) / 2, (getp3s[i].Y + getp3s[i + 1].Y) / 2, getp3s[i].Z);
                                            onedimension.DimensionStyle = dimstyleid;
                                            onedimension.LayerId = otherlayerid;

                                            btr.AppendEntity(onedimension);
                                            tr.AddNewlyCreatedDBObject(onedimension, true);
                                        }

                                        {
                                            AlignedDimension onedimension = new AlignedDimension();
                                            onedimension.XLine1Point = getp1s[0];
                                            onedimension.XLine2Point = getp1s[getp1s.Count - 1];
                                            onedimension.DimLinePoint = new Point3d((getp2s[0].X + getp2s[getp2s.Count - 1].X) / 2, (getp2s[0].Y + getp2s[getp2s.Count - 1].Y) / 2, getp2s[0].Z);
                                            onedimension.DimensionStyle = dimstyleid;
                                            onedimension.LayerId = otherlayerid;

                                            btr.AppendEntity(onedimension);
                                            tr.AddNewlyCreatedDBObject(onedimension, true);
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion

                            tr.Commit();
                        }
                        #endregion

                        #region 生成表格
                        using (Transaction tr = db.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                            #region
                            if (thisfiletbs.Count > 0)
                            {
                                int cols = colwidth.Count - 1;
                                int rows = thisfiletbs.Count + 2;

                                #region
                                Table tb = new Table();
                                tb.TableStyle = db.Tablestyle;
                                tb.IsTitleSuppressed = true;
                                tb.IsHeaderSuppressed = true;


                                tb.SetSize(rows, cols);

                                for (int i = 0; i < rows; i++)
                                {
                                    if (i == 0)
                                        tb.Rows[i].Height = colheight[0];
                                    else
                                        tb.Rows[i].Height = colheight[colheight.Count - 1];
                                    tb.Rows[i].TextHeight = zigao;
                                }

                                for (int i = 0; i < cols; i++)
                                {
                                    tb.Columns[i].Alignment = CellAlignment.MiddleCenter;
                                    tb.Columns[i].Width = colwidth[i + 1];
                                    tb.Columns[i].TextHeight = zigao;
                                    tb.Columns[i].TextStyleId = db.Textstyle;
                                }

                                Point3d newpos = new Point3d(savemaxp.X + Commons.StaticValues.tablepy, savemaxp.Y + colheight[0] + (rows - 1) * colheight[colheight.Count - 1], savemaxp.Z);//偏移100距离
                                tb.Position = newpos;

                                #region
                                for (int i = 0; i < rows; i++)
                                {
                                    if (i == 0)
                                    {
                                        tb.MergeCells(CellRange.Create(tb, 0, 0, 0, cols - 1));
                                        tb.Cells[i, 0].Value = title;
                                        tb.Cells[i, 0].Borders.Top.LineWeight = LineWeight.LineWeight040;
                                        tb.Cells[i, 0].Borders.Left.LineWeight = LineWeight.LineWeight005;
                                        tb.Cells[i, 0].Borders.Right.LineWeight = LineWeight.LineWeight005;
                                    }
                                    else if (i == 1)
                                    {
                                        for (int j = 0; j < heads.Count && j < cols; j++)
                                            tb.Cells[i, j].Value = heads[j].Trim();

                                        tb.Cells[i, 0].Borders.Left.LineWeight = LineWeight.LineWeight005;
                                        tb.Cells[i, cols - 1].Borders.Right.LineWeight = LineWeight.LineWeight005;
                                    }
                                    else
                                    {
                                        int index = i - 2;
                                        tb.Cells[i, 0].Value = thisfiletbs[index].elementname.Trim();
                                        tb.Cells[i, 1].Value = thisfiletbs[index].elementtype.Trim();
                                        tb.Cells[i, 2].Value = thisfiletbs[index].elementmaterial.Trim();
                                        tb.Cells[i, 3].Value = thisfiletbs[index].description.Trim();


                                        tb.Cells[i, 0].Borders.Left.LineWeight = LineWeight.LineWeight005;
                                        tb.Cells[i, cols - 1].Borders.Right.LineWeight = LineWeight.LineWeight005;

                                        if (i == rows - 1)
                                        {
                                            for (int j = 0; j < cols; j++)
                                                tb.Cells[i, j].Borders.Bottom.LineWeight = LineWeight.LineWeight005;
                                        }
                                    }
                                }

                                tb.LayerId = otherlayerid;

                                btr.AppendEntity(tb);
                                tr.AddNewlyCreatedDBObject(tb, true);
                                #endregion

                                #region 重绘多边形外框
                                {
                                    double polylinekuandu = Commons.StaticValues.tablepolylinekuandu;

                                    Point3d pos = tb.Position;

                                    Point3d thisminp = new Point3d(pos.X, pos.Y - (colheight[0] + (rows - 1) * colheight[colheight.Count - 1]), pos.Z);
                                    Point3d thismaxp = new Point3d(pos.X + colwidth[0], pos.Y, pos.Z);

                                    Polyline oneply = new Polyline();
                                    oneply.AddVertexAt(0, new Point2d(thisminp.X, thisminp.Y), 0, polylinekuandu, polylinekuandu);
                                    oneply.AddVertexAt(1, new Point2d(thisminp.X, thismaxp.Y), 0, polylinekuandu, polylinekuandu);
                                    oneply.AddVertexAt(2, new Point2d(thismaxp.X, thismaxp.Y), 0, polylinekuandu, polylinekuandu);
                                    oneply.AddVertexAt(3, new Point2d(thismaxp.X, thisminp.Y), 0, polylinekuandu, polylinekuandu);
                                    oneply.Closed = true;
                                    oneply.LayerId = otherlayerid;

                                    btr.AppendEntity(oneply);
                                    tr.AddNewlyCreatedDBObject(oneply, true);
                                }
                                #endregion

                                #endregion
                            }
                            #endregion
                            tr.Commit();
                        }
                        #endregion

                        string onefilename = (Path.GetExtension(onefile).ToLower() != ".dwg" ? (onefile + ".dwg") : onefile);
                        db.SaveAs(savepaths + "\\" + onefilename, db.SecurityParameters);

                        Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("文件" + onefilename + "生成成功!");
                    }

                    #endregion
                }
                #endregion
            }
        }

        #region

        /// <summary>
        /// 移动图形
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="one"></param>
        /// <param name="two"></param>
        private void move(Entity ent, Point3d one, Point3d two)
        {
            Vector3d vec = two - one;
            Matrix3d matrix = Matrix3d.Displacement(vec);
            ent.TransformBy(matrix);
        }
        /// <summary>
        /// 计算长度和宽度
        /// </summary>
        /// <param name="db"></param>
        /// <param name="wenben"></param>
        /// <param name="gaodu"></param>
        /// <param name="getwidth"></param>
        /// <returns></returns>
        private double getwidthorheight(Database db,string wenben, double gaodu, bool getwidth = true)
        {
            double getlength = 0;
            ObjectId inputids = ObjectId.Null;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                DBText text = new DBText();
                text.Position = Point3d.Origin;
                text.Height = gaodu;
                text.TextString = wenben;

                inputids = btr.AppendEntity(text);
                tr.AddNewlyCreatedDBObject(text, true);

                tr.Commit();
            }

            if (!inputids.IsNull)
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    DBObject getone = tr.GetObject(inputids, OpenMode.ForWrite);

                    if (getone is DBText)
                    {
                        if (getwidth)
                            try
                            {
                                getlength = (getone as DBText).GeometricExtents.MaxPoint.X;
                            }
                            catch { }
                        else
                            try
                            {
                                getlength = (getone as DBText).GeometricExtents.MaxPoint.Y;
                            }
                            catch { }
                    }
                    getone.Erase();
                    tr.Commit();
                }
            }

            return getlength;
        }

        /// <summary>
        /// 获取点
        /// </summary>
        /// <param name="onecirlce"></param>
        /// <param name="oneline"></param>
        /// <param name="juli"></param>
        /// <returns></returns>
        private Point3d getmaxps(Circle onecirlce, Line oneline, double juli)
        {
            List<Point3d> getallps = new List<Point3d>();
            foreach (DBObject onedbo in oneline.GetOffsetCurves(juli))
            {
                if (onedbo is Line)
                {
                    Point3dCollection getjiaodian = new Point3dCollection();
                    (onedbo as Line).IntersectWith(onecirlce, Intersect.ExtendThis, getjiaodian, IntPtr.Zero, IntPtr.Zero);

                    if (getjiaodian.Count > 0)
                    {
                        foreach (Point3d onep in getjiaodian)
                            getallps.Add(onep);
                    }

                    break;
                }
            }
            foreach (DBObject onedbo in oneline.GetOffsetCurves(-juli))
            {
                if (onedbo is Line)
                {
                    Point3dCollection getjiaodian = new Point3dCollection();
                    (onedbo as Line).IntersectWith(onecirlce, Intersect.ExtendThis, getjiaodian, IntPtr.Zero, IntPtr.Zero);

                    if (getjiaodian.Count > 0)
                    {
                        foreach (Point3d onep in getjiaodian)
                            getallps.Add(onep);
                    }

                    break;
                }
            }
            getallps = (from onep in getallps
                        orderby onep.X descending, onep.Y ascending
                        select onep).ToList();
            return getallps.Count > 0 ? getallps[0] : onecirlce.Center;
        }
        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="oneline"></param>
        /// <param name="chuizhiline"></param>
        /// <param name="juli"></param>
        /// <returns></returns>
        private Point3d getjiaodians(Line oneline, Line chuizhiline, double juli)
        {
            Point3d getps = Point3d.Origin;
            foreach (DBObject onedbo in chuizhiline.GetOffsetCurves(juli))
            {
                if (onedbo is Line)
                {
                    Point3dCollection getjiaodian = new Point3dCollection();
                    (onedbo as Line).IntersectWith(oneline, Intersect.ExtendBoth, getjiaodian, IntPtr.Zero, IntPtr.Zero);

                    if (getjiaodian.Count > 0)
                        getps = getjiaodian[0];

                    break;
                }
            }
            return getps;
        }
        /// <summary>
        /// 判断偏移距离符号
        /// </summary>
        /// <param name="oneline"></param>
        /// <param name="chuizhiline"></param>
        /// <returns></returns>
        private int judgejulioffsetfuhao(Line oneline, Line chuizhiline)
        {
            int iszhengshu = 1;
            double juli = Commons.StaticValues.comparejingdu;

            foreach (DBObject onedbo in chuizhiline.GetOffsetCurves(juli))
            {
                if (onedbo is Line)
                {
                    Point3dCollection getjiaodian = new Point3dCollection();
                    (onedbo as Line).IntersectWith(oneline, Intersect.OnBothOperands, getjiaodian, IntPtr.Zero, IntPtr.Zero);

                    if (getjiaodian.Count > 0)
                        iszhengshu = -1;

                    break;
                }
            }
            return iszhengshu;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 按中心点生成矩形
        /// </summary>
        /// <param name="onep"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Polyline createplinebycenterp(Point2d onep, double width, double height)
        {
            Point2d leftps = new Point2d(onep.X - width / 2, onep.Y - height / 2);
            Point2d rightps = new Point2d(onep.X + width / 2, onep.Y + height / 2);

            Polyline newply = new Polyline();
            newply.AddVertexAt(0, leftps, 0, 0, 0);
            newply.AddVertexAt(1, new Point2d(leftps.X,rightps.Y), 0, 0, 0);
            newply.AddVertexAt(2, rightps, 0, 0, 0);
            newply.AddVertexAt(3, new Point2d(rightps.X, leftps.Y), 0, 0, 0);

            return newply;
        }

        private Polyline createplinebycenterpSecond(Point2d onep, double width, double height)
        {
           
            Point2d rightps = new Point2d(onep.X + width / 2, onep.Y + height / 2);

            Point2d ptMiddle = new Point2d(rightps.X + Math.Cos(Math.PI / 4) * 400, rightps.Y + Math.Sin(Math.PI / 4) * 400);

            Point2d endPoint = new Point2d(ptMiddle.X + 600, ptMiddle.Y);

            Polyline newply = new Polyline();
            newply.AddVertexAt(0, rightps, 0, 0, 0);
            newply.AddVertexAt(1, ptMiddle, 0, 0, 0);
            newply.AddVertexAt(2, endPoint, 0, 0, 0);
       

            return newply;
        }

        /// <summary>
        /// 获取大点
        /// </summary>
        /// <param name="inputp"></param>
        /// <param name="basep"></param>
        /// <returns></returns>
        private Point3d getmaxp(Point3d inputp, Point3d basep)
        {
            double x = inputp.X, y = inputp.Y, z = inputp.Z;
            if (x < basep.X)
                x = basep.X;
            if (y < basep.Y)
                y = basep.Y;
            if (z < basep.Y)
                z = basep.Y;

            return new Point3d(x, y, z);
        }
        #endregion

        #region
        private string getobjstring(object inputobj)
        {
            return inputobj == null ? string.Empty : inputobj.ToString();
        }
        /// <summary>
        /// 设置图层
        /// </summary>
        /// <param name="layername"></param>
        /// <returns></returns>
        private ObjectId GetLayid(Database db,string layername,ObjectId onelinetypeid)
        {
            if (string.IsNullOrEmpty(layername))
                return ObjectId.Null;

            ObjectId layerid = ObjectId.Null;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                LayerTable lt = tr.GetObject(db.LayerTableId, OpenMode.ForWrite) as LayerTable;
                if (!lt.Has(layername.Trim()))
                {
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = layername.Trim();

                    if (!onelinetypeid.IsNull)
                        ltr.LinetypeObjectId = onelinetypeid;

                    lt.UpgradeOpen();

                    layerid = lt.Add(ltr);
                    tr.AddNewlyCreatedDBObject(ltr, true);
                    lt.DowngradeOpen();
                }

                tr.Commit();
            }

            return layerid;
        }

        /// <summary>
        /// 设置线型
        /// </summary>
        /// <param name="lineName"></param>
        private ObjectId LoadLinetype(Database db, string lineName)
        {
            if (string.IsNullOrEmpty(lineName))
                return ObjectId.Null;

            if (lineName.Trim() == Commons.StaticValues.defaultlinetypename)
                return ObjectId.Null;

            ObjectId lineid = ObjectId.Null;
            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable acLineTypTbl = acTrans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                if (!acLineTypTbl.Has(lineName))
                {
                    if (!File.Exists(Commons.StaticValues.linename))
                        File.Copy(Commons.StaticValues.assemblypath + Commons.StaticValues.linename, Commons.StaticValues.linename, true);
                    db.LoadLineTypeFile(lineName, Commons.StaticValues.linename);
                }

                lineid = acLineTypTbl[lineName.Trim()];
                acTrans.Commit();
            }

            return lineid;
        }
        #endregion
    }
}
