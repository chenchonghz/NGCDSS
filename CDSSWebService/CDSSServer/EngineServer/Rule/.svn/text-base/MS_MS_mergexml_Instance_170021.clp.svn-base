(defrule MS_MS_mergexml_Instance_170021_0
(filepath ?filepath)
(DM_Drug ?DM_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?DM_Drug YES DM_Drug))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170021)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_170027.clp"))
(FactUsed "DM_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170021_1
(filepath ?filepath)
(DM_Drug ?DM_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?DM_Drug YES DM_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_170021)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "噻唑烷二酮类或糖苷酶抑制剂或联合治疗")
(FactUsed "DM_Drug")
)
)
