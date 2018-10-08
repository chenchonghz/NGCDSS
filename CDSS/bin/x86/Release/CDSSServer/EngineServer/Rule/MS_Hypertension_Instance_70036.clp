(defrule MS_Hypertension_Instance_70036_0
(filepath ?filepath)
(hypertension_Diagnose_Stage ?hypertension_Diagnose_Stage)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?hypertension_Diagnose_Stage First_Stage hypertension_Diagnose_Stage))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?hypertension_Diagnose_Stage Second_Stage hypertension_Diagnose_Stage))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_70036)
then
(undefrule *)
(InterpretationIndex "高血压1级或2级，诊断为中危。")
(Recommendation "高血压危险度:中危")
(OperateFact "Hyperuricaemia_Diagnose_risklevel" "risk_medium")
(FactUsed "hypertension_Diagnose_Stage")
)
)


(defrule MS_Hypertension_Instance_70036_1
(filepath ?filepath)
(hypertension_Diagnose_Stage ?hypertension_Diagnose_Stage)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?hypertension_Diagnose_Stage Third_Stage hypertension_Diagnose_Stage))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_70036)
then
(undefrule *)
(InterpretationIndex "高血压3级，诊断为极高危。")
(Recommendation "高血压危险度:很高危")
(OperateFact "Hyperuricaemia_Diagnose_risklevel" "risk_very_high")
(FactUsed "hypertension_Diagnose_Stage")
)
)
