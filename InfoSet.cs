using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Reflection;

namespace WaW
{
    /// <summary>
    /// 用户设置信息类
    /// </summary>
    /// <remarks>存储用户名、主机名、本机地址等信息</remarks>
    public class InfoSet
    {
        private static IPEndPoint _ipport = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);// IP地址和端口
        private static string _user;// 用户名
        private static Version _version = new Version();//程序版本号
        private static string _hostname;// 主机名


        /// <summary>
        /// .cctor
        /// </summary>
        static InfoSet()
        {  
            try
            {
                _user = Environment.UserName;//初始化用户名
                _hostname = Environment.UserDomainName;//初始化主机名
                _version = Assembly.GetExecutingAssembly().GetName().Version;//初始化版本号
                _ipport.Port = PreDefine.WAW_DEFAULTPORT;//初始化端口

                //获取本机IP
                IPHostEntry ipentry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in ipentry.AddressList)
                {
                    if (ip.AddressFamily.ToString() == AddressFamily.InterNetwork.ToString())
                    {
                        _ipport.Address = ip;
                        break;
                    }
                    else
                        _ipport.Address = ipentry.AddressList[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"初始化用户基本信息出错：" );
            }


        }

        /// <summary>
        /// IP地址和端口
        /// </summary>
        public static  IPEndPoint IpPort
        {
            get
            {
                return _ipport;
            }

            set
            {
                _ipport = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public static  string User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }

        /// <summary>
        /// 程序版本号
        /// </summary>
        public static  Version Ver
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;
            }
        }

        /// <summary>
        /// 主机名
        /// </summary>
        public static  string HostName
        {
            get
            {
                return _hostname;
            }

            set
            {
                _hostname = value;
            }
        }
    }
}