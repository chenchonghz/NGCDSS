(defrule MS_MS_mergexml_Instance_170006_0
(filepath ?filepath)
(BMI ?BMI)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf <= ?BMI 23.0 BMI))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170006)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "àçßòÍé¶şÍªÀà»òÌÇÜÕÃ¸ÒÖÖÆ¼Á")
(FactUsed "BMI")
)
)


(defrule MS_MS_mergexml_Instance_170006_1
(filepath ?filepath)
(BMI ?BMI)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?BMI 23.0 BMI))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170006)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "¶ş¼×Ë«ëÒ»òÌÇÜÕÃ¸ÒÖÖÆ¼Á")
(FactUsed "BMI")
)
)
