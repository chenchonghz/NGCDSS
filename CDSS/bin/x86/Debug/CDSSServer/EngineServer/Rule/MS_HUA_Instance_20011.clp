(defrule MS_HUA_Instance_20011_0
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf > ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_20011)
then
(undefrule *)
(InterpretationIndex "ÑªÄòËá>440mmol/L£¬È·ÕïÎª¸ßÄòËáÑªÖ¢¡£")
(Recommendation "¸ßÄòËáÑªÖ¢:ÓÐ")
(OperateFact "HUA_Diagnose" "Hyperuricaemia")
(load (str-cat ?filepath "MS_HUA_Instance_120015.clp"))
(FactUsed "UA_Variable")
)
)


(defrule MS_HUA_Instance_20011_1
(filepath ?filepath)
(UA_Variable ?UA_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf <= ?UA_Variable 440.0 UA_Variable))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_20011)
then
(undefrule *)
(InterpretationIndex "ÑªÄòËá<=440mmol/L£¬Õï¶ÏÎªÑªÄòËáÕý³£¡£")
(Recommendation "¸ßÄòËáÑªÖ¢:ÎÞ")
(OperateFact "HUA_Diagnose" "HUA_Normal")
(FactUsed "UA_Variable")
)
)
