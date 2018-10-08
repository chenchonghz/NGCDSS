(defrule MS_Hypertension_Instance_130033_0
(filepath ?filepath)
(HUA_Diagnose ?HUA_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?HUA_Diagnose Hyperuricaemia HUA_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_130033)
then
(undefrule *)
(InterpretationIndex "�Ǹ�����Ѫ֢�����鲻ʹ����������������ж��Ƿ��Ѫѹ<=1����")
(Recommendation "��ʹ�������")
(load (str-cat ?filepath "MS_Hypertension_Instance_110025.clp"))
(FactUsed "HUA_Diagnose")
)
)


(defrule MS_Hypertension_Instance_130033_1
(filepath ?filepath)
(HUA_Diagnose ?HUA_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?HUA_Diagnose Hyperuricaemia HUA_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_130033)
then
(undefrule *)
(InterpretationIndex "���Ǹ�����Ѫ֢�������ж��Ƿ��Ѫѹ<=1����")
(load (str-cat ?filepath "MS_Hypertension_Instance_110025.clp"))
(FactUsed "HUA_Diagnose")
)
)
