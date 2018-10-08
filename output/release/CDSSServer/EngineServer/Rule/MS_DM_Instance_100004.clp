(defrule MS_DM_Instance_100004_0
(filepath ?filepath)
(T2DMHis ?T2DMHis)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?T2DMHis YES T2DMHis))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath DM_Instance_100004)
then
(undefrule *)
(InterpretationIndex "无2型糖尿病史，有糖尿病史，继续判断是否为IFG病史。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_110000.clp"))
(FactUsed "T2DMHis")
)
)


(defrule MS_DM_Instance_100004_1
(filepath ?filepath)
(T2DMHis ?T2DMHis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?T2DMHis YES T2DMHis))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_100004)
then
(undefrule *)
(InterpretationIndex "有2型糖尿病史，确诊为2型糖尿病。")
(Recommendation "糖尿病类型:2型糖尿病")
(OperateFact "DM_Diagnose" "T2DM")
(FactUsed "T2DMHis")
)
)
