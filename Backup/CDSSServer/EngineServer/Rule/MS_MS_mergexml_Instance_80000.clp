(defrule MS_MS_mergexml_Instance_80000_0
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_80000)
then
(undefrule *)
(InterpretationIndex "血压正常")
(Recommendation "高血压:无")
(OperateFact "Hypertension_Diagnose" "Hypertension_Normal")
(FactUsed "Hypertension_History")
)
)
