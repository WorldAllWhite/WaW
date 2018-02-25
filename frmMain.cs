using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace WaW
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 用户状态
        /// </summary>
        private static wawState _userstate;

        private Thread udplistenthread;
        private DataReceive startreceive;

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindChatWindow(string lpClassName, string lpWindowName);

        public frmMain()
        {
            InitializeComponent();
            _userstate = wawState.SignIn;
            Rectangle rec = Screen.GetWorkingArea(this);
            this.ClientSize = new Size(234, rec.Height - 100);
            this.Location = new Point((int)(rec.Width * 0.8), (int)(rec.Height * 0.05));
            this.MaximumSize = new Size(260, rec.Height);
            this.MinimumSize = new Size(234, 100);
            this.chTag.Width = 0;
            this.chUser.Width = (int)(this.lvwUsers.Width * 0.3);
            this.chIP.Width = (int)(this.lvwUsers.Width * 0.4);
            this.chHostname.Width = this.lvwUsers.Width - this.chUser.Width - this.chHostname.Width;
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public static wawState Userstate
        {
            get
            {
                return _userstate;
            }

            set
            {
                _userstate = value;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //开启监听线程
            startreceive = new DataReceive(lvwUsers, lblUserCount);
            udplistenthread = new Thread(new ThreadStart(startreceive.StartListenUdp));
            udplistenthread.IsBackground = true;
            udplistenthread.Start();

            MsgBoardCast boardcast = new MsgBoardCast();
            boardcast.BoardCast(wawCMD.WAW_BC_SIGNIN);
            boardcast.BoardCastClose();

            int i = 0;
            foreach(IPAddress addr in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if(addr.AddressFamily==AddressFamily.InterNetwork)
                {
                    cmbIpList.Items.Add(addr.ToString());
                    if (addr.ToString()==InfoSet.IpPort.Address.ToString())
                    {
                        cmbIpList.SelectedIndex = i;
                    }
                    i++;                    
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MsgBoardCast boardcast = new MsgBoardCast();
            boardcast.BoardCast(wawCMD.WAW_BC_GETLIST);
            boardcast.BoardCastClose();
        }

        private void lvwUsers_ItemActivate(object sender, EventArgs e)
        {
            if(lvwUsers.SelectedItems[0].Index!=-1)
            {
                string windowname="与"+lvwUsers.SelectedItems[0].SubItems[0].Text+"聊天中";
                IntPtr handle = FindChatWindow(null, windowname);
                if(handle!=IntPtr.Zero)
                {
                    Form frm = (Form)FromHandle(handle);
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                }
                else
                {
                    string user = lvwUsers.SelectedItems[0].SubItems[1].Text;
                    string ip = lvwUsers.SelectedItems[0].SubItems[2].Text;
                    string hostname = lvwUsers.SelectedItems[0].SubItems[3].Text;
                    frmChat chatform = new frmChat(ip, user, hostname, string.Empty);
                    chatform.Text= "与" + user + "聊天中";
                    chatform.Show();
                }
            }
        }

        private void cmbIpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InfoSet.IpPort.Address = IPAddress.Parse(cmbIpList.SelectedItem.ToString());
            startreceive.StopListenUdp();
            //关闭当前监听线程，创建新线程重新在新地址和端口上监听
            if(udplistenthread != null)
            {
                udplistenthread.Abort();
            }
            startreceive = new DataReceive(lvwUsers, lblUserCount);
            udplistenthread = new Thread(new ThreadStart(startreceive.StartListenUdp));
            udplistenthread.IsBackground = true;
            udplistenthread.Start();

        }
    }
}
