(defrule MS_Hypertension_Instance_110015_0
(filepath ?filepath)
(HT_Down_drug ?HT_Down_drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?HT_Down_drug YES HT_Down_drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_110015)
then
(undefrule *)
(InterpretationIndex "�Ѿ����ù���ѹҩ������ά��ԭ�����Ʒ����������ڸ��飨3����-6���£���")
(Recommendation "ά��ԭ�����Ʒ���")
(FactUsed "HT_Down_drug")
)
)
