(defrule MS_Hypertension_Instance_130021_0
(filepath ?filepath)
(heart_rate ?heart_rate)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?heart_rate 80.0 heart_rate))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_130021)
then
(undefrule *)
(InterpretationIndex "脉(心)率>80次/分，建议加用BB，并继续判断是否有高尿酸血症。")
(Recommendation "加用贝特受体阻滞剂")
(load (str-cat ?filepath "MS_Hypertension_Instance_130033.clp"))
(FactUsed "heart_rate")
)
)


(defrule MS_Hypertension_Instance_130021_1
(filepath ?filepath)
(heart_rate ?heart_rate)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf > ?heart_rate 80.0 heart_rate))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_130021)
then
(undefrule *)
(InterpretationIndex "脉(心)率<=80次/分，继续判断是否有高尿酸血症。")
(load (str-cat ?filepath "MS_Hypertension_Instance_130033.clp"))
(FactUsed "heart_rate")
)
)
