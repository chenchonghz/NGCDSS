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
(InterpretationIndex "有糖尿病史，无1型糖尿病病史，继续判断是否为2型糖尿病史。")
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
(InterpretationIndex "有1型糖尿病史，确诊为1型糖尿病")
(Recommendation "糖尿病类型:1型糖尿病")
(OperateFact "DM_Diagnose" "T1DM")
(FactUsed "T1DMHis")
)
)
