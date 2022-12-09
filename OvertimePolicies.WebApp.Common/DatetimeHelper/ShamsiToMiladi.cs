using System.Globalization;
using System;

namespace OvertimePolicies.WebApp.Common.DatetimeHelper
{
    public class ShamsiToMiladi
    {
        string dat, sal, mah, roz;
        public System.DateTime shamsitomiladi(string s)
        {
            dat = s;
            sal = dat.Substring(0, 4);
            mah = dat.Substring(4, 2);
            roz = dat.Substring(6, 2);

            try
            {
                PersianCalendar pc = new PersianCalendar();
                System.DateTime dt = new System.DateTime(Convert.ToInt32(sal), Convert.ToInt32(mah), Convert.ToInt32(roz), pc);
                //ret = pc.ToDateTime(Convert.ToInt32(sal), Convert.ToInt32(mah), Convert.ToInt32(roz), 0, 0, 0, 0).ToString();
                //DateTime dt = DateTime.Parse(s, new CultureInfo("fa-IR"));
                return dt;
            }
            catch (Exception)
            {
                return System.DateTime.Now;
            }

        }
    }
}