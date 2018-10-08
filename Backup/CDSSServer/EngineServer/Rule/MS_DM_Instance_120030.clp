(defrule MS_DM_Instance_120030_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_120030)
then
(undefrule *)
(InterpretationIndex "BMI>23�����Ʒ���Ϊ����ʳ���˶�+����˫�һ�AGI��")
(Recommendation "��ʳ���˶�+����˫�һ�AGI(����10)��
����˫��0.5 2-3��/d
��AGI(��������50mg, 3��/d
�����в���0.2mg,  3��
�׸��д� 25mg, 3��/d)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_120030_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_120030)
then
(undefrule *)
(InterpretationIndex "BMI<=23�����Ʒ���Ϊ����ʳ���˶����������ͪ���AGI��")
(Recommendation "��ʳ���˶����������ͪ���AGI(����11)��
�������ͪ��(������ͪ  15mg, 1��/d
�޸���ͪ  4mg, 1��/d)��
AGI(��������50mg, 3��/d
�����в��ǣ�0.2mg, 3��
�׸��д� 25mg, 3��/d)")
(FactUsed "BMI")
)
)
