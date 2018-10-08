(defrule MS_DM_Instance_40081_0
(filepath ?filepath)
(ICA ?ICA)
(GAD65 ?GAD65)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?ICA YES ICA))
(bind ?CIL021 (Leaf equals ?GAD65 YES GAD65))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_40081)
then
(undefrule *)
(InterpretationIndex "ICA、GAD65中至少有一项阳性，诊断为1型糖尿病。")
(Recommendation "糖尿病类型:1型糖尿病")
(OperateFact "DM_Diagnose" "T1DM")
(FactUsed "ICA" "GAD65")
)
)


(defrule MS_DM_Instance_40081_1
(filepath ?filepath)
(ICA ?ICA)
(GAD65 ?GAD65)
=>
(bind ?Threshhold 0)

(bind ?COL020 (Leaf equals ?ICA YES ICA))
(bind ?COL021 (Leaf equals ?GAD65 YES GAD65))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath DM_Instance_40081)
then
(undefrule *)
(InterpretationIndex "ICA、GAD65均为阴性，难以分型。")
(Recommendation "糖尿病无法分型")
(OperateFact "DM_Diagnose" "DM_NoneType")
(FactUsed "ICA" "GAD65")
)
)
