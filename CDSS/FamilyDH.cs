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
        public bool bLoaded = false;    //标记数据是否已经加载过
        private StringBuilder sbFather = new StringBuilder(100);
        private StringBuilder sbMother = new StringBuilder(100);
        private StringBuilder sbSisBrother = new StringBuilder(100);
        private StringBuilder sbChildren = new StringBuilder(100);
        private StringBuilder sbOther = new StringBuilder(100);

        public FamilyDH()
        {
            InitializeComponent();
        }

#region 系统事件
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilyDH_Load(object sender, EventArgs e)
        {
            SetSisBrotherAndchildrenCount();
        }

        /// <summary>
        /// 可视性改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FamilyDH_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                SetSisBrotherAndchildrenCount();

                if (!PatInfo.bNewPatient && !bLoaded)
                    LoadDataFromVarToUI(); //在窗体显示出来的时候加载数据
                //如果是新入病人，在页面显示出来时设置兄弟姐妹子女数
                //if (PatInfo.bNewPatient)
                //    SetSisBrotherAndchildrenCount();
            }
        }

#endregion


#region 用户事件

        /// <summary>
        /// 当页面内容改变时引发该事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e"></param>
        protected void DataModified(object sender, EventArgs e)
        {
            //如果在这之前数据未保存过,则直接返回
            if (IsModified || (!PatInfo.bNewPatient && !bLoaded))
                return;

            //引发事件,通知父窗体更新保存按钮状态
            RaiseDataChangedEvent(this, e);
            IsModified = true;
        }

        /// <summary>
        /// 疾病控件数据已更改事件
        /// </summary>
        private void FDH_DataChanged(object sender, EventArgs e)
        {
            DataModified(sender, e);
            ShowFDH();            
        }

#endregion


