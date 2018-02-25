using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace WaW
{
    public partial class frmChat : Form
    {
        private string Remoteip = string.Empty;
        private string Remoteuser = string.Empty;
        private string Remotehostname = string.Empty;
        private string Msgdetail = string.Empty;

        private string Localip = string.Empty;
        private string Localuser = string.Empty;
        private string Localhostname = string.Empty;

        private bool isTextBoxNotEmpty = true;//记录输入文本框是否为空

        public frmChat(string remoteip, string remoteuser, string remotehostname, string msgdetail)
        {
            Remoteip = remoteip;
            Remoteuser = remoteuser;
            Remotehostname = remotehostname;
            Msgdetail = msgdetail;

            InitializeComponent();
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            Localip = InfoSet.IpPort.Address.ToString();
            Localuser = InfoSet.User;
            Localhostname = InfoSet.HostName;
            if(Msgdetail!=string.Empty)
            {
                rtfRcv.AppendTextAsRtf(Msgdetail+'\n');
                rtfRcv.ScrollToCaret();
                rtfRcv.Select(rtfRcv.Text.Length, 0);
            }
        }
        protected override void WndProc(ref Message m)
        {
            if(m.Msg==PreDefine.WM_COPYDATA)
            {
                PreDefine.COPYDATASTRUCT mystruct = new PreDefine.COPYDATASTRUCT();
                mystruct = (PreDefine.COPYDATASTRUCT)m.GetLParam(mystruct.GetType());
                Msgdetail = mystruct.lpData;

                rtfRcv.AppendTextAsRtf(Msgdetail + '\n');
                rtfRcv.ScrollToCaret();
                rtfRcv.Select(rtfRcv.Text.Length, 0);
            }
            else
            {
                base.WndProc(ref m);
            }
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void rtfSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车符的处理
            if(e.KeyChar==13)
                SendMessage();
        }

        private void SendMessage()
        {
            if(rtfSend.Text=="")
            {
                rtfSend.Text = "请输入发送消息...";
                rtfSend.BackColor = Color.OldLace;
                isTextBoxNotEmpty = false;
                rtfSend.ReadOnly = true;
            }
            if(isTextBoxNotEmpty)
            {
                try
                {
                    IPEndPoint ipport = new IPEndPoint(IPAddress.Parse(Remoteip), PreDefine.WAW_DEFAULTPORT);
                    MsgSend sendmsg = new MsgSend(ipport, wawCMD.WAW_SENDMSG, rtfSend.Rtf);
                    sendmsg.Send();
                    sendmsg.SendClose();

                    rtfRcv.AppendTextAsRtf(Localuser + "  " + DateTime.Now.ToLongTimeString() + "\r\n", new Font(Font, FontStyle.Regular),RtfColor.Green);
                    rtfRcv.AppendTextAsRtf("    ");
                    rtfRcv.AppendRtf(rtfSend.Rtf);
                    rtfRcv.Select(rtfRcv.Text.Length, 0);
                    rtfRcv.ScrollToCaret();
                    rtfSend.Text = string.Empty;
                }
                catch(Exception ex)
                {
                    rtfRcv.AppendText("发送消息失败！" + "\r\n");
                }
            }
        }

        private void rtfSend_MouseClick(object sender, MouseEventArgs e)
        {
            if(!isTextBoxNotEmpty)
            {
                rtfSend.Text = "";
                rtfSend.BackColor = Color.White;
                isTextBoxNotEmpty = true;
                rtfSend.ReadOnly = false;
            }
        }
    }
}
