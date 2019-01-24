using System;
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
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string serverAdd, serverPort, edgeAdd, edgeGroup, edgePasswd, para, path;
            serverAdd = supernodeAddEdit.Text;
            serverPort = supernodePortEdit.Text;
            edgeAdd = ipSet.Text;
            edgeGroup = group.Text;
            edgePasswd = password.Text;

            para = " -a " + edgeAdd + " -c " + edgeGroup + " -k " + edgePasswd + " -l " + serverAdd + ":" + serverPort + " " + Form2.paraInForm2;
            path = System.Environment.CurrentDirectory;

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


        private void Advance_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Hide();
        }
    }
}
