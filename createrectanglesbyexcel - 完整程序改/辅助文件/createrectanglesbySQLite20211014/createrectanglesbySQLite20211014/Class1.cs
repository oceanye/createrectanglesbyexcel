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
using OfficeOpenXml;
using System.Data.SQLite;
using System.Data;

[assembly: CommandClass(typeof(createrectanglesbyexcel.Class1))]
[assembly: ExtensionApplication(typeof(createrectanglesbyexcel.Class1))]
namespace createrectanglesbyexcel
{
    public class Class1 : IExtensionApplication
    {
        public void Initialize()
        {
            if (StaticValues.inputexcels == null)
                StaticValues.inputexcels = new excelinfos();

            if (string.IsNullOrEmpty(StaticValues.assemblypath))
                StaticValues.assemblypath = path();
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

        /// <summary>
        /// 按要求生成
        /// </summary>
        [CommandMethod("CR")]

        public void createpls()
        {
            Document mydoc = Application.DocumentManager.MdiActiveDocument;


            string dataPath = @"D:\createrectanglesbySQLite20211014\CBG测试.db"; //路径位置请按要求修改

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dataPath))
            {

                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand();

                cmd.Connection = conn;


                //SQL命令

                string sqlBeam = "SELECT*FROM Beam";
                
                System.Data.DataTable tableBeam = GetData(sqlBeam, cmd);

                string sqlColumn = "SELECT*FROM Column";

                System.Data.DataTable tableColumn = GetData(sqlColumn, cmd);

                string sqlLayer = "SELECT*FROM Layer";

                System.Data.DataTable tableLayer = GetData(sqlLayer, cmd);

                string sqlProperty = "SELECT*FROM Property";

                System.Data.DataTable tableProperty = GetData(sqlProperty, cmd);

                //遍历读值
                for (int i = 0; i < tableBeam.Rows.Count; i++)
                {
                    #region 梁信息

                    oneliang newone = new oneliang();
                    newone.linename = getobjstring(tableBeam.Rows[i].ItemArray[0]);
                    newone.pstartname = getobjstring(tableBeam.Rows[i].ItemArray[1]);
                    newone.pendname = getobjstring(tableBeam.Rows[i].ItemArray[2]);
                    newone.kuandu = Globals.getdouble(getobjstring(tableBeam.Rows[i].ItemArray[3]), (double)StaticValues.errvalues);
                    newone.wenzi = getobjstring(tableBeam.Rows[i].ItemArray[4]);
                    newone.sanjiao = getobjstring(tableBeam.Rows[i].ItemArray[5]);
                    newone.pstartpy = Globals.getdouble(getobjstring(tableBeam.Rows[i].ItemArray[6]), (double)StaticValues.errvalues);
                    newone.pendpy = Globals.getdouble(getobjstring(tableBeam.Rows[i].ItemArray[7]), (double)StaticValues.errvalues);

                    StaticValues.inputexcels.liangbiaos.Add(newone);

                  
                    #endregion
                }

                for (int i = 0; i < tableColumn.Rows.Count; i++)
                {
                    #region 柱信息

                    onezhuzi newone = new onezhuzi();
                    newone.pname = getobjstring(tableColumn.Rows[i].ItemArray[0]);
                    newone.cn = getobjstring(tableColumn.Rows[i].ItemArray[5]);
                    newone.px = Globals.getdouble(getobjstring(tableColumn.Rows[i].ItemArray[1]), (double)StaticValues.errvalues);
                    newone.py = Globals.getdouble(getobjstring(tableColumn.Rows[i].ItemArray[2]), (double)StaticValues.errvalues);
                    newone.pb = Globals.getdouble(getobjstring(tableColumn.Rows[i].ItemArray[3]), (double)StaticValues.errvalues);
                    newone.ph = Globals.getdouble(getobjstring(tableColumn.Rows[i].ItemArray[4]), (double)StaticValues.errvalues);

                    StaticValues.inputexcels.zhubiaos.Add(newone);

                    #endregion

                }

                for (int i = 0; i < tableLayer.Rows.Count; i++)
                {
                    oneset newone = new oneset();
                    newone.flag = getobjstring(tableLayer.Rows[i].ItemArray[0]);
                    newone.flagvalue = getobjstring(tableLayer.Rows[i].ItemArray[1]);
                    newone.linetype = getobjstring(tableLayer.Rows[i].ItemArray[2]);

                    StaticValues.inputexcels.allsets.Add(newone);

                }

               




            }

