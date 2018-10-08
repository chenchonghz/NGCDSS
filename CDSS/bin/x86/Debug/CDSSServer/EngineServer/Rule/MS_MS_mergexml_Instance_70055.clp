(defrule MS_MS_mergexml_Instance_70055_0
(filepath ?filepath)
(Dyslipidemia_Diagnosed ?Dyslipidemia_Diagnosed)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_Diagnosed Dyslipidemia_Normal1 Dyslipidemia_Diagnosed))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_70055)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "���Ҽ�����
ÿ�곣�����1��Ѫ֬4���⡣")
(FactUsed "Dyslipidemia_Diagnosed")
)
)


(defrule MS_MS_mergexml_Instance_70055_1
(filepath ?filepath)
(Dyslipidemia_Diagnosed ?Dyslipidemia_Diagnosed)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_Diagnosed Dyslipidemia_Normal1 Dyslipidemia_Diagnosed))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_70055)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_70046.clp"))
(FactUsed "Dyslipidemia_Diagnosed")
)
)
