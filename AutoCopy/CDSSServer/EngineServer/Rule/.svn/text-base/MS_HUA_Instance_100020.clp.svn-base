(defrule MS_HUA_Instance_100020_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100020)
then
(undefrule *)
(InterpretationIndex "ÑªÄòËá>440umol/L £¬¼ÌÐøÅÐ¶ÏÊÇ·ñÎªÔ­·¢¡£")
(load (str-cat ?filepath "MS_HUA_Instance_100023.clp"))
(FactUsed "UA_Variable")
)
)


(defrule MS_HUA_Instance_100020_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100020)
then
(undefrule *)
(InterpretationIndex "ÑªÄòËá<=440umol/L £¬¼ÌÐøÅÐ¶ÏÊÇ·ñÒÑ·þÓÃ½µÄòËáÒ©¡£")
(load (str-cat ?filepath "MS_HUA_Instance_100025.clp"))
(FactUsed "UA_Variable")
)
)
