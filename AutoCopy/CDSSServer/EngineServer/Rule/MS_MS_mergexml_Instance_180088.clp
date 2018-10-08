(defrule MS_MS_mergexml_Instance_180088_0
(filepath ?filepath)
(Shuanggua_Drug ?Shuanggua_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_180088)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "继用二甲双胍1.5g/日")
(FactUsed "Shuanggua_Drug")
)
)


(defrule MS_MS_mergexml_Instance_180088_1
(filepath ?filepath)
(Shuanggua_Drug ?Shuanggua_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_180088)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "辅用二甲双胍1.5g/日")
(FactUsed "Shuanggua_Drug")
)
)
