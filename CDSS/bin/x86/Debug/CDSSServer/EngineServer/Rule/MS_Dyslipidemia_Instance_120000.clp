(defrule MS_Dyslipidemia_Instance_120000_0
(filepath ?filepath)
(TG_Variable ?TG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?TG_Variable 1.5 TG_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_120000)
then
(undefrule *)
(InterpretationIndex "TG<1.5mmol/L，继续判断是否服用过调脂药。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_60002.clp"))
(FactUsed "TG_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_120000_1
(filepath ?filepath)
(TG_Variable ?TG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?TG_Variable 1.5 TG_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_120000)
then
(undefrule *)
(InterpretationIndex "TG>=1.5mmol/L，继续判断是否服用过调脂药。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_120006.clp"))
(FactUsed "TG_Variable")
)
)
