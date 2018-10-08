(defrule MS_Dyslipidemia_Instance_110018_0
(filepath ?filepath)
(LDLch_Variable ?LDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?LDLch_Variable 3.37 LDLch_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110018)
then
(undefrule *)
(InterpretationIndex "LDL-c<3.37mmol/L，继续判断是否服用过调脂类药。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_60026.clp"))
(FactUsed "LDLch_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_110018_1
(filepath ?filepath)
(LDLch_Variable ?LDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?LDLch_Variable 3.37 LDLch_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110018)
then
(undefrule *)
(InterpretationIndex "LDL-c>=3.37mmol/L，继续判断是否已用过调脂药。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_90028.clp"))
(FactUsed "LDLch_Variable")
)
)
