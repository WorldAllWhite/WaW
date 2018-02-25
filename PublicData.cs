using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaW
{
    // Enum for possible RTF colors
    public enum RtfColor
    {
        Black, Maroon, Green, Olive, Navy, Purple, Teal, Gray, Silver,
        Red, Lime, Yellow, Blue, Fuchsia, Aqua, White
    }

    public enum wawState
    {
        SignIn,
        Absence,
        Busy
    }

    public enum wawCMD:uint
    {
        /// <summary>
        /// 无操作
        /// </summary>
        WAW_NOOPERATION = 0x01,
        /// <summary>
        /// 发送用户状态
        /// </summary>
        WAW_SENDSTATE = 0x02,
        /// <summary>
        /// 发送消息
        /// </summary>
        WAW_SENDMSG = 0x03,
        /// <summary>
        /// 通报收到消息
        /// </summary>
        WAW_MSGRECVD = 0x04,
        /// <summary>
        /// 消息丢弃通知
        /// </summary>
        WAW_MSGDISCARD = 0x05,
        /// <summary>
        /// 正在进行输入的通知
        /// </summary>
        WAW_INPUTING = 0x06,
        /// <summary>
        /// 发送多播消息
        /// </summary>
        WAW_MULTICAST = 0x07,
        /// <summary>
        /// 请求获取对方高版本程序集
        /// </summary>
        WAW_GETNEWVER = 0x08,
        /// <summary>
        /// 允许进行版本更新
        /// </summary>
        WAW_GETNEWVER_ACCEPT = 0x09,
        /// <summary>
        /// 请求通过TCP传输文件(夹)
        /// </summary>
        WAW_SENDFILEDATA = 0x0A,
        /// <summary>
        /// 同意接收文件(夹)
        /// </summary>
        WAW_SENDFILEDATA_ACCEPT = 0x0B,
        /// <summary>
        /// 拒绝接收文件(夹)
        /// </summary>
        WAW_SENDFILEDATA_REFUSE = 0x0C,
        /// <summary>
        /// 停止接收文件(夹)
        /// </summary>
        WAW_SENDFILEDATA_STOP = 0x0D,
        /// <summary>
        /// 取消接收文件(夹)
        /// </summary>
        WAW_SENDFILEDATA_CANCEL = 0x0E,
        /// <summary>
        /// 广播上线
        /// </summary>
        WAW_BC_SIGNIN = 0x0F,
        /// <summary>
        /// 广播下线
        /// </summary>
        WAW_BC_SIGNOUT = 0x10,
        /// <summary>
        /// 广播离开
        /// </summary>
        WAW_BC_ABSENCE = 0x11,
        /// <summary>
        /// 广播忙碌
        /// </summary>
        WAW_BC_BUSY = 0x12,
        /// <summary>
        /// 广播重新上线
        /// </summary>
        //WAW_BC_PRESENT = 0x13,
        /// <summary>
        /// 广播刷新获取用户列表
        /// </summary>
        WAW_BC_GETLIST = 0x14,
        /// <summary>
        /// 广播查看是否有高级版本
        /// </summary>
        WAW_BC_CHECKNEWVER = 0x15
}


}