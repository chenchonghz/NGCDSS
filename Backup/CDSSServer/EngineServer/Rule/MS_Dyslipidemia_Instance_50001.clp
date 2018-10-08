(defrule MS_Dyslipidemia_Instance_50001_0
(filepath ?filepath)
(Dyslipidemia_TC_EVENT ?Dyslipidemia_TC_EVENT)
=>
(if
(eq ?Dyslipidemia_TC_EVENT on)
then
(undefrule *)
(InterpretationIndex "TC÷Œ¡∆")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110040.clp"))))
