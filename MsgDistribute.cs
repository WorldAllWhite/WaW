using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WaW
{
    /// <summary>
    /// 将消息分发给聊天窗口
    /// </summary>
    public class MsgDistribute
    {
        private string _remoteip;
        private string _remoteuser;
        private string _remotehostname;
        private string _msgdetail;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteip">目标地址</param>
        /// <param name="remoteuser">目标用户名</param>
        /// <param name="remotehostname">目标主机名</param>
        /// <param name="msgdetail">消息内容</param>
        public MsgDistribute(string remoteip,string remoteuser,string remotehostname,string msgdetail)
        {
            Remoteip = remoteip;
            Remoteuser = remoteuser;
            Remotehostname = remotehostname;
            Msgdetail = msgdetail;
        }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string Remoteip
        {
            get
            {
                return _remoteip;
            }

            set
            {
                _remoteip = value;
            }
        }

        /// <summary>
        /// 目标用户名
        /// </summary>
        public string Remoteuser
        {
            get
            {
                return _remoteuser;
            }

            set
            {
                _remoteuser = value;
            }
        }

        /// <summary>
        /// 目标主机名
        /// </summary>
        public string Remotehostname
        {
            get
            {
                return _remotehostname;
            }

            set
            {
                _remotehostname = value;
            }
        }

        /// <summary>
        /// 目标发送的消息内容
        /// </summary>
        public string Msgdetail
        {
            get
            {
                return _msgdetail;
            }

            set
            {
                _msgdetail = value;
            }
        }

        /// <summary>
        /// 该函数通过窗口类名或窗口标题名查找窗口并返回该窗口的句柄，函数不会搜索子窗口。
        /// 在搜索的时候不一定两者都知道，但至少要知道其中的一个。
        /// 有的窗口的标题是比较容易得到的，如"计算器"，所以搜索时应使用标题进行搜索。
        /// 但有的软件的标题不是固定的，如"记事本"，如果打开的文件不同，窗口标题也不同，这时使用窗口类搜索就比较方便。
        /// </summary>
        /// <param name="lpClassName">窗口的类名，如果为NULL，则查找所有和lpWindowName参数匹配的窗口。</param>
        /// <param name="lpWindowName">窗口的标题，如果为NULL,所有窗口名字都是匹配的。</param>
        /// <returns>查找到的窗口句柄</returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]//EntryPoint指定User32.dll中的函数FindWindow，并将下面的函数Find_Windows指定为函数FindWindow的别名，这样可以避免函数名重复
        private static extern IntPtr Find_Windows(string lpClassName, string lpWindowName);
        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口。
        /// </summary>
        /// <param name="hWnd">接收消息的窗口句柄</param>
        /// <param name="Msg">Windows消息常量值</param>
        /// <param name="wParam">可选参数（与具体的消息类型有关），通常表示与消息有关的信息，如相关的窗口或控件句柄、相关的鼠标参数等</param>
        /// <param name="lParam">可选参数（与具体的消息类型有关），通常表示是一个指向内存中数据的指针，如需要传递的数据等。</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int Send_Messages(IntPtr hWnd, int Msg, int wParam, ref PreDefine.COPYDATASTRUCT lParam);

        /// <summary>
        /// 使某个指定的窗口产生一次闪烁的效果，它同样不会改变窗口的活动状态. 如果要使窗口闪烁指定的次数，则需要使用FlashWindowEx函数
        /// </summary>
        /// <param name="hWnd">要闪烁的窗口的句柄</param>
        /// <param name="bInvert">为true时，程序窗口标题栏从活动切换到非活动状态、或反向切换; 为false时，窗口标题栏还原为最初的状态</param>
        /// <returns>表示调用FlashWindow函数之前窗口的活动状态，若指定窗口在调用函数之前是激活的，那么返回非零值，否则返回零值</returns>
        [DllImport("User32.dll", EntryPoint = "FlashWindow")]
        private static extern bool Flash_Windows(IntPtr hWnd, bool bInvert);

        /// <summary>
        /// 将消息分发给对应的聊天窗口
        /// </summary>
        public void DistributeToChatForm()
        {
            try
            {
                //如果存在，将消息传到这个窗口，如果不存在，创建一个新窗口

                //找到当前已经打开的聊天窗口的句柄
                string windowsName = "与" + Remoteuser + "聊天中";
                IntPtr handle = Find_Windows(null, windowsName);
                if (handle != IntPtr.Zero)
                {
                    //进程间通信时，必须对要发送的数据进行编码封装，直接发string类型，收到会出错
                    byte[] msg_bytes = Encoding.Default.GetBytes(Msgdetail);
                    int msg_len = msg_bytes.Length;
                    PreDefine.COPYDATASTRUCT msg_struct;
                    msg_struct.dwData = (IntPtr)0;//该程序中此变量不被使用
                    msg_struct.lpData = Msgdetail;
                    msg_struct.cbData = Encoding.Default.GetBytes(Msgdetail).Length + 1;

                    Send_Messages(handle, PreDefine.WM_COPYDATA, 0, ref msg_struct);
                    Flash_Windows(handle, true);
                }
                else
                {
                    frmChat chatForm = new frmChat(Remoteip, Remoteuser, Remotehostname, Msgdetail);
                    chatForm.Text = windowsName;
                    chatForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;//指定窗口状态为最小化状态
                    chatForm.ShowDialog();//显示窗口，由于窗口状态为最小化，窗口只在任务栏上显示
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "消息分发出错");
            }
        }
    }
}