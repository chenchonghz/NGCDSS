(defrule MS_Dyslipidemia_Instance_110153_0
(filepath ?filepath)
(HDLch_Variable ?HDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?HDLch_Variable 1.0 HDLch_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110153)
then
(undefrule *)
(InterpretationIndex "HDL-c>1.0mmol/L，处于正常范围，继续判断是否已服用过调脂药。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_70030.clp"))
(FactUsed "HDLch_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_110153_1
(filepath ?filepath)
(HDLch_Variable ?HDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf <= ?HDLch_Variable 1.0 HDLch_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110153)
then
(undefrule *)
(InterpretationIndex "HDL-c<=1.0mmol/L，需要进行调整，继续判断是否已服用药物。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110160.clp"))
(FactUsed "HDLch_Variable")
)
)
