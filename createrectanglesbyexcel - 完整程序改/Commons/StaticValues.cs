using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace createrectanglesbyexcel.Commons
{
    public abstract class StaticValues
    {
        public static string assemblypath = string.Empty;
        public const string linename = "acad.lin";

        public const int errvalues = 0;


        public const char splitsanjiao = '&';

        public static double juxingpy = 50;//矩形偏移
        public static double zigao = 250;//字高
        public static double wenzipy = 50;//文字偏移
        public static double sanjiaokuandu = 300;//三角底部宽度
        public static double sanjiaogaodu = 250;//三角高度

        public static double circlediameter = 400;//画圆直径
        public static double circlezigao = 20;//画圆文字高度
        public static double zhouwang = 1000;//轴网距离
        public static double zhouwang1 = 500;//轴网标注1
        public static double zhouwang2 = 750;//轴网标注2

        public const string defaultlinetypename = "Continous";

        public static string connectstring = "Data Source={0};Version=3;Pooling=True;";
        public static string dbname = "CBGtest.db";

        public const string templatedwg = "template.dwg";

        public static char newsplitchar = ',';
        public static char newsplitchar1 = '，';

        public const double comparejingdu = 0.1;

        public const char newtbwidthchar = '@';
        public const double tablepy = 100;
        public static double tablepolylinekuandu = 5;//table外框宽度
    }
}
