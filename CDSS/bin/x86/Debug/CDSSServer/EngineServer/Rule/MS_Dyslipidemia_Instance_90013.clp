(defrule MS_Dyslipidemia_Instance_90013_0
(filepath ?filepath)
(Dyslipidemia_LDLC_EVENT ?Dyslipidemia_LDLC_EVENT)
=>
(if
(eq ?Dyslipidemia_LDLC_EVENT on)
then
(undefrule *)
(InterpretationIndex "LDLC÷Œ¡∆")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110102.clp"))))
