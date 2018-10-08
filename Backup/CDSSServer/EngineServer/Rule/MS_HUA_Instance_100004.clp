(defrule MS_HUA_Instance_100004_0
(filepath ?filepath)
(Hyperuricaemia_History ?Hyperuricaemia_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Hyperuricaemia_History YES Hyperuricaemia_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100004)
then
(undefrule *)
(InterpretationIndex "ÓÐ¸ßÄòËáÑªÖ¢²¡Ê·£¬¼ÌÐøÅÐ¶ÏÊÇ·ñÓÐÍ´·ç²¡Ê·¡£")
(load (str-cat ?filepath "MS_HUA_Instance_10008.clp"))
(FactUsed "Hyperuricaemia_History")
)
)


(defrule MS_HUA_Instance_100004_1
(filepath ?filepath)
(Hyperuricaemia_History ?Hyperuricaemia_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Hyperuricaemia_History YES Hyperuricaemia_History))
(bind ?RO0 ?COL010)
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_100004)
then
(undefrule *)
(InterpretationIndex "ÎÞ¸ßÄòËáÑªÖ¢²¡Ê·£¬¼ÌÐøÅÐ¶ÏÊÇ·ñÑªÄòËá>440umol/L¡£")
(load (str-cat ?filepath "MS_HUA_Instance_20011.clp"))
(FactUsed "Hyperuricaemia_History")
)
)
