(defrule MS_Dyslipidemia_Instance_20010_0
(filepath ?filepath)
(TC_Variable ?TC_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?TC_Variable 5.7 TC_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_20010)
then
(undefrule *)
(InterpretationIndex "TC>=5.7mmol/L£¬Õï¶ÏÎª¸ßµ¨¹Ì´¼ÑªÖ¢£»¼ÌÐøÅÐ¶ÏTG¡£")
(Recommendation "¸ßµ¨¹Ì´¼ÑªÖ¢")
(OperateFact "Dyslipidemia_Diagnose_TC" "Dyslipidemia_TC")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20018.clp"))
(FactUsed "TC_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_20010_1
(filepath ?filepath)
(TC_Variable ?TC_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?TC_Variable 5.7 TC_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_20010)
then
(undefrule *)
(InterpretationIndex "TC>=5.7mmol/L²»·ûºÏ£¬¼ÌÐøÅÐ¶ÏTG¡£")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20018.clp"))
(FactUsed "TC_Variable")
)
)
