(defrule MS_Hypertension_Instance_110025_0
(filepath ?filepath)
(hypertension_Diagnose_Stage ?hypertension_Diagnose_Stage)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?hypertension_Diagnose_Stage pre_Stage hypertension_Diagnose_Stage))
(bind ?CIL021 (Leaf equals ?hypertension_Diagnose_Stage First_Stage hypertension_Diagnose_Stage))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_110025)
then
(undefrule *)
(InterpretationIndex "高血压<=1级，继续判断是否肾动脉狭窄(双)或肾功能异常。")
(load (str-cat ?filepath "MS_Hypertension_Instance_130046.clp"))
(FactUsed "hypertension_Diagnose_Stage")
)
)


(defrule MS_Hypertension_Instance_110025_1
(filepath ?filepath)
(hypertension_Diagnose_Stage ?hypertension_Diagnose_Stage)
=>
(bind ?Threshhold 0)

(bind ?COL020 (Leaf equals ?hypertension_Diagnose_Stage pre_Stage hypertension_Diagnose_Stage))
(bind ?COL021 (Leaf equals ?hypertension_Diagnose_Stage First_Stage hypertension_Diagnose_Stage))
(if
(or (Transform ?COL020)  (Transform ?COL021) )
then
(bind ?CON010 TRUE)
else
(bind ?CON010 NULL)
(bind ?CON010 (AddOrNot ?COL020 ?CON010))
(bind ?CON010 (AddOrNot ?COL021 ?CON010))
(if(eq ?CON010 NULL)
then
(bind ?CON010 FALSE)
)
)
(bind ?RO0 ?CON010)
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_110025)
then
(undefrule *)
(InterpretationIndex "高血压>1级，继续判断是否肾动脉狭窄(双)或肾功能异常。")
(load (str-cat ?filepath "MS_Hypertension_Instance_130048.clp"))
(FactUsed "hypertension_Diagnose_Stage")
)
)
