(defrule MS_Dyslipidemia_Instance_90012_0
(filepath ?filepath)
(Dyslipidemia_TG_EVENT ?Dyslipidemia_TG_EVENT)
=>
(if
(eq ?Dyslipidemia_TG_EVENT on)
then
(undefrule *)
(InterpretationIndex "TG÷Œ¡∆")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_120000.clp"))))
