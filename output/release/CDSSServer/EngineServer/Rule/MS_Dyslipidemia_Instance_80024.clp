(defrule MS_Dyslipidemia_Instance_80024_0
(filepath ?filepath)
(Dyslipidemia_LDLch_His ?Dyslipidemia_LDLch_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_LDLch_His YES Dyslipidemia_LDLch_His))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_80024)
then
(undefrule *)
(InterpretationIndex "�иߵ��ܶ�֬����Ѫ֢��ʷ�����Ϊ�ߵ��ܶ�֬����Ѫ֢��")
(Recommendation "�ߵ��ܶ�֬����Ѫ֢")
(OperateFact "Dyslipidemia_Diagnose_LDLC" "Dyslipidemia_LDLch")
(FactUsed "Dyslipidemia_LDLch_His")
)
)
