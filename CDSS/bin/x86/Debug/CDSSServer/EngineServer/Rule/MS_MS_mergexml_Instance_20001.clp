(defrule MS_MS_mergexml_Instance_20001_0
(filepath ?filepath)
(DM_SelfMonitor_Event ?DM_SelfMonitor_Event)
=>
(if
(eq ?DM_SelfMonitor_Event on)
then
(undefrule *)
(InterpretationIndex "лгдР╡║втнр╪Ю╡Б")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_20005.clp"))))
