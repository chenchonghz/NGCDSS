(defrule MS_MS_mergexml_Instance_20016_0
(filepath ?filepath)
(Hypertension_Diagnose ?Hypertension_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Hypertension_Diagnose NO Hypertension_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20016)
then
(undefrule *)
(InterpretationIndex "Ѫѹ����������Ѫѹ��Ⲣ��¼��Ѫѹ�ⶨ1��/1-4�ܡ�
�������ʲⶨ����¼�����ʲⶨ�� 2��/�գ�����Ѫѹ�ⶨͬʱ���С�ע��۲������󲿡��㱳��������Ƶ�ʼ������Ƿ���ȡ�
���زⶨ���� 1��/�ܡ�")
(Recommendation "����Ѫѹ��Ⲣ��¼��Ѫѹ�ⶨ1��/1-4�ܡ�")
(Recommendation "�������ʲⶨ����¼��
���ʲⶨ�� 2��/�գ�����Ѫѹ�ⶨͬʱ���С�ע��۲������󲿡��㱳��������Ƶ�ʼ������Ƿ���ȡ�")
(Recommendation "���زⶨ  �� 1��/�ܡ�")
(FactUsed "Hypertension_Diagnose")
)
)


(defrule MS_MS_mergexml_Instance_20016_1
(filepath ?filepath)
(Hypertension_Diagnose ?Hypertension_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Hypertension_Diagnose NO Hypertension_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_20016)
then
(undefrule *)
(InterpretationIndex "��Ѫѹ������Ѫѹ��Ⲣ��¼��Ѫѹ�ⶨ�� 2��/�ա�
�������ʲⶨ����¼�����ʲⶨ�� 2��/�գ�����Ѫѹ�ⶨͬʱ���С�ע��۲������󲿡��㱳��������Ƶ�ʼ������Ƿ���ȡ�
���زⶨ���� 1��/�ܡ�")
(Recommendation "����Ѫѹ��Ⲣ��¼���� 2��/�ա�")
(Recommendation "�������ʲⶨ����¼��
���ʲⶨ�� 2��/�գ�����Ѫѹ�ⶨͬʱ���С�ע��۲������󲿡��㱳��������Ƶ�ʼ������Ƿ���ȡ�")
(Recommendation "���زⶨ  �� 1��/�ܡ�")
(FactUsed "Hypertension_Diagnose")
)
)
