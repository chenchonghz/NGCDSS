using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    public class ComboBoxTreeView : ComboBox
    {
        #region 变量\声明

        private const int WM_LBUTTONDOWN = 0x201, WM_LBUTTONDBLCLK = 0x203;
        private ToolStripControlHost treeViewHost;
        private ToolStripDropDown dropDown;
        private TreeNode selectNode = new TreeNode("");
        private Timer timer;
        private bool isDropDown = false;

        #endregion

        #region 外部属性

        /// <summary>
        /// 下拉框需要绑定的TreeView控件
        /// </summary>
        public TreeView TreeView
        {
            get { return treeViewHost.Control as TreeView; }
        }

        /// <summary>
        /// 当前选中的节点
        /// </summary>
        public TreeNode SelectNode
        {
            get { return selectNode; }
            set
            {
                if (value == null) return;
                TreeNode[] nodes = TreeView.Nodes.Find(value.Name, true);
                if (nodes.Length > 0)
                {
                    TreeView.SelectedNode = nodes[0];
                    selectNode = value;
                    GetSelectNode();
                }
            }
        }

        #endregion

        #region 构造函数

        public ComboBoxTreeView()
        {
            TreeView treeView = new TreeView();
            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            treeView.BorderStyle = BorderStyle.None;
            treeView.KeyDown += new KeyEventHandler(treeView_KeyDown);

            treeViewHost = new ToolStripControlHost(treeView);
            dropDown = new ToolStripDropDown();
            dropDown.Width = this.Width;
            dropDown.DropShadowEnabled = false;
            dropDown.Items.Add(treeViewHost);
            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += delegate(object sender, EventArgs e)
            {
                dropDown.Close();
                isDropDown = false;
                timer.Stop();
            };
        }

        #endregion

        #region 函数

        /// <summary>
        /// 获得已选中的点，并激发SelectIndexChange事件
        /// </summary>
        protected void GetSelectNode()
        {
            if ((selectNode != null) && (selectNode.Nodes.Count == 0))
            {
                this.Items.Clear();
                this.Items.Add(selectNode.Text);
                this.Tag = selectNode;
                this.SelectedIndex = 0;
                OnSelectedIndexChanged(null);
                timer.Start();
            }
        }
        
        public void GetSelectNode(string rootName, string childName)
        {
            TreeNode[] rootNodes = TreeView.Nodes.Find(rootName, false);
            if (rootNodes.Length > 0)
            {
                TreeNode[] chilNodes = rootNodes[0].Nodes.Find(childName, false);
                if (chilNodes.Length > 0)
                {
                    selectNode = chilNodes[0];
                    GetSelectNode();
                }
                else
                {
                    throw new Exception("错误原因：模板与当前软件不兼容\r\n可能情况：\r\n(1)模板被随意修改\r\n(2)软件配置文件发生改变");
                }
            }
            //else
            //{
              //  throw new Exception("错误原因：模板与当前软件不兼容\r\n可能情况：\r\n(1)模板被随意修改\r\n(2)软件配置文件发生改变");
            //}
        }
        
        /// <summary>
        /// 下拉框打开
        /// </summary>
        private void ShowDropDown()
        {
            if (dropDown != null)
            {
                this.Focus();
                if (isDropDown)
                {
                    isDropDown = false;
                    OnDropDownClosed(null);
                }
                else
                {
                    treeViewHost.Size = new Size(DropDownWidth - 2, DropDownHeight);
                    dropDown.Show(this, 0, this.Height);                    
                    OnDropDown(null);
                    isDropDown = true;
                    treeViewHost.Control.Focus();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK || m.Msg == WM_LBUTTONDOWN)
            {
                ShowDropDown();
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                ShowDropDown();
            }
            //else
            //{
            //    switch (e.KeyCode)
            //    {
            //        case Keys.Up:
            //            SelectNode = SelectNode.PrevNode;
            //            break;
            //        case Keys.Down:
            //            SelectNode = SelectNode.NextNode;
            //            break;
            //        default:
            //            break;
            //    }
            //}
            base.OnKeyDown(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dropDown != null)
                {
                    dropDown.Dispose();
                    dropDown = null;
                }
                if (treeViewHost != null)
                {
                    treeViewHost.Dispose();
                    treeViewHost = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 内部事件

        protected void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Space:
                    GetSelectNode();
                    break;
                case Keys.Escape:
                    {
                        dropDown.Close();
                        isDropDown = false;
                    }
                    break;
            }
        }

        protected void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectNode = e.Node;
            GetSelectNode();
        }

        #endregion
    }
}