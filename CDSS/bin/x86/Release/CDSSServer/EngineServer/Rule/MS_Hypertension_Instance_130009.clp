(defrule MS_Hypertension_Instance_130009_0
(filepath ?filepath)
(DM_Diagnose ?DM_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?DM_Diagnose IGT DM_Diagnose))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?DM_Diagnose IFG DM_Diagnose))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_130009)
then
(undefrule *)
(InterpretationIndex "IGT或IFG，建议不宜用β-B和利尿剂，并继续判断是否脉(心)率>80次/分。")
(Recommendation "不宜用贝特受体阻滞剂和利尿剂")
(load (str-cat ?filepath "MS_Hypertension_Instance_130021.clp"))
(FactUsed "DM_Diagnose")
)
)


(defrule MS_Hypertension_Instance_130009_1
(filepath ?filepath)
(DM_Diagnose ?DM_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?DM_Diagnose IGT DM_Diagnose))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf equals ?DM_Diagnose IFG DM_Diagnose))
(bind ?RO1 ?COL110)
(if
(eq ?RO1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(bind ?ShortData (AddOrNot ?RO1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_130009)
then
(undefrule *)
(InterpretationIndex "不是IGT和IFG，继续判断是否脉(心)率>80次/分。")
(load (str-cat ?filepath "MS_Hypertension_Instance_130021.clp"))
(FactUsed "DM_Diagnose")
)
)
