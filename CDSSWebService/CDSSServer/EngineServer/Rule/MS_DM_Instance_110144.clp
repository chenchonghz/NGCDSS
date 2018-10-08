(defrule MS_DM_Instance_110144_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110144)
then
(undefrule *)
(InterpretationIndex "BMI>23，治疗方案为：二甲双胍。")
(Recommendation "二甲双胍(方案8)：
二甲双胍0.5 3次/d 或0.85, 2次/d")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110144_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110144)
then
(undefrule *)
(InterpretationIndex "BMI<=23，治疗方案为：阿卡波糖或磺脲类或非磺脲类胰岛素促泌剂或格咧酮类或AG")
(Recommendation "阿卡波糖或磺脲类或非磺脲类胰岛素促泌剂或格咧酮类或AGI(方案9)：
二甲双胍0.5, 3次/d或
短效磺脲类(格列喹酮 15mg, 3次/d)或
非磺脲类胰岛素促泌剂(瑞格列奈 0.5mg 3次/d
那格列奈 120mg, 3次/d)或
格咧酮类(吡格列酮  15mg, 1次/d
罗格列酮  4mg, 1次/d)或
AGI(阿卡波糖50mg, 3次/d
伏格列波糖，0.2mg, 3次
米格列醇 25mg, 3次/d)")
(FactUsed "BMI")
)
)
