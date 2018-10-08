(defrule MS_Hypertension_Instance_110006_0
(filepath ?filepath)
(SBP_Current_Variable ?SBP_Current_Variable)
(DBP_Current_Variable ?DBP_Current_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf < ?SBP_Current_Variable 150.0 SBP_Current_Variable))
(bind ?CIL021 (Leaf < ?DBP_Current_Variable 80.0 DBP_Current_Variable))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_110006)
then
(undefrule *)
(InterpretationIndex "SBP<150mmHg且DBP<80mmHg，继续判断是否服用过药物。")
(load (str-cat ?filepath "MS_Hypertension_Instance_110015.clp"))
(FactUsed "SBP_Current_Variable" "DBP_Current_Variable")
)
)


(defrule MS_Hypertension_Instance_110006_1
(filepath ?filepath)
(SBP_Current_Variable ?SBP_Current_Variable)
(DBP_Current_Variable ?DBP_Current_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?SBP_Current_Variable 150.0 SBP_Current_Variable))
(bind ?CIL021 (Leaf >= ?DBP_Current_Variable 80.0 DBP_Current_Variable))
(if
(or (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_110006)
then
(undefrule *)
(InterpretationIndex "SBP>=150mmHg或DBP>=80mmHg，继续判断是否IGT或IFG。")
(load (str-cat ?filepath "MS_Hypertension_Instance_130009.clp"))
(FactUsed "SBP_Current_Variable" "DBP_Current_Variable")
)
)
