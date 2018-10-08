(defrule MS_DM_Instance_110128_0
(filepath ?filepath)
(BMI ?BMI)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?BMI 18.5 BMI))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110128)
then
(undefrule *)
(InterpretationIndex "BMI>=18.5�����Ʒ���Ϊ����������+������ڷ�����ҩ+�ȵ��ء�")
(Recommendation "��������+������ڷ�����ҩ+�ȵ���
(����5)��
����˫��0.25~0.5, 3��/d +
������ڷ�����ҩ(�������� 1mg, 1 ��/d��
�������ػ���Ƭ(������MR)30mg, 1��/�磻
������຿���Ƭ(������)�� 5mg, 1��/�գ�
������� 5mg, 1��/d;
�����ͪ  15mg, 1��/d)��
�ǻ������ȵ��ش��ڼ�(������� 0.5mg, 3 ��/d
�Ǹ����� 120mg, 3��/d)+
˯ǰNPH/�ʾ��ȵ���/determie���峿�ʾ��ȵ��ػ����ǰ70/30��BIASP 30��MIX25����ʼ����ȷ����0.1~0.25/kg��ո�Ѫ��(mmol/L)��6-10U(����)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110128_1
(filepath ?filepath)
(BMI ?BMI)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?BMI 18.5 BMI))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110128)
then
(undefrule *)
(InterpretationIndex "BMI<18.5������Ƥ��ע���ȵ������ơ�")
(Recommendation "Ƥ��ע���ȵ�������(����3��:
1.TDI���㣺0.2-0.3U/kg.d��
2. CSII: �ٿ����ȵ��أ�����ϳ��������ȵ��ػ������ȵ��ػ��Ŷ��ȵ��أ��ڻ�������TDI��������������;�ۣ�������������������ǰ���������ƽ������������ǰ�򰴣����������ı����ֱ����������ǰ��Ҳ�ɰ����������������ֱ����������ǰ��˯ǰ�Ӳ�ǰ.

3.MDI:ÿ���Ĵη���
< 1>.���ǰRI/�����ȵ���/�Ŷ��ȵ���(30%TDI)�����ǰ�����ǰRI/�����ȵ���/�Ŷ��ȵ���(22.5%TDI)��˯ǰNPH/determir/�ʾ��ȵ���(25%TDI);
<2>.���ǰRI+NPH���вͺ����ǰRI��˯ǰNPH��
2.ÿ��3�η���
<1>.���ǰ RI/�����ȵ���/�Ŷ��ȵ��� +NPH/determir/PZI (��Ч����Ч=2:1,52.5% TDI)��  ���RI/�����ȵ���/�Ŷ��ȵ���(22.5% TDI),   ��˯ǰNPH/determir/PZI/�ʾ��ȵ���(25%  TDI) ;  
 <2>.���ǰ RI (30% TDI) , ���ǰRI  (22.5% TDI), ���ǰ RI+�ʾ��ȵ���/NPH/determir/PZI  (47.5% TDI)��
 <3>.���ǰRI+PZI �����ǰ RI �����ǰ RI+PZI
<4>.RI+NPH �ֱ�������ǰ
ע�����ǰRI(��) 25-30%; �в�ǰRI(��) 15-20%�����RI(����)20-25%; ˯ǰNPH(С) 20%��RI+NPH��RI�����ǰ15-30����ע��
���Ŷ��ȵ��ػ������ȵ��ز�ǰ15���ӻ��ǰ���̻�ͺ�ע��")
(FactUsed "BMI")
)
)
