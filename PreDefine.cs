using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WaW
{
    /// <summary>
    /// 程序预定义的常量
    /// </summary>
    public class PreDefine
    {
        public const int WAW_DEFAULTPORT = 9012;//默认端口号
        public const int WM_COPYDATA = 0x004A;//Windows消息常量值：当一个应用程序传递数据给另一个应用程序时发送此消息

        /// <summary>
        /// 数据报头部封装
        /// </summary>
        /// <returns>封装后的字节数组</returns>
        public static byte[] DataHeadPreProcess(wawCMD cmd)
        {
            byte[] datehead_bytes = new byte[128];

            try
            {
                //封装版本号
                byte[] ver_bytes = new byte[16];
                byte[] temp = new byte[50];//用于处理用户名、主机名的临时字节数组
                byte[] vermajor_bytes = BitConverter.GetBytes(InfoSet.Ver.Major);
                byte[] verminor_bytes = BitConverter.GetBytes(InfoSet.Ver.Minor);
                byte[] verbuild_bytes = BitConverter.GetBytes(InfoSet.Ver.Build);
                byte[] verrevision_bytes = BitConverter.GetBytes(InfoSet.Ver.Revision);
                Array.Copy(vermajor_bytes, 0, ver_bytes, 0, 4);
                Array.Copy(verminor_bytes, 0, ver_bytes, 4, 4);
                Array.Copy(verbuild_bytes, 0, ver_bytes, 8, 4);
                Array.Copy(verrevision_bytes, 0, ver_bytes, 12, 4);

                //封装包编号
                byte[] serialnum_bytes = new byte[8];
                Array.Copy(BitConverter.GetBytes(DateTime.Now.ToFileTimeUtc()), serialnum_bytes, 8);

                //封装用户名
                byte[] user_bytes = Enumerable.Repeat((byte)0x0, 50).ToArray();//将用户名数组全部填'\0'
                Array.Copy(Encoding.UTF8.GetBytes(InfoSet.User), user_bytes, Encoding.UTF8.GetBytes(InfoSet.User).Length<50? Encoding.UTF8.GetBytes(InfoSet.User).Length:50);

                //封装主机名
                byte[] hostname_bytes = Enumerable.Repeat((byte)0x0, 50).ToArray();//将主机名数组全部填'\0'
                Array.Copy(Encoding.UTF8.GetBytes(InfoSet.HostName), hostname_bytes, Encoding.UTF8.GetBytes(InfoSet.HostName).Length<50? Encoding.UTF8.GetBytes(InfoSet.HostName).Length:50);

                //封装命令编号
                byte[] cmd_bytes = new byte[4];
                Array.Copy(BitConverter.GetBytes((uint)cmd), cmd_bytes, 4);

                Array.Copy(ver_bytes, 0, datehead_bytes, 0, 16);
                Array.Copy(serialnum_bytes, 0, datehead_bytes, 16, 8);
                Array.Copy(user_bytes, 0, datehead_bytes, 24, 50);
                Array.Copy(hostname_bytes, 0, datehead_bytes, 74, 50);
                Array.Copy(cmd_bytes, 0, datehead_bytes, 124, 4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"数据报头部封装出错" );
            }

            return datehead_bytes;
        }

        /// <summary>
        /// c#数据与非托管数据之间的转换需要使用MarshalAs属性
        /// 其中string的转换也许是最常用的，特别是在调用本地dll时。指针倒反而简单了，IntPtr直接使用。
        /// 
        /// COPYDATASTRUCT的设计考虑了数据的大小。
        /// 1. 如果是“小数据”，一个long能放下, 放在dwData里就完了。
        /// 2. 如果是“大数据”，就用一个指针lpData和字节长度cbData一起表示。
        /// </summary>
        public struct COPYDATASTRUCT
        {
            /// <summary>
            /// 32位的自定义数据
            /// </summary>
            public IntPtr dwData;
            /// <summary>
            /// 指针数据大小
            /// </summary>
            public int cbData;
            /// <summary>
            /// 数据指针
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;

        }
    }
}