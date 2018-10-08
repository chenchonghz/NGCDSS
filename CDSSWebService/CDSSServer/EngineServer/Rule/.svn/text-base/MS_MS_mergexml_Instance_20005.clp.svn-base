(defrule MS_MS_mergexml_Instance_20005_0
(filepath ?filepath)
(HbA1c ?HbA1c)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?HbA1c 6.5 HbA1c))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20005)
then
(undefrule *)
(InterpretationIndex "HbA1c<6.5%，自我监测建议监测早、晚餐前血糖，2-3天/周，并继续判断血压测定是否正常。")
(Recommendation "自我监测管理：
监测早、晚餐前血糖，2-3天/周。
以后根据血糖控制情况及平时生活、工作变化情况调整血糖测定次数。")
(Recommendation "糖尿病随访建议：
随访频率，1次/3-6月；可根据血糖控制情况调整随访时间和次数。")
(FactUsed "HbA1c")
)
)


(defrule MS_MS_mergexml_Instance_20005_1
(filepath ?filepath)
(HbA1c ?HbA1c)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?HbA1c 6.5 HbA1c))
(bind ?CIL021 (Leaf <= ?HbA1c 8.0 HbA1c))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20005)
then
(undefrule *)
(InterpretationIndex "HbA1c6.5-8%，监测三餐前、晚睡前血糖  ，2-4天/周，并继续进行血压测定。")
(Recommendation "自我监测管理：
监测三餐前、晚睡前血糖，2-4天/周。
以后根据血糖控制情况及平时生活、工作变化情况调整血糖测定次数。")
(Recommendation "糖尿病随访建议：
随访频率，1次/4-8周；可根据血糖控制情况调整随访时间和次数。")
(FactUsed "HbA1c")
)
)


(defrule MS_MS_mergexml_Instance_20005_2
(filepath ?filepath)
(HbA1c ?HbA1c)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?HbA1c 8.0 HbA1c))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20005)
then
(undefrule *)
(InterpretationIndex "HbA1c>8%，监测三餐前、晚睡前血糖，3-4天/周 +早餐前、三餐后2h血糖2-3天/周（与上交替），并继续进行血压测定。")
(Recommendation "自我监测管理：
监测三餐前、晚睡前血糖，3-4天/周 +早餐前、三餐后2h血糖2-3天/周（与上交替）。
以后根据血糖控制情况及平时生活、工作变化情况调整血糖测定次数。")
(Recommendation "糖尿病随访建议：
随访频率，1次/2-4周；可根据血糖控制情况调整随访时间和次数。")
(FactUsed "HbA1c")
)
)
