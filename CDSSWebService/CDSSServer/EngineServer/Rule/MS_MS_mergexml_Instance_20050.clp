(defrule MS_MS_mergexml_Instance_20050_0
(filepath ?filepath)
(Dyslipidemia_SelfMonitor_EVENT ?Dyslipidemia_SelfMonitor_EVENT)
=>
(if
(eq ?Dyslipidemia_SelfMonitor_EVENT on)
then
(undefrule *)
(InterpretationIndex "Ѫ֬������ý���")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_70055.clp"))))
