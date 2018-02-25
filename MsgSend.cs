using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace WaW
{
    /// <summary>
    /// 发送非广播消息
    /// </summary>
    public class MsgSend
    {
        private UdpClient send_client = new UdpClient();
        private IPEndPoint remote_ipport;
        byte[] sendbuff;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipendpoint"></param>
        /// <param name="txt">要发送的消息文本</param>
        public MsgSend(IPEndPoint ipendpoint,wawCMD cmd, string txt)
        {
            remote_ipport = ipendpoint;

            List<byte> bytesource = new List<byte>();
            bytesource.AddRange(PreDefine.DataHeadPreProcess(cmd));
            bytesource.AddRange(Encoding.UTF8.GetBytes(txt));
            sendbuff = bytesource.ToArray();

            //也可使用如下方法进行byte数组拼接，一般来讲，对于大byte数组，采用泛型效率要高很多
            //byte[] sendbuf = PreDefine.DataHeadPreProcess(cmd).Concat(Encoding.UTF8.GetBytes(txt)).ToArray();
        }

        public void Send()
        {

            send_client.Send(sendbuff, sendbuff.Length, remote_ipport);
        }

        public void SendClose()
        {
            send_client.Client.Shutdown(SocketShutdown.Both);
            send_client.Client.Close();
        }
    }
}