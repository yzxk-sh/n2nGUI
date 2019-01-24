using System;
/*
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
using System.Windows.Forms;
using System.Diagnostics;



namespace n2nGUI
{
    public partial class Form1 : Form
    {
        int processID;
        public Form1()
        {
            int x = (SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            InitializeComponent();
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = (Point)new Size(x, y);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string serverAdd, serverPort, edgeAdd, edgeGroup, edgePasswd;
            serverAdd = supernodeAddEdit.Text;
            serverPort = supernodePortEdit.Text;
            edgeAdd = ipSet.Text;
            edgeGroup = group.Text;
            edgePasswd = password.Text;

            string para = " -a " + edgeAdd + " -c " + edgeGroup + " -k " + edgePasswd + " -l " + serverAdd + ":" + serverPort;
            string path = System.Environment.CurrentDirectory;
            //          Process myProcess = Process.Start("D:/kyle/Downloads/n2n/win32/DotNet/Release/edge.exe", para);        
            //Process myProcess = Process.Start(path + "/edge.exe", para);
            //processID = myProcess.Id;
            //Console.WriteLine(path);

            ProcessStartInfo Info = new ProcessStartInfo
            {
                FileName = "edge.exe",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                Arguments = para,
                WorkingDirectory = path
            };


            System.Diagnostics.Process Proc;
            try
            { // //启动外部程序 // 
                Proc = System.Diagnostics.Process.Start(Info);
                processID = Proc.Id;
            }
            catch (System.ComponentModel.Win32Exception exc)
            {
                Console.WriteLine("系统找不到指定的程序文件。/r{0}", exc);

            }
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            Process myProcessA = Process.GetProcessById(processID);     // 通过ID关联进程
            myProcessA.Kill();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //取消关闭窗口
            e.Cancel = true;
            //最小化主窗口
            this.WindowState = FormWindowState.Minimized;
            //不在系统任务栏显示主窗口图标
            this.ShowInTaskbar = false;
            this.notifyIcon.Visible = true;

        }

        private void notifyIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = true;
                this.ShowInTaskbar = true;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processID != 0)
            {
                Abort_Click(null, null);
            }
            Application.Exit();
        }
    }
}