            //    if (System.Windows.Forms.DialogResult.OK == Application.ShowModalDialog(new forms.Form1()))
            //{
                #region 获取EXCEL信息
                //if (string.IsNullOrEmpty(StaticValues.openfiles))
                //{
                //    Application.ShowAlertDialog("请选择导入EXCEL!");
                //    return;
                //}
                #region 读取EXCEL文件
                //StaticValues.inputexcels = new excelinfos();
                //if (File.Exists(StaticValues.openfiles))
                //{
                //    using (OfficeOpenXml.ExcelPackage ep = new OfficeOpenXml.ExcelPackage())
                //    {
                //        using (FileStream filestream = new FileStream(StaticValues.openfiles, FileMode.Open))
                //        {
                //            ep.Load(filestream);

                //            for (int i = 1; i <= ep.Workbook.Worksheets.Count; i++)
                //            {
                //                ExcelWorksheet sheet = ep.Workbook.Worksheets[i];

                //                #region
                //                if (sheet.Name.Trim() == StaticValues.zhubiaonames)
                //                {
                //                    int biaotirow = 1;
                //                    #region
                //                    if (sheet.Dimension != null && (sheet.Dimension.Columns >= 5))
                //                    {
                //                        for (int j = sheet.Dimension.Start.Row + biaotirow; j <= sheet.Dimension.End.Row; j++)
                //                        {
                //                            onezhuzi newone = new onezhuzi();
                //                            newone.pname = getobjstring(sheet.Cells[j, 1].Value);
                //                            newone.cn = getobjstring(sheet.Cells[j, 6].Value);
                //                            newone.px = Globals.getdouble(getobjstring(sheet.Cells[j, 2].Value), (double)StaticValues.errvalues);
                //                            newone.py = Globals.getdouble(getobjstring(sheet.Cells[j, 3].Value), (double)StaticValues.errvalues);
                //                            newone.pb = Globals.getdouble(getobjstring(sheet.Cells[j, 4].Value), (double)StaticValues.errvalues);
                //                            newone.ph = Globals.getdouble(getobjstring(sheet.Cells[j, 5].Value), (double)StaticValues.errvalues);

                //                            StaticValues.inputexcels.zhubiaos.Add(newone);
                //                        }
                //                    }
                //                    #endregion
                //                }
                //                else if (sheet.Name.Trim() == StaticValues.liangbiaonames)
                //                {
                //                    int biaotirow = 1;
                //                    #region
                //                    if (sheet.Dimension != null && (sheet.Dimension.Columns >= 8))
                //                    {
                //                        for (int j = sheet.Dimension.Start.Row + biaotirow; j <= sheet.Dimension.End.Row; j++)
                //                        {
                //                            oneliang newone = new oneliang();
                //                            newone.linename = getobjstring(sheet.Cells[j, 1].Value);
                //                            newone.pstartname = getobjstring(sheet.Cells[j, 2].Value);
                //                            newone.pendname = getobjstring(sheet.Cells[j, 3].Value);
                //                            newone.kuandu = Globals.getdouble(getobjstring(sheet.Cells[j, 4].Value), (double)StaticValues.errvalues);
                //                            newone.wenzi = getobjstring(sheet.Cells[j, 5].Value);
                //                            newone.sanjiao = getobjstring(sheet.Cells[j, 6].Value);
                //                            newone.pstartpy = Globals.getdouble(getobjstring(sheet.Cells[j, 7].Value), (double)StaticValues.errvalues);
                //                            newone.pendpy = Globals.getdouble(getobjstring(sheet.Cells[j, 8].Value), (double)StaticValues.errvalues);

                //                            StaticValues.inputexcels.liangbiaos.Add(newone);
                //                        }
                //                    }
                //                    #endregion
                //                }
                //                else if (sheet.Name.Trim() == StaticValues.shuxingnames)
                //                {
                //                    int biaotirow = 0;
                //                    #region
                //                    if (sheet.Dimension != null && (sheet.Dimension.Columns >= 3))
                //                    {
                //                        for (int j = sheet.Dimension.Start.Row + biaotirow; j <= sheet.Dimension.End.Row; j++)
                //                        {
                //                            oneset newone = new oneset();
                //                            newone.flag = getobjstring(sheet.Cells[j, 1].Value);
                //                            newone.flagvalue = getobjstring(sheet.Cells[j, 2].Value);
                //                            newone.linetype = getobjstring(sheet.Cells[j, 3].Value);

                //                            StaticValues.inputexcels.allsets.Add(newone);
                //                        }
                //                    }
                //                    #endregion
                //                }
                //                #endregion
                //            }
                //        }
                //    }
                //}

