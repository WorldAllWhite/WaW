using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace WaW
{
    /// <summary>
    /// 发送广播消息
    /// </summary>
    public class MsgBoardCast
    {
        private UdpClient udpclient;
        private IPEndPoint boardcast_ipport;

        public MsgBoardCast()
        {
            udpclient = new UdpClient();
            boardcast_ipport = new IPEndPoint(IPAddress.Broadcast, PreDefine.WAW_DEFAULTPORT);
        }

        /// <summary>
        /// 发送广播消息
        /// </summary>
        /// <param name="cmd">消息命令字类型</param>
        public void BoardCast(wawCMD cmd)
        {
            List<byte> bytesource = new List<byte>();
            if((cmd==wawCMD.WAW_BC_ABSENCE)||(cmd==wawCMD.WAW_BC_BUSY)||(cmd==wawCMD.WAW_BC_CHECKNEWVER)||(cmd==wawCMD.WAW_BC_GETLIST)||(cmd==wawCMD.WAW_BC_SIGNIN)||(cmd==wawCMD.WAW_BC_SIGNOUT))
            {
                bytesource.AddRange(PreDefine.DataHeadPreProcess(cmd));
                byte[] sendbuff = bytesource.ToArray();
                udpclient.Send(sendbuff, sendbuff.Length, boardcast_ipport);
            }
            
        }

        public void BoardCastClose()
        {
            udpclient.Client.Shutdown(SocketShutdown.Both);
            udpclient.Client.Close();
        }
    }
}