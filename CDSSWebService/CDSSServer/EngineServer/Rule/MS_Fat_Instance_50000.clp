(defrule MS_Fat_Instance_50000_0
(filepath ?filepath)
(Fat_History ?Fat_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Fat_History YES Fat_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Fat_Instance_50000)
then
(undefrule *)
(InterpretationIndex "没有肥胖病史，判断BMI和腰围。")
(load (str-cat ?filepath "MS_Fat_Instance_10000.clp"))
(FactUsed "Fat_History")
)
)


(defrule MS_Fat_Instance_50000_1
(filepath ?filepath)
(Fat_History ?Fat_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Fat_History YES Fat_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Fat_Instance_50000)
then
(undefrule *)
(InterpretationIndex "有肥胖病史，确诊为肥胖。")
(Recommendation "肥胖:有")
(OperateFact "Fat_Diagnose" "Fat")
(FactUsed "Fat_History")
)
)
