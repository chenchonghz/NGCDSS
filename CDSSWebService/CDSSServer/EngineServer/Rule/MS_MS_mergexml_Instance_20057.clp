(defrule MS_MS_mergexml_Instance_20057_0
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20057)
then
(undefrule *)
(InterpretationIndex "血尿酸控制欠佳（≥440 umol/L）者，开始饮食控制或加服降尿酸药物后2-4周复查血尿酸，以便调整治疗。")
(Recommendation "生活方式管理：
低嘌呤饮食；
痛风发作期嘌呤摄入量<250g/日
自我监测管理：
加服降尿酸药物后2-4周复查血尿酸，以便调整治疗。")
(FactUsed "UA_Variable")
)
)


(defrule MS_MS_mergexml_Instance_20057_1
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20057)
then
(undefrule *)
(InterpretationIndex "血尿酸控制良好（<440 umol/L）者，3-6月复查血尿酸。")
(Recommendation "生活方式管理：
低嘌呤饮食；
痛风发作期嘌呤摄入量<250g/日
自我监测管理：
3-6月复查血尿酸。")
(FactUsed "UA_Variable")
)
)
