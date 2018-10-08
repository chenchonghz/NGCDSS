(defrule MS_Fat_Instance_10000_0
(filepath ?filepath)
(BMI ?BMI)
(Sex ?Sex)
(waistline_Variable ?waistline_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?BMI 25 BMI))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL130 (Leaf equals ?Sex female Sex))
(bind ?CIL131 (Leaf >= ?waistline_Variable 80.0 waistline_Variable))
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
(bind ?CIL132 (Leaf equals ?Sex male Sex))
(bind ?CIL133 (Leaf >= ?waistline_Variable 90.0 waistline_Variable))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Fat_Instance_10000)
then
(undefrule *)
(InterpretationIndex "BMI>=25或者女性腰围>=80cm或者男性腰围>=90cm，确诊为肥胖。")
(Recommendation "肥胖:有")
(OperateFact "Fat_Diagnose" "Fat")
(FactUsed "BMI" "Sex" "waistline_Variable")
)
)


(defrule MS_Fat_Instance_10000_1
(filepath ?filepath)
(BMI ?BMI)
(Sex ?Sex)
(waistline_Variable ?waistline_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?BMI 25 BMI))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL130 (Leaf equals ?Sex female Sex))
(bind ?CIL131 (Leaf >= ?waistline_Variable 80.0 waistline_Variable))
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
(bind ?CIL132 (Leaf equals ?Sex male Sex))
(bind ?CIL133 (Leaf >= ?waistline_Variable 90.0 waistline_Variable))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Fat_Instance_10000)
then
(undefrule *)
(InterpretationIndex "BMI>=25、女性腰围>=80cm、男性腰围>=90cm均不符合，确诊为无肥胖。")
(Recommendation "肥胖:无")
(OperateFact "Fat_Diagnose" "NO")
(FactUsed "BMI" "Sex" "waistline_Variable")
)
)
