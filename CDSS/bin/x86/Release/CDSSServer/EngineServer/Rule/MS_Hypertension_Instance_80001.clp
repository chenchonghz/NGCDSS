(defrule MS_Hypertension_Instance_80001_0
(filepath ?filepath)
(Hypertension_Therapy_EVENT ?Hypertension_Therapy_EVENT)
=>
(if
(eq ?Hypertension_Therapy_EVENT on)
then
(undefrule *)
(InterpretationIndex "��Ѫѹ����")
(load (str-cat ?filepath "MS_Hypertension_Instance_100003.clp"))))
