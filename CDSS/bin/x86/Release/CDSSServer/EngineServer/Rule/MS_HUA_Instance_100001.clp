(defrule MS_HUA_Instance_100001_0
(filepath ?filepath)
(Hyperuricaemia_Therapy_EVENT ?Hyperuricaemia_Therapy_EVENT)
=>
(if
(eq ?Hyperuricaemia_Therapy_EVENT on)
then
(undefrule *)
(InterpretationIndex "¸ßÄòËáÑªÖ¢ÖÎÁÆ")
(load (str-cat ?filepath "MS_HUA_Instance_100020.clp"))))
