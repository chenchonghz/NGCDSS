(defrule MS_MS_mergexml_Instance_20016_0
(filepath ?filepath)
(Hypertension_Diagnose ?Hypertension_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Hypertension_Diagnose NO Hypertension_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_20016)
then
(undefrule *)
(InterpretationIndex "血压正常，自我血压监测并记录：血压测定1次/1-4周。
自我脉率测定并记录：脉率测定≥ 2次/日，可与血压测定同时进行。注意观察两侧腕部、足背动脉搏动频率及幅度是否均等。
体重测定：≥ 1次/周。")
(Recommendation "自我血压监测并记录：血压测定1次/1-4周。")
(Recommendation "自我脉率测定并记录：
脉率测定≥ 2次/日，可与血压测定同时进行。注意观察两侧腕部、足背动脉搏动频率及幅度是否均等。")
(Recommendation "体重测定  ≥ 1次/周。")
(FactUsed "Hypertension_Diagnose")
)
)


(defrule MS_MS_mergexml_Instance_20016_1
(filepath ?filepath)
(Hypertension_Diagnose ?Hypertension_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Hypertension_Diagnose NO Hypertension_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_20016)
then
(undefrule *)
(InterpretationIndex "高血压，自我血压监测并记录：血压测定≥ 2次/日。
自我脉率测定并记录：脉率测定≥ 2次/日，可与血压测定同时进行。注意观察两侧腕部、足背动脉搏动频率及幅度是否均等。
体重测定：≥ 1次/周。")
(Recommendation "自我血压监测并记录：≥ 2次/日。")
(Recommendation "自我脉率测定并记录：
脉率测定≥ 2次/日，可与血压测定同时进行。注意观察两侧腕部、足背动脉搏动频率及幅度是否均等。")
(Recommendation "体重测定  ≥ 1次/周。")
(FactUsed "Hypertension_Diagnose")
)
)
