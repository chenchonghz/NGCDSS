(defrule MS_MS_mergexml_Instance_90010_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_90010)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "��ѡ������
1.��ǿЧ��͡�������淥��͡�����з���͡��
2.����ҩ�������
3.�ӵ��̴��������Ƽ�")
(Recommendation "TC�ѷ�ҩ��δ��ָ��")
(OperateFact "TC_Reach_Standard" "NO")
(FactUsed "statins_Drug")
)
)


(defrule MS_MS_mergexml_Instance_90010_1
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_90010)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "��ѡ������
1.������͡��ҩ�
2.�粻��������͡��ҩ������շ���͡,������͡,�ӵ��̴��������Ƽ���")
(Recommendation "TC���η�ҩ�����������͡�ࣩ")
(OperateFact "TC_First_Drug" "YES")
(FactUsed "statins_Drug")
)
)