                #endregion
                #endregion
                #region 按次序生成
                //{
                    using (Transaction tr = mydoc.Database.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)tr.GetObject(mydoc.Database.BlockTableId, OpenMode.ForWrite);
                        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        Dictionary<string, Point3d> getppps = new Dictionary<string, Point3d>();//保存第一步生成的点
                        Dictionary<string, Polyline> getppls = new Dictionary<string, Polyline>();//保存第一步生成的矩形
                        #region 第一步生成P
                        {

                            ObjectId layerid1 = ObjectId.Null;

                            {
                                ObjectId linetypeid1 = ObjectId.Null;
                                string findname = "P-图层名";
                                var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                if (tt.Count > 0)
                                {
                                    string onelinetype = tt[0].linetype;
                                    linetypeid1 = LoadLinetype(onelinetype);
                                    string onelayer = tt[0].flagvalue;
                                    layerid1 = GetLayid(onelayer, linetypeid1);
                                }
                            }

                            #region 第一步生成P


                            ObjectId layerid11 = ObjectId.Null;

                            {
                                ObjectId linetypeid11 = ObjectId.Null;
                                string findname = "P-图层名2";
                                var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                if (tt.Count > 0)
                                {
                                    string onelinetype = tt[0].linetype;
                                    linetypeid11 = LoadLinetype(onelinetype);
                                    string onelayer = tt[0].flagvalue;
                                    layerid11 = GetLayid(onelayer, linetypeid11);
                                }
                            }



                            #region 生成多边形
                            foreach (onezhuzi thiszhuzi in StaticValues.inputexcels.zhubiaos)
                            {
                                Polyline oneply = createplinebycenterp(new Point2d(thiszhuzi.px, thiszhuzi.py), thiszhuzi.pb, thiszhuzi.ph);
                                if (!layerid1.IsNull && !layerid11.IsNull)
                                    if (thiszhuzi.cn.StartsWith("V"))
                                    {
                                        oneply.LayerId = layerid11;
                                        oneply.Closed = true;
                                        btr.AppendEntity(oneply);
                                        tr.AddNewlyCreatedDBObject(oneply, true);

                                        getppps.Add(thiszhuzi.pname.Trim(), new Point3d(thiszhuzi.px, thiszhuzi.py, StaticValues.errvalues));
                                        getppls.Add(thiszhuzi.pname.Trim(), oneply);
                                    }

                                    else
                                    {
                                        oneply.LayerId = layerid1;
                                        oneply.Closed = true;
                                        btr.AppendEntity(oneply);
                                        tr.AddNewlyCreatedDBObject(oneply, true);

                                        getppps.Add(thiszhuzi.pname.Trim(), new Point3d(thiszhuzi.px, thiszhuzi.py, StaticValues.errvalues));
                                        getppls.Add(thiszhuzi.pname.Trim(), oneply);
                                    }


                            }
                            #endregion

                        }
                        #endregion


                        #endregion

