using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities.RecordTreeNodeStateFuction;

namespace CDSS
{
    public partial class Statistic : Form
    {
        private HighLevelQueryForm query = new HighLevelQueryForm();
        private StatisticsForm statistic = new StatisticsForm();

        //��ű���TreeNode״̬��XML�ļ�·��
        string filePath = "RecordHighLeverQueryFormTreeNodeState.xml";
        RecordTreeNodeState recordTreeNodeState = new RecordTreeNodeState();

        public Statistic()
        {
            InitializeComponent();

            query.TopLevel = statistic.TopLevel = false;
            query.Dock = statistic.Dock = DockStyle.Fill;
            tabPageConsult.Controls.Add(this.query);
            tabPageStatistic.Controls.Add(this.statistic);
            query.Show();
            statistic.Show();
        }

        /// <summary>
        /// ��¼TreeNode��״̬
        /// </summary>
        public void RecordTreeNodeState()
        {
            recordTreeNodeState.RecordState(this.query.treeDisplayCloumns, filePath);
        }
    }
}