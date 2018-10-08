(defrule MS_Dyslipidemia_Instance_110170_0
(filepath ?filepath)
(statins_Drug ?statins_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?statins_Drug YES statins_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_110170)
then
(undefrule *)
(InterpretationIndex "û�з�����͡��ҩ������ж��Ƿ���ñ�����ҩ�")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110179.clp"))
(FactUsed "statins_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_110170_1
(filepath ?filepath)
(statins_Drug ?statins_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?statins_Drug YES statins_Drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110170)
then
(undefrule *)
(InterpretationIndex "�Ѿ����ù���͡��ҩ���ѡ����Ϊ������HDLҩ�������淥��͡�������ᣬ��1���¶��ڸ��顣")
(Recommendation "��ѡ������
����HDLҩ�������淥��͡�������ᡣ")
(Recommendation "HDLch�ѷ�ҩ��δ��ָ��")
(OperateFact "HDLch_Reach_Standard" "NO")
(FactUsed "statins_Drug")
)
)
