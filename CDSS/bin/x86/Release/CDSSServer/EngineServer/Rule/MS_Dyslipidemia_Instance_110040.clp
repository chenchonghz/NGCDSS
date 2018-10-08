(defrule MS_Dyslipidemia_Instance_110040_0
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?risk_score 2.0 risk_score))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110040)
then
(undefrule *)
(InterpretationIndex "代谢综合征危险度低危（评分<2)，继续判断是否TC<6.22mmol/L。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110071.clp"))
(FactUsed "risk_score")
)
)


(defrule MS_Dyslipidemia_Instance_110040_1
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf < ?risk_score 2.0 risk_score))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_110040)
then
(undefrule *)
(InterpretationIndex "代谢综合征危险度不是低危（评分>=2），继续判断是否代谢综合征危险度中危。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110073.clp"))
(FactUsed "risk_score")
)
)
