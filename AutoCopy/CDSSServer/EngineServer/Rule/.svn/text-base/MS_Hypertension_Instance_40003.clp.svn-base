(defrule MS_Hypertension_Instance_40003_0
(filepath ?filepath)
(Sex ?Sex)
(Cr_Variable ?Cr_Variable)
(ACr_Variable ?ACr_Variable)
(DM_History ?DM_History)
(Cerebrovascular_Disease_History ?Cerebrovascular_Disease_History)
(Cardiopathy_History ?Cardiopathy_History)
(Peripheral_Vascular_Disease_History ?Peripheral_Vascular_Disease_History)
(Retinopathy_History ?Retinopathy_History)
(Nephropathy ?Nephropathy)
=>
(bind ?Threshhold 0)

(bind ?CIL040 (Leaf equals ?Sex male Sex))
(bind ?CIL041 (Leaf > ?Cr_Variable 115.0 Cr_Variable))
(bind ?CIL042 (Leaf <= ?Cr_Variable 133.0 Cr_Variable))
(if
(and (Transform ?CIL040)  (Transform ?CIL041)  (Transform ?CIL042) )
then
(bind ?CIN030 TRUE)
else
(bind ?CIN030 NULL)
(bind ?CIN030 (AddOrNot ?CIL040 ?CIN030))
(bind ?CIN030 (AddOrNot ?CIL041 ?CIN030))
(bind ?CIN030 (AddOrNot ?CIL042 ?CIN030))
(if(eq ?CIN030 NULL)
then
(bind ?CIN030 FALSE)
)
)
(bind ?CIL043 (Leaf equals ?Sex female Sex))
(bind ?CIL044 (Leaf > ?Cr_Variable 107.0 Cr_Variable))
(bind ?CIL045 (Leaf <= ?Cr_Variable 124.0 Cr_Variable))
(if
(and (Transform ?CIL043)  (Transform ?CIL044)  (Transform ?CIL045) )
then
(bind ?CIN031 TRUE)
else
(bind ?CIN031 NULL)
(bind ?CIN031 (AddOrNot ?CIL043 ?CIN031))
(bind ?CIN031 (AddOrNot ?CIL044 ?CIN031))
(bind ?CIN031 (AddOrNot ?CIL045 ?CIN031))
(if(eq ?CIN031 NULL)
then
(bind ?CIN031 FALSE)
)
)
(bind ?CIL046 (Leaf equals ?Sex male Sex))
(bind ?CIL047 (Leaf >= ?ACr_Variable 31.0 ACr_Variable))
(if
(and (Transform ?CIL046)  (Transform ?CIL047) )
then
(bind ?CIN032 TRUE)
else
(bind ?CIN032 NULL)
(bind ?CIN032 (AddOrNot ?CIL046 ?CIN032))
(bind ?CIN032 (AddOrNot ?CIL047 ?CIN032))
(if(eq ?CIN032 NULL)
then
(bind ?CIN032 FALSE)
)
)
(bind ?CIL048 (Leaf equals ?Sex female Sex))
(bind ?CIL049 (Leaf >= ?ACr_Variable 31.0 ACr_Variable))
(if
(and (Transform ?CIL048)  (Transform ?CIL049) )
then
(bind ?CIN033 TRUE)
else
(bind ?CIN033 NULL)
(bind ?CIN033 (AddOrNot ?CIL048 ?CIN033))
(bind ?CIN033 (AddOrNot ?CIL049 ?CIN033))
(if(eq ?CIN033 NULL)
then
(bind ?CIN033 FALSE)
)
)
(if
(or (Transform ?CIN030)  (Transform ?CIN031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIN030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIN031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(if
(or (Transform ?CIN032)  (Transform ?CIN033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIN032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIN033 ?CIN021))
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

(bind ?CIL110 (Leaf equals ?DM_History YES DM_History))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL220 (Leaf equals ?Cerebrovascular_Disease_History YES Cerebrovascular_Disease_History))
(bind ?CIL221 (Leaf equals ?Cardiopathy_History YES Cardiopathy_History))
(bind ?CIL222 (Leaf equals ?Peripheral_Vascular_Disease_History YES Peripheral_Vascular_Disease_History))
(bind ?CIL223 (Leaf equals ?Retinopathy_History YES Retinopathy_History))
(bind ?CIL224 (Leaf equals ?Nephropathy YES Nephropathy))
(if
(or (Transform ?CIL220)  (Transform ?CIL221)  (Transform ?CIL222)  (Transform ?CIL223)  (Transform ?CIL224) )
then
(bind ?CIN210 TRUE)
else
(bind ?CIN210 NULL)
(bind ?CIN210 (AddOrNot ?CIL220 ?CIN210))
(bind ?CIN210 (AddOrNot ?CIL221 ?CIN210))
(bind ?CIN210 (AddOrNot ?CIL222 ?CIN210))
(bind ?CIN210 (AddOrNot ?CIL223 ?CIN210))
(bind ?CIN210 (AddOrNot ?CIL224 ?CIN210))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_40003)
then
(undefrule *)
(InterpretationIndex "存在靶器官损害或糖尿病或并存的临床情况，诊断为极高危。")
(Recommendation "高血压危险度:很高危")
(OperateFact "Hyperuricaemia_Diagnose_risklevel" "risk_very_high")
(FactUsed "Sex" "Cr_Variable" "ACr_Variable" "DM_History" "Cerebrovascular_Disease_History" "Cardiopathy_History" "Peripheral_Vascular_Disease_History" "Retinopathy_History" "Nephropathy")
)
)


(defrule MS_Hypertension_Instance_40003_1
(filepath ?filepath)
(Sex ?Sex)
(Cr_Variable ?Cr_Variable)
(ACr_Variable ?ACr_Variable)
(DM_History ?DM_History)
(Cerebrovascular_Disease_History ?Cerebrovascular_Disease_History)
(Cardiopathy_History ?Cardiopathy_History)
(Peripheral_Vascular_Disease_History ?Peripheral_Vascular_Disease_History)
(Retinopathy_History ?Retinopathy_History)
(Nephropathy ?Nephropathy)
=>
(bind ?Threshhold 0)

(bind ?COL040 (Leaf equals ?Sex male Sex))
(bind ?COL041 (Leaf > ?Cr_Variable 115.0 Cr_Variable))
(bind ?COL042 (Leaf <= ?Cr_Variable 133.0 Cr_Variable))
(if
(and (Transform ?COL040)  (Transform ?COL041)  (Transform ?COL042) )
then
(bind ?CON030 TRUE)
else
(bind ?CON030 NULL)
(bind ?CON030 (AddOrNot ?COL040 ?CON030))
(bind ?CON030 (AddOrNot ?COL041 ?CON030))
(bind ?CON030 (AddOrNot ?COL042 ?CON030))
(if(eq ?CON030 NULL)
then
(bind ?CON030 FALSE)
)
)
(bind ?COL043 (Leaf equals ?Sex female Sex))
(bind ?COL044 (Leaf > ?Cr_Variable 107.0 Cr_Variable))
(bind ?COL045 (Leaf <= ?Cr_Variable 124.0 Cr_Variable))
(if
(and (Transform ?COL043)  (Transform ?COL044)  (Transform ?COL045) )
then
(bind ?CON031 TRUE)
else
(bind ?CON031 NULL)
(bind ?CON031 (AddOrNot ?COL043 ?CON031))
(bind ?CON031 (AddOrNot ?COL044 ?CON031))
(bind ?CON031 (AddOrNot ?COL045 ?CON031))
(if(eq ?CON031 NULL)
then
(bind ?CON031 FALSE)
)
)
(bind ?COL046 (Leaf equals ?Sex male Sex))
(bind ?COL047 (Leaf >= ?ACr_Variable 31.0 ACr_Variable))
(if
(and (Transform ?COL046)  (Transform ?COL047) )
then
(bind ?CON032 TRUE)
else
(bind ?CON032 NULL)
(bind ?CON032 (AddOrNot ?COL046 ?CON032))
(bind ?CON032 (AddOrNot ?COL047 ?CON032))
(if(eq ?CON032 NULL)
then
(bind ?CON032 FALSE)
)
)
(bind ?COL048 (Leaf equals ?Sex female Sex))
(bind ?COL049 (Leaf >= ?ACr_Variable 31.0 ACr_Variable))
(if
(and (Transform ?COL048)  (Transform ?COL049) )
then
(bind ?CON033 TRUE)
else
(bind ?CON033 NULL)
(bind ?CON033 (AddOrNot ?COL048 ?CON033))
(bind ?CON033 (AddOrNot ?COL049 ?CON033))
(if(eq ?CON033 NULL)
then
(bind ?CON033 FALSE)
)
)
(if
(or (Transform ?CON030)  (Transform ?CON031) )
then
(bind ?CON020 TRUE)
else
(bind ?CON020 NULL)
(bind ?CON020 (AddOrNot ?CON030 ?CON020))
(bind ?CON020 (AddOrNot ?CON031 ?CON020))
(if(eq ?CON020 NULL)
then
(bind ?CON020 FALSE)
)
)
(if
(or (Transform ?CON032)  (Transform ?CON033) )
then
(bind ?CON021 TRUE)
else
(bind ?CON021 NULL)
(bind ?CON021 (AddOrNot ?CON032 ?CON021))
(bind ?CON021 (AddOrNot ?CON033 ?CON021))
(if(eq ?CON021 NULL)
then
(bind ?CON021 FALSE)
)
)
(if
(or (Transform ?CON020)  (Transform ?CON021) )
then
(bind ?CON010 TRUE)
else
(bind ?CON010 NULL)
(bind ?CON010 (AddOrNot ?CON020 ?CON010))
(bind ?CON010 (AddOrNot ?CON021 ?CON010))
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

(bind ?COL110 (Leaf equals ?DM_History YES DM_History))
(bind ?RO1 ?COL110)
(if
(eq ?RO1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL220 (Leaf equals ?Cerebrovascular_Disease_History YES Cerebrovascular_Disease_History))
(bind ?COL221 (Leaf equals ?Cardiopathy_History YES Cardiopathy_History))
(bind ?COL222 (Leaf equals ?Peripheral_Vascular_Disease_History YES Peripheral_Vascular_Disease_History))
(bind ?COL223 (Leaf equals ?Retinopathy_History YES Retinopathy_History))
(bind ?COL224 (Leaf equals ?Nephropathy YES Nephropathy))
(if
(or (Transform ?COL220)  (Transform ?COL221)  (Transform ?COL222)  (Transform ?COL223)  (Transform ?COL224) )
then
(bind ?CON210 TRUE)
else
(bind ?CON210 NULL)
(bind ?CON210 (AddOrNot ?COL220 ?CON210))
(bind ?CON210 (AddOrNot ?COL221 ?CON210))
(bind ?CON210 (AddOrNot ?COL222 ?CON210))
(bind ?CON210 (AddOrNot ?COL223 ?CON210))
(bind ?CON210 (AddOrNot ?COL224 ?CON210))
(if(eq ?CON210 NULL)
then
(bind ?CON210 FALSE)
)
)
(bind ?RO2 ?CON210)
(if
(eq ?RO2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(bind ?ShortData (AddOrNot ?RO1 ?ShortData))
(bind ?ShortData (AddOrNot ?RO2 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_40003)
then
(undefrule *)
(InterpretationIndex "不存在靶器官损害或糖尿病或并存的临床情况，判断其他的危险因素个数。")
(load (str-cat ?filepath "MS_Hypertension_Instance_40005.clp"))
(FactUsed "Sex" "Cr_Variable" "ACr_Variable" "DM_History" "Cerebrovascular_Disease_History" "Cardiopathy_History" "Peripheral_Vascular_Disease_History" "Retinopathy_History" "Nephropathy")
)
)
