(defrule MS_MSEvaluate_Instance_8_0
(filepath ?filepath)
(DM_Diagnose ?DM_Diagnose)
(Hypertension_Diagnose ?Hypertension_Diagnose)
(Dyslipidemia_Diagnose_TG ?Dyslipidemia_Diagnose_TG)
(Dyslipidemia_Diagnose_HDLC ?Dyslipidemia_Diagnose_HDLC)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?DM_Diagnose DM DM_Diagnose))
(bind ?CIL021 (Leaf equals ?DM_Diagnose T1DM DM_Diagnose))
(bind ?CIL022 (Leaf equals ?DM_Diagnose T2DM DM_Diagnose))
(bind ?CIL023 (Leaf equals ?DM_Diagnose IGT DM_Diagnose))
(bind ?CIL024 (Leaf equals ?DM_Diagnose IFG DM_Diagnose))
(bind ?CIL025 (Leaf equals ?DM_Diagnose IGR DM_Diagnose))
(bind ?CIL026 (Leaf equals ?DM_Diagnose DM_NoneType DM_Diagnose))
(if
(or (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022)  (Transform ?CIL023)  (Transform ?CIL024)  (Transform ?CIL025)  (Transform ?CIL026) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL022 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL023 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL024 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL025 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL026 ?CIN010))
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

(bind ?CIL110 (Leaf equals ?Hypertension_Diagnose Hypertension Hypertension_Diagnose))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf equals ?Dyslipidemia_Diagnose_TG Dyslipidemia_TG Dyslipidemia_Diagnose_TG))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf equals ?Dyslipidemia_Diagnose_HDLC Dyslipidemia_HDLch Dyslipidemia_Diagnose_HDLC))
(bind ?RI3 ?CIL310)
(if
(eq ?RI3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(bind ?ShortData (AddOrNot ?RI3 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 2 ?ShortData ?filepath MSEvaluate_Instance_8)
then
(undefrule *)
(InterpretationIndex "以下情况符合至少2项：高甘油三脂、低HDL、高血压、糖代谢异常，评估为有代谢综合征。")
(Recommendation "代谢综合征:有")
(OperateFact "Metabolic_Syndrome_Conclude" "Metabolic_Syndrome")
(FactUsed "DM_Diagnose" "Hypertension_Diagnose" "Dyslipidemia_Diagnose_TG" "Dyslipidemia_Diagnose_HDLC")
)
)


(defrule MS_MSEvaluate_Instance_8_1
(filepath ?filepath)
(TG_Variable ?TG_Variable)
(Sex ?Sex)
(HDLch_Variable ?HDLch_Variable)
(DBP_Top_Variable ?DBP_Top_Variable)
(SBP_Top_Variable ?SBP_Top_Variable)
(DM_Diagnose ?DM_Diagnose)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?TG_Variable 1.7 TG_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL130 (Leaf equals ?Sex male Sex))
(bind ?CIL131 (Leaf < ?HDLch_Variable 1.0 HDLch_Variable))
(if
(and (Transform ?CIL130)  (Transform ?CIL131) )
then
(bind ?CIN120 TRUE)
else
(bind ?CIN120 NULL)
(bind ?CIN120 (AddOrNot ?CIL130 ?CIN120))
(bind ?CIN120 (AddOrNot ?CIL131 ?CIN120))
(if(eq ?CIN120 NULL)
then
(bind ?CIN120 FALSE)
)
)
(bind ?CIL132 (Leaf equals ?Sex female Sex))
(bind ?CIL133 (Leaf < ?HDLch_Variable 1.3 HDLch_Variable))
(if
(and (Transform ?CIL132)  (Transform ?CIL133) )
then
(bind ?CIN121 TRUE)
else
(bind ?CIN121 NULL)
(bind ?CIN121 (AddOrNot ?CIL132 ?CIN121))
(bind ?CIN121 (AddOrNot ?CIL133 ?CIN121))
(if(eq ?CIN121 NULL)
then
(bind ?CIN121 FALSE)
)
)
(if
(or (Transform ?CIN120)  (Transform ?CIN121) )
then
(bind ?CIN110 TRUE)
else
(bind ?CIN110 NULL)
(bind ?CIN110 (AddOrNot ?CIN120 ?CIN110))
(bind ?CIN110 (AddOrNot ?CIN121 ?CIN110))
(if(eq ?CIN110 NULL)
then
(bind ?CIN110 FALSE)
)
)
(bind ?RI1 ?CIN110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL220 (Leaf >= ?DBP_Top_Variable 85.0 DBP_Top_Variable))
(bind ?CIL221 (Leaf >= ?SBP_Top_Variable 130.0 SBP_Top_Variable))
(if
(or (Transform ?CIL220)  (Transform ?CIL221) )
then
(bind ?CIN210 TRUE)
else
(bind ?CIN210 NULL)
(bind ?CIN210 (AddOrNot ?CIL220 ?CIN210))
(bind ?CIN210 (AddOrNot ?CIL221 ?CIN210))
(if(eq ?CIN210 NULL)
then
(bind ?CIN210 FALSE)
)
)
(bind ?RI2 ?CIN210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL320 (Leaf equals ?DM_Diagnose DM DM_Diagnose))
(bind ?CIL321 (Leaf equals ?DM_Diagnose T1DM DM_Diagnose))
(bind ?CIL322 (Leaf equals ?DM_Diagnose T2DM DM_Diagnose))
(bind ?CIL323 (Leaf equals ?DM_Diagnose IGT DM_Diagnose))
(bind ?CIL324 (Leaf equals ?DM_Diagnose IFG DM_Diagnose))
(bind ?CIL325 (Leaf equals ?DM_Diagnose IGR DM_Diagnose))
(bind ?CIL326 (Leaf equals ?DM_Diagnose DM_NoneType DM_Diagnose))
(if
(or (Transform ?CIL320)  (Transform ?CIL321)  (Transform ?CIL322)  (Transform ?CIL323)  (Transform ?CIL324)  (Transform ?CIL325)  (Transform ?CIL326) )
then
(bind ?CIN310 TRUE)
else
(bind ?CIN310 NULL)
(bind ?CIN310 (AddOrNot ?CIL320 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIL321 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIL322 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIL323 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIL324 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIL325 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIL326 ?CIN310))
(if(eq ?CIN310 NULL)
then
(bind ?CIN310 FALSE)
)
)
(bind ?RI3 ?CIN310)
(if
(eq ?RI3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(bind ?ShortData (AddOrNot ?RI3 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot < ?Threshhold 2 ?ShortData ?filepath MSEvaluate_Instance_8)
then
(undefrule *)
(InterpretationIndex "以下情况符合不足2项：高甘油三脂、低HDL、高血压、糖代谢异常，评估为无代谢综合征。")
(Recommendation "无代谢综合征")
(OperateFact "Metabolic_Syndrome_Conclude" "NO")
(FactUsed "TG_Variable" "Sex" "HDLch_Variable" "DBP_Top_Variable" "SBP_Top_Variable" "DM_Diagnose")
)
)
