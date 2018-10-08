using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CDSSCtrlLib;

namespace Utilities.RecordTreeNodeStateFuction
{
    public class RecordTreeNodeState
    {
        /// <summary>
        /// 保存TreeNode状态到指定的XML文件中
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="filePath"></param>
        public void RecordState(TreeView treeView,string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            doc.AppendChild(decl);
            XmlElement root = doc.CreateElement("TreeNodeState");
            doc.AppendChild(root);
            foreach (TreeNode treeNode in treeView.Nodes)
            {
                XmlElement priNode = doc.CreateElement(treeNode.Name);
                if (treeNode.Checked == true)
                {                   
                    root.AppendChild(priNode);
                }               
                foreach (TreeNode child in treeNode.Nodes)
                {
                    if (child.Checked)
                    {                                   
                        XmlElement ele = doc.CreateElement(child.Name);
                        priNode.AppendChild(ele);               
                    }
                }
            }
            doc.Save(filePath);
        }

        /// <summary>
        /// 加载XML保存的TreeNode状态到相应的显示界面（此方法针对本系统中特定的三态CheckTreeView）
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="filePath"></param>
        public void LoadState(TreeView treeView,string filePath)
        {           
            foreach (TreeNode treeNode in treeView .Nodes)
            {
                treeNode.Checked = false;
                foreach (TreeNode childNode in treeNode.Nodes)
                {
                    childNode.Checked = false;
                }
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodeList = root.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                CheckTreeNode treeNode = treeView.Nodes[node.Name] as CheckTreeNode;                
                XmlNodeList childNodeList = node.ChildNodes;
                foreach (XmlNode childNode in childNodeList)
                {
                    CheckTreeNode childTreeNode = treeView.Nodes[node.Name].Nodes[childNode.Name] as CheckTreeNode;
                    childTreeNode.Toggle(CheckTreeNode.CheckBoxState.Unchecked);                                      
                }
            }
        }

        /// <summary>
        /// 加载XML保存的TreeNode状态到相应的显示界面（此方法针对普通的TreeView）
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="filePath"></param>
        public void LoadState2(TreeView treeView, string filePath)
        {
            foreach (TreeNode treeNode in treeView.Nodes)
            {
                treeNode.Checked = false;
                foreach (TreeNode childNode in treeNode.Nodes)
                {
                    childNode.Checked = false;
                }
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodeList = root.ChildNodes;
            foreach (XmlNode node in nodeList)
            {               
                treeView.Nodes[node.Name].Checked = true;
                XmlNodeList childNodeList = node.ChildNodes;
                foreach (XmlNode childNode in childNodeList)
                {                 
                    treeView.Nodes[node.Name].Nodes[childNode.Name].Checked = true;                   
                }
            }
        }
    }
}
