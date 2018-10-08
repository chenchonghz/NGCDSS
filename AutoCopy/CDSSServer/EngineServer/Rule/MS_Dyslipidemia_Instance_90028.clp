(defrule MS_Dyslipidemia_Instance_90028_0
(filepath ?filepath)
(Dyslipidemia_Drug ?Dyslipidemia_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_Drug YES Dyslipidemia_Drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_90028)
then
(undefrule *)
(InterpretationIndex "�ѷ��ù���֬ҩ�������ж��Ƿ������͡��ҩ�")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_100010.clp"))
(FactUsed "Dyslipidemia_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_90028_1
(filepath ?filepath)
(Dyslipidemia_Drug ?Dyslipidemia_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Dyslipidemia_Drug YES Dyslipidemia_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_90028)
then
(undefrule *)
(InterpretationIndex "û�з��ù���֬ҩ�����������͡��ҩ��շ���͡��������͡���工��͡��������͡��ƥ������͡�����з���͡�����淥��͡����1���¶��ڸ��顣")
(Recommendation "��ҽ��ָ����ѡ�ñ�����ҩ��")
(Recommendation "LDLch���η�ҩ�����������͡�ࣩ")
(OperateFact "LDLch_First_Drug" "YES")
(FactUsed "Dyslipidemia_Drug")
)
)
