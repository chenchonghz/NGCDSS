(defrule MS_Dyslipidemia_Instance_110013_0
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?risk_score 2.0 risk_score))
(bind ?CIL021 (Leaf < ?risk_score 4.0 risk_score))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110013)
then
(undefrule *)
(InterpretationIndex "代谢综合征危险度中危（评分2-4），继续判断是否LDL-c<3.37mmol/L。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110018.clp"))
(FactUsed "risk_score")
)
)


(defrule MS_Dyslipidemia_Instance_110013_1
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?risk_score 4.0 risk_score))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110013)
then
(undefrule *)
(InterpretationIndex "代谢综合征危险度不是中危（评分>=4或评分<2），继续判断是否代谢综合征危险度高危。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110112.clp"))
(FactUsed "risk_score")
)
)