#region 功能函数

        /// <summary>
        /// 设置兄弟姐妹和子女数
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
        /// 清空界面显示，恢复初始状态
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

            //设置数据未加载标志
            this.bLoaded = false;
            this.IsModified = false;
        }

        /// <summary>
        /// 按成员分类显示家族疾病史
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
                    sbFather.Append("，");
                }
                if (fdh.MatherHas)
                {
                    sbMother.Append(fdh.DiseaseName);
                    sbMother.Append("，");
                }
                if (fdh.SisBrotherHas)
                {
                    sbSisBrother.Append(fdh.DiseaseName);
                    if (fdh.SisBrotherHasCount > 0)
                        sbSisBrother.AppendFormat("({0}人)，", fdh.SisBrotherHasCount.ToString());
                    else
                        sbSisBrother.Append("，");
                }
                if (fdh.ChildrenHas)
                {
                    sbChildren.Append(fdh.DiseaseName);
                    if (fdh.ChildrenHasCount > 0)
                        sbChildren.AppendFormat("({0}人)，", fdh.ChildrenHasCount.ToString());
                    else
                        sbChildren.Append("，");
                }
                if (fdh.OtherHas)
                {
                    sbOther.Append(fdh.DiseaseName);
                    if (fdh.OtherHasDetail.Length > 0)
                        sbOther.AppendFormat("({0})，", fdh.OtherHasDetail);
                    else
                        sbOther.Append("，");
                }
            }

            this.txtFather.Text = sbFather.ToString();
            this.txtMother.Text = sbMother.ToString();
            this.txtSisBrother.Text = sbSisBrother.ToString();
            this.txtChildren.Text = sbChildren.ToString();
            this.txtOther.Text = sbOther.ToString();
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        public override void LoadDataFromVarToUI()
        {
            //SetSisBrotherAndchildrenCount();//给控件设置兄弟姐妹子女数
            txtFather.Text = GlobalData.FamilyDiseaseHistoryInfo.FatherHistory;
            txtMother.Text =GlobalData.FamilyDiseaseHistoryInfo.MotherHistory ;
            txtSisBrother.Text =GlobalData.FamilyDiseaseHistoryInfo.SiblingsHistory ;
            txtChildren.Text =GlobalData.FamilyDiseaseHistoryInfo.ChildrenHistory ;
            txtOther.Text  = GlobalData.FamilyDiseaseHistoryInfo.OtherHistory;

            //TODO:给各个用户控件加载数据
            #region 
            string temp="";

            #region 父亲
            temp = txtFather.Text;
            if (temp.Contains("糖尿病"))
                fdhTangNiaoBing.FatherHas = true;
            if (temp.Contains("高血压"))
                fdhGaoXueYa.FatherHas = true;
            if (temp.Contains("高脂血症"))
                fdhGaoZhiXueZheng.FatherHas = true;
            if (temp.Contains("高尿酸血症"))
                fdhGaoNiaoSuanXueZheng.FatherHas = true;
            if (temp.Contains("冠心病"))
                fdhGuanXinBing.FatherHas = true;
            if (temp.Contains("心梗"))
                fdhXinGeng.FatherHas = true;
            if (temp.Contains("脑出血"))
                fdhNaoChuXue.FatherHas = true;
            if (temp.Contains("脑血栓"))
                fdhNaoXueShuan.FatherHas = true;
            if (temp.Contains("胆囊炎"))
                fdhDanNangyan.FatherHas = true;
            if (temp.Contains("胆石症"))
                fdhDanShiZheng.FatherHas = true;
            if (temp.Contains("肾脏病"))
                fdhShenZangBing.FatherHas = true;
            if (temp.Contains("肿瘤"))
                fdhZhongLiu.FatherHas = true;
            if (temp.Contains("肥胖"))
                fdhFeiPang.FatherHas = true;
            if (temp.Contains("其他"))
                fdhQiTa.FatherHas = true;
            #endregion

            #region 母亲
            temp = txtMother.Text;
            if (temp.Contains("糖尿病"))
                fdhTangNiaoBing.MatherHas = true;
            if (temp.Contains("高血压"))
                fdhGaoXueYa.MatherHas = true;
            if (temp.Contains("高脂血症"))
                fdhGaoZhiXueZheng.MatherHas = true;
            if (temp.Contains("高尿酸血症"))
                fdhGaoNiaoSuanXueZheng.MatherHas = true;
            if (temp.Contains("冠心病"))
                fdhGuanXinBing.MatherHas = true;
            if (temp.Contains("心梗"))
                fdhXinGeng.MatherHas = true;
            if (temp.Contains("脑出血"))
                fdhNaoChuXue.MatherHas = true;
            if (temp.Contains("脑血栓"))
                fdhNaoXueShuan.MatherHas = true;
            if (temp.Contains("胆囊炎"))
                fdhDanNangyan.MatherHas = true;
            if (temp.Contains("胆石症"))
                fdhDanShiZheng.MatherHas = true;
            if (temp.Contains("肾脏病"))
                fdhShenZangBing.MatherHas = true;
            if (temp.Contains("肿瘤"))
                fdhZhongLiu.MatherHas = true;
            if (temp.Contains("肥胖"))
                fdhFeiPang.MatherHas = true;
            if (temp.Contains("其他"))
                fdhQiTa.MatherHas = true;
            #endregion

            #region 兄弟姐妹
            temp = txtSisBrother.Text;
            if (temp.Contains("糖尿病"))
            {
                fdhTangNiaoBing.SisBrotherHas = true;
                if (temp.IndexOf("糖尿病(") != -1)   
                {
                    string Disease = temp.Substring(temp.IndexOf("糖尿病("), temp.Length - temp.IndexOf("糖尿病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhTangNiaoBing.SisBrotherHasCount = int.Parse(temp2[0].ToString()); 
                }
                else
                    fdhTangNiaoBing.SisBrotherHasCount = 0;
            }
            if (temp.Contains("高血压"))
            {
                fdhGaoXueYa.SisBrotherHas = true;
                if (temp.IndexOf("高血压(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高血压("), temp.Length - temp.IndexOf("高血压("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGaoXueYa.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoXueYa.SisBrotherHasCount = 0;
            }
            if (temp.Contains("高脂血症"))
            {
                fdhGaoZhiXueZheng.SisBrotherHas = true;
                if (temp.IndexOf("高脂血症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高脂血症("), temp.Length - temp.IndexOf("高脂血症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGaoZhiXueZheng.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoZhiXueZheng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("高尿酸血症"))
            {
                fdhGaoNiaoSuanXueZheng.SisBrotherHas = true;
                if (temp.IndexOf("高尿酸血症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高尿酸血症("), temp.Length - temp.IndexOf("高尿酸血症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGaoNiaoSuanXueZheng.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoNiaoSuanXueZheng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("冠心病"))
            {
                fdhGuanXinBing.SisBrotherHas = true;
                if (temp.IndexOf("冠心病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("冠心病("), temp.Length - temp.IndexOf("冠心病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGuanXinBing.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGuanXinBing.SisBrotherHasCount = 0;
            }
            if (temp.Contains("心梗"))
            {
                fdhXinGeng.SisBrotherHas = true;
                if (temp.IndexOf("心梗(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("心梗("), temp.Length - temp.IndexOf("心梗("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhXinGeng.SisBrotherHasCount = int.Parse(temp2[0].ToString()); 
                }
                else
                    fdhXinGeng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("脑出血"))
            {
                fdhNaoChuXue.SisBrotherHas = true;
                if (temp.IndexOf("脑出血(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("脑出血("), temp.Length - temp.IndexOf("脑出血("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhNaoChuXue.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoChuXue.SisBrotherHasCount = 0;
            }
            if (temp.Contains("脑血栓"))
            {
                fdhNaoXueShuan.SisBrotherHas = true;
                if (temp.IndexOf("脑血栓(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("脑血栓("), temp.Length - temp.IndexOf("脑血栓("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhNaoXueShuan.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoXueShuan.SisBrotherHasCount = 0;
            }
            if (temp.Contains("胆囊炎"))
            {
                fdhDanNangyan.SisBrotherHas = true;
                if (temp.IndexOf("胆囊炎(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("胆囊炎("), temp.Length - temp.IndexOf("胆囊炎("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhDanNangyan.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanNangyan.SisBrotherHasCount = 0;
            }
            if (temp.Contains("胆石症"))
            {
                fdhDanShiZheng.SisBrotherHas = true;
                if (temp.IndexOf("胆石症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("胆石症("), temp.Length - temp.IndexOf("胆石症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhDanShiZheng.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanShiZheng.SisBrotherHasCount = 0;
            }
            if (temp.Contains("肾脏病"))
            {
                fdhShenZangBing.SisBrotherHas = true;
                if (temp.IndexOf("肾脏病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肾脏病("), temp.Length - temp.IndexOf("肾脏病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhShenZangBing.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhShenZangBing.SisBrotherHasCount = 0;
            }
            if (temp.Contains("肿瘤"))
            {
                fdhZhongLiu.SisBrotherHas = true;
                if (temp.IndexOf("肿瘤(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肿瘤("), temp.Length - temp.IndexOf("肿瘤("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhZhongLiu.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhZhongLiu.SisBrotherHasCount = 0;
            }
            if (temp.Contains("肥胖"))
            {
                fdhFeiPang.SisBrotherHas = true;
                if (temp.IndexOf("肥胖(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肥胖("), temp.Length - temp.IndexOf("肥胖("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhFeiPang.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhFeiPang.SisBrotherHasCount = 0;
            }
            if (temp.Contains("其他"))
            {
                fdhQiTa.SisBrotherHas = true;
                if (temp.IndexOf("其他(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("其他("), temp.Length - temp.IndexOf("其他("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhQiTa.SisBrotherHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhQiTa.SisBrotherHasCount = 0;
            }
            #endregion

            #region 子女
            temp = txtSisBrother.Text;
            if (temp.Contains("糖尿病"))
            {
                fdhTangNiaoBing.ChildrenHas = true;
                if (temp.IndexOf("糖尿病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("糖尿病("), temp.Length - temp.IndexOf("糖尿病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhTangNiaoBing.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhTangNiaoBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("高血压"))
            {
                fdhGaoXueYa.ChildrenHas = true;
                if (temp.IndexOf("高血压(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高血压("), temp.Length - temp.IndexOf("高血压("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGaoXueYa.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoXueYa.ChildrenHasCount = 0;
            }
            if (temp.Contains("高脂血症"))
            {
                fdhGaoZhiXueZheng.ChildrenHas = true;
                if (temp.IndexOf("高脂血症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高脂血症("), temp.Length - temp.IndexOf("高脂血症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGaoZhiXueZheng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoZhiXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("高尿酸血症"))
            {
                fdhGaoNiaoSuanXueZheng.ChildrenHas = true;
                if (temp.IndexOf("高尿酸血症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高尿酸血症("), temp.Length - temp.IndexOf("高尿酸血症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGaoNiaoSuanXueZheng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGaoNiaoSuanXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("冠心病"))
            {
                fdhGuanXinBing.ChildrenHas = true;
                if (temp.IndexOf("冠心病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("冠心病("), temp.Length - temp.IndexOf("冠心病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhGuanXinBing.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhGuanXinBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("心梗"))
            {
                fdhXinGeng.ChildrenHas = true;
                if (temp.IndexOf("心梗(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("心梗("), temp.Length - temp.IndexOf("心梗("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhXinGeng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhXinGeng.ChildrenHasCount = 0;
            }
            if (temp.Contains("脑出血"))
            {
                fdhNaoChuXue.ChildrenHas = true;
                if (temp.IndexOf("脑出血(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("脑出血("), temp.Length - temp.IndexOf("脑出血("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhNaoChuXue.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoChuXue.ChildrenHasCount = 0;
            }
            if (temp.Contains("脑血栓"))
            {
                fdhNaoXueShuan.ChildrenHas = true;
                if (temp.IndexOf("脑血栓(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("脑血栓("), temp.Length - temp.IndexOf("脑血栓("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhNaoXueShuan.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhNaoXueShuan.ChildrenHasCount = 0;
            }
            if (temp.Contains("胆囊炎"))
            {
                fdhDanNangyan.ChildrenHas = true;
                if (temp.IndexOf("胆囊炎(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("胆囊炎("), temp.Length - temp.IndexOf("胆囊炎("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhDanNangyan.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanNangyan.ChildrenHasCount = 0;
            }
            if (temp.Contains("胆石症"))
            {
                fdhDanShiZheng.ChildrenHas = true;
                if (temp.IndexOf("胆石症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("胆石症("), temp.Length - temp.IndexOf("胆石症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhDanShiZheng.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhDanShiZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("肾脏病"))
            {
                fdhShenZangBing.ChildrenHas = true;
                if (temp.IndexOf("肾脏病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肾脏病("), temp.Length - temp.IndexOf("肾脏病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhShenZangBing.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhShenZangBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("肿瘤"))
            {
                fdhZhongLiu.ChildrenHas = true;
                if (temp.IndexOf("肿瘤(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肿瘤("), temp.Length - temp.IndexOf("肿瘤("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhZhongLiu.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhZhongLiu.ChildrenHasCount = 0;
            }
            if (temp.Contains("肥胖"))
            {
                fdhFeiPang.ChildrenHas = true;
                if (temp.IndexOf("肥胖(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肥胖("), temp.Length - temp.IndexOf("肥胖("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhFeiPang.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhFeiPang.ChildrenHasCount = 0;
            }
            if (temp.Contains("其他"))
            {
                fdhQiTa.ChildrenHas = true;
                if (temp.IndexOf("其他(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("其他("), temp.Length - temp.IndexOf("其他("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split('人');
                    fdhQiTa.ChildrenHasCount = int.Parse(temp2[0].ToString());
                }
                else
                    fdhQiTa.ChildrenHasCount = 0;
            }
            #endregion

            #region 其他
            temp = txtOther.Text;
            if (temp.Contains("糖尿病"))
            {
                fdhTangNiaoBing.OtherHas = true;
                if (temp.IndexOf("糖尿病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("糖尿病("), temp.Length - temp.IndexOf("糖尿病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhTangNiaoBing.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhTangNiaoBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("高血压"))
            {
                fdhGaoXueYa.OtherHas = true;
                if (temp.IndexOf("高血压(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高血压("), temp.Length - temp.IndexOf("高血压("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGaoXueYa.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGaoXueYa.ChildrenHasCount = 0;
            }
            if (temp.Contains("高脂血症"))
            {
                fdhGaoZhiXueZheng.OtherHas = true;
                if (temp.IndexOf("高脂血症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高脂血症("), temp.Length - temp.IndexOf("高脂血症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGaoZhiXueZheng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGaoZhiXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("高尿酸血症"))
            {
                fdhGaoNiaoSuanXueZheng.OtherHas = true;
                if (temp.IndexOf("高尿酸血症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("高尿酸血症("), temp.Length - temp.IndexOf("高尿酸血症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGaoNiaoSuanXueZheng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGaoNiaoSuanXueZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("冠心病"))
            {
                fdhGuanXinBing.OtherHas = true;
                if (temp.IndexOf("冠心病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("冠心病("), temp.Length - temp.IndexOf("冠心病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhGuanXinBing.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhGuanXinBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("心梗"))
            {
                fdhXinGeng.OtherHas = true;
                if (temp.IndexOf("心梗(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("心梗("), temp.Length - temp.IndexOf("心梗("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhXinGeng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhXinGeng.ChildrenHasCount = 0;
            }
            if (temp.Contains("脑出血"))
            {
                fdhNaoChuXue.OtherHas = true;
                if (temp.IndexOf("脑出血(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("脑出血("), temp.Length - temp.IndexOf("脑出血("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhNaoChuXue.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhNaoChuXue.ChildrenHasCount = 0;
            }
            if (temp.Contains("脑血栓"))
            {
                fdhNaoXueShuan.OtherHas = true;
                if (temp.IndexOf("脑血栓(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("脑血栓("), temp.Length - temp.IndexOf("脑血栓("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhNaoXueShuan.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhNaoXueShuan.ChildrenHasCount = 0;
            }
            if (temp.Contains("胆囊炎"))
            {
                fdhDanNangyan.OtherHas = true;
                if (temp.IndexOf("胆囊炎(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("胆囊炎("), temp.Length - temp.IndexOf("胆囊炎("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhDanNangyan.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhDanNangyan.ChildrenHasCount = 0;
            }
            if (temp.Contains("胆石症"))
            {
                fdhDanShiZheng.OtherHas = true;
                if (temp.IndexOf("胆石症(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("胆石症("), temp.Length - temp.IndexOf("胆石症("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhDanShiZheng.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhDanShiZheng.ChildrenHasCount = 0;
            }
            if (temp.Contains("肾脏病"))
            {
                fdhShenZangBing.OtherHas = true;
                if (temp.IndexOf("肾脏病(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肾脏病("), temp.Length - temp.IndexOf("肾脏病("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhShenZangBing.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhShenZangBing.ChildrenHasCount = 0;
            }
            if (temp.Contains("肿瘤"))
            {
                fdhZhongLiu.OtherHas = true;
                if (temp.IndexOf("肿瘤(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肿瘤("), temp.Length - temp.IndexOf("肿瘤("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhZhongLiu.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhZhongLiu.ChildrenHasCount = 0;
            }
            if (temp.Contains("肥胖"))
            {
                fdhFeiPang.OtherHas = true;
                if (temp.IndexOf("肥胖(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("肥胖("), temp.Length - temp.IndexOf("肥胖("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhFeiPang.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhFeiPang.ChildrenHasCount = 0;
            }
            if (temp.Contains("其他"))
            {
                fdhQiTa.OtherHas = true;
                if (temp.IndexOf("其他(") != -1)
                {
                    string Disease = temp.Substring(temp.IndexOf("其他("), temp.Length - temp.IndexOf("其他("));
                    string[] temp1 = Disease.Split('(');
                    string[] temp2 = temp1[1].Split(')');
                    fdhQiTa.OtherHasDetail = temp2[0].ToString();
                }
                else
                    fdhQiTa.OtherHasDetail = "";
            }
            #endregion
            #endregion

            //设置数据已加载标志
            this.bLoaded = true;
        }


        /// <summary>
        /// 保存数据
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