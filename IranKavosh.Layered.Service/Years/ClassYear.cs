using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IranKavosh.Layered.Service.Years
{
    public static class ClassYear
    {
        public static List<string> GetYear()
        {
            List<string> yearList = new List<string>();
            int s = 1360;
            int m = 1981;
            for (int i = 0; i <= 90; i++)
            {
                yearList.Add(m++.ToString() + "-" + s++.ToString());
            }

            return yearList;
        }
        public static List<string> GetYearForHeaderChapter()
        {
            List<string> yearList = new List<string>();
            int s = 1360;
            int m = 1981;
            for (int i = 0; i <= 90; i++)
            {
                yearList.Add(s++.ToString() + "-" + m++.ToString());
            }

            return yearList;
        }
    }
}
