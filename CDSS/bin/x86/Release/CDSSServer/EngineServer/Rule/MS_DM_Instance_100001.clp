(defrule MS_DM_Instance_100001_0
(filepath ?filepath)
(T1DMHis ?T1DMHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?T1DMHis YES T1DMHis))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath DM_Instance_100001)
then
(undefrule *)
(InterpretationIndex "������ʷ����1�����򲡲�ʷ�������ж��Ƿ�Ϊ2������ʷ��")
(load (str-cat ?filepath "MS_DM_Instance_100004.clp"))
(FactUsed "T1DMHis")
)
)


(defrule MS_DM_Instance_100001_1
(filepath ?filepath)
(T1DMHis ?T1DMHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?T1DMHis YES T1DMHis))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_100001)
then
(undefrule *)
(InterpretationIndex "��1������ʷ��ȷ��Ϊ1������")
(Recommendation "��������:1������")
(OperateFact "DM_Diagnose" "T1DM")
(FactUsed "T1DMHis")
)
)
