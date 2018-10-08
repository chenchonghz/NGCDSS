using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace CDSSCtrlLib
{
    /// <summary>
    /// 支持三态的TreeNode
    /// </summary>
    public class CheckTreeNode : TreeNode
    {
        #region Fields

        /// <summary>
        /// 本Node的选择状态
        /// </summary>
        private CheckBoxState checkBoxState = CheckBoxState.Unchecked;

        #endregion

        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CheckTreeNode()
            : base()
        { 
        }

        /// <summary>
        /// 初始化显示文本的构造函数
        /// </summary>
        /// <param name="text">显示的文本</param>
        public CheckTreeNode(string text)
            : base(text)
        { 
        }

        #endregion

        #region Enums

        /// <summary>
        /// 三态选择状态
        /// </summary>
        public enum CheckBoxState
        {
            /// <summary>
            /// 选中
            /// </summary>
            Unchecked = 1,

            /// <summary>
            /// 未选中
            /// </summary>
            Checked = 2,

            /// <summary>
            /// 部分选中
            /// </summary>
            Indeterminate = CheckBoxState.Unchecked | CheckBoxState.Checked
        }

        #endregion

        #region Properties

        /// <summary>
        /// 本Node的三态状态
        /// </summary>
        /// <value>Node状态</value>
        [Category("Checked TreeView"),
        Description("当前Node的状态 Unchecked, Checked, or Indeterminate"),
        DefaultValue(CheckBoxState.Unchecked),
        TypeConverter(typeof(CheckBoxState)),
        Editor("CDSSCtrl.CheckTreeNode.CheckBoxState", typeof(CheckBoxState))]
        public CheckBoxState State
        {
            get 
            { 
                return this.checkBoxState; 
            }

            set
            {
                if (this.checkBoxState != value)
                {
                    this.checkBoxState = value;

                    // Ensure if checkboxes are used to make the checkbox checked or unchecked.
                    // When go to a fully drawn control, this will be managed in the drawing code.
                    // Setting the Checked property in code will cause the OnAfterCheck to be called
                    // and the action will be 'Unknown'; do not handle that case.
                    if ((this.TreeView != null) && this.TreeView.CheckBoxes)
                    {
                        this.Checked = (this.checkBoxState == CheckBoxState.Checked);
                    }                        
                }
            }
        }

        /// <summary>
        /// 根据兄弟节点的状态获取父节点的状态
        /// </summary>
        /// <value>The state of the siblings.</value>
        private CheckBoxState SiblingsState
        {
            get
            {
                // If parent is null, cannot have any siblings or if the parent
                // has only one child (i.e. this node) then return the state of this 
                // instance as the state.
                if ((this.Parent == null) || (this.Parent.Nodes.Count == 1))
                {
                    return this.State;
                }
                    
                // The parent has more than one child.  Walk through parent's child
                // nodes to determine the state of all this node's siblings,
                // including this node.
                CheckBoxState state = 0;
                foreach (TreeNode node in this.Parent.Nodes)
                {
                    CheckTreeNode child = node as CheckTreeNode;
                    if (child != null)
                    {
                        state |= child.State;
                    }
                        
                    // If the state is now indeterminate then know there
                    // is a combination of checked and unchecked nodes
                    // and no longer need to continue evaluating the rest
                    // of the sibling nodes.
                    if (state == CheckBoxState.Indeterminate)
                    {
                        break;
                    }                       
                }

                return (state == 0) ? CheckBoxState.Unchecked : state;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 改变当前节点的状态
        /// </summary>
        /// <param name="fromState">当前节点的起始状态</param>
        public void Toggle(CheckBoxState fromState)
        {
            switch (fromState)
            {
                case CheckBoxState.Unchecked:
                    this.State = CheckBoxState.Checked;
                    break;
                case CheckBoxState.Checked:
                case CheckBoxState.Indeterminate:
                default:
                    this.State = CheckBoxState.Unchecked;
                    break;
            }

            this.UpdateStateOfRelatedNodes();
        }

        /// <summary>
        /// 改变当前节点的状态
        /// </summary>
        public new void Toggle()
        {
            this.Toggle(this.State);
        }

        /// <summary>
        /// 更新与当前节点兄弟节点的状态
        /// </summary>
        public void UpdateStateOfRelatedNodes()
        {
            CheckTreeView tv = this.TreeView as CheckTreeView;
            if ((tv != null) && tv.CheckBoxes && tv.UseThreeStateCheckBoxes)
            {
                tv.BeginUpdate();

                // If want to cascade checkbox state changes to child nodes of this node and
                // if the current state is not intermediate, update the state of child nodes.
                if (this.State != CheckBoxState.Indeterminate)
                {
                    this.UpdateChildNodeState();
                }
                    
                this.UpdateParentNodeState(true);

                tv.EndUpdate();
            }
        }

        /// <summary>
        /// 更新当前节点子节点的状态
        /// </summary>
        private void UpdateChildNodeState()
        {
            CheckTreeNode child;
            foreach (TreeNode node in this.Nodes)
            {
                // It is possible node is not a ENSCheckTreeNode, so check first.
                if (node is CheckTreeNode)
                {
                    child = node as CheckTreeNode;
                    child.State = this.State;
                    child.Checked = (this.State != CheckBoxState.Unchecked);
                    child.UpdateChildNodeState();
                }
            }
        }

        /// <summary>
        /// 更新当前节点父节点的状态
        /// </summary>
        /// <param name="isStartingPoint">是否从该节点开始</param>
        private void UpdateParentNodeState(bool isStartingPoint)
        {
            // If isStartingPoint is false, then know this is not the initial call
            // to the recursive method as we want to force on the first time
            // this is called to set the instance's parent node state based on
            // the state of all the siblings of this node, including the state
            // of this node.  So, if not the startpoint (!isStartingPoint) and
            // the state of this instance is indeterminate (CheckBoxState.Indeterminate)
            // then know to set all subsequent parents to the indeterminate
            // state.  However, if not in an indeterminate state, then still need
            // to evaluate the state of all the siblings of this node, including the state
            // of this node before setting the state of the parent of this instance.
            CheckTreeNode parent = this.Parent as CheckTreeNode;
            if (parent != null)
            {
                CheckBoxState state = CheckBoxState.Unchecked;

                // Determine the new state
                if (!isStartingPoint && (this.State == CheckBoxState.Indeterminate))
                {
                    state = CheckBoxState.Indeterminate;
                }
                else
                {
                    state = this.SiblingsState;
                }
                    
                // Update parent state if not the same.
                if (parent.State != state)
                {
                    parent.State = state;
                    parent.Checked = (state != CheckBoxState.Unchecked);
                    parent.UpdateParentNodeState(false);
                }
            }
        }

        #endregion
    }
}
