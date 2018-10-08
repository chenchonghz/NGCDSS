(defrule MS_MS_mergexml_Instance_120018_0
(filepath ?filepath)
(IGRHis ?IGRHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?IGRHis YES IGRHis))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_120018)
then
(undefrule *)
(InterpretationIndex "IGR病史，现确诊为IGR")
(Recommendation "糖尿病:有")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "糖代谢水平:IGR")
(OperateFact "DM_Diagnose" "IGR")
(FactUsed "IGRHis")
)
)


(defrule MS_MS_mergexml_Instance_120018_1
(filepath ?filepath)
(IGRHis ?IGRHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?IGRHis YES IGRHis))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_120018)
then
(undefrule *)
(InterpretationIndex "糖代谢异常，但未确诊，需进行分型")
(Recommendation "糖尿病:有")
(OperateFact "DM_Diagnose" "DM")
(load (str-cat ?filepath "MS_DM_Instance_40073.clp"))
(FactUsed "IGRHis")
)
)
