(defrule MS_Dyslipidemia_Instance_110179_0
(filepath ?filepath)
(fibrates_Drug ?fibrates_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?fibrates_Drug YES fibrates_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_110179)
then
(undefrule *)
(InterpretationIndex "û�з��ñ�����ҩ����б�����ҩ�����ƣ���1���¶��ڸ��顣")
(Recommendation "��ҽ��ָ����ѡ�ñ�����ҩ��")
(Recommendation "HDLch���η�ҩ�����������͡�ࣩ")
(OperateFact "HDLch_First_Drug" "YES")
(FactUsed "fibrates_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_110179_1
(filepath ?filepath)
(fibrates_Drug ?fibrates_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?fibrates_Drug YES fibrates_Drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110179)
then
(undefrule *)
(InterpretationIndex "�ѷ��ñ�����ҩ����ƽ��飬�����ᣬ��1���¶��ڸ��顣")
(Recommendation "������")
(Recommendation "HDLch�ѷ�ҩ��δ��ָ��")
(OperateFact "HDLch_Reach_Standard" "NO")
(FactUsed "fibrates_Drug")
)
)
