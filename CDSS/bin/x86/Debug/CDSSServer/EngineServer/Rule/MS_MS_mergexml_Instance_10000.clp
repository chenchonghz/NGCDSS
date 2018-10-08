(defrule MS_MS_mergexml_Instance_10000_0
(filepath ?filepath)
(FBG_Variable ?FBG_Variable)
(HbA1c ?HbA1c)
=>
(bind ?Threshhold 0)

(bind ?COL020 (Leaf > ?FBG_Variable 13.9 FBG_Variable))
(bind ?COL021 (Leaf > ?FBG_Variable 13.9 FBG_Variable))
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

(bind ?COL110 (Leaf > ?HbA1c 10.0 HbA1c))
(bind ?RO1 ?COL110)
(if
(eq ?RO1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(bind ?ShortData (AddOrNot ?RO1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_10000)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_30002.clp"))
(FactUsed "FBG_Variable" "HbA1c")
)
)


(defrule MS_MS_mergexml_Instance_10000_1
(filepath ?filepath)
(FBG_Variable ?FBG_Variable)
(HbA1c ?HbA1c)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf > ?FBG_Variable 13.9 FBG_Variable))
(bind ?CIL021 (Leaf > ?FBG_Variable 13.9 FBG_Variable))
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

(bind ?CIL110 (Leaf > ?HbA1c 10.0 HbA1c))
(bind ?RI1 ?CIL110)
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
(if(NotifyOrNot equals ?Threshhold 2 ?ShortData ?filepath MS_mergexml_Instance_10000)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "∆§œ¬◊¢…‰“»µ∫Àÿ÷Œ¡∆")
(FactUsed "FBG_Variable" "HbA1c")
)
)
