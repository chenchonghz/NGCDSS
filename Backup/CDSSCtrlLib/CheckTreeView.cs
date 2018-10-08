using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    /// <summary>
    /// ���и�ѡ���TreeView�ؼ�
    /// </summary>
    public partial class CheckTreeView : TreeView
    {
        #region Fields

        /// <summary>
        /// ��ѡ�е�Ԫ��
        /// </summary>
        private ArrayList checkedItemIndex = new ArrayList();

        /// <summary>
        /// Flag. If true, use three state checkboxes, otherwise, use the default behavior of the TreeView and associated TreeNodes.
        /// </summary>
        private bool useThreeStateCheckBoxes = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ENSCheckTreeView"/> class.
        /// </summary>
        public CheckTreeView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// ѡ�л��ȡ��ѡ�е�Ԫ��
        /// </summary>
        /// <value>The index of the check item.</value>
        public ArrayList CheckItemIndex
        {
            get
            {
                this.checkedItemIndex.Clear();
                for (int parentCount = 0; parentCount < this.Nodes.Count; parentCount++)
                {
                    foreach (CheckTreeNode child in this.Nodes[parentCount].Nodes)
                    {
                        if (child.State == CheckTreeNode.CheckBoxState.Checked)
                        {
                            this.checkedItemIndex.Add(child.Name);
                        }
                    }
                }

                return this.checkedItemIndex;
            }

            set
            {
                if (value != null)
                {
                    this.checkedItemIndex = value;
                    for (int parentCount = 0; parentCount < this.Nodes.Count; parentCount++)
                    {
                        foreach (CheckTreeNode child in this.Nodes[parentCount].Nodes)
                        {
                            if (this.checkedItemIndex.IndexOf(child.Name) != -1)
                            {
                                child.Toggle(CheckTreeNode.CheckBoxState.Unchecked);
                            }
                            else
                            {
                                child.Toggle(CheckTreeNode.CheckBoxState.Checked);
                            }
                        }
                    }
                }              
                else
                {
                    this.CheckAll();
                }
            }
        }

        /// <summary>
        /// ѡ���Ƿ�ʹ����̬
        /// </summary>
        /// <value>
        /// ʹ��Ϊtrue
        /// </value>
        [Category("Checked TreeView"),
        Description("Flag. If true, use three state checkboxes, otherwise, use the default behavior of the TreeView and associated TreeNodes."),
        DefaultValue(true),
        TypeConverter(typeof(bool)),
        Editor("System.Boolean", typeof(bool))]
        public bool UseThreeStateCheckBoxes
        {
            get { return this.useThreeStateCheckBoxes; }
            set { this.useThreeStateCheckBoxes = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ���Ӹ�ѡ��
        /// </summary>
        /// <param name="parentCode">��һ�㸴ѡ��Ĵ���</param>
        /// <param name="parentName">����ѡ�������</param>
        /// <param name="childID">�Ӹ�ѡ��Ĵ���</param>
        /// <param name="childName">�Ӹ�ѡ�������</param>
        public void AddGroup(string parentCode, string parentName, string childID, string childName)
        {
            if (this.Nodes.Find(parentCode, false).Length == 0)
            {
                CheckTreeNode parentNode = new CheckTreeNode(parentName);
                parentNode.Name = parentCode;
                this.Nodes.Add(parentNode);
            }

            CheckTreeNode childNode = new CheckTreeNode(childName);
            childNode.Name = childID;
            this.Nodes.Find(parentCode, false)[0].Nodes.Add(childNode);
        }

        /// <summary>
        /// ��ȡѡ�е�����
        /// </summary>
        /// <returns>
        /// ѡ���������
        /// </returns>
        public string[] GetSelectedItmes()
        {
            List<string> listItem = new List<string>();
            for (int parentCount = 0; parentCount < this.Nodes.Count; parentCount++)
            {
                foreach (CheckTreeNode child in this.Nodes[parentCount].Nodes)
                {
                    if (child.State == CheckTreeNode.CheckBoxState.Checked)
                    {
                        listItem.Add(child.Text);
                    }
                }
            }

            return listItem.ToArray();
        }

        /// <summary>
        /// ȡ������ѡ��
        /// </summary>
        public void UncheckAll()
        {
            this.ApplyCheckStateToAllNodes(CheckTreeNode.CheckBoxState.Checked);
        }

        /// <summary>
        /// ѡ������
        /// </summary>
        public void CheckAll()
        {
            this.ApplyCheckStateToAllNodes(CheckTreeNode.CheckBoxState.Unchecked);
        }

        /// <summary>
        /// ��������һ���ڵ�
        /// </summary>
        public void ScrollToTop()
        {
            if (this.Nodes.Count != 0)
            {
                this.Nodes[0].EnsureVisible();
            }           
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCheck"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs"></see> that contains the event data.</param>
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (this.UseThreeStateCheckBoxes)
            {
                switch (e.Action)
                {
                    case TreeViewAction.ByKeyboard:
                    case TreeViewAction.ByMouse:
                        {
                            if (e.Node is CheckTreeNode)
                            {
                                // Toggle to the next state.
                                CheckTreeNode tn = e.Node as CheckTreeNode;
                                tn.Toggle();
                            }

                            break;
                        }

                    case TreeViewAction.Collapse:
                    case TreeViewAction.Expand:
                    case TreeViewAction.Unknown:
                    default:
                        {
                            // Do nothing.
                            break;
                        }
                }
            }

            base.OnAfterCheck(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.DrawNode"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs"></see> that contains the event data.</param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            Color backColor, foreColor;
            if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {
                backColor = SystemColors.Highlight;
                foreColor = SystemColors.Window;
            }
            else
            {
                backColor = e.Node.BackColor;
                foreColor = e.Node.ForeColor;
            }

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, e.Node.Bounds);
            }

            System.Windows.Forms.VisualStyles.CheckBoxState status = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            CheckTreeNode node = e.Node as CheckTreeNode;
            switch (node.State)
            {
                case CheckTreeNode.CheckBoxState.Checked:
                    status = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                    break;
                case CheckTreeNode.CheckBoxState.Indeterminate:
                    status = System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal;
                    break;
                case CheckTreeNode.CheckBoxState.Unchecked:
                    status = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                    break;
            }

            Point location = new Point(e.Node.Bounds.Location.X - 14, e.Node.Bounds.Location.Y + 2);
            TextRenderer.DrawText(e.Graphics, e.Node.Text, this.Font, e.Node.Bounds, foreColor, backColor);
            CheckBoxRenderer.DrawCheckBox(e.Graphics, location, status);

            if ((e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Node.Bounds, foreColor, backColor);
            }

            e.DrawDefault = false;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs"></see> that contains the event data.</param>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            this.SelectedNode = e.Node;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Applies the check state to all nodes.
        /// </summary>
        /// <param name="fromState">From state.</param>
        private void ApplyCheckStateToAllNodes(CheckTreeNode.CheckBoxState fromState)
        {
            CheckTreeNode ts = null;
            foreach (TreeNode node in this.Nodes)
            {
                ts = node as CheckTreeNode;
                ts.Toggle(fromState);
            }
        }

        #endregion
    }
}
