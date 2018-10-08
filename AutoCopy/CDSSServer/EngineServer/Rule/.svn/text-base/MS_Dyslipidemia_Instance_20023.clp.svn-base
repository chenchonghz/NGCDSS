(defrule MS_Dyslipidemia_Instance_20023_0
(filepath ?filepath)
(Sex ?Sex)
(HDLch_Variable ?HDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf equals ?Sex male Sex))
(bind ?CIL031 (Leaf < ?HDLch_Variable 1.0 HDLch_Variable))
(if
(and (Transform ?CIL030)  (Transform ?CIL031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIL031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL032 (Leaf equals ?Sex female Sex))
(bind ?CIL033 (Leaf < ?HDLch_Variable 1.3 HDLch_Variable))
(if
(and (Transform ?CIL032)  (Transform ?CIL033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIL032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIL033 ?CIN021))
(if(eq ?CIN021 NULL)
then
(bind ?CIN021 FALSE)
)
)
(if
(or (Transform ?CIN020)  (Transform ?CIN021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN021 ?CIN010))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_20023)
then
(undefrule *)
(InterpretationIndex "男性HDL-ch<1.0mmol/L或女性HDL-ch<1.3mmol/L，诊断为低高密度脂蛋白血症；继续判断LDL-ch。")
(Recommendation "低高密度脂蛋白血症")
(OperateFact "Dyslipidemia_Diagnose_HDLC" "Dyslipidemia_HDLch")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20035.clp"))
(FactUsed "Sex" "HDLch_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_20023_1
(filepath ?filepath)
(Sex ?Sex)
(HDLch_Variable ?HDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf equals ?Sex male Sex))
(bind ?CIL031 (Leaf < ?HDLch_Variable 1.0 HDLch_Variable))
(if
(and (Transform ?CIL030)  (Transform ?CIL031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIL031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL032 (Leaf equals ?Sex female Sex))
(bind ?CIL033 (Leaf < ?HDLch_Variable 1.3 HDLch_Variable))
(if
(and (Transform ?CIL032)  (Transform ?CIL033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIL032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIL033 ?CIN021))
(if(eq ?CIN021 NULL)
then
(bind ?CIN021 FALSE)
)
)
(if
(or (Transform ?CIN020)  (Transform ?CIN021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN021 ?CIN010))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_20023)
then
(undefrule *)
(InterpretationIndex "男性HDL-ch>=1.0mmol/L或女性HDL-ch>=1.3mmol/L，HDL-ch正常；继续判断LDL-ch。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20035.clp"))
(FactUsed "Sex" "HDLch_Variable")
)
)
