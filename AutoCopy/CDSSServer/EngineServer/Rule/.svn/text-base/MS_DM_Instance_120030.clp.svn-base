(defrule MS_DM_Instance_120030_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_120030)
then
(undefrule *)
(InterpretationIndex "BMI>23，治疗方案为：饮食、运动+二甲双胍或AGI。")
(Recommendation "饮食、运动+二甲双胍或AGI(方案10)：
二甲双胍0.5 2-3次/d
或AGI(阿卡波糖50mg, 3次/d
伏格列波糖0.2mg,  3次
米格列醇 25mg, 3次/d)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_120030_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_120030)
then
(undefrule *)
(InterpretationIndex "BMI<=23，治疗方案为：饮食、运动或噻唑烷二酮类或AGI。")
(Recommendation "饮食、运动或噻唑烷二酮类或AGI(方案11)：
噻唑烷二酮类(吡格列酮  15mg, 1次/d
罗格列酮  4mg, 1次/d)或
AGI(阿卡波糖50mg, 3次/d
伏格列波糖，0.2mg, 3次
米格列醇 25mg, 3次/d)")
(FactUsed "BMI")
)
)
