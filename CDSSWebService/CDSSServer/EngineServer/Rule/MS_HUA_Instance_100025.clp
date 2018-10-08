(defrule MS_HUA_Instance_100025_0
(filepath ?filepath)
(UA_Down_drug ?UA_Down_drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?UA_Down_drug YES UA_Down_drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100025)
then
(undefrule *)
(InterpretationIndex "已服用降尿酸药，建议维持治疗。")
(Recommendation "维持原有药物治疗")
(FactUsed "UA_Down_drug")
)
)
