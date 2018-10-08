(defrule MS_DM_Instance_40075_0
(filepath ?filepath)
(BMI ?BMI)
(Fat_History ?Fat_History)
(DM_Family_History ?DM_Family_History)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?BMI 24.0 BMI))
(bind ?CIL021 (Leaf equals ?Fat_History YES Fat_History))
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

(bind ?CIL110 (Leaf equals ?DM_Family_History YES DM_Family_History))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf > ?Age 30.0 Age))
(bind ?RI2 ?CIL210)
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
(if(NotifyOrNot >= ?Threshhold 2 ?ShortData ?filepath DM_Instance_40075)
then
(undefrule *)
(InterpretationIndex "肥胖或、糖尿病家族史、年龄>30岁三条中满足2条或以上，诊断为2型糖尿病。")
(Recommendation "糖尿病类型:2型糖尿病")
(OperateFact "DM_Diagnose" "T2DM")
(FactUsed "BMI" "Fat_History" "DM_Family_History" "Age")
)
)


(defrule MS_DM_Instance_40075_1
(filepath ?filepath)
(BMI ?BMI)
(Fat_History ?Fat_History)
(DM_Family_History ?DM_Family_History)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?BMI 24.0 BMI))
(bind ?CIL021 (Leaf equals ?Fat_History YES Fat_History))
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

(bind ?CIL110 (Leaf equals ?DM_Family_History YES DM_Family_History))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf > ?Age 30.0 Age))
(bind ?RI2 ?CIL210)
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
(if(NotifyOrNot < ?Threshhold 2 ?ShortData ?filepath DM_Instance_40075)
then
(undefrule *)
(InterpretationIndex "肥胖、糖尿病家族史、年龄>30岁3条中最多满足1条，查ICA、GAD65。")
(load (str-cat ?filepath "MS_DM_Instance_40081.clp"))
(FactUsed "BMI" "Fat_History" "DM_Family_History" "Age")
)
)
