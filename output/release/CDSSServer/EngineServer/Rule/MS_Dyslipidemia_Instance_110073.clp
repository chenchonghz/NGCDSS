(defrule MS_Dyslipidemia_Instance_110073_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110073)
then
(undefrule *)
(InterpretationIndex "��л�ۺ���Σ�ն���Σ������2-4���������ж��Ƿ�TC<5.18mmol/L��")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110096.clp"))
(FactUsed "risk_score")
)
)


(defrule MS_Dyslipidemia_Instance_110073_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110073)
then
(undefrule *)
(InterpretationIndex "��л�ۺ���Σ�նȲ�����Σ������2-4���������ж��Ƿ��л�ۺ���Σ�նȸ�Σ��")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110100.clp"))
(FactUsed "risk_score")
)
)
