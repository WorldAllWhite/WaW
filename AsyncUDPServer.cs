using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace WaW
{
    public class AsyncUDPServer
    {
        //定义事件
        public event MessageReceivedEventHandle MessageReceived;

        private bool _isRunning;//是否需要关闭套接字
        IPEndPoint localIP;//本地IP和端口
        UdpClient listener;//本地监听套接字

        public AsyncUDPServer(IPEndPoint ipendpoint)
        {
            _isRunning = false;
            localIP = ipendpoint;
        }

        public void Start()
        {
            if(!_isRunning)
            {
                _isRunning = true;
                listener = new UdpClient(localIP);

                UdpState state = new UdpState();
                state.u = listener;
                state.e = localIP;
                
                listener.BeginReceive(new AsyncCallback(ReceiveCallback), state);

            }
        }
        public void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = ((UdpState)ar.AsyncState).u;
            IPEndPoint e = ((UdpState)ar.AsyncState).e;
            try
            {
                MessageEventArgs args = new MessageEventArgs();
                args.buffer = u.EndReceive(ar, ref args.remoteIP);
                if (MessageReceived != null)
                {
                    MessageReceived(this, args);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Udp停止接收数据时出错！");
            }
            finally
            {
                if(_isRunning&&listener!=null)
                {
                    _isRunning = false;
                    Start();
                }
            }
        }
        public void Stop()
        {
            if(_isRunning)
            {
                _isRunning = false;
                listener.Close();
            }
        }
    }
    public class UdpState
    {
        public UdpClient u=null;
        public IPEndPoint e;
    }

    
    public delegate void MessageReceivedEventHandle(object Sender, MessageEventArgs e);
    public class MessageEventArgs : EventArgs
    {
        public byte[] buffer;
        public IPEndPoint remoteIP;

        public MessageEventArgs() : base()
        {
            remoteIP = new IPEndPoint(IPAddress.Any, 0);
        }
    }
}