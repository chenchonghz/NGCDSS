(defrule MS_HUA_Instance_10008_0
(filepath ?filepath)
(Gout_History ?Gout_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Gout_History YES Gout_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_10008)
then
(undefrule *)
(InterpretationIndex "��ʹ�粡ʷ��ȷ��Ϊʹ�硣")
(Recommendation "ʹ��:��")
(OperateFact "HUA_Diagnose_Gouty" "Gouty")
(load (str-cat ?filepath "MS_HUA_Instance_100018.clp"))
(FactUsed "Gout_History")
)
)


(defrule MS_HUA_Instance_10008_1
(filepath ?filepath)
(Gout_History ?Gout_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Gout_History YES Gout_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_10008)
then
(undefrule *)
(InterpretationIndex "��ʹ�粡ʷ��ȷ�������Ѫ֢��")
(Recommendation "������Ѫ֢:��")
(OperateFact "HUA_Diagnose" "Hyperuricaemia")
(load (str-cat ?filepath "MS_HUA_Instance_120015.clp"))
(FactUsed "Gout_History")
)
)