                        #region 第二步生成PL
                        {
                            ObjectId layerid2 = ObjectId.Null;

                            #region
                            {
                                ObjectId linetypeid2 = ObjectId.Null;
                                string findname = "L-图层名";
                                var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                if (tt.Count > 0)
                                {
                                    string onelinetype = tt[0].linetype;
                                    linetypeid2 = LoadLinetype(onelinetype);
                                    string onelayer = tt[0].flagvalue;
                                    layerid2 = GetLayid(onelayer, linetypeid2);
                                }
                            }


                            #endregion

                            ObjectId layerid3 = ObjectId.Null;
                            #region
                            {
                                ObjectId linetypeid3 = ObjectId.Null;
                                string findname = "其余标注图层名";
                                var tt1 = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                if (tt1.Count > 0)
                                {
                                    string onelinetype = tt1[0].linetype;
                                    linetypeid3 = LoadLinetype(onelinetype);
                                    string onelayer = tt1[0].flagvalue;
                                    layerid3 = GetLayid(onelayer, linetypeid3);
                                }
                            }
                            #endregion

                            #region
                            {
                                double liangduanjianju = 50;
                                #region
                                {
                                    string findname = "梁端间距";
                                    var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                    if (tt.Count > 0)
                                        liangduanjianju = Globals.getdouble(tt[0].flagvalue, liangduanjianju);
                                }
                                #endregion

                                double zigao = 250;
                                #region
                                {
                                    string findname = "字高";
                                    var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                    if (tt.Count > 0)
                                        zigao = Globals.getdouble(tt[0].flagvalue, zigao);
                                }
                                #endregion

                                double zidijiange = 50;
                                #region
                                {
                                    string findname = "字底间距";
                                    var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                    if (tt.Count > 0)
                                        zidijiange = Globals.getdouble(tt[0].flagvalue, zidijiange);
                                }
                                #endregion


                                double sanjiaodikuan = 300;
                                #region
                                {
                                    string findname = "三角底度";
                                    var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                    if (tt.Count > 0)
                                        sanjiaodikuan = Globals.getdouble(tt[0].flagvalue, sanjiaodikuan);
                                }
                                #endregion

                                double sanjiaogaodu = 250;
                                #region
                                {
                                    string findname = "三角高度";
                                    var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();
                                    if (tt.Count > 0)
                                        sanjiaogaodu = Globals.getdouble(tt[0].flagvalue, sanjiaogaodu);
                                }
                                #endregion

                                foreach (oneliang thisliang in StaticValues.inputexcels.liangbiaos)
                                {
                                    Polyline getstartpl = getppls[thisliang.pstartname];
                                    Polyline getendpl = getppls[thisliang.pendname];

                                    Point3d linestartp = getppps[thisliang.pstartname];
                                    Point3d lineendp = getppps[thisliang.pendname];

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

                                        foreach (DBObject onedbo in (oneline.Clone() as Line).GetOffsetCurves(thisliang.pstartpy))//正负控制方向
                                        {
                                            if (onedbo is Line)
                                            {
                                                linestartp = (onedbo as Line).StartPoint;
                                                break;
                                            }
                                        }

                                        foreach (DBObject onedbo in (oneline.Clone() as Line).GetOffsetCurves(thisliang.pendpy))
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
                                        double pianyiabs = thisliang.kuandu / 2;

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
                                                List<string> getsanjiaos = thisliang.sanjiao.Split(new char[] { StaticValues.splitsanjiao }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                                if (getsanjiaos.Contains(thisliang.pstartname))
                                                {
                                                    if (thisliang.wenzi.Contains("GKL"))
                                                    {
                                                        Polyline newply = new Polyline();
                                                        newply.AddVertexAt(0, new Point2d(oneline.StartPoint.X, oneline.StartPoint.Y), 0, sanjiaodikuan, 0);
                                                        newply.AddVertexAt(1, new Point2d(oneline.StartPoint.X + sanjiaogaodu * Math.Cos(oneline.Angle), oneline.StartPoint.Y + sanjiaogaodu * Math.Sin(oneline.Angle)), 0, 0, 0);
                                                        newply.LayerId = layerid3;

                                                        btr.AppendEntity(newply);
                                                        tr.AddNewlyCreatedDBObject(newply, true);
                                                    }

                                                }

                                                if (getsanjiaos.Contains(thisliang.pendname))
                                                {
                                                    if (thisliang.wenzi.Contains("GKL"))
                                                    {
                                                        Polyline newply = new Polyline();
                                                        newply.AddVertexAt(0, new Point2d(oneline.EndPoint.X, oneline.EndPoint.Y), 0, sanjiaodikuan, 0);
                                                        newply.AddVertexAt(1, new Point2d(oneline.EndPoint.X - sanjiaogaodu * Math.Cos(oneline.Angle), oneline.EndPoint.Y - sanjiaogaodu * Math.Sin(oneline.Angle)), 0, 0, 0);
                                                        newply.LayerId = layerid3;

                                                        btr.AppendEntity(newply);
                                                        tr.AddNewlyCreatedDBObject(newply, true);
                                                    }

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
                                                newtext.TextString = thisliang.wenzi;
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

                        tr.Commit();
                    }
              //  }
                #endregion

               // mydoc.Database.SaveAs(StaticValues.savefiles, DwgVersion.Current);

            
          //  }
        }

        public System.Data.DataTable GetData(string sql, SQLiteCommand cmd)
        {
            System.Data.DataTable table;

            cmd.CommandText = sql;



            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            table = ds.Tables[0];

            cmd.ExecuteNonQuery();

            return table;



        }

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
            newply.AddVertexAt(1, new Point2d(leftps.X, rightps.Y), 0, 0, 0);
            newply.AddVertexAt(2, rightps, 0, 0, 0);
            newply.AddVertexAt(3, new Point2d(rightps.X, leftps.Y), 0, 0, 0);

            return newply;
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
        private ObjectId GetLayid(string layername, ObjectId onelinetypeid)
        {
            if (string.IsNullOrEmpty(layername))
                return ObjectId.Null;

            if (!StaticValues.layersettings.Keys.Contains(layername.Trim()))
            {
                Database db = HostApplicationServices.WorkingDatabase;
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

                        ObjectId layerid = lt.Add(ltr);
                        tr.AddNewlyCreatedDBObject(ltr, true);
                        lt.DowngradeOpen();

                        string findname = "P-图层名2";
                        var tt = StaticValues.inputexcels.allsets.Where(oneobj => oneobj.flag.Trim() == findname.Trim()).ToList();

                        if (ltr.Name == tt[0].flagvalue)
                        {
                            ltr.IsOff = true;
                        }

                    }

                    StaticValues.layersettings.Add(layername.Trim(), lt[layername.Trim()]);

                    tr.Commit();
                }
            }

            return StaticValues.layersettings[layername.Trim()];
        }

        /// <summary>
        /// 设置线型
        /// </summary>
        /// <param name="lineName"></param>
        private ObjectId LoadLinetype(string lineName)
        {
            if (string.IsNullOrEmpty(lineName))
                return ObjectId.Null;

            if (lineName.Trim() == StaticValues.defaultlinetypename)
                return ObjectId.Null;

            if (!StaticValues.linetypesettings.Keys.Contains(lineName.Trim()))
            {
                Database db = HostApplicationServices.WorkingDatabase;
                using (Transaction acTrans = db.TransactionManager.StartTransaction())
                {
                    LinetypeTable acLineTypTbl = acTrans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                    if (!acLineTypTbl.Has(lineName))
                    {
                        if (!File.Exists(StaticValues.linename))
                            File.Copy(StaticValues.assemblypath + StaticValues.linename, StaticValues.linename, true);
                        db.LoadLineTypeFile(lineName, StaticValues.linename);
                    }

                    StaticValues.linetypesettings.Add(lineName.Trim(), acLineTypTbl[lineName.Trim()]);
                    acTrans.Commit();
                }
            }
            return StaticValues.linetypesettings[lineName.Trim()];
        }
        #endregion
    }

