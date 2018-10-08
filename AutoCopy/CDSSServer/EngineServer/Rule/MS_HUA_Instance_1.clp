(defrule MS_HUA_Instance_1_0
(filepath ?filepath)
(Hyperuricaemia_Diagnose_EVENT ?Hyperuricaemia_Diagnose_EVENT)
=>
(if
(eq ?Hyperuricaemia_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "¸ßÄòËáÑªÖ¢Õï¶Ï")
(load (str-cat ?filepath "MS_HUA_Instance_100004.clp"))))
