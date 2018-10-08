(defrule MS_MS_mergexml_Instance_60041_0
(filepath ?filepath)
(Dyslipidemia_Drug ?Dyslipidemia_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_Drug YES Dyslipidemia_Drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_60041)
then
(undefrule *)
(InterpretationIndex "已经服用过药物，维持原有治疗方案，并2-3个月定期复查。")
(Recommendation "维持原有治疗方案")
(Recommendation "TC达标")
(OperateFact "TC_Reach_Standard" "YES")
(FactUsed "Dyslipidemia_Drug")
)
)


(defrule MS_MS_mergexml_Instance_60041_1
(filepath ?filepath)
(Dyslipidemia_Drug ?Dyslipidemia_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Dyslipidemia_Drug YES Dyslipidemia_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_60041)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "TC达标")
(OperateFact "TC_Reach_Standard" "YES")
(FactUsed "Dyslipidemia_Drug")
)
)
