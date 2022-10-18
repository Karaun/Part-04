using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 时间系统转换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择你要打开的文件";
            ofd.Filter = "txt|*.txt|All|*.*";
            ofd.ShowDialog();

            string path = ofd.FileName;

            if (path == "")
            {
                return;
            }

            using (FileStream FsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = FsRead.Read(buffer, 0, buffer.Length);
                txtSource.Text = Encoding.UTF8.GetString(buffer, 0, r);


            }
        }

        private void 导出结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "请选择保存路径";
            sfd.Filter = "tx|*.txt|All|*.*";
            sfd.ShowDialog();

            string outcome = sfd.FileName;
            if (outcome == "")
            {
                return;
            }
            using (FileStream FsWrite = new FileStream(outcome, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(txtResult.Text);
                FsWrite.Write(buffer, 0, buffer.Length);

            }
        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           List<Time> Time_list = new List<Time>();//时间列表

            //存储路径数据
           Time_list = Time.GetTime1(txtSource.Text);

            List<string> JD_Time = new List<string>();//存储JD
            for (int i = 0; i < Time_list.Count; i++)
            {
                JD_Time.Add(Time.Jd(Time_list[i]).ToString("#0.00000"));
            }


            List<string> Date_list = new List<string>();//存储公历
            for (int i = 0; i < JD_Time.Count; i++)
            {
                string date = Time.Gregorian_calendar(Convert.ToDouble(JD_Time[i]));
                date = date + " " + Time.Time_Format(Time_list[i]);
                Date_list.Add(date);
                //MessageBox.Show(Date_list[i]);
            }

            List<string> Day_count = new List<string>();//存储年积日
            for (int i = 0; i < Time_list.Count; i++)
            {
                Day_count.Add(Time.day_count(Time_list[i]));
                
            }


            List<string> State_list = new List<string>();//存储状态数据
            for (int i = 0; i < Time_list.Count; i++)
            {
                State_list.Add(Time.State(Time_list[i]));
            }

            string result = null;
            string t1 = "------------JD------------";
            string t2 = "------------公历(年 月 日 时：分：秒)------------";
            string t3 = "------------年积日------------";
            string t4 = "------------三天打鱼两天晒网------------";

            result = t1 + "\r\n" + display(JD_Time) + "\r\n" + t2 + "\r\n" + display(Date_list) + "\r\n" + t3 + "\r\n" + display(Day_count) + "\r\n" + t4 + "\r\n" + display(State_list);
            txtResult.Text = result;
        }


        public string display(List<string> list)
        {
            string result = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                {
                    result += list[i] + "\r\n";
                }
                else
                {
                    result += list[i];
                }
            }

            return result;
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
