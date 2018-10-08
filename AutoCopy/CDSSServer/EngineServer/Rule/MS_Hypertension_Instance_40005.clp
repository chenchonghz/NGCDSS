(defrule MS_Hypertension_Instance_40005_0
(filepath ?filepath)
(Sex ?Sex)
(Age ?Age)
(smoke_history ?smoke_history)
(Dyslipidemia_History ?Dyslipidemia_History)
(Cardiovascular_Disease_Family_History ?Cardiovascular_Disease_Family_History)
(Fat_History ?Fat_History)
(lack_of_exercise ?lack_of_exercise)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?Sex male Sex))
(bind ?CIL021 (Leaf > ?Age 55.0 Age))
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

(bind ?CIL120 (Leaf equals ?Sex female Sex))
(bind ?CIL121 (Leaf > ?Age 65.0 Age))
(if
(and (Transform ?CIL120)  (Transform ?CIL121) )
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

(bind ?CIL210 (Leaf equals ?smoke_history YES smoke_history))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf equals ?Dyslipidemia_History YES Dyslipidemia_History))
(bind ?RI3 ?CIL310)
(if
(eq ?RI3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL410 (Leaf equals ?Cardiovascular_Disease_Family_History YES Cardiovascular_Disease_Family_History))
(bind ?RI4 ?CIL410)
(if
(eq ?RI4 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL510 (Leaf equals ?Fat_History YES Fat_History))
(bind ?RI5 ?CIL510)
(if
(eq ?RI5 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL610 (Leaf equals ?lack_of_exercise YES lack_of_exercise))
(bind ?RI6 ?CIL610)
(if
(eq ?RI6 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(bind ?ShortData (AddOrNot ?RI3 ?ShortData))
(bind ?ShortData (AddOrNot ?RI4 ?ShortData))
(bind ?ShortData (AddOrNot ?RI5 ?ShortData))
(bind ?ShortData (AddOrNot ?RI6 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 3 ?ShortData ?filepath Hypertension_Instance_40005)
then
(undefrule *)
(InterpretationIndex "男性>55岁、女性>65岁、吸烟史、血脂异常、心血管病家族史、肥胖、缺乏体力活动这些危险因素中符合3项以上，继续判断血压几级。")
(load (str-cat ?filepath "MS_Hypertension_Instance_70024.clp"))
(FactUsed "Sex" "Age" "smoke_history" "Dyslipidemia_History" "Cardiovascular_Disease_Family_History" "Fat_History" "lack_of_exercise")
)
)


(defrule MS_Hypertension_Instance_40005_1
(filepath ?filepath)
(Sex ?Sex)
(Age ?Age)
(smoke_history ?smoke_history)
(Dyslipidemia_History ?Dyslipidemia_History)
(Cardiovascular_Disease_Family_History ?Cardiovascular_Disease_Family_History)
(Fat_History ?Fat_History)
(lack_of_exercise ?lack_of_exercise)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?Sex male Sex))
(bind ?CIL021 (Leaf > ?Age 55.0 Age))
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

(bind ?CIL120 (Leaf equals ?Sex female Sex))
(bind ?CIL121 (Leaf > ?Age 65.0 Age))
(if
(and (Transform ?CIL120)  (Transform ?CIL121) )
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

(bind ?CIL210 (Leaf equals ?smoke_history YES smoke_history))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf equals ?Dyslipidemia_History YES Dyslipidemia_History))
(bind ?RI3 ?CIL310)
(if
(eq ?RI3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL410 (Leaf equals ?Cardiovascular_Disease_Family_History YES Cardiovascular_Disease_Family_History))
(bind ?RI4 ?CIL410)
(if
(eq ?RI4 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL510 (Leaf equals ?Fat_History YES Fat_History))
(bind ?RI5 ?CIL510)
(if
(eq ?RI5 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL610 (Leaf equals ?lack_of_exercise YES lack_of_exercise))
(bind ?RI6 ?CIL610)
(if
(eq ?RI6 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(bind ?ShortData (AddOrNot ?RI3 ?ShortData))
(bind ?ShortData (AddOrNot ?RI4 ?ShortData))
(bind ?ShortData (AddOrNot ?RI5 ?ShortData))
(bind ?ShortData (AddOrNot ?RI6 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot < ?Threshhold 3 ?ShortData ?filepath Hypertension_Instance_40005)
then
(undefrule *)
(InterpretationIndex "男性>55岁、女性>65岁、吸烟史、血脂异常、心血管病家族史、肥胖、缺乏体力活动这些危险因素中符合不足3项，继续判断是否至少1项符合。")
(load (str-cat ?filepath "MS_Hypertension_Instance_70026.clp"))
(FactUsed "Sex" "Age" "smoke_history" "Dyslipidemia_History" "Cardiovascular_Disease_Family_History" "Fat_History" "lack_of_exercise")
)
)
