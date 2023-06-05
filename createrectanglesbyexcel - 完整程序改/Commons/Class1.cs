using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Geometry;

namespace createrectanglesbyexcel.Commons
{
    /// <summary>
    /// 线段参数
    /// </summary>
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

    [Serializable]
    public partial class GridTableEx : sqlite.Model.GridTable
    {
        public GridTableEx() : base()
        {
            startp = Point3d.Origin;
            endp = Point3d.Origin;
            thislinetype = linepostype.none;
            isdrawline = true;
            getleftps = new Dictionary<double, Point3d>();
            getrightps = new Dictionary<double, Point3d>();
        }

        public GridTableEx(sqlite.Model.GridTable inputone):this()
        {
            this.id = inputone.id;
            this.thisid = inputone.thisid;
            this.gridname = inputone.gridname;
            this.gridpoint = inputone.gridpoint;
            this.gridgroup = inputone.gridgroup;
            this.gridtablepos = inputone.gridtablepos;
            this.filename = inputone.filename;
            this.isdrawline = true;

            this.getleftps = new Dictionary<double, Point3d>();
            this.getrightps = new Dictionary<double, Point3d>();

            if (!string.IsNullOrEmpty(this.gridpoint))
            {
                string tempstring = this.gridpoint.Replace("(", string.Empty).Replace("（", string.Empty).Replace(")", string.Empty).Replace("）", string.Empty).Trim();
                List<double> thissplits = tempstring.Trim().Split(new char[] { StaticValues.newsplitchar, StaticValues.newsplitchar1 }, StringSplitOptions.RemoveEmptyEntries).Select(oneobj=>Globals.DataParse(oneobj,(double)StaticValues.errvalues)).ToList();
                if (thissplits.Count/3 > 0)
                    this.startp = new Point3d(thissplits.Take(3).ToArray());
                if (thissplits.Count / 3 > 1)
                    this.endp = new Point3d(thissplits.Skip(3).Take(3).ToArray());

                setpostype();//设置类型
                setpoints();//判断起始、终止
            }

            
        }

        internal void setpostype()
        {
            if (!string.IsNullOrEmpty(this.gridtablepos))
            {
                switch (this.gridtablepos.ToLower().Trim())
                {
                    case "startpoint":
                        this.thislinetype = linepostype.startpoint;
                        break;
                    case "endpoint":
                        this.thislinetype = linepostype.endpoint;
                        break;
                    case "both":
                        this.thislinetype = linepostype.both;
                        break;
                    default:
                        this.thislinetype = linepostype.none;
                        break;
                }
            }
        }

        internal void setpoints()
        {
            KeyValuePair<Point3d, Point3d> getps = new KeyValuePair<Point3d, Point3d>(this.startp, this.endp);
            if (this.startp.X > this.endp.X)
                getps = swapps(this.startp, this.endp);
            else if (this.startp.X == this.endp.X)
            {
                if (this.startp.Y > this.endp.Y)
                    getps = swapps(this.startp, this.endp);
            }

            this.startp = getps.Key;
            this.endp = getps.Value;
        }

        internal KeyValuePair<Point3d, Point3d> swapps(Point3d onep, Point3d twop)
        {
            return new KeyValuePair<Point3d, Point3d>(twop, onep);
        }


        public Point3d startp;
        public Point3d endp;
        public linepostype thislinetype;
        public bool isdrawline;

        public Dictionary<double, Point3d> getleftps;
        public Dictionary<double, Point3d> getrightps;
    }

    public enum linepostype
    {
        none = 0, startpoint = 1, endpoint = 2, both = 3
    }
}
