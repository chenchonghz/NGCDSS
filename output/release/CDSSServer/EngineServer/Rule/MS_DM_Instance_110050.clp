(defrule MS_DM_Instance_110050_0
(filepath ?filepath)
(BMI ?BMI)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?BMI 23.0 BMI))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110050)
then
(undefrule *)
(InterpretationIndex "BMI>23kg/m2�����Ʒ���Ϊ��Ƥ��ע���ȵ�������+����˫�ҡ�")
(Recommendation "����˫��+������ڷ�����ҩ(����4)��
����˫��0.5 , 3��/d+
������(�������� 1mg, 1 ��/d��
�������ػ���Ƭ(������MR)30mg, 1��/�磻
������຿���Ƭ(������)�� 5mg, 1��/�գ�
�������� 40mg  1 ��/d
������� 5mg, 1��/d;
�����ͪ 15mg, 3��/d
���б��� 2.5mg-5  1��/d)��
�ǻ������ȵ��ش��ڼ�(������� 0.5mg, 3��/d)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110050_1
(filepath ?filepath)
(BMI ?BMI)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf <= ?BMI 23.0 BMI))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110050)
then
(undefrule *)
(InterpretationIndex "BMI<=23�������ж��Ƿ�BMI>=18.5��")
(load (str-cat ?filepath "MS_DM_Instance_110128.clp"))
(FactUsed "BMI")
)
)
