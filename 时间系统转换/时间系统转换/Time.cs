using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 时间系统转换
{
    class Time
    {
        #region 时间类
        public Time(double i, double j, double k, double l, double z, double x)
        {
            year = i;
            month = j;
            day = k;
            hour = l;
            minute = z;
            second = x;
        }

        private double Year;
        private double Month;
        private double Day;
        private double Hour;
        private double Minute;
        private double Second;

        public double year
        {
            set { Year = value; }
            get { return Year; }
        }

        public double month
        {
            set { Month = value; }
            get { return Month; }
        }
        public double day
        {
            set { Day = value; }
            get { return Day; }
        }
        public double hour
        {
            set { Hour = value; }
            get { return Hour; }
        }
        public double minute
        {
            set { Minute = value; }
            get { return Minute; }
        }
        public double second
        {
            set { Second = value; }
            get { return Second; }
        }
        #endregion


        public static double Jd(Time t)//公历转儒略日(JD)
        {
            double JD = 1721013.5 + 367 * t.year;
            double temp = Math.Floor((t.month + 9.0) / 12.0);
            JD -= Math.Floor(7.0 / 4.0 * (t.year + temp));
            JD += Math.Floor(275.0 * t.month / 9.0);
            JD += t.day + t.hour / 24.0 + t.minute / 1440.0 + t.second / 86400.0;
            return JD;

        }
        //Math.Floor为向下取整
        //Math.Round：根据四舍五入取整
        //Math.Ceiling：向上取整，有小数，整数加1


        public static string Gregorian_calendar(double jd)//儒略日转公历
        {
            double a = Math.Floor(jd + 0.5);
            double b = a + 1537.0;
            double c = Math.Floor((b - 122.1) / 365.25);
            double d = Math.Floor(365.25 * c);
            double e = Math.Floor((b - d) / 30.600);
            string day = null;
            string month = null;


            day = (b - d - Math.Floor(30.6001 * e) + 1 / (jd + 0.5)).ToString("#00");
            if (e < 14)
            {
                e--;
            }
            else if (e == 14.0 || e == 15.0)
            {
                e -= 13;
            }
            month = (Math.Floor(e - 12 * Math.Floor(e / 14.0))).ToString("#00");
            string year = (c - 4715.0 - Math.Floor((7 + Convert.ToDouble(month)) / 10.0)).ToString("#0");

            string date = year + " " + month + " " + day;
            return date;
        }

        public static string Time_Format(Time t)//时间(小时：分钟：秒)显示
        {
            string hour = null;
            string minute = null;
            string second = null;

            hour = t.hour.ToString("#00");
            minute = t.minute.ToString("#00");
            second = t.second.ToString("#00.000000");



            string tt = hour + ":" + minute + ":" + second;
            return tt;
        }


        public static string day_count(Time t)//年积日
        {
            string day_count = null;
            int[] tab = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((t.year % 4 == 0 && t.year % 100 != 0) || (t.year % 400 == 0))
            {
                tab[1] = 29;
            }

            int count = 0;
            for (int i = 0; i < Convert.ToInt32(t.month) - 1; i++)
            {
                count += tab[i];
            }

            count += Convert.ToInt32(t.day);

            day_count = count.ToString();
            return day_count;
        }

        //时间状态
        public static string State(Time t)
        {
            string temp = null;
            double jd = Jd(t);
            int day = Convert.ToInt32(day_count(t));
            int i = day % 5;
            if (i < 3)
            {
                temp = "打鱼日";
            }
            else if (i >= 3 && i <= 5)
            {
                temp = "晒网日";
            }

            string state = Gregorian_calendar(jd) + "," + temp;

            return state;

        }


        //通过符号分割时间
        public static List<Time> GetTime1(string text)
        {
            string line;//读取行数
            string[] str = text.Split('\n');//存储每行数据
            string[] temp = new string[1000000];//创建集合存储起点，终点，距离数据
            List<Time> Time_list = new List<Time>();//时间列表

            //存储时间数据
            for (int x = 0; x < str.Length; x++)
            {
                line = str[x];
                temp = line.Split(' ');
                string Y = temp[0];
                string M = temp[1];
                string D = temp[2];
                string H = temp[3];
                string U = temp[4];
                string S = temp[5];
                //MessageBox.Show(Y + M + D + H + U + S);

                Time tt = new Time(Convert.ToDouble(Y), Convert.ToDouble(M), Convert.ToDouble(D), Convert.ToDouble(H), Convert.ToDouble(U), Convert.ToDouble(S));
                Time_list.Add(tt);

            }
            return Time_list;
        }


        //输入时间列表进行切割
        public static List<Time> GetTime2(List<string> time)
        {
            List<Time> Time_list = new List<Time>();//时间列表

            //获取数据继续分割
            for (int i = 0; i < time.Count; i++)
            {

                double Y = Convert.ToDouble(time[i].Substring(0, 4));
                double M = Convert.ToDouble(time[i].Substring(4, 2));
                double D = Convert.ToDouble(time[i].Substring(6, 2));
                double H = Convert.ToDouble(time[i].Substring(8, 2));
                double U = Convert.ToDouble(time[i].Substring(10, 2));
                double S = Convert.ToDouble(time[i].Substring(12, 2));
                //MessageBox.Show(Y + M + D + H + U + S);

                Time tt = new Time(Convert.ToDouble(Y), Convert.ToDouble(M), Convert.ToDouble(D), Convert.ToDouble(H), Convert.ToDouble(U), Convert.ToDouble(S));
                Time_list.Add(tt);

            }
            return Time_list;
        }

    }
}
