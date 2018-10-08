(defrule MS_HUA_Instance_100083_0
(filepath ?filepath)
(UPh ?UPh)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?UPh 6.0 UPh))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100083)
then
(undefrule *)
(InterpretationIndex "尿Ph<6.0，建议1.加服碳酸氢纳；
2.定期复查，保持尿pH6~6.5。")
(Recommendation "加服碳酸氢纳")
(FactUsed "UPh")
)
)