    public abstract class StaticValues
    {
        public static string assemblypath = string.Empty;
        public const string linename = "acad.lin";

        public static string openfiles = string.Empty;
        public static string savefiles = string.Empty;

        public const int errvalues = 0;


        public const char splitsanjiao = '&';

        public static double juxingpy = 50;//矩形偏移
        public static double zigao = 250;//字高
        public static double wenzipy = 50;//文字偏移
        public static double sanjiaokuandu = 300;
        public static double sanjiaochangdu = 250;

        public static excelinfos inputexcels = null;

        public const string zhubiaonames = "柱表";
        public const string liangbiaonames = "梁表";
        public const string shuxingnames = "属性设置";

        public const string defaultlinetypename = "Continous";

        public static Dictionary<string, ObjectId> layersettings = new Dictionary<string, ObjectId>();//图层设置信息
        public static Dictionary<string, ObjectId> linetypesettings = new Dictionary<string, ObjectId>();//图层设置信息
    }

    public abstract class Globals
    {
        public static double getdouble(string inputs, double defaultvalue)
        {
            try
            {
                return double.Parse(inputs);
            }
            catch { }

            return defaultvalue;
        }
    }


    #region 类说明
    /// <summary>
    /// 导入的EXCEL信息
    /// </summary>
    public class excelinfos
    {
        public excelinfos()
        {
            zhubiaos = new List<onezhuzi>();
            liangbiaos = new List<oneliang>();
            allsets = new List<oneset>();
        }
        public List<onezhuzi> zhubiaos { get; set; }
        public List<oneliang> liangbiaos { get; set; }
        public List<oneset> allsets { get; set; }
    }

    public class onezhuzi
    {
        public onezhuzi()
        {
            pname = string.Empty;
        }
        public string pname { get; set; }
        public double px { get; set; }
        public double py { get; set; }
        public double pb { get; set; }
        public double ph { get; set; }
        public string cn { get; set; }
    }

    public class oneliang
    {
        public oneliang()
        {
            linename = string.Empty;
            pstartname = string.Empty;
            pendname = string.Empty;
            wenzi = string.Empty;
            sanjiao = string.Empty;
        }
        public string linename { get; set; }
        public string pstartname { get; set; }
        public string pendname { get; set; }
        public double kuandu { get; set; }
        public string wenzi { get; set; }
        public string sanjiao { get; set; }
        public double pstartpy { get; set; }
        public double pendpy { get; set; }
    }

    public class oneset
    {
        public oneset()
        {
            flag = string.Empty;
            flagvalue = string.Empty;
            linetype = string.Empty;
        }
        public string flag { get; set; }
        public string flagvalue { get; set; }
        public string linetype { get; set; }
    }

    public class lineparams
    {
        public lineparams() { }
        public lineparams(double x, double y, double z)
        {
            A = x;
            B = y;
            C = z;
        }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
    }

    #endregion
}
