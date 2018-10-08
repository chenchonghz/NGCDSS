(defrule MS_MSEvaluate_Instance_2_0
(filepath ?filepath)
(Fat_Diagnose ?Fat_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Fat_Diagnose Fat Fat_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MSEvaluate_Instance_2)
then
(undefrule *)
(InterpretationIndex "有中心性肥胖（BMI>=25或腰围过大：女性>=80cm，男性>=90cm），继续判断血脂、血压和糖代谢。")
(load (str-cat ?filepath "MS_MSEvaluate_Instance_8.clp"))
(FactUsed "Fat_Diagnose")
)
)


(defrule MS_MSEvaluate_Instance_2_1
(filepath ?filepath)
(Fat_Diagnose ?Fat_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Fat_Diagnose Fat Fat_Diagnose))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MSEvaluate_Instance_2)
then
(undefrule *)
(InterpretationIndex "无中心性肥胖（BMI>=25或腰围过大：女性>=80cm，男性>=90cm），评估为无代谢综合征。")
(Recommendation "无代谢综合征")
(OperateFact "Metabolic_Syndrome_Conclude" "NO")
(FactUsed "Fat_Diagnose")
)
)
