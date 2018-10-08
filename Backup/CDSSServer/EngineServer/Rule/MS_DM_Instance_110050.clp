(defrule MS_DM_Instance_110050_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110050)
then
(undefrule *)
(InterpretationIndex "BMI>23kg/m2，治疗方案为：皮下注射胰岛素治疗+二甲双胍。")
(Recommendation "二甲双胍+磺脲类口服降糖药(方案4)：
二甲双胍0.5 , 3次/d+
磺脲类(格列美脲 1mg, 1 次/d；
格列齐特缓释片(达美康MR)30mg, 1次/早；
格列吡嗪控释片(瑞易宁)， 5mg, 1次/日；
格列齐特 40mg  1 次/d
格列吡嗪 5mg, 1次/d;
格列喹酮 15mg, 3次/d
格列本脲 2.5mg-5  1次/d)或
非磺脲类胰岛素促泌剂(瑞格列奈 0.5mg, 3次/d)")
(FactUsed "BMI")
)
)


(defrule MS_DM_Instance_110050_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110050)
then
(undefrule *)
(InterpretationIndex "BMI<=23，继续判断是否BMI>=18.5。")
(load (str-cat ?filepath "MS_DM_Instance_110128.clp"))
(FactUsed "BMI")
)
)
