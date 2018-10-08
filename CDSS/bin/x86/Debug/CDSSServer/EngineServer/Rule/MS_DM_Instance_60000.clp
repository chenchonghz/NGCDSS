(defrule MS_DM_Instance_60000_0
(filepath ?filepath)
(DM_History ?DM_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?DM_History YES DM_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath DM_Instance_60000)
then
(undefrule *)
(InterpretationIndex "没有糖尿病史，继续判断是否至少有两个血糖值异常。")
(load (str-cat ?filepath "MS_DM_Instance_40023.clp"))
(FactUsed "DM_History")
)
)


(defrule MS_DM_Instance_60000_1
(filepath ?filepath)
(DM_History ?DM_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?DM_History YES DM_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_60000)
then
(undefrule *)
(InterpretationIndex "有糖尿病史，继续判断是否为1型糖尿病史。")
(load (str-cat ?filepath "MS_DM_Instance_100001.clp"))
(FactUsed "DM_History")
)
)
