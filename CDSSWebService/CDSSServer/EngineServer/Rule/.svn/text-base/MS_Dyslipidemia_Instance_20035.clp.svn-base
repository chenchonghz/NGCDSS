(defrule MS_Dyslipidemia_Instance_20035_0
(filepath ?filepath)
(LDLch_Variable ?LDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?LDLch_Variable 3.4 LDLch_Variable))
(bind ?RI0 ?CIL010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_20035)
then
(undefrule *)
(InterpretationIndex "LDLch>=3.4mmol/L，诊断为高低密度脂蛋白血症。")
(Recommendation "高低密度脂蛋白血症")
(OperateFact "Dyslipidemia_Diagnose_LDLC" "Dyslipidemia_LDLch")
(FactUsed "LDLch_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_20035_1
(filepath ?filepath)
(LDLch_Variable ?LDLch_Variable)
(TC_Variable ?TC_Variable)
(TG_Variable ?TG_Variable)
(Sex ?Sex)
(HDLch_Variable ?HDLch_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?LDLch_Variable 3.4 LDLch_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?TC_Variable 5.7 TC_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf >= ?TG_Variable 1.7 TG_Variable))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL330 (Leaf equals ?Sex male Sex))
(bind ?CIL331 (Leaf < ?HDLch_Variable 1.0 HDLch_Variable))
(if
(and (Transform ?CIL330)  (Transform ?CIL331) )
then
(bind ?CIN320 TRUE)
else
(bind ?CIN320 NULL)
(bind ?CIN320 (AddOrNot ?CIL330 ?CIN320))
(bind ?CIN320 (AddOrNot ?CIL331 ?CIN320))
(if(eq ?CIN320 NULL)
then
(bind ?CIN320 FALSE)
)
)
(bind ?CIL332 (Leaf equals ?Sex female Sex))
(bind ?CIL333 (Leaf < ?HDLch_Variable 1.3 HDLch_Variable))
(if
(and (Transform ?CIL332)  (Transform ?CIL333) )
then
(bind ?CIN321 TRUE)
else
(bind ?CIN321 NULL)
(bind ?CIN321 (AddOrNot ?CIL332 ?CIN321))
(bind ?CIN321 (AddOrNot ?CIL333 ?CIN321))
(if(eq ?CIN321 NULL)
then
(bind ?CIN321 FALSE)
)
)
(if
(or (Transform ?CIN320)  (Transform ?CIN321) )
then
(bind ?CIN310 TRUE)
else
(bind ?CIN310 NULL)
(bind ?CIN310 (AddOrNot ?CIN320 ?CIN310))
(bind ?CIN310 (AddOrNot ?CIN321 ?CIN310))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_20035)
then
(undefrule *)
(InterpretationIndex "各项血脂指标均在正常范围。")
(Recommendation "血脂正常")
(OperateFact "Dyslipidemia_Diagnosed" "Dyslipidemia_Normal1")
(FactUsed "LDLch_Variable" "TC_Variable" "TG_Variable" "Sex" "HDLch_Variable")
)
)
