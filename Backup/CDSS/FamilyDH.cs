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
    public partial class FamilyDH : InfoFormBaseClass
    {
        public bool bLoaded = false;    //��������Ƿ��Ѿ����ع�
        private StringBuilder sbFather = new StringBuilder(100);
        private StringBuilder sbMother = new StringBuilder(100);
        private StringBuilder sbSisBrother = new StringBuilder(100);
        private StringBuilder sbChildren = new StringBuilder(100);
        private StringBuilder sbOther = new StringBuilder(100);

        public FamilyDH()
        {
            InitializeComponent();
        }

#region ϵͳ�¼�
        /// <summary>
        /// ҳ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilyDH_Load(object sender, EventArgs e)
        {
            SetSisBrotherAndchildrenCount();
        }

        /// <summary>
        /// �����Ըı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilyDH_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                SetSisBrotherAndchildrenCount();

                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI(); //�ڴ�����ʾ������ʱ���������
                //��������벡�ˣ���ҳ����ʾ����ʱ�����ֵܽ�����Ů��
                //if (PatInfo.bNewPatient)
                //    SetSisBrotherAndchildrenCount();
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

        /// <summary>
        /// �����ؼ������Ѹ����¼�
        /// </summary>
        private void FDH_DataChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            ShowFDH();            
        }

#endregion


