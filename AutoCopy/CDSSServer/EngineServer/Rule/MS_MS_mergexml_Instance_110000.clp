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
(InterpretationIndex "IFG病史，现确诊为IFG")
(Recommendation "糖尿病:有")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "糖代谢水平:IFG")
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
(InterpretationIndex "无IFG病史，继续判断是否为IGT")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_120010.clp"))
(FactUsed "IFGHis")
)
)
