(defrule MS_DM_Instance_110158_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110158)
then
(undefrule *)
(InterpretationIndex "BMI>23�����Ʒ���Ϊ������˫��I+�ж�Ч�������ǻ������ȵ��ش��ڼ���AGI(����6)��")
(Recommendation "����˫��I+�ж�Ч�������ǻ������ȵ��ش��ڼ���AGI(����6)��
����˫��0.5 3��/��+
�ж�Ч������(������� 5mg, 1��/d;�����ͪ15mg, 1��/d)
��ǻ������ȵ��ش��ڼ�
(������� 0.5mg 3��/d
�Ǹ����� 120mg , 3 ��/d )
��AGI(��������50mg, 3��/d
�����в��ǣ�0.2mg, 3��
�׸��д� 25mg, 3��/d)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110158_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110158)
then
(undefrule *)
(InterpretationIndex "BMI<=23�����Ʒ���Ϊ����������+�ж�Ч�������ǻ������ȵ��ش��ڼ������ͪ��(����7)��")
(Recommendation "��������+�ж�Ч�������ǻ������ȵ��ش��ڼ������ͪ��(����7)��
����˫�� 0.25~0.5, 3��/d+
�ж�Ч������(������� 5mg,1��/d;
�����ͪ15mg, 1��/d)��
�ǻ������ȵ��ش��ڼ�(������� 0.5mg, 3��/d
�Ǹ����� 120mg, 3��/d)��
����ͪ��(������ͪ  15mg, 1��/d
�޸���ͪ  4mg, 1��/d)")
(FactUsed "BMI")
)
)
