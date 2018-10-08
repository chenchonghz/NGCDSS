(defrule MS_DM_Instance_110158_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110158)
then
(undefrule *)
(InterpretationIndex "BMI>23，治疗方案为：二甲双胍I+中短效磺脲类或非磺脲类胰岛素促泌剂或AGI(方案6)。")
(Recommendation "二甲双胍I+中短效磺脲类或非磺脲类胰岛素促泌剂或AGI(方案6)：
二甲双胍0.5 3次/日+
中短效磺脲类(格列吡嗪 5mg, 1次/d;格列喹酮15mg, 1次/d)
或非磺脲类胰岛素促泌剂
(瑞格列奈 0.5mg 3次/d
那格列奈 120mg , 3 次/d )
或AGI(阿卡波糖50mg, 3次/d
伏格列波糖，0.2mg, 3次
米格列醇 25mg, 3次/d)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110158_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110158)
then
(undefrule *)
(InterpretationIndex "BMI<=23，治疗方案为：阿卡波糖+中短效磺脲类或非磺脲类胰岛素促泌剂或格咧酮类(方案7)。")
(Recommendation "阿卡波糖+中短效磺脲类或非磺脲类胰岛素促泌剂或格咧酮类(方案7)：
二甲双胍 0.25~0.5, 3次/d+
中短效磺脲类(格列吡嗪 5mg,1次/d;
格列喹酮15mg, 1次/d)或
非磺脲类胰岛素促泌剂(瑞格列奈 0.5mg, 3次/d
那格列奈 120mg, 3次/d)或
格咧酮类(吡格列酮  15mg, 1次/d
罗格列酮  4mg, 1次/d)")
(FactUsed "BMI")
)
)
