(defrule MS_Dyslipidemia_Instance_100010_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_100010)
then
(undefrule *)
(InterpretationIndex "���ù���͡��ҩ�����ƿ�ѡ������
1.����ҩ�������
2.����������͡��ҩ�
3.�ӵ��̴��������Ƽ���
��1���¶��ڸ��顣")
(Recommendation "��ѡ������
1.��ǿЧ��͡�������淥��͡�����з���͡��
2.����ҩ�������
3.�ӵ��̴��������Ƽ�")
(Recommendation "LDLch�ѷ�ҩ��δ��ָ��")
(OperateFact "LDLch_Reach_Standard" "NO")
(FactUsed "statins_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_100010_1
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_100010)
then
(undefrule *)
(InterpretationIndex "�޷��ù���͡��ҩ����ƿ�ѡ������
1.������͡��ҩ�
2.�粻��������͡��ҩ��ӵ��̴��������Ƽ���
��1���¶��ڸ��顣")
(Recommendation "��ѡ������
1.������͡��ҩ�
2.�粻��������͡��ҩ������շ���͡,������͡,�ӵ��̴��������Ƽ���")
(Recommendation "LDLch���η�ҩ�����������͡�ࣩ")
(OperateFact "LDLch_First_Drug" "YES")
(FactUsed "statins_Drug")
)
)
