(defrule MS_DM_Instance_40076_0
(filepath ?filepath)
(BMI ?BMI)
(Fat_History ?Fat_History)
(DM_Family_History ?DM_Family_History)
(GDM_History ?GDM_History)
(Macrosomia_History ?Macrosomia_History)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf >= ?BMI 24.0 BMI))
(bind ?CIL031 (Leaf equals ?Fat_History YES Fat_History))
(if
(or (Transform ?CIL030)  (Transform ?CIL031) )
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
(bind ?CIL020 (Leaf equals ?DM_Family_History YES DM_Family_History))
(bind ?CIL021 (Leaf equals ?GDM_History YES GDM_History))
(if
(or (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIN020) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
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

(bind ?CIL120 (Leaf equals ?Macrosomia_History YES Macrosomia_History))
(bind ?CIL121 (Leaf > ?Age 30.0 Age))
(if
(or (Transform ?CIL120)  (Transform ?CIL121) )
then
(bind ?CIN110 TRUE)
else
(bind ?CIN110 NULL)
(bind ?CIN110 (AddOrNot ?CIL120 ?CIN110))
(bind ?CIN110 (AddOrNot ?CIL121 ?CIN110))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 2 ?ShortData ?filepath DM_Instance_40076)
then
(undefrule *)
(InterpretationIndex "(肥胖或有糖尿病家族史或有GDM病史) 且（年龄>30岁或巨大儿生产史），诊断为2型糖尿病。")
(Recommendation "糖尿病类型:2型糖尿病")
(OperateFact "DM_Diagnose" "T2DM")
(FactUsed "BMI" "Fat_History" "DM_Family_History" "GDM_History" "Macrosomia_History" "Age")
)
)


(defrule MS_DM_Instance_40076_1
(filepath ?filepath)
(BMI ?BMI)
(Fat_History ?Fat_History)
(DM_Family_History ?DM_Family_History)
(GDM_History ?GDM_History)
(Macrosomia_History ?Macrosomia_History)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf >= ?BMI 24.0 BMI))
(bind ?CIL031 (Leaf equals ?Fat_History YES Fat_History))
(if
(or (Transform ?CIL030)  (Transform ?CIL031) )
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
(bind ?CIL020 (Leaf equals ?DM_Family_History YES DM_Family_History))
(bind ?CIL021 (Leaf equals ?GDM_History YES GDM_History))
(if
(or (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIN020) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
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

(bind ?CIL120 (Leaf equals ?Macrosomia_History YES Macrosomia_History))
(bind ?CIL121 (Leaf > ?Age 30.0 Age))
(if
(or (Transform ?CIL120)  (Transform ?CIL121) )
then
(bind ?CIN110 TRUE)
else
(bind ?CIN110 NULL)
(bind ?CIN110 (AddOrNot ?CIL120 ?CIN110))
(bind ?CIN110 (AddOrNot ?CIL121 ?CIN110))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot < ?Threshhold 2 ?ShortData ?filepath DM_Instance_40076)
then
(undefrule *)
(InterpretationIndex "(肥胖或糖尿病家族史或有GDM病史) 、（年龄>30岁或巨大儿生产史） 2组没有同时满足，查ICA、GAD65。")
(load (str-cat ?filepath "MS_DM_Instance_40081.clp"))
(FactUsed "BMI" "Fat_History" "DM_Family_History" "GDM_History" "Macrosomia_History" "Age")
)
)
