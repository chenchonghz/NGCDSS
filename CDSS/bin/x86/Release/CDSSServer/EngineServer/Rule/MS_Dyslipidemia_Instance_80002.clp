(defrule MS_Dyslipidemia_Instance_80002_0
(filepath ?filepath)
(Dyslipidemia_TC_His ?Dyslipidemia_TC_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_TC_His YES Dyslipidemia_TC_His))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_80002)
then
(undefrule *)
(InterpretationIndex "�иߵ��̴���ʷ��ȷ��Ϊ�ߵ��̴�Ѫ֢�������ж��Ƿ��е͸��ܶ�֬����Ѫ֢��ʷ��")
(Recommendation "�ߵ��̴�Ѫ֢")
(OperateFact "Dyslipidemia_Diagnose_TC" "Dyslipidemia_TC")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_80013.clp"))
(FactUsed "Dyslipidemia_TC_His")
)
)


(defrule MS_Dyslipidemia_Instance_80002_1
(filepath ?filepath)
(Dyslipidemia_TC_His ?Dyslipidemia_TC_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_TC_His YES Dyslipidemia_TC_His))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_80002)
then
(undefrule *)
(InterpretationIndex "�޸ߵ��̴���ʷ�������ж��Ƿ��е͸��ܶ�֬����Ѫ֢��ʷ��")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_80013.clp"))
(FactUsed "Dyslipidemia_TC_His")
)
)
