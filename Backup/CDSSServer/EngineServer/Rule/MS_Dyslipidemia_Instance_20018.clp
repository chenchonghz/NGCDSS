(defrule MS_Dyslipidemia_Instance_20018_0
(filepath ?filepath)
(TG_Variable ?TG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?TG_Variable 1.7 TG_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_20018)
then
(undefrule *)
(InterpretationIndex "TG>=1.7mmol/L£¬Õï¶ÏÎª¸ß¸ÊÓÍÈýÖ¬ÑªÖ¢£»¼ÌÐøÅÐ¶ÏHDL-ch¡£")
(Recommendation "¸ß¸ÊÓÍÈýõ¥Ö¢")
(OperateFact "Dyslipidemia_Diagnose_TG" "Dyslipidemia_TG")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20023.clp"))
(FactUsed "TG_Variable")
)
)


(defrule MS_Dyslipidemia_Instance_20018_1
(filepath ?filepath)
(TG_Variable ?TG_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?TG_Variable 1.7 TG_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_20018)
then
(undefrule *)
(InterpretationIndex "TG>=1.7mmol/L²»·ûºÏ£¬¼ÌÐøÅÐ¶ÏHDL-ch¡£")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20023.clp"))
(FactUsed "TG_Variable")
)
)
