(defrule MS_MS_mergexml_Instance_130002_0
(filepath ?filepath)
(HbA1c ?HbA1c)
(FBG_Variable ?FBG_Variable)
(twoHPBG_Variable ?twoHPBG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?HbA1c 6.5 HbA1c))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf < ?FBG_Variable 4.4 FBG_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf >= ?FBG_Variable 6.1 FBG_Variable))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf < ?twoHPBG_Variable 4.4 twoHPBG_Variable))
(bind ?RI3 ?CIL310)
(if
(eq ?RI3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL410 (Leaf >= ?twoHPBG_Variable 7.8 twoHPBG_Variable))
(bind ?RI4 ?CIL410)
(if
(eq ?RI4 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(bind ?ShortData (AddOrNot ?RI3 ?ShortData))
(bind ?ShortData (AddOrNot ?RI4 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_130002)
then
(undefrule *)
(InterpretationIndex "HbA1c>=6.5%或FBG<4.4mmol/l或FBG>6.1mmol/l或2hPBG<4.4mmol/l或2hPBG>8.0mmol/L，继续判断是否FBG>13.9mmol/L或随机BG>16.7mmol/l 且HbA1c>10%。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_100000.clp"))
(FactUsed "HbA1c" "FBG_Variable" "twoHPBG_Variable")
)
)


(defrule MS_MS_mergexml_Instance_130002_1
(filepath ?filepath)
(HbA1c ?HbA1c)
(FBG_Variable ?FBG_Variable)
(twoHPBG_Variable ?twoHPBG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?HbA1c 6.5 HbA1c))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL120 (Leaf >= ?FBG_Variable 4.4 FBG_Variable))
(bind ?CIL121 (Leaf <= ?FBG_Variable 6.1 FBG_Variable))
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

(bind ?CIL220 (Leaf >= ?twoHPBG_Variable 4.4 twoHPBG_Variable))
(bind ?CIL221 (Leaf <= ?twoHPBG_Variable 8.0 twoHPBG_Variable))
(if
(and (Transform ?CIL220)  (Transform ?CIL221) )
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 3 ?ShortData ?filepath MS_mergexml_Instance_130002)
then
(undefrule *)
(InterpretationIndex "HbA1c<6.5%且FBG 4.4~6.1mmol/l 且2hPBG4.4~8.0mmol/L，无需采用抗血小板药物治疗")
(Recommendation "不需要采用抗血小板药物")
(FactUsed "HbA1c" "FBG_Variable" "twoHPBG_Variable")
)
)
