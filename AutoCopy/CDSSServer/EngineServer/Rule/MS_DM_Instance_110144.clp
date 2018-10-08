(defrule MS_DM_Instance_110144_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110144)
then
(undefrule *)
(InterpretationIndex "BMI>23�����Ʒ���Ϊ������˫�ҡ�")
(Recommendation "����˫��(����8)��
����˫��0.5 3��/d ��0.85, 2��/d")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110144_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110144)
then
(undefrule *)
(InterpretationIndex "BMI<=23�����Ʒ���Ϊ���������ǻ�������ǻ������ȵ��ش��ڼ������ͪ���AG")
(Recommendation "�������ǻ�������ǻ������ȵ��ش��ڼ������ͪ���AGI(����9)��
����˫��0.5, 3��/d��
��Ч������(�����ͪ 15mg, 3��/d)��
�ǻ������ȵ��ش��ڼ�(������� 0.5mg 3��/d
�Ǹ����� 120mg, 3��/d)��
����ͪ��(������ͪ  15mg, 1��/d
�޸���ͪ  4mg, 1��/d)��
AGI(��������50mg, 3��/d
�����в��ǣ�0.2mg, 3��
�׸��д� 25mg, 3��/d)")
(FactUsed "BMI")
)
)
