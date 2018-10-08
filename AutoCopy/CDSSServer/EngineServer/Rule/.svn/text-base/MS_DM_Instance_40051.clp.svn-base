(defrule MS_DM_Instance_40051_0
(filepath ?filepath)
(FBG_Variable ?FBG_Variable)
(twoHPBG_Variable ?twoHPBG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_Variable 6.1 FBG_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?twoHPBG_Variable 7.8 twoHPBG_Variable))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath DM_Instance_40051)
then
(undefrule *)
(InterpretationIndex "血糖值没有都正常（空腹血糖<6.1mmol/L、餐后2小时血糖<7.8mmol/L、随机血糖<7.8mmol/L），行OGTT。")
(load (str-cat ?filepath "MS_DM_Instance_40106.clp"))
(FactUsed "FBG_Variable" "twoHPBG_Variable")
)
)


(defrule MS_DM_Instance_40051_1
(filepath ?filepath)
(FBG_Variable ?FBG_Variable)
(twoHPBG_Variable ?twoHPBG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_Variable 6.1 FBG_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?twoHPBG_Variable 7.8 twoHPBG_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath DM_Instance_40051)
then
(undefrule *)
(InterpretationIndex "血糖值都正常（空腹血糖<6.1mmol/L、餐后2小时血糖<7.8mmol/L、随机血糖<7.8mmol/L），诊断没有糖尿病。")
(Recommendation "无糖尿病")
(OperateFact "DM_Diagnose" "DM_Normal")
(FactUsed "FBG_Variable" "twoHPBG_Variable")
)
)
