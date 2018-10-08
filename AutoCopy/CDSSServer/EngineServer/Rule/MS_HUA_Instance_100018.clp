(defrule MS_HUA_Instance_100018_0
(filepath ?filepath)
(arthritis_flare ?arthritis_flare)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?arthritis_flare YES arthritis_flare))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100018)
then
(undefrule *)
(InterpretationIndex "�ؽں��ף�ȷ��Ϊ����ʹ��ؽ��ס�")
(Recommendation "����ʹ��ؽ���")
(OperateFact "HUA_Diagnose_Acute" "HUAcute")
(Recommendation "������Ѫ֢:��")
(OperateFact "HUA_Diagnose" "Hyperuricaemia")
(load (str-cat ?filepath "MS_HUA_Instance_120015.clp"))
(FactUsed "arthritis_flare")
)
)


(defrule MS_HUA_Instance_100018_1
(filepath ?filepath)
(arthritis_flare ?arthritis_flare)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?arthritis_flare YES arthritis_flare))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_100018)
then
(undefrule *)
(InterpretationIndex "�޹ؽں��ף�ȷ��Ϊ������Ѫ֢��")
(Recommendation "������Ѫ֢:��")
(OperateFact "HUA_Diagnose" "Hyperuricaemia")
(load (str-cat ?filepath "MS_HUA_Instance_120015.clp"))
(FactUsed "arthritis_flare")
)
)
