(defrule MS_Hypertension_Instance_70028_0
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_70028)
then
(undefrule *)
(InterpretationIndex "高血压1级，诊断为低危。")
(Recommendation "高血压危险度:低危")
(OperateFact "Hyperuricaemia_Diagnose_risklevel" "risk_low")
(FactUsed "hypertension_Diagnose_Stage")
)
)


(defrule MS_Hypertension_Instance_70028_1
(filepath ?filepath)
(hypertension_Diagnose_Stage ?hypertension_Diagnose_Stage)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?hypertension_Diagnose_Stage Second_Stage hypertension_Diagnose_Stage))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_70028)
then
(undefrule *)
(InterpretationIndex "高血压2级，诊断为中危。")
(Recommendation "高血压危险度:中危")
(OperateFact "Hyperuricaemia_Diagnose_risklevel" "risk_medium")
(FactUsed "hypertension_Diagnose_Stage")
)
)


(defrule MS_Hypertension_Instance_70028_2
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_70028)
then
(undefrule *)
(InterpretationIndex "高血压3级，诊断为高危。")
(Recommendation "高血压危险度:高危")
(OperateFact "Hyperuricaemia_Diagnose_risklevel" "risk_high")
(FactUsed "hypertension_Diagnose_Stage")
)
)
