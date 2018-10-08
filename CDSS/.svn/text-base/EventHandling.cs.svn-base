using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CDSSCtrlLib;
using System.Configuration;

namespace CDSS
{
    internal class EventHandling : IMessageFilter
    {
        #region --Fields/Attributes/Propertys--

        //消息循环监测中状态变化标记
        public bool oldActiveTag = false;
        public bool ActiveTag = false;

        //是否已人为获得焦点标志
        public bool FocusTag = false;

        //已添加事件的控件容器
        public List<object> ctrList = new List<object>();

        //用户重新操作软件时间，用户离开软件时间，用户重新使用软件时间，用户禁用软件时间，控件获得焦点时间
        DateTime s_time, e_time, s_levelTime, e_levelTime, s_date;

        #endregion

        #region --Functions--

        public bool PreFilterMessage(ref Message m)
        {
            if (Control.FromHandle(m.HWnd) == null) return false;
            if (!ctrList.Contains(Control.FromHandle(m.HWnd)))
            {
                if (Control.FromHandle(m.HWnd).Name == "MainForm")
                {
                    UserStateMonitor userState = new UserStateMonitor();
                    userState.UserStateChanged += delegate(object sender, UserStateChangedEventArgs e)
                    {
                        string str = "";
                        if (e.Active)
                        {
                            str = "激活！";
                            s_time = DateTime.Now;
                            Console.WriteLine("程序 " + str + "时间：" + s_time);
                        }
                        else
                        {
                            str = "离开！";
                            s_time = DateTime.MinValue;
                            e_time = DateTime.Now;
                            Console.WriteLine("程序 " + str + "时间：" + e_time);
                        }
                        if (e_time != DateTime.MinValue && s_time != DateTime.MinValue)
                        {
                            Console.WriteLine("程序 " + str + "时间：" + s_time.Subtract(e_time));
                            object[] obj = new object[] { "", "", "", "", "用户离开", e_time + "至" + s_time, s_time.Subtract(e_time) };
                            UserLogOperate.SaveOperationLog(obj);
                        }
                    };
                    userState.TimerTick += delegate(object sender, EventArgs e)
                    {
                        ActiveTag = (Form.ActiveForm == null ? false : true);
                        if (ActiveTag != oldActiveTag)
                        {
                            oldActiveTag = ActiveTag;
                            if (ActiveTag)
                            {
                                userState.StateChangedEnable = true;
                                s_levelTime = DateTime.Now;
                                Console.WriteLine("程序在运行：" + Form.ActiveForm.Name + "时间：" + s_levelTime);
                            }
                            else
                            {
                                userState.StateChangedEnable = false;
                                s_levelTime = DateTime.MinValue;
                                e_levelTime = DateTime.Now;
                                Console.WriteLine("程序未运行：无" + "时间：" + e_levelTime);
                            }
                            if (e_levelTime != DateTime.MinValue && s_levelTime != DateTime.MinValue)
                            {
                                Console.WriteLine("程序在运行，停用时间：" + s_levelTime.Subtract(e_levelTime));
                                object[] obj = new object[] { "", "", "", "", "程序未使用", e_levelTime + "至" + s_levelTime, s_levelTime.Subtract(e_levelTime) };
                                UserLogOperate.SaveOperationLog(obj);
                            }
                        }
                    };
                    userState.Timeout = int.Parse(ConfigurationManager.AppSettings["LevelTime"]) * 1000;
                    userState.Start();
                }

                if (!(Control.FromHandle(m.HWnd) is TextBox) && !(Control.FromHandle(m.HWnd) is MaskedTextBox))
                    Control.FromHandle(m.HWnd).Click += new EventHandler(MyMessager_Click);
                if (!(Control.FromHandle(m.HWnd) is Form))
                {
                    Control.FromHandle(m.HWnd).LostFocus += new EventHandler(MyMessager_LostFocus);
                    Control.FromHandle(m.HWnd).GotFocus += new EventHandler(MyMessager_GotFocus);
                }
                //将已添加事件控件记录，防止对控件重复添加事件，影响性能
                ctrList.Add(Control.FromHandle(m.HWnd));
            }
            return false;
        }

        #endregion

        #region --Events--

        private void MyMessager_GotFocus(object sender, EventArgs e)
        {
            if (((Control)sender).FindForm() != null)
            {
                s_date = DateTime.Now;
                Console.WriteLine("NAME:" + ((Control)sender).Name + " 获得焦点" + DateTime.Now.ToString());
                object[] obj = new object[] { "", ((Control)sender).FindForm().Text, ((Control)sender).Name, ((Control)sender).GetType(), "获得焦点", DateTime.Now.ToString(), TimeSpan.Zero };
                UserLogOperate.SaveOperationLog(obj);
                //FocusTag = true;
            }
        }

        private void MyMessager_LostFocus(object sender, EventArgs e)
        {
            if (((Control)sender).FindForm() != null)
            {
                //if (FocusTag)
                //{
                Console.WriteLine("NAME:" + ((Control)sender).Name + " 失去焦点" + DateTime.Now.ToString() + "操作时间：" + DateTime.Now.Subtract(s_date));
                object[] obj = new object[] { "", ((Control)sender).FindForm().Text, ((Control)sender).Name, ((Control)sender).GetType(), "失去焦点", DateTime.Now.ToString(), s_date == DateTime.MinValue ? TimeSpan.Zero : DateTime.Now.Subtract(s_date) };
                UserLogOperate.SaveOperationLog(obj);
                //FocusTag = false;
                //}
            }
        }

        private void MyMessager_Click(object sender, EventArgs e)
        {
            if (((Control)sender).FindForm() != null)
            {

                if ((sender is TreeView) && ((TreeView)sender).SelectedNode != null)
                {
                    object[] obj = new object[] { "", ((TreeView)sender).FindForm().Text, ((TreeView)sender).SelectedNode.Text, ((Control)sender).GetType(), "鼠标点击", DateTime.Now.ToString(), TimeSpan.Zero };
                    UserLogOperate.SaveOperationLog(obj);
                    Console.WriteLine("NAME:" + ((TreeView)sender).SelectedNode.Text + "所在窗体：" + ((TreeView)sender).FindForm().Text + " 鼠标点击" + DateTime.Now.ToShortDateString());
                }
                else
                {
                    object[] obj = new object[] { "", ((Control)sender).FindForm().Text, ((Control)sender).Name, ((Control)sender).GetType(), "鼠标点击", DateTime.Now.ToString(), TimeSpan.Zero };
                    UserLogOperate.SaveOperationLog(obj);
                    Console.WriteLine("NAME:" + ((Control)sender).Name + "所在窗体：" + ((Control)sender).FindForm().Text + " 鼠标点击" + DateTime.Now.ToShortDateString());
                }
            }
            else
            {
                Console.WriteLine("NAME:" + ((Control)sender).Name + " 鼠标点击" + DateTime.Now.ToShortDateString());
            }
        }

        #endregion

    }


}
