(defrule MS_Hypertension_Instance_140008_0
(filepath ?filepath)
(SBP_Current_Variable ?SBP_Current_Variable)
(DBP_Current_Variable ?DBP_Current_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?SBP_Current_Variable 120.0 SBP_Current_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf < ?DBP_Current_Variable 80.0 DBP_Current_Variable))
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
(if(NotifyOrNot equals ?Threshhold 2 ?ShortData ?filepath Hypertension_Instance_140008)
then
(undefrule *)
(InterpretationIndex "SBP<120mmHg且DBP<80mmHg，继续判断是否服用过降压药。")
(load (str-cat ?filepath "MS_Hypertension_Instance_110015.clp"))
(FactUsed "SBP_Current_Variable" "DBP_Current_Variable")
)
)


(defrule MS_Hypertension_Instance_140008_1
(filepath ?filepath)
(SBP_Current_Variable ?SBP_Current_Variable)
(DBP_Current_Variable ?DBP_Current_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?SBP_Current_Variable 120.0 SBP_Current_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?DBP_Current_Variable 80.0 DBP_Current_Variable))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_140008)
then
(undefrule *)
(InterpretationIndex "SBP>=120mmHg或DBP>=80mmHg，继续判断是否有IGT或IFG。")
(load (str-cat ?filepath "MS_Hypertension_Instance_130009.clp"))
(FactUsed "SBP_Current_Variable" "DBP_Current_Variable")
)
)
