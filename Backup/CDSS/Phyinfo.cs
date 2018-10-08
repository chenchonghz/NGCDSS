using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDSSCtrlLib;
using CDSSSystemData;
namespace CDSS
{
    public partial class Phyinfo : InfoFormBaseClass
    {
        public bool bLoaded = false;    //��������Ƿ��Ѿ����ع�

        #region ϵͳ�¼�
        public Phyinfo()
        {
            InitializeComponent();            
        }

        private void rbt_movedisorderN_CheckedChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            if (rbt_movedisorderN.Checked)
                cmb_movedisorderSite.Enabled = false;
            else
                cmb_movedisorderSite.Enabled = true;
        }

        #endregion

        #region �û��¼�
        /// <summary>
        /// ��ҳ�����ݸı�ʱ�������¼�
        /// </summary>
        /// <param name="sender">�¼�������</param>
        /// <param name="e"></param>
        protected void DataModified(object sender, EventArgs e)
        {
            //�������֮ǰ����δ�����,��ֱ�ӷ���
            if (IsModified || (!PatInfo.bNewPatient && !bLoaded))
                return;

            //�����¼�,֪ͨ��������±��水ť״̬
            RaiseDataChangedEvent(this, e);
            IsModified = true;
        }

        private void txb_Height_TextChanged(object sender, EventArgs e)
        {
            if (!IsFloat(txb_Height.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_Height.Focus();
                txb_Height.Text = "";
                return;
            }            
            else
                labeltip.Text = "";
        }

        private void txb_WC_TextChanged(object sender, EventArgs e)
        {
            if (!IsFloat(txb_WC.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_WC.Focus();
                txb_WC.Text = "";
                return;
            }
            else
                labeltip.Text = "";
        }

        private void txb_Weight_TextChanged(object sender, EventArgs e)
        {
            if (!IsFloat(txb_Weight.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_Weight.Focus();
                txb_Weight.Text = "";
                return;
            }
            else
                labeltip.Text = "";
        }

        private void txb_HR_TextChanged(object sender, EventArgs e)
        {
            if (!IsInt(txb_HR.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_HR.Focus();
                txb_HR.Text = "";
                return;
            }
            else
                labeltip.Text = "";
        }

        private void txb_HC_TextChanged(object sender, EventArgs e)
        {
            if (!IsFloat(txb_HC.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_HC.Focus();
                txb_HC.Text = "";
                return;
            }
            else
                labeltip.Text = "";
        }

        private void txb_PhySBP1_TextChanged(object sender, EventArgs e)
        {
            if (!IsInt(txb_PhySBP1.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_PhySBP1.Focus();
                txb_PhySBP1.Text = "";
                return;
            }
            else
                labeltip.Text="";
        }

        private void txb_PhySBP2_TextChanged(object sender, EventArgs e)
        {
            if (!IsInt(txb_PhySBP2.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_PhySBP2.Focus();
                txb_PhySBP2.Text = "";
                return;
            }
            else
                labeltip.Text="";

        }

        private void txb_PhyDBP1_TextChanged(object sender, EventArgs e)
        {
            if (!IsInt(txb_PhyDBP1.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_PhyDBP1.Focus();
                txb_PhyDBP1.Text = "";
                return;
            }
            else
                labeltip.Text = "";
           
        }

        private void txb_PhyDBP2_TextChanged(object sender, EventArgs e)
        {
            if (!IsInt(txb_PhyDBP2.Text))
            {
                labeltip.Text = "������ַ���Ч������������";
                txb_PhyDBP2.Focus();
                txb_PhyDBP2.Text = "";
                return;
            }
            else
                labeltip.Text = "";
        }

        private void Phyinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI(); //�ڴ�����ʾ������ʱ���������
            }
        }

        #endregion

        #region ���ܺ���
        public override void LoadDataFromVarToUI()
        {
            txb_Height.Text = GlobalData.PhysicalInfo.Height;
            txb_WC.Text = GlobalData.PhysicalInfo.WC;
            txb_Weight.Text = GlobalData.PhysicalInfo.Weigh;
            txb_HC.Text = GlobalData.PhysicalInfo.HC;
            txb_HR.Text = GlobalData.PhysicalInfo.HR;
            if (GlobalData.PhysicalInfo.HasDyskinesia)   //���˶��ϰ�
            {
                this.rbt_movedisorderY.Checked = true;
                this.cmb_movedisorderSite.Text = GlobalData.PhysicalInfo.DyskinesiaPart;
            }
            else
            {
                this.rbt_movedisorderY.Checked = false ;
            }
            txb_PhySBP1.Text = GlobalData.PhysicalInfo.SBP1;
            txb_PhySBP2.Text = GlobalData.PhysicalInfo.SBP2;
            txb_PhyDBP1.Text = GlobalData.PhysicalInfo.DBP1;
            txb_PhyDBP2.Text = GlobalData.PhysicalInfo.DBP2;

            //���������Ѽ��ر�־
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return ;

            GlobalData.PhysicalInfo.Clear();

            GlobalData.PhysicalInfo.Height=txb_Height.Text  ;
            GlobalData.PhysicalInfo.WC=txb_WC.Text  ;
            GlobalData.PhysicalInfo.Weigh =txb_Weight.Text ;
            GlobalData.PhysicalInfo.HC=txb_HC.Text  ;
            GlobalData.PhysicalInfo.HR=txb_HR.Text  ;
            if (this.rbt_movedisorderY.Checked )   //���˶��ϰ�
            {
                GlobalData.PhysicalInfo.HasDyskinesia= true;
                GlobalData.PhysicalInfo.DyskinesiaPart=this.cmb_movedisorderSite.Text  ;
            }
            else
            {
                GlobalData.PhysicalInfo.HasDyskinesia = false;
            }
            GlobalData.PhysicalInfo.SBP1=txb_PhySBP1.Text  ;
            GlobalData.PhysicalInfo.SBP2=txb_PhySBP2.Text  ;
            GlobalData.PhysicalInfo.DBP1=txb_PhyDBP1.Text  ;
            GlobalData.PhysicalInfo.DBP2=txb_PhyDBP2.Text  ;

            IsModified = false;
        }

        /**************************************************************************
        * ����ˣ�XY��
        * ���ʱ�䣺20081221��
        * ���˵�������ݱ������Ƿ����������жϼ���ʾ���ܣ�
        * ��Ӳ��֣���ӡ��жϱ�ѡ���Ƿ�Ϊ�պ�������
        ***************************************************************************/
        /// <summary>
        /// �жϱ�ѡ���Ƿ�Ϊ�պ���
        /// </summary>
        /// <returns></returns>
        public bool ForbidNon()
        {
            if (!PatInfo.bMustFill)
            {
                return false;
            }
            else
            {
                //LoadData();
                if (txb_Height.Text != "" && txb_Weight.Text != "" && txb_HR.Text != "" && txb_WC.Text != "")
                {
                    if (txb_PhySBP1.Text != "")
                    {
                        if (txb_PhyDBP1.Text != "" || txb_PhyDBP2.Text != "")
                            return false;
                    }
                    else if (txb_PhySBP2.Text != "")
                    {
                        if (txb_PhyDBP1.Text != "" || txb_PhyDBP2.Text != "")
                            return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
        }

        public bool IsFloat(string result)
        {
            int length = result.Length;
            for (int i = 0; i < length; i++)
            {
                if (!Char.IsDigit(result[0]))
                    return false;
                if (Char.IsDigit(result[i]) || result[i] == '.')
                    continue;
                else
                    return false;
             }
             return true;
        }

        public bool IsInt(string result)
        {
            int length = result.Length;
            for (int i = 0; i < length; i++)
            {
                if (!Char.IsDigit(result[0]))
                    return false;
                if (Char.IsDigit(result[i]))
                    continue;
                else
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// ���ҳ������
        /// </summary>
        public void ClearData()
        {
            foreach (System.Windows.Forms.Control count in this.panel1.Controls)
            {
                if (count is CDSSCtrlLib.TextBoxNumControl)
                    count.Text = String.Empty;
                if (count is ComboBox)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.groupBox1.Controls)
            {
                if (count is CDSSCtrlLib.TextBoxNumControl)
                    count.Text = String.Empty;
            }
            foreach (System.Windows.Forms.Control count in this.groupBox2.Controls)
            {
                if (count is CDSSCtrlLib.TextBoxNumControl)
                    count.Text = String.Empty;
            }           
            rbt_movedisorderN.Checked = true;
            cmb_movedisorderSite.Enabled = false;
            cmb_movedisorderSite.Text = "";


            //��������δ���ر�־
            this.bLoaded = false;
            this.IsModified = false;
        }

        #endregion
    }
}