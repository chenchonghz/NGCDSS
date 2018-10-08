(defrule MS_HUA_Instance_100023_0
(filepath ?filepath)
(HUA_Diagnose_PS ?HUA_Diagnose_PS)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?HUA_Diagnose_PS primary HUA_Diagnose_PS))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100023)
then
(undefrule *)
(InterpretationIndex "ԭ���������ж��Ƿ��Է�����")
(load (str-cat ?filepath "MS_HUA_Instance_100045.clp"))
(FactUsed "HUA_Diagnose_PS")
)
)


(defrule MS_HUA_Instance_100023_1
(filepath ?filepath)
(HUA_Diagnose_PS ?HUA_Diagnose_PS)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?HUA_Diagnose_PS secondary HUA_Diagnose_PS))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100023)
then
(undefrule *)
(InterpretationIndex "�Ǽ̷�����������ԭ�������������ж��Ƿ�Ϊ���Է�����")
(Recommendation "����ԭ����")
(load (str-cat ?filepath "MS_HUA_Instance_100045.clp"))
(FactUsed "HUA_Diagnose_PS")
)
)
