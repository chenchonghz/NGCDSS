(defrule MS_Hypertension_Instance_140001_0
(filepath ?filepath)
(Hypertension_Therapy_Suggestion_EVENT ?Hypertension_Therapy_Suggestion_EVENT)
=>
(if
(eq ?Hypertension_Therapy_Suggestion_EVENT on)
then
(undefrule *)
(InterpretationIndex "��Ѫѹ�������ʽ���������Ҽ��")
(Recommendation "���ʽ����
1.��ʳ���˶������ר��
2.����ƽ����̬�����⾫�����
3.��֤˯��6-8h/�գ����ˣ�
4.�����������")
(Recommendation "���Ҽ�����
1.Ѫѹ�ⶨ >=2��/��
2.���ʲⶨ>=2��/��
3.���زⶨ��1��ÿ��")))
