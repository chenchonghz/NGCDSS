(defrule MS_MS_mergexml_Instance_100000_0
(filepath ?filepath)
(risk_score ?risk_score)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?risk_score 3 risk_score))
(bind ?CIL021 (Leaf >= ?Age 40 Age))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_100000)
then
(undefrule *)
(InterpretationIndex "危险度>=3且病人年龄>=40，因此需要加用阿司匹林")
(Recommendation "加用阿司匹林")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_10000.clp"))
(FactUsed "risk_score" "Age")
)
)


(defrule MS_MS_mergexml_Instance_100000_1
(filepath ?filepath)
(risk_score ?risk_score)
(Age ?Age)
=>
(bind ?Threshhold 0)

(bind ?COL020 (Leaf >= ?risk_score 3 risk_score))
(bind ?COL021 (Leaf >= ?Age 40 Age))
(if
(and (Transform ?COL020)  (Transform ?COL021) )
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_100000)
then
(undefrule *)
(InterpretationIndex "病人危险度<3或年龄<40,不需要使用阿司匹林。")
(Recommendation "不需要采用抗血小板药物")
(FactUsed "risk_score" "Age")
)
)
