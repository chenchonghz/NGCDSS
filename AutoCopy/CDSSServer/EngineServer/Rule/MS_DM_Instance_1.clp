(defrule MS_DM_Instance_1_0
(filepath ?filepath)
(DM_Diagnose_EVENT ?DM_Diagnose_EVENT)
=>
(if
(eq ?DM_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "�������")
(load (str-cat ?filepath "MS_DM_Instance_60000.clp"))))
