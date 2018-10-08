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
    public partial class Labinfo : InfoFormBaseClass
    {
        public bool bLoaded = false;    //��������Ƿ��Ѿ����ع�
        private int DataCode;

        #region ϵͳ�¼�
        public Labinfo()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void Labinfo_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI(); //�ڴ�����ʾ������ʱ���������
            }
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


        #endregion

        #region ���ܺ���
        public override void LoadDataFromVarToUI()
        {
            #region Ѫ��
            dateTimePicker1.Value = GlobalData.LabExamInfo.LabExamDateTime;
            txb_BG.Text = GlobalData.LabExamInfo.BG;
            txb_FBG.Text = GlobalData.LabExamInfo.FBG;
            txb_2hPBG.Text = GlobalData.LabExamInfo.TWOHPBG ;
            txb_NumFood.Text = GlobalData.LabExamInfo.FoodCount ;
            txb_OGTTFBG.Text = GlobalData.LabExamInfo.OGTTFBG ;
            txb_OGTTPBG.Text = GlobalData.LabExamInfo.OGTTPBG ;
            #endregion

            #region �Լ��Ѫ��
            //���ǰ
            string BeforeBreakfast=GlobalData.LabExamInfo.BeforeBreakfast;
            if (BeforeBreakfast.IndexOf('-') == -1)
            {
                txb_BeforeBreakfast_L.Text = "";
                txb_BeforeBreakfast_R.Text = "";
            }
            else
            {
                txb_BeforeBreakfast_L.Text = BeforeBreakfast.Substring(0, BeforeBreakfast.IndexOf('-'));
                txb_BeforeBreakfast_R.Text = BeforeBreakfast.Substring(BeforeBreakfast.IndexOf('-') + 1);
            }

            //��ͺ�2h
            string AfterBreakfast=GlobalData.LabExamInfo.AfterBreakfast ;
            if (AfterBreakfast.IndexOf('-') == -1)
            {
                txb_AfterBreakfast_L.Text = "";
                txb_AfterBreakfast_R.Text = "";
            }
            else
            {
                txb_AfterBreakfast_L.Text = AfterBreakfast.Substring(0, AfterBreakfast.IndexOf('-'));
                txb_AfterBreakfast_R.Text = AfterBreakfast.Substring(AfterBreakfast.IndexOf('-') + 1);
            }

            //���ǰ
            string BeforeLunch=GlobalData.LabExamInfo.BeforeLunch ;
            if (BeforeLunch.IndexOf('-') == -1)
            { 
                txb_BeforeLunch_L.Text ="";
                txb_BeforeLunch_R.Text = "";
            }
            else
            {
                txb_BeforeLunch_L.Text = BeforeLunch.Substring(0, BeforeLunch.IndexOf('-'));
                txb_BeforeLunch_R.Text = BeforeLunch.Substring(BeforeLunch.IndexOf('-') + 1);
            }

            //��ͺ�2h
            string AfterLunch=GlobalData.LabExamInfo.AfterLunch ;
            if (AfterLunch.IndexOf('-') == -1)
            {
                txb_AfterLunch_L.Text ="";
                txb_AfterLunch_R.Text = "";
            }
            else
            {
                txb_AfterLunch_L.Text = AfterLunch.Substring(0, AfterLunch.IndexOf('-'));
                txb_AfterLunch_R.Text = AfterLunch.Substring(AfterLunch.IndexOf('-') + 1);
            }

            //���ǰ
            string BeforeSupper=GlobalData.LabExamInfo.BeforeSupper ;
            if (BeforeSupper.IndexOf('-') == -1)
            {
                txb_BeforeSupper_L.Text ="";
                txb_BeforeSupper_R.Text = "";
            }
            else
            {
                txb_BeforeSupper_L.Text = BeforeSupper.Substring(0, BeforeSupper.IndexOf('-'));
                txb_BeforeSupper_R.Text = BeforeSupper.Substring(BeforeSupper.IndexOf('-') + 1);
            }

            //��ͺ�2h
            string AfterSupper=GlobalData.LabExamInfo.AfterSupper ;
            if (AfterSupper.IndexOf('-') == -1)
            {
                txb_AfterSupper_L.Text ="";
                txb_AfterSupper_R.Text ="";
            }
            else
            {
                txb_AfterSupper_L.Text = AfterSupper.Substring(0, AfterSupper.IndexOf('-'));
                txb_AfterSupper_R.Text = AfterSupper.Substring(AfterSupper.IndexOf('-') + 1);
            }

            //��˯ǰ
            string beforeSleep=GlobalData.LabExamInfo.BeforeSleep ;
            if (beforeSleep.IndexOf('-') == -1)
            {
                txb_beforeSleepL.Text ="";
                txb_beforeSleepR.Text = "";
            }
            else
            {
                txb_beforeSleepL.Text = beforeSleep.Substring(0, beforeSleep.IndexOf('-'));
                txb_beforeSleepR.Text = beforeSleep.Substring(beforeSleep.IndexOf('-') + 1);
            }

            //�賿
            string LC=GlobalData.LabExamInfo.LC ;
            if (LC.IndexOf('-') == -1)
            {
                txb_LCL.Text ="";
                txb_LCR.Text = "";
            }
            else
            {
                txb_LCL.Text = LC.Substring(0, LC.IndexOf('-'));
                txb_LCR.Text = LC.Substring(LC.IndexOf('-') + 1);
            }
            #endregion

            #region Ѫ֬
            txb_TC.Text = GlobalData.LabExamInfo.TC ;
            txb_HDLC.Text = GlobalData.LabExamInfo.HDLC;
            txb_TG.Text = GlobalData.LabExamInfo.TG ;
            txb_LDLC.Text = GlobalData.LabExamInfo.LDLC ;

            #endregion
            
            #region ��������
            txb_Cr.Text = GlobalData.LabExamInfo.CR;
            txb_Basajzym.Text = GlobalData.LabExamInfo.AlanineAminotransferase ;
            txb_UN.Text = GlobalData.LabExamInfo.UN ;
            txb_Tdasajzym.Text = GlobalData.LabExamInfo.AspartateAminotransferase ;
            #endregion 

            #region ����
            txb_AlbCr.Text = GlobalData.LabExamInfo.ALBCR ;
            txb_US.Text = GlobalData.LabExamInfo.US ;
            txb_Ndbdl.Text = GlobalData.LabExamInfo.UrinaryProtein ;
            txb_Ntt.Text = GlobalData.LabExamInfo.NTT;
            txb_UPh.Text = GlobalData.LabExamInfo .UPH ;
            txb_UUA.Text = GlobalData.LabExamInfo.UUA ;
            txb_HbA1c.Text = GlobalData.LabExamInfo.HBA1C;
            txb_BCl.Text = GlobalData.LabExamInfo.BCL ;
            txb_BUA.Text = GlobalData.LabExamInfo.BUA ;
            txb_BKa.Text = GlobalData.LabExamInfo.BKA ;
            txb_BNa.Text = GlobalData.LabExamInfo.BNA ;
            txb_BCO2CP.Text = GlobalData.LabExamInfo.BCO2CP;
            txb_BGa.Text = GlobalData.LabExamInfo.BGA  ;
            txb_BP.Text = GlobalData.LabExamInfo.BP ;
            txb_xqzdb.Text = GlobalData.LabExamInfo.SerumTotalProtein;
            txb_xqbdb.Text = GlobalData.LabExamInfo.SerumAlbumin ;
            txb_Kfyds.Text = GlobalData.LabExamInfo.FastingInsulin ;
            txb_kfCt.Text = GlobalData.LabExamInfo.FastingCPeptide;
            txb_Chyds.Text = GlobalData.LabExamInfo.PostprandialInsulin;
            txb_chCt.Text = GlobalData.LabExamInfo.PostprandialCPeptide ;

            if(GlobalData.LabExamInfo.ICA.Equals("����"))
            {
                rbt_ICAM.Checked = true;
            }
            else if (GlobalData.LabExamInfo.ICA.Equals("����"))
            {
                rbt_ICAF.Checked = true;
            }
            else if (GlobalData.LabExamInfo.ICA.Equals("δ���"))
            {
                rbt_ICAN.Checked = true;
            }

            if (GlobalData.LabExamInfo.GDA65.Equals("����"))
            {
                rbt_GDA65M.Checked = true;
            }
            else if (GlobalData.LabExamInfo.GDA65.Equals("����"))
            {
                rbt_GDA65F.Checked = true;
            }
            else if (GlobalData.LabExamInfo.GDA65.Equals("δ���"))
            {
                rbt_GDA65N.Checked = true;
            }
            #endregion

            //���������Ѽ��ر�־
            this.bLoaded = true;
        }

        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return ;
            GlobalData.LabExamInfo.Clear ();

            #region Ѫ��
            GlobalData.LabExamInfo.LabExamDateTime=dateTimePicker1.Value ;
            GlobalData.LabExamInfo.BG=txb_BG.Text ;
            GlobalData.LabExamInfo.FBG= txb_FBG.Text ;
            GlobalData.LabExamInfo.TWOHPBG=txb_2hPBG.Text  ;
            GlobalData.LabExamInfo.FoodCount=txb_NumFood.Text  ;
            GlobalData.LabExamInfo.OGTTFBG=txb_OGTTFBG.Text  ;
            GlobalData.LabExamInfo.OGTTPBG =txb_OGTTPBG.Text ;
            #endregion

            #region �Լ��Ѫ��
            //���ǰ
            GlobalData.LabExamInfo.BeforeBreakfast = txb_BeforeBreakfast_L.Text+'-'+txb_BeforeBreakfast_R.Text  ;

            //��ͺ�2h
            GlobalData.LabExamInfo.AfterBreakfast=txb_AfterBreakfast_L.Text+'-'+txb_AfterBreakfast_R.Text ;

            //���ǰ
            GlobalData.LabExamInfo.BeforeLunch= txb_BeforeLunch_L.Text+'-'+txb_BeforeLunch_R.Text;

            //��ͺ�2h
            GlobalData.LabExamInfo.AfterLunch=txb_AfterLunch_L.Text +'-'+txb_AfterLunch_R.Text ;

            //���ǰ
            GlobalData.LabExamInfo.BeforeSupper=txb_BeforeSupper_L.Text+'-'+txb_BeforeSupper_R.Text ;

            //��ͺ�2h
            GlobalData.LabExamInfo.AfterSupper=txb_AfterSupper_L.Text +'-'+txb_AfterSupper_R.Text;

            //��˯ǰ
            GlobalData.LabExamInfo.BeforeSleep = txb_beforeSleepL.Text + '-' + txb_beforeSleepR.Text;

            //�賿
            GlobalData.LabExamInfo.LC=txb_LCL.Text +'-'+txb_LCR.Text;
            #endregion

            #region Ѫ֬
            GlobalData.LabExamInfo.TC=txb_TC.Text  ;
            GlobalData.LabExamInfo.HDLC =txb_HDLC.Text ;
            GlobalData.LabExamInfo.TG=txb_TG.Text  ;
            GlobalData.LabExamInfo.LDLC=txb_LDLC.Text  ;
            #endregion

            #region ��������
            GlobalData.LabExamInfo.CR= txb_Cr.Text  ;
            GlobalData.LabExamInfo.AlanineAminotransferase =txb_Basajzym.Text ;
            GlobalData.LabExamInfo.UN=txb_UN.Text  ;
            GlobalData.LabExamInfo.AspartateAminotransferase=txb_Tdasajzym.Text  ;
            #endregion

            #region ����
            GlobalData.LabExamInfo.ALBCR=txb_AlbCr.Text  ;
            GlobalData.LabExamInfo.US=txb_US.Text ;
            GlobalData.LabExamInfo.UrinaryProtein=txb_Ndbdl.Text  ;
            GlobalData.LabExamInfo.NTT=txb_Ntt.Text  ;
            GlobalData.LabExamInfo.UPH=txb_UPh.Text  ;
            GlobalData.LabExamInfo.UUA=txb_UUA.Text  ;
            GlobalData.LabExamInfo.HBA1C=txb_HbA1c.Text  ;
            GlobalData.LabExamInfo.BCL=txb_BCl.Text  ;
            GlobalData.LabExamInfo.BUA=txb_BUA.Text  ;
            GlobalData.LabExamInfo.BKA=txb_BKa.Text  ;
            GlobalData.LabExamInfo.BNA=txb_BNa.Text  ;
            GlobalData.LabExamInfo.BCO2CP=txb_BCO2CP.Text  ;
            GlobalData.LabExamInfo.BGA=txb_BGa.Text  ;
            GlobalData.LabExamInfo.BP=txb_BP.Text  ;
            GlobalData.LabExamInfo.SerumTotalProtein=txb_xqzdb.Text  ;
            GlobalData.LabExamInfo.SerumAlbumin=txb_xqbdb.Text  ;
            GlobalData.LabExamInfo.FastingInsulin=txb_Kfyds.Text  ;
            GlobalData.LabExamInfo.FastingCPeptide=txb_kfCt.Text  ;
            GlobalData.LabExamInfo.PostprandialInsulin=txb_Chyds.Text  ;
            GlobalData.LabExamInfo.PostprandialCPeptide =txb_chCt.Text ;

            if (rbt_ICAM.Checked)
            {
               GlobalData.LabExamInfo.ICA="����";
            }
            else if ( rbt_ICAF.Checked)
            {
                GlobalData.LabExamInfo.ICA="����";
            }
            else if (rbt_ICAN.Checked)
            {
                 GlobalData.LabExamInfo.ICA ="δ���";
            }

            if (rbt_GDA65M.Checked)
            {
                 GlobalData.LabExamInfo.GDA65="����";
            }
            else if (rbt_GDA65F.Checked)
            {
                 GlobalData.LabExamInfo.GDA65="����";
            }
            else if ( rbt_GDA65N.Checked)
            {
                GlobalData.LabExamInfo.GDA65="δ���";
            }
            #endregion
            IsModified = false;
        }

         /**************************************************************************
         * ����ˣ�XY��
         * ���ʱ�䣺20081221��
         * ���˵�����ж����ݱ�����ʱ���øú�����
         * ��Ӳ��֣���ӡ������ı���Tag���Եĺ�������
         * ��ע��Tag=1Ϊ�������ı���Tag=0Ϊ�Ǳ������ı��� 
         ***************************************************************************/
        /// <summary>
        /// �����ؼ�Tag���Եĺ���
        /// </summary>
        /// <param name="Controls"></param>
        private bool SearchTag(Control.ControlCollection Controls)
        {
            foreach (Control control in Controls)
            {
                if (control is CDSSCtrlLib.TextBoxNumControl)
                {
                    if (((CDSSCtrlLib.TextBoxNumControl)control).Tag != null)
                    {
                        if (((CDSSCtrlLib.TextBoxNumControl)control).Tag.ToString() == "1")
                        {
                            if (((CDSSCtrlLib.TextBoxNumControl)control).Text == "")
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (control is Panel)
                {
                    bool a = SearchTag(control.Controls);
                    if (a == true)
                        return a;
                }
                else if (control is GroupBox)
                {
                    bool b = SearchTag(control.Controls);
                    if (b == true)
                        return b;
                }
            }

            return false;
        }

        /**************************************************************************
        * ����ˣ�XY��
        * ���ʱ�䣺20081221��
        * ���˵�������ݱ������Ƿ����������жϼ���ʾ���ܣ�
        * ��Ӳ��֣���ӡ��жϱ�ѡ���Ƿ�Ϊ�պ�������
        * ��ע��Ѫѹ������жϱ�׼��Ѫѹ1��2��ֻҪ����ѹ������ѹ����һ����Ϊ��ȷ�� 
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
                LoadDataFromVarToUI();
                bool SearchTagResult = SearchTag(this.Controls);
                if (SearchTagResult == true)
                {
                    return true;
                }
                else if (rbt_ICAM.Checked == false && rbt_ICAF.Checked == false && rbt_ICAN.Checked == false)
                {
                    return true;
                }
                else if (rbt_GDA65M.Checked == false && rbt_GDA65F.Checked == false && rbt_GDA65N.Checked == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// ���ҳ������
        /// </summary>
        public void ClearData()
        {
            ResetControl(this.Controls);

            rbt_ICAM.Checked = false;
            rbt_ICAF.Checked = false;
            rbt_ICAN.Checked = false;
            rbt_GDA65M.Checked = false;
            rbt_GDA65F.Checked = false;
            rbt_GDA65N.Checked = false;
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker1.MaxDate = DateTime.Now;

            panel1.AutoScroll = true;
            panel1.AutoScrollPosition = new Point(0, 0);

           // PatInfo.Cr = 0;
            //��������δ���ر�־
            this.bLoaded = false;
            this.IsModified = false;

        }

        /// <summary>
        /// ��պ���
        /// </summary>
        /// <param name="Controls"></param>
        private void ResetControl(Control.ControlCollection Controls)
        {
            foreach (Control control in Controls)
            {
                if (control is CDSSCtrlLib.TextBoxNumControl)
                {
                    ((CDSSCtrlLib.TextBoxNumControl)control).Text = String.Empty;
                }
                if (control is RadioButton)
                {
                    ((RadioButton)control).Checked = false;
                }
                else if (control is Panel)
                {
                    ResetControl(control.Controls);
                }
                else if (control is GroupBox)
                {
                    ResetControl(control.Controls);
                }
            }

        }

        #endregion         
    }
}