(defrule MS_HUA_Instance_100048_0
(filepath ?filepath)
(UUA ?UUA)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf <= ?UUA 3.6 UUA))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100048)
then
(undefrule *)
(InterpretationIndex "尿尿酸<=3.6mol/L，继续判断是否肾功能正常且年龄<60。")
(load (str-cat ?filepath "MS_HUA_Instance_100051.clp"))
(FactUsed "UUA")
)
)


(defrule MS_HUA_Instance_100048_1
(filepath ?filepath)
(UUA ?UUA)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?UUA 3.6 UUA))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100048)
then
(undefrule *)
(InterpretationIndex "尿尿酸>3.6mol/L，治疗建议为：1.饮食指导；2.加服别嘌呤醇；")
(Recommendation "加服别嘌呤醇；")
(FactUsed "UUA")
)
)
