(defrule MS_HUA_Instance_100051_0
(filepath ?filepath)
(abnormal_renal_function ?abnormal_renal_function)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?abnormal_renal_function NO abnormal_renal_function))
(bind ?CIL021 (Leaf < ?Age 60.0 Age))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100051)
then
(undefrule *)
(InterpretationIndex "����������������<60��������ʳָ�����ӷ�������¡��")
(Recommendation "��ʳָ�����ӷ�������¡")
(load (str-cat ?filepath "MS_HUA_Instance_100083.clp"))
(FactUsed "abnormal_renal_function" "Age")
)
)


(defrule MS_HUA_Instance_100051_1
(filepath ?filepath)
(Age ?Age)
(abnormal_renal_function ?abnormal_renal_function)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?Age 60.0 Age))
(bind ?CIL021 (Leaf equals ?abnormal_renal_function YES abnormal_renal_function))
(if
(or (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100051)
then
(undefrule *)
(InterpretationIndex "�������쳣������>=60��������ʳָ��+�����ʴ���")
(Recommendation "�ӷ������ʴ���")
(FactUsed "Age" "abnormal_renal_function")
)
)
