(defrule MS_DM_Instance_40023_0
(filepath ?filepath)
(FBG_Variable ?FBG_Variable)
(twoHPBG_Variable ?twoHPBG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_Variable 7.0 FBG_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?twoHPBG_Variable 11.1 twoHPBG_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 2 ?ShortData ?filepath DM_Instance_40023)
then
(undefrule *)
(InterpretationIndex "至少有两次血糖值异常（空腹血糖>=7mmol/L、餐后2小时血糖>=11.1mmol/L、随机血糖>=11.1mmol/L），确诊为糖尿病，继续进行分型。")
(Recommendation "糖尿病:有")
(OperateFact "DM_Diagnose" "DM")
(load (str-cat ?filepath "MS_DM_Instance_40073.clp"))
(FactUsed "FBG_Variable" "twoHPBG_Variable")
)
)


(defrule MS_DM_Instance_40023_1
(filepath ?filepath)
(FBG_Variable ?FBG_Variable)
(twoHPBG_Variable ?twoHPBG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_Variable 7.0 FBG_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?twoHPBG_Variable 11.1 twoHPBG_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot < ?Threshhold 2 ?ShortData ?filepath DM_Instance_40023)
then
(undefrule *)
(InterpretationIndex "少于两个血糖值异常，继续判断是否至少有一个血糖值异常。")
(load (str-cat ?filepath "MS_DM_Instance_40022.clp"))
(FactUsed "FBG_Variable" "twoHPBG_Variable")
)
)
