(defrule MS_MS_mergexml_Instance_170002_0
(filepath ?filepath)
(abnormal_renal_function ?abnormal_renal_function)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?abnormal_renal_function NO abnormal_renal_function))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170002)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_170004.clp"))
(FactUsed "abnormal_renal_function")
)
)
