(defrule MS_MS_mergexml_Instance_20054_0
(filepath ?filepath)
(HUA_SelfMonitor_EVENT ?HUA_SelfMonitor_EVENT)
=>
(if
(eq ?HUA_SelfMonitor_EVENT on)
then
(undefrule *)
(InterpretationIndex "¸ßÄòËáÑªÖ¢Ëæ·Ã½¨Òé")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_20057.clp"))))
