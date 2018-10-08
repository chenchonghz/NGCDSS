(defrule MS_HUA_Instance_100045_0
(filepath ?filepath)
(HUA_Diagnose_Acute ?HUA_Diagnose_Acute)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?HUA_Diagnose_Acute HUAcute HUA_Diagnose_Acute))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_100045)
then
(undefrule *)
(InterpretationIndex "不是急性发作，继续判断是否尿尿酸>3.6mol/L。")
(load (str-cat ?filepath "MS_HUA_Instance_100048.clp"))
(FactUsed "HUA_Diagnose_Acute")
)
)


(defrule MS_HUA_Instance_100045_1
(filepath ?filepath)
(HUA_Diagnose_Acute ?HUA_Diagnose_Acute)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?HUA_Diagnose_Acute HUAcute HUA_Diagnose_Acute))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100045)
then
(undefrule *)
(InterpretationIndex "是急性发作，治疗方案建议：秋水仙碱；非甾体抗生药。")
(Recommendation "秋水仙碱；
非甾体抗炎药；")
(load (str-cat ?filepath "MS_HUA_Instance_100048.clp"))
(FactUsed "HUA_Diagnose_Acute")
)
)
