(defrule MS_HUA_Instance_20007_0
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_20007)
then
(undefrule *)
(InterpretationIndex "有高尿酸血症病史，确诊为高尿酸血症。")
(Recommendation "高尿酸血症:有")
(OperateFact "HUA_Diagnose" "Hyperuricaemia")
(load (str-cat ?filepath "MS_HUA_Instance_120015.clp"))
(FactUsed "UA_Variable")
)
)


(defrule MS_HUA_Instance_20007_1
(filepath ?filepath)
(Hyperuricaemia_History ?Hyperuricaemia_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Hyperuricaemia_History YES Hyperuricaemia_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_20007)
then
(undefrule *)
(InterpretationIndex "没有高尿酸血症病史，查血尿酸。")
(load (str-cat ?filepath "MS_HUA_Instance_20011.clp"))
(FactUsed "Hyperuricaemia_History")
)
)
