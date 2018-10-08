(defrule MS_Dyslipidemia_Instance_110102_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110102)
then
(undefrule *)
(InterpretationIndex "��л�ۺ���Σ�նȵ�Σ������<2���������ж��Ƿ�LDL-c<4.14mmol/L��")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_90018.clp"))
(FactUsed "risk_score")
)
)


(defrule MS_Dyslipidemia_Instance_110102_1
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?risk_score 2.0 risk_score))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110102)
then
(undefrule *)
(InterpretationIndex "��л�ۺ���Σ�նȲ��ǵ�Σ������>=2���������жϴ�л�ۺ���Σ�ն��Ƿ���Σ��")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110013.clp"))
(FactUsed "risk_score")
)
)
