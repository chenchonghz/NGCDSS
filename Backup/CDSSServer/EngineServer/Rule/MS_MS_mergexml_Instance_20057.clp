(defrule MS_MS_mergexml_Instance_20057_0
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20057)
then
(undefrule *)
(InterpretationIndex "Ѫ�������Ƿ�ѣ���440 umol/L���ߣ���ʼ��ʳ���ƻ�ӷ�������ҩ���2-4�ܸ���Ѫ���ᣬ�Ա�������ơ�")
(Recommendation "���ʽ����
��������ʳ��
ʹ�緢��������������<250g/��
���Ҽ�����
�ӷ�������ҩ���2-4�ܸ���Ѫ���ᣬ�Ա�������ơ�")
(FactUsed "UA_Variable")
)
)


(defrule MS_MS_mergexml_Instance_20057_1
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20057)
then
(undefrule *)
(InterpretationIndex "Ѫ����������ã�<440 umol/L���ߣ�3-6�¸���Ѫ���ᡣ")
(Recommendation "���ʽ����
��������ʳ��
ʹ�緢��������������<250g/��
���Ҽ�����
3-6�¸���Ѫ���ᡣ")
(FactUsed "UA_Variable")
)
)
