(defrule MS_DM_Instance_40022_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_40022)
then
(undefrule *)
(InterpretationIndex "һ��Ѫ��ֵ�쳣���ո�Ѫ��>=7mmol/L���ͺ�2СʱѪ��>=11.1mmol/L�����Ѫ��>=11.1mmol/L������OGTT��")
(load (str-cat ?filepath "MS_DM_Instance_40106.clp"))
(FactUsed "FBG_Variable" "twoHPBG_Variable")
)
)


(defrule MS_DM_Instance_40022_1
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath DM_Instance_40022)
then
(undefrule *)
(InterpretationIndex "Ѫ��ֵ��û���쳣���ո�Ѫ��>=7mmol/L���ͺ�2СʱѪ��>=11.1mmol/L�����Ѫ��>=11.1mmol/L�����ж��Ƿ�������")
(load (str-cat ?filepath "MS_DM_Instance_40051.clp"))
(FactUsed "FBG_Variable" "twoHPBG_Variable")
)
)
