(defrule MS_Hypertension_Instance_80000_0
(filepath ?filepath)
(Hypertension_History ?Hypertension_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Hypertension_History YES Hypertension_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_80000)
then
(undefrule *)
(InterpretationIndex "有高血压病史，诊断为有高血压。")
(Recommendation "高血压:有")
(OperateFact "Hypertension_Diagnose" "Hypertension")
(load (str-cat ?filepath "MS_Hypertension_Instance_4.clp"))
(FactUsed "Hypertension_History")
)
)


(defrule MS_Hypertension_Instance_80000_1
(filepath ?filepath)
(Hypertension_History ?Hypertension_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Hypertension_History YES Hypertension_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_80000)
then
(undefrule *)
(InterpretationIndex "没有高血压病史，判断血压情况。")
(load (str-cat ?filepath "MS_Hypertension_Instance_4.clp"))
(FactUsed "Hypertension_History")
)
)
