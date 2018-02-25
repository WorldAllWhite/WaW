using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WaW
{
    /// <summary>
    /// 原始网络数据处理
    /// </summary>
    public class DataReceive
    {
        delegate void listviewupdateProc(ListViewItem item,int i);
        delegate void labelcountProc();

        private ListView UserList;
        private Label UserCount;

        AsyncUDPServer server;

        public DataReceive(ListView userlist, Label usercount)
        {
            UserList = userlist;
            UserCount = usercount;
        }

        public void StartListenUdp()
        {
            server = new AsyncUDPServer(InfoSet.IpPort);
            server.MessageReceived += new MessageReceivedEventHandle(DataConfig);
            server.Start();
        }
         public void StopListenUdp()
        {
            server.Stop();
        }
        private void DataConfig(object Sender, MessageEventArgs e)
        {
            
            byte[] ver_bytes = new byte[16];
            byte[] serialnum_bytes = new byte[8];
            byte[] user_bytes = new byte[50];
            byte[] hostname_bytes = new byte[50];
            byte[] cmd_bytes = new byte[4];
            string user = string.Empty;
            string hostname = string.Empty;
            string msgstr = string.Empty;
            MsgDistribute distri = null;

            IPEndPoint remote_ipend = e.remoteIP;
            byte[] rec_buff = e.buffer;
            Array.Copy(rec_buff, 0, ver_bytes, 0, 16);
            Array.Copy(rec_buff, 16, serialnum_bytes, 0, 8);
            Array.Copy(rec_buff, 24, user_bytes, 0, 50);
            Array.Copy(rec_buff, 74, hostname_bytes, 0, 50);
            Array.Copy(rec_buff, 124, cmd_bytes, 0, 4);
            wawCMD cmd = (wawCMD)BitConverter.ToUInt32(cmd_bytes, 0);
            List<byte> bytesource = rec_buff.ToList();
            bytesource.RemoveRange(0, 128);
            byte[] message = bytesource.ToArray();

            user = Encoding.UTF8.GetString(user_bytes);
            hostname = Encoding.UTF8.GetString(hostname_bytes);
            msgstr = Encoding.UTF8.GetString(message);

            //是否为广播消息
            if ((cmd == wawCMD.WAW_BC_ABSENCE) ||
                (cmd == wawCMD.WAW_BC_BUSY) ||
                (cmd == wawCMD.WAW_BC_CHECKNEWVER) ||
                (cmd == wawCMD.WAW_BC_GETLIST) ||
                (cmd == wawCMD.WAW_BC_SIGNIN) ||
                (cmd == wawCMD.WAW_BC_SIGNOUT))
            {
                switch (cmd)
                {
                    case wawCMD.WAW_BC_ABSENCE:
                        break;
                    case wawCMD.WAW_BC_BUSY:
                        break;
                    case wawCMD.WAW_BC_CHECKNEWVER:
                        break;
                    case wawCMD.WAW_BC_GETLIST:
                        try
                        {
                            ListViewItem item = new ListViewItem();
                            ListViewItem.ListViewSubItem subitem_user = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_ip = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_hostname = new ListViewItem.ListViewSubItem();
                            subitem_user.Text = Encoding.UTF8.GetString(user_bytes);//此为第二列，listviewitem的名称显示在第一列
                            subitem_ip.Text = remote_ipend.Address.ToString();
                            subitem_hostname.Text = Encoding.UTF8.GetString(hostname_bytes);

                            item.SubItems.Add(subitem_user);
                            item.SubItems.Add(subitem_ip);
                            item.SubItems.Add(subitem_hostname);
                            item.Tag = (object)wawState.SignIn;//存储用户当前状态

                            UserlistUpdate(item, 1);
                            UserCountUpdate();

                            //回复消息
                            MsgSend signinreply = new MsgSend(remote_ipend, wawCMD.WAW_SENDSTATE, frmMain.Userstate.ToString());
                            signinreply.Send();
                            signinreply.SendClose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "消息接收处理WAW_BC_GETLIST出错");
                        }
                        break;
                    case wawCMD.WAW_BC_SIGNIN:
                        try
                        {
                            ListViewItem item = new ListViewItem();
                            ListViewItem.ListViewSubItem subitem_user = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_ip = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_hostname = new ListViewItem.ListViewSubItem();
                            subitem_user.Text = Encoding.UTF8.GetString(user_bytes);//此为第二列，listviewitem的名称显示在第一列
                            subitem_ip.Text = remote_ipend.Address.ToString();
                            subitem_hostname.Text = Encoding.UTF8.GetString(hostname_bytes);

                            item.SubItems.Add(subitem_user);
                            item.SubItems.Add(subitem_ip);
                            item.SubItems.Add(subitem_hostname);
                            item.Tag = (object)wawState.SignIn;//存储用户当前状态

                            UserlistUpdate(item, 1);
                            UserCountUpdate();

                            //回复消息
                            MsgSend signinreply = new MsgSend(remote_ipend, wawCMD.WAW_SENDSTATE, frmMain.Userstate.ToString());
                            signinreply.Send();
                            signinreply.SendClose();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "消息接收处理WAW_BC_SIGNIN出错");
                        }
                        break;
                    case wawCMD.WAW_BC_SIGNOUT:
                        try
                        {
                            ListViewItem item = new ListViewItem();
                            ListViewItem.ListViewSubItem subitem_user = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_ip = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_hostname = new ListViewItem.ListViewSubItem();
                            subitem_user.Text = Encoding.UTF8.GetString(user_bytes);
                            subitem_ip.Text = remote_ipend.Address.ToString();
                            subitem_hostname.Text = Encoding.UTF8.GetString(hostname_bytes);
                            item.SubItems.Add(subitem_user);
                            item.SubItems.Add(subitem_ip);
                            item.SubItems.Add(subitem_hostname);

                            UserlistUpdate(item, 2);
                            UserCountUpdate();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "消息接收处理WAW_BC_SIGNOUT出错");
                        }
                        break;
                    default:
                        break;
                }
            }
            else//非广播消息处理
            {
                switch (cmd)
                {
                    case wawCMD.WAW_SENDSTATE:
                        try
                        {
                            ListViewItem item = new ListViewItem();
                            ListViewItem.ListViewSubItem subitem_user = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_ip = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem subitem_hostname = new ListViewItem.ListViewSubItem();
                            subitem_user.Text = Encoding.UTF8.GetString(user_bytes);
                            subitem_ip.Text = remote_ipend.Address.ToString();
                            subitem_hostname.Text = Encoding.UTF8.GetString(hostname_bytes);
                            item.SubItems.Add(subitem_user);
                            item.SubItems.Add(subitem_ip);
                            item.SubItems.Add(subitem_hostname);
                            switch (Encoding.UTF8.GetString(message))
                            {
                                case "SignIn":
                                    item.Tag = (object)wawState.SignIn;
                                    break;
                                case "Absence":
                                    item.Tag = (object)wawState.Absence;
                                    break;
                                case "Busy":
                                    item.Tag = (object)wawState.Busy;
                                    break;
                                default:
                                    break;
                            }

                            UserlistUpdate(item, 1);
                            UserCountUpdate();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "消息接收处理WAW_SENDSTATE出错");
                        }
                        break;
                    case wawCMD.WAW_GETNEWVER:
                        break;
                    case wawCMD.WAW_GETNEWVER_ACCEPT:
                        break;
                    case wawCMD.WAW_INPUTING:
                        break;
                    case wawCMD.WAW_MSGDISCARD:
                        break;
                    case wawCMD.WAW_MSGRECVD:
                        break;
                    case wawCMD.WAW_MULTICAST:
                        break;
                    case wawCMD.WAW_NOOPERATION:
                        break;
                    case wawCMD.WAW_SENDFILEDATA:
                        break;
                    case wawCMD.WAW_SENDFILEDATA_ACCEPT:
                        break;
                    case wawCMD.WAW_SENDFILEDATA_CANCEL:
                        break;
                    case wawCMD.WAW_SENDFILEDATA_REFUSE:
                        break;
                    case wawCMD.WAW_SENDFILEDATA_STOP:
                        break;
                    case wawCMD.WAW_SENDMSG:
                        distri = new MsgDistribute(remote_ipend.Address.ToString(), user, hostname, msgstr);
                        Thread threadDistribute = new Thread(new ThreadStart(distri.DistributeToChatForm));
                        threadDistribute.IsBackground = true;
                        threadDistribute.Start();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 跨线程更新UI界面，更新列表项,i=1表示添加列表项，i=2表示移除列表项
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i">i=1表示添加列表项，i=2表示移除列表项</param>
        private void UserlistUpdate(ListViewItem item, int j)
        {
            if (UserList.InvokeRequired)
            {
                while (!UserList.IsHandleCreated)
                {
                    if (UserList.Disposing || UserList.IsDisposed)
                        return;
                }
                listviewupdateProc add = new listviewupdateProc(UserlistUpdate);
                UserList.BeginInvoke(add, new object[] { item, j });
            }
            else
            {
                if (j == 1)
                {
                    bool flag = true;
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        if (item.SubItems[1].Text == UserList.Items[i].SubItems[1].Text)
                        {
                            if (item.SubItems[0].Text != UserList.Items[i].SubItems[0].Text || item.SubItems[2].Text != UserList.Items[i].SubItems[2].Text || item.Tag != UserList.Items[i].Tag)
                            {
                                flag = true;
                                UserList.Items[i].Remove();
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                    if (flag)
                    {
                        UserList.Items.Add(item);
                    }
                }
                else if (j == 2)
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        if (item.SubItems[1].Text == UserList.Items[i].SubItems[1].Text)
                        {
                            UserList.Items[i].Remove();
                        }
                    }
                }
                else
                    return;

            }
        }
        /// <summary>
        /// 跨线程更新UI界面，刷新当前用户数
        /// </summary>
        private void UserCountUpdate()
        {
            if (UserCount.InvokeRequired)
            {
                while (!UserCount.IsHandleCreated)
                {
                    if (UserCount.IsDisposed || UserCount.Disposing)
                        return;
                }
                labelcountProc count = new labelcountProc(UserCountUpdate);
                UserCount.BeginInvoke(count);
            }
            else
            {
                UserCount.Text = "当前用户数：" + UserList.Items.Count.ToString();
            }
        }
    }
}