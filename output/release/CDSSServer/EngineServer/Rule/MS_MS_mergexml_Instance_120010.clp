(defrule MS_MS_mergexml_Instance_120010_0
(filepath ?filepath)
(IGTHis ?IGTHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?IGTHis YES IGTHis))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_120010)
then
(undefrule *)
(InterpretationIndex "IGT��ʷ����ȷ��ΪIGT")
(Recommendation "����:��")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "�Ǵ�лˮƽ:IGT")
(OperateFact "DM_Diagnose" "IGT")
(FactUsed "IGTHis")
)
)


(defrule MS_MS_mergexml_Instance_120010_1
(filepath ?filepath)
(IGTHis ?IGTHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?IGTHis YES IGTHis))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_120010)
then
(undefrule *)
(InterpretationIndex "��IGT��ʷ���ж��Ƿ�IGR")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_120018.clp"))
(FactUsed "IGTHis")
)
)
