(defrule MS_MS_mergexml_Instance_110000_0
(filepath ?filepath)
(IFGHis ?IFGHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?IFGHis YES IFGHis))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_110000)
then
(undefrule *)
(InterpretationIndex "IFG��ʷ����ȷ��ΪIFG")
(Recommendation "����:��")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "�Ǵ�лˮƽ:IFG")
(OperateFact "DM_Diagnose" "IFG")
(FactUsed "IFGHis")
)
)


(defrule MS_MS_mergexml_Instance_110000_1
(filepath ?filepath)
(IFGHis ?IFGHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?IFGHis YES IFGHis))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_110000)
then
(undefrule *)
(InterpretationIndex "��IFG��ʷ�������ж��Ƿ�ΪIGT")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_120010.clp"))
(FactUsed "IFGHis")
)
)
