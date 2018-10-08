(defrule MS_MS_mergexml_Instance_170068_0
(filepath ?filepath)
(Insolin ?Insolin)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Insolin YES Insolin))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170068)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "�����ȵ�������������")
(FactUsed "Insolin")
)
)


(defrule MS_MS_mergexml_Instance_170068_1
(filepath ?filepath)
(Gelietong_Drug ?Gelietong_Drug)
(AGI_Drug ?AGI_Drug)
(Shuanggua_Drug ?Shuanggua_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL031 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL030)  (Transform ?CIL031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIL031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL032 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL033 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(if
(and (Transform ?CIL032)  (Transform ?CIL033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIL032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIL033 ?CIN021))
(if(eq ?CIN021 NULL)
then
(bind ?CIN021 FALSE)
)
)
(bind ?CIL034 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(bind ?CIL035 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL034)  (Transform ?CIL035) )
then
(bind ?CIN022 TRUE)
else
(bind ?CIN022 NULL)
(bind ?CIN022 (AddOrNot ?CIL034 ?CIN022))
(bind ?CIN022 (AddOrNot ?CIL035 ?CIN022))
(if(eq ?CIN022 NULL)
then
(bind ?CIN022 FALSE)
)
)
(if
(or (Transform ?CIN020)  (Transform ?CIN021)  (Transform ?CIN022) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN022 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170068)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "����������������������ͪ��+��������������")
(FactUsed "Gelietong_Drug" "AGI_Drug" "Shuanggua_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170068_2
(filepath ?filepath)
(Shuanggua_Drug ?Shuanggua_Drug)
(Gelietong_Drug ?Gelietong_Drug)
(AGI_Drug ?AGI_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(bind ?CIL021 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL022 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL022 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170068)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "Ƥ��ע���ȵ�������")
(FactUsed "Shuanggua_Drug" "Gelietong_Drug" "AGI_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170068_3
(filepath ?filepath)
(Huangniao_Drug ?Huangniao_Drug)
(Shuanggua_Drug ?Shuanggua_Drug)
(Gelietong_Drug ?Gelietong_Drug)
(AGI_Drug ?AGI_Drug)
(Gelienai_Drug ?Gelienai_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL031 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(if
(and (Transform ?CIL030)  (Transform ?CIL031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIL031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL032 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL033 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(if
(and (Transform ?CIL032)  (Transform ?CIL033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIL032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIL033 ?CIN021))
(if(eq ?CIN021 NULL)
then
(bind ?CIN021 FALSE)
)
)
(bind ?CIL034 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL035 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL034)  (Transform ?CIL035) )
then
(bind ?CIN022 TRUE)
else
(bind ?CIN022 NULL)
(bind ?CIN022 (AddOrNot ?CIL034 ?CIN022))
(bind ?CIN022 (AddOrNot ?CIL035 ?CIN022))
(if(eq ?CIN022 NULL)
then
(bind ?CIN022 FALSE)
)
)
(bind ?CIL036 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL037 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(if
(and (Transform ?CIL036)  (Transform ?CIL037) )
then
(bind ?CIN023 TRUE)
else
(bind ?CIN023 NULL)
(bind ?CIN023 (AddOrNot ?CIL036 ?CIN023))
(bind ?CIN023 (AddOrNot ?CIL037 ?CIN023))
(if(eq ?CIN023 NULL)
then
(bind ?CIN023 FALSE)
)
)
(bind ?CIL038 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL039 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(if
(and (Transform ?CIL038)  (Transform ?CIL039) )
then
(bind ?CIN024 TRUE)
else
(bind ?CIN024 NULL)
(bind ?CIN024 (AddOrNot ?CIL038 ?CIN024))
(bind ?CIN024 (AddOrNot ?CIL039 ?CIN024))
(if(eq ?CIN024 NULL)
then
(bind ?CIN024 FALSE)
)
)
(bind ?CIL0310 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL0311 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL0310)  (Transform ?CIL0311) )
then
(bind ?CIN025 TRUE)
else
(bind ?CIN025 NULL)
(bind ?CIN025 (AddOrNot ?CIL0310 ?CIN025))
(bind ?CIN025 (AddOrNot ?CIL0311 ?CIN025))
(if(eq ?CIN025 NULL)
then
(bind ?CIN025 FALSE)
)
)
(if
(or (Transform ?CIN020)  (Transform ?CIN021)  (Transform ?CIN022)  (Transform ?CIN023)  (Transform ?CIN024)  (Transform ?CIN025) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN022 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN023 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN024 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN025 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170068)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "���������������������������")
(FactUsed "Huangniao_Drug" "Shuanggua_Drug" "Gelietong_Drug" "AGI_Drug" "Gelienai_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170068_4
(filepath ?filepath)
(Shuanggua_Drug ?Shuanggua_Drug)
(Gelienai_Drug ?Gelienai_Drug)
(Gelietong_Drug ?Gelietong_Drug)
(Huangniao_Drug ?Huangniao_Drug)
(AGI_Drug ?AGI_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(bind ?CIL021 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL022 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL023 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL024 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(or (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022)  (Transform ?CIL023)  (Transform ?CIL024) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL022 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL023 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL024 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170068)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "1.��������������������������ࣻ2.�������ͪ��+����ø���Ƽ�+��������������")
(Recommendation "Ƥ��ע���ȵ�������+����˫�ң�����3+����8����
1.TDI���㣺0.2-0.3U/kg.d��
2. CSII: �ٿ����ȵ��أ�����ϳ��������ȵ��ػ������ȵ��ػ��Ŷ��ȵ��أ��ڻ�������TDI��������������;�ۣ�������������������ǰ���������ƽ������������ǰ�򰴣����������ı����ֱ����������ǰ��Ҳ�ɰ����������������ֱ����������ǰ��˯ǰ�Ӳ�ǰ.

3.MDI:ÿ���Ĵη���
 <1>.���ǰRI/�����ȵ���/�Ŷ��ȵ���(30%TDI)�����ǰ�����ǰRI/�����ȵ���/�Ŷ��ȵ���(22.5%TDI)��˯ǰNPH/determir/�ʾ��ȵ���(25%TDI);
<2>.���ǰRI+NPH���вͺ����ǰRI��˯ǰNPH��
2.ÿ��3�η���
<1>.���ǰ RI/�����ȵ���/�Ŷ��ȵ��� +NPH/determir/PZI (��Ч����Ч=2:1,52.5% TDI)��  ���RI/�����ȵ���/�Ŷ��ȵ���(22.5% TDI),   ��˯ǰNPH/determir/PZI/�ʾ��ȵ���(25%  TDI) ;  
<2>.���ǰ RI (30% TDI) , ���ǰRI  (22.5% TDI), ���ǰ RI+�ʾ��ȵ���/NPH/determir/PZI  (47.5% TDI)��
<3>.���ǰRI+PZI �����ǰ RI �����ǰ RI+PZI
<4>.RI+NPH �ֱ�������ǰ
ע�����ǰRI(��) 25-30%; �в�ǰRI(��) 15-20%�����RI(����)20-25%; ˯ǰNPH(С) 20%��RI+NPH��RI�����ǰ15-30����ע��
���Ŷ��ȵ��ػ������ȵ��ز�ǰ15���ӻ��ǰ���̻�ͺ�ע�䣻
����˫��0.5 3��/d ��0.85, 2��/d")
(FactUsed "Shuanggua_Drug" "Gelienai_Drug" "Gelietong_Drug" "Huangniao_Drug" "AGI_Drug")
)
)