#region ���ܺ���

        /// <summary>
        /// �����ֵܽ��ú���Ů��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSisBrotherAndchildrenCount()
        {
            lblSisBrotherCount.Text = GlobalData.PatBasicInfo.PatSiblingsCount.ToString();
            lblChildrenCount.Text = GlobalData.PatBasicInfo.PatChildCount.ToString();

            CDSSCtrlLib.FamilyDiseaseHistory fdh;
            foreach (Control ctrl in this.tlpFDH.Controls)
            {
                fdh = null;
                fdh = ctrl as CDSSCtrlLib.FamilyDiseaseHistory;
                if (fdh == null) continue;

                fdh.SisBrotherCount =(int)GlobalData.PatBasicInfo.PatSiblingsCount;
                fdh.ChildrenCount =(int)GlobalData.PatBasicInfo.PatChildCount;
            }
        }


        /// <summary>
        /// ��ս�����ʾ���ָ���ʼ״̬
        /// </summary>
        public  void ClearData()
        {
            CDSSCtrlLib.FamilyDiseaseHistory fdh;
            foreach (Control ctrl in this.tlpFDH.Controls)
            {
                fdh = null;
                fdh = ctrl as CDSSCtrlLib.FamilyDiseaseHistory;
                if (fdh == null) continue;
                fdh.ClearData();
            }

            this.txtFather.Text = string.Empty;
            this.txtMother.Text = string.Empty;
            this.txtSisBrother.Text = string.Empty;
            this.txtChildren.Text = string.Empty;
            this.txtOther.Text = string.Empty;

            //��������δ���ر�־
            this.bLoaded = false;
            this.IsModified = false;
        }

        /// <summary>
        /// ����Ա������ʾ���弲��ʷ
        /// </summary>
        public void ShowFDH()
        {
            sbFather.Length = 0;
            sbMother.Length = 0;
            sbSisBrother.Length = 0;
            sbChildren.Length = 0;
            sbOther.Length = 0;
            CDSSCtrlLib.FamilyDiseaseHistory fdh;
            foreach (Control ctrl in this.tlpFDH.Controls)
            {
                fdh = null;
                fdh = ctrl as CDSSCtrlLib.FamilyDiseaseHistory;
                if (fdh == null) continue;

                if (fdh.FatherHas)
                {
                    sbFather.Append(fdh.DiseaseName);
                    sbFather.Append("��");
                }
                if (fdh.MatherHas)
                {
                    sbMother.Append(fdh.DiseaseName);
                    sbMother.Append("��");
                }
                if (fdh.SisBrotherHas)
                {
                    sbSisBrother.Append(fdh.DiseaseName);
                    if (fdh.SisBrotherHasCount > 0)
                        sbSisBrother.AppendFormat("({0}��)��", fdh.SisBrotherHasCount.ToString());
                    else
                        sbSisBrother.Append("��");
                }
                if (fdh.ChildrenHas)
                {
                    sbChildren.Append(fdh.DiseaseName);
                    if (fdh.ChildrenHasCount > 0)
                        sbChildren.AppendFormat("({0}��)��", fdh.ChildrenHasCount.ToString());
                    else
                        sbChildren.Append("��");
                }
                if (fdh.OtherHas)
                {
                    sbOther.Append(fdh.DiseaseName);
                    if (fdh.OtherHasDetail.Length > 0)
                        sbOther.AppendFormat("({0})��", fdh.OtherHasDetail);
                    else
                        sbOther.Append("��");
                }
            }

            this.txtFather.Text = sbFather.ToString();
            this.txtMother.Text = sbMother.ToString();
            this.txtSisBrother.Text = sbSisBrother.ToString();
            this.txtChildren.Text = sbChildren.ToString();
            this.txtOther.Text = sbOther.ToString();
        }


        /// <summary>
        /// ��������
        /// </summary>
        public override void LoadDataFromVarToUI()
        {
            //SetSisBrotherAndchildrenCount();//���ؼ������ֵܽ�����Ů��
            txtFather.Text = GlobalData.FamilyDiseaseHistoryInfo.FatherHistory;
            txtMother.Text =GlobalData.FamilyDiseaseHistoryInfo.MotherHistory ;
            txtSisBrother.Text =GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory ;
            txtChildren.Text =GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory ;
            txtOther.Text  = GlobalData.FamilyDiseaseHistoryInfo.OtherHistory;

            //TODO:�������û��ؼ���������
            #region 
            string temp="";

            #region ����
            temp = txtFather.Text;
            if (temp.Contains("����"))
                fdhTangNiaoBing.FatherHas = true;
            if (temp.Contains("��Ѫѹ"))
                fdhGaoXueYa.FatherHas = true;
            if (temp.Contains("��֬Ѫ֢"))
                fdhGaoZhiXueZheng.FatherHas = true;
            if (temp.Contains("������Ѫ֢"))
                fdhGaoNiaoSuanXueZheng.FatherHas = true;
            if (temp.Contains("���Ĳ�"))
                fdhGuanXinBing.FatherHas = true;
            if (temp.Contains("�Ĺ�"))
                fdhXinGeng.FatherHas = true;
            if (temp.Contains("�Գ�Ѫ"))
                fdhNaoChuXue.FatherHas = true;
            if (temp.Contains("��Ѫ˨"))
                fdhNaoXueShuan.FatherHas = true;
            if (temp.Contains("������"))
                fdhDanNangyan.FatherHas = true;
            if (temp.Contains("��ʯ֢"))
                fdhDanShiZheng.FatherHas = true;
            if (temp.Contains("���ಡ"))
                fdhShenZangBing.FatherHas = true;
            if (temp.Contains("����"))
                fdhZhongLiu.FatherHas = true;
            if (temp.Contains("����"))
                fdhFeiPang.FatherHas = true;
            if (temp.Contains("����"))
                fdhQiTa.FatherHas = true;
            #endregion

            #region ĸ��
            temp = txtMother.Text;
            if (temp.Contains("����"))
                fdhTangNiaoBing.MatherHas = true;
            if (temp.Contains("��Ѫѹ"))
                fdhGaoXueYa.MatherHas = true;
            if (temp.Contains("��֬Ѫ֢"))
                fdhGaoZhiXueZheng.MatherHas = true;
            if (temp.Contains("������Ѫ֢"))
                fdhGaoNiaoSuanXueZheng.MatherHas = true;
            if (temp.Contains("���Ĳ�"))
                fdhGuanXinBing.MatherHas = true;
            if (temp.Contains("�Ĺ�"))
                fdhXinGeng.MatherHas = true;
            if (temp.Contains("�Գ�Ѫ"))
                fdhNaoChuXue.MatherHas = true;
            if (temp.Contains("��Ѫ˨"))
                fdhNaoXueShuan.MatherHas = true;
            if (temp.Contains("������"))
                fdhDanNangyan.MatherHas = true;
            if (temp.Contains("��ʯ֢"))
                fdhDanShiZheng.MatherHas = true;
            if (temp.Contains("���ಡ"))
                fdhShenZangBing.MatherHas = true;
            if (temp.Contains("����"))
                fdhZhongLiu.MatherHas = true;
            if (temp.Contains("����"))
                fdhFeiPang.MatherHas = true;
            if (temp.Contains("����"))
                fdhQiTa.MatherHas = true;
            #endregion

            #region �ֵܽ���
            temp = txtSisBrother.Text;
            if (temp.Contains("����"))
            {
                fdhTangNiaoBing.SisBrotherHas = true;
                if (temp.IndexOf("����(") != -1)   
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhTangNiaoBing.SisBrotherHasCount = int.Parse(temp2[0].ToString()); 
                }
                else
                    fdhTangNiaoBing.SisBrotherHasCount = 0;
            }
            if (temp.Contains("��Ѫѹ"))
            {
                fdhGaoXueYa.SisBrotherHas = true;
                if (temp.IndexOf("��Ѫѹ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��Ѫѹ("), temp.Length - temp.IndexOf("��Ѫѹ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGaoXueYa.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoXueYa.SisBrotherHasCount = 0;
            }
            if (temp.Contains("��֬Ѫ֢"))
            {
                fdhGaoZhiXueZheng.SisBrotherHas = true;
                if (temp.IndexOf("��֬Ѫ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��֬Ѫ֢("), temp.Length - temp.IndexOf("��֬Ѫ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGaoZhiXueZheng.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoZhiXueZheng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("������Ѫ֢"))
            {
                fdhGaoNiaoSuanXueZheng.SisBrotherHas = true;
                if (temp.IndexOf("������Ѫ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("������Ѫ֢("), temp.Length - temp.IndexOf("������Ѫ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGaoNiaoSuanXueZheng.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoNiaoSuanXueZheng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("���Ĳ�"))
            {
                fdhGuanXinBing.SisBrotherHas = true;
                if (temp.IndexOf("���Ĳ�(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("���Ĳ�("), temp.Length - temp.IndexOf("���Ĳ�("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGuanXinBing.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGuanXinBing.SisBrotherHasCount = 0;
            }
            if (temp.Contains("�Ĺ�"))
            {
                fdhXinGeng.SisBrotherHas = true;
                if (temp.IndexOf("�Ĺ�(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("�Ĺ�("), temp.Length - temp.IndexOf("�Ĺ�("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhXinGeng.SisBrotherHasCount = int.Parse(temp2[0].ToString()); 
                }
                else
                    fdhXinGeng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("�Գ�Ѫ"))
            {
                fdhNaoChuXue.SisBrotherHas = true;
                if (temp.IndexOf("�Գ�Ѫ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("�Գ�Ѫ("), temp.Length - temp.IndexOf("�Գ�Ѫ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhNaoChuXue.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoChuXue.SisBrotherHasCount = 0;
            }
            if (temp.Contains("��Ѫ˨"))
            {
                fdhNaoXueShuan.SisBrotherHas = true;
                if (temp.IndexOf("��Ѫ˨(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��Ѫ˨("), temp.Length - temp.IndexOf("��Ѫ˨("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhNaoXueShuan.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoXueShuan.SisBrotherHasCount = 0;
            }
            if (temp.Contains("������"))
            {
                fdhDanNangyan.SisBrotherHas = true;
                if (temp.IndexOf("������(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("������("), temp.Length - temp.IndexOf("������("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhDanNangyan.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanNangyan.SisBrotherHasCount = 0;
            }
            if (temp.Contains("��ʯ֢"))
            {
                fdhDanShiZheng.SisBrotherHas = true;
                if (temp.IndexOf("��ʯ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��ʯ֢("), temp.Length - temp.IndexOf("��ʯ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhDanShiZheng.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanShiZheng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("���ಡ"))
            {
                fdhShenZangBing.SisBrotherHas = true;
                if (temp.IndexOf("���ಡ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("���ಡ("), temp.Length - temp.IndexOf("���ಡ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhShenZangBing.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhShenZangBing.SisBrotherHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhZhongLiu.SisBrotherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhZhongLiu.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhZhongLiu.SisBrotherHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhFeiPang.SisBrotherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhFeiPang.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhFeiPang.SisBrotherHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhQiTa.SisBrotherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhQiTa.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhQiTa.SisBrotherHasCount = 0;
            }
            #endregion

            #region ��Ů
            temp = txtSisBrother.Text;
            if (temp.Contains("����"))
            {
                fdhTangNiaoBing.ChildrenHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhTangNiaoBing.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhTangNiaoBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("��Ѫѹ"))
            {
                fdhGaoXueYa.ChildrenHas = true;
                if (temp.IndexOf("��Ѫѹ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��Ѫѹ("), temp.Length - temp.IndexOf("��Ѫѹ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGaoXueYa.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoXueYa.ChildrenHasCount = 0;
            }
            if (temp.Contains("��֬Ѫ֢"))
            {
                fdhGaoZhiXueZheng.ChildrenHas = true;
                if (temp.IndexOf("��֬Ѫ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��֬Ѫ֢("), temp.Length - temp.IndexOf("��֬Ѫ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGaoZhiXueZheng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoZhiXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("������Ѫ֢"))
            {
                fdhGaoNiaoSuanXueZheng.ChildrenHas = true;
                if (temp.IndexOf("������Ѫ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("������Ѫ֢("), temp.Length - temp.IndexOf("������Ѫ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGaoNiaoSuanXueZheng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoNiaoSuanXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("���Ĳ�"))
            {
                fdhGuanXinBing.ChildrenHas = true;
                if (temp.IndexOf("���Ĳ�(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("���Ĳ�("), temp.Length - temp.IndexOf("���Ĳ�("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhGuanXinBing.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGuanXinBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("�Ĺ�"))
            {
                fdhXinGeng.ChildrenHas = true;
                if (temp.IndexOf("�Ĺ�(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("�Ĺ�("), temp.Length - temp.IndexOf("�Ĺ�("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhXinGeng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhXinGeng.ChildrenHasCount = 0;
            }
            if (temp.Contains("�Գ�Ѫ"))
            {
                fdhNaoChuXue.ChildrenHas = true;
                if (temp.IndexOf("�Գ�Ѫ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("�Գ�Ѫ("), temp.Length - temp.IndexOf("�Գ�Ѫ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhNaoChuXue.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoChuXue.ChildrenHasCount = 0;
            }
            if (temp.Contains("��Ѫ˨"))
            {
                fdhNaoXueShuan.ChildrenHas = true;
                if (temp.IndexOf("��Ѫ˨(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��Ѫ˨("), temp.Length - temp.IndexOf("��Ѫ˨("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhNaoXueShuan.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoXueShuan.ChildrenHasCount = 0;
            }
            if (temp.Contains("������"))
            {
                fdhDanNangyan.ChildrenHas = true;
                if (temp.IndexOf("������(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("������("), temp.Length - temp.IndexOf("������("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhDanNangyan.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanNangyan.ChildrenHasCount = 0;
            }
            if (temp.Contains("��ʯ֢"))
            {
                fdhDanShiZheng.ChildrenHas = true;
                if (temp.IndexOf("��ʯ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��ʯ֢("), temp.Length - temp.IndexOf("��ʯ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhDanShiZheng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanShiZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("���ಡ"))
            {
                fdhShenZangBing.ChildrenHas = true;
                if (temp.IndexOf("���ಡ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("���ಡ("), temp.Length - temp.IndexOf("���ಡ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhShenZangBing.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhShenZangBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhZhongLiu.ChildrenHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhZhongLiu.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhZhongLiu.ChildrenHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhFeiPang.ChildrenHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhFeiPang.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhFeiPang.ChildrenHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhQiTa.ChildrenHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('��');
                    fdhQiTa.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhQiTa.ChildrenHasCount = 0;
            }
            #endregion

            #region ����
            temp = txtOther.Text;
            if (temp.Contains("����"))
            {
                fdhTangNiaoBing.OtherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhTangNiaoBing.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhTangNiaoBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("��Ѫѹ"))
            {
                fdhGaoXueYa.OtherHas = true;
                if (temp.IndexOf("��Ѫѹ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��Ѫѹ("), temp.Length - temp.IndexOf("��Ѫѹ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGaoXueYa.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGaoXueYa.ChildrenHasCount = 0;
            }
            if (temp.Contains("��֬Ѫ֢"))
            {
                fdhGaoZhiXueZheng.OtherHas = true;
                if (temp.IndexOf("��֬Ѫ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��֬Ѫ֢("), temp.Length - temp.IndexOf("��֬Ѫ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGaoZhiXueZheng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGaoZhiXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("������Ѫ֢"))
            {
                fdhGaoNiaoSuanXueZheng.OtherHas = true;
                if (temp.IndexOf("������Ѫ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("������Ѫ֢("), temp.Length - temp.IndexOf("������Ѫ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGaoNiaoSuanXueZheng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGaoNiaoSuanXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("���Ĳ�"))
            {
                fdhGuanXinBing.OtherHas = true;
                if (temp.IndexOf("���Ĳ�(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("���Ĳ�("), temp.Length - temp.IndexOf("���Ĳ�("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGuanXinBing.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGuanXinBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("�Ĺ�"))
            {
                fdhXinGeng.OtherHas = true;
                if (temp.IndexOf("�Ĺ�(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("�Ĺ�("), temp.Length - temp.IndexOf("�Ĺ�("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhXinGeng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhXinGeng.ChildrenHasCount = 0;
            }
            if (temp.Contains("�Գ�Ѫ"))
            {
                fdhNaoChuXue.OtherHas = true;
                if (temp.IndexOf("�Գ�Ѫ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("�Գ�Ѫ("), temp.Length - temp.IndexOf("�Գ�Ѫ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhNaoChuXue.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhNaoChuXue.ChildrenHasCount = 0;
            }
            if (temp.Contains("��Ѫ˨"))
            {
                fdhNaoXueShuan.OtherHas = true;
                if (temp.IndexOf("��Ѫ˨(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��Ѫ˨("), temp.Length - temp.IndexOf("��Ѫ˨("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhNaoXueShuan.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhNaoXueShuan.ChildrenHasCount = 0;
            }
            if (temp.Contains("������"))
            {
                fdhDanNangyan.OtherHas = true;
                if (temp.IndexOf("������(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("������("), temp.Length - temp.IndexOf("������("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhDanNangyan.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhDanNangyan.ChildrenHasCount = 0;
            }
            if (temp.Contains("��ʯ֢"))
            {
                fdhDanShiZheng.OtherHas = true;
                if (temp.IndexOf("��ʯ֢(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("��ʯ֢("), temp.Length - temp.IndexOf("��ʯ֢("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhDanShiZheng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhDanShiZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("���ಡ"))
            {
                fdhShenZangBing.OtherHas = true;
                if (temp.IndexOf("���ಡ(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("���ಡ("), temp.Length - temp.IndexOf("���ಡ("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhShenZangBing.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhShenZangBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhZhongLiu.OtherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhZhongLiu.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhZhongLiu.ChildrenHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhFeiPang.OtherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhFeiPang.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhFeiPang.ChildrenHasCount = 0;
            }
            if (temp.Contains("����"))
            {
                fdhQiTa.OtherHas = true;
                if (temp.IndexOf("����(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("����("), temp.Length - temp.IndexOf("����("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhQiTa.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhQiTa.OtherHasDetail = "";
            }
            #endregion
            #endregion

            //���������Ѽ��ر�־
            this.bLoaded = true;
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public override void LoadDataFromUIToVar()
        {
            if (!IsModified)
                return ;

            GlobalData.FamilyDiseaseHistoryInfo.Clear();

            GlobalData.FamilyDiseaseHistoryInfo.FatherHistory =txtFather.Text;
            GlobalData.FamilyDiseaseHistoryInfo.MotherHistory=txtMother.Text  ;
            GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory=txtSisBrother.Text;
            GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory=txtChildren .Text;
            GlobalData.FamilyDiseaseHistoryInfo.OtherHistory=txtOther.Text;

            IsModified = false;
        }

#endregion



    }
}