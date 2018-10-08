(defrule MS_MS_mergexml_Instance_170104_0
(filepath ?filepath)
(DM_Drug ?DM_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?DM_Drug YES DM_Drug))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_170104)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "����˫��+������ڷ�����ҩ�����˫��+Ƥ���ȵ�������")
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
(FactUsed "DM_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170104_1
(filepath ?filepath)
(DM_Drug ?DM_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?DM_Drug YES DM_Drug))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170104)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
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
(FactUsed "DM_Drug")
)
)
