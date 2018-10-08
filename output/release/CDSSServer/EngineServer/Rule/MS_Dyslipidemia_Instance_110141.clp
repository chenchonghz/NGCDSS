(defrule MS_Dyslipidemia_Instance_110141_0
(filepath ?filepath)
(TC_Variable ?TC_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?TC_Variable 3.11 TC_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110141)
then
(undefrule *)
(InterpretationIndex "TC<3.11mmol/L，继续判断是否服用过调脂药。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_60041.clp"))
(FactUsed "TC_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_110141_1
(filepath ?filepath)
(TC_Variable ?TC_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?TC_Variable 3.11 TC_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110141)
then
(undefrule *)
(InterpretationIndex "TC>=3.11mmol/L，继续判断是否已经服用过调脂药。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_90020.clp"))
(FactUsed "TC_Variable")
)
)
