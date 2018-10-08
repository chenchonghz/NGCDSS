(defrule MS_MSEvaluate_Instance_1_0
(filepath ?filepath)
(MS_Evaluate_Event ?MS_Evaluate_Event)
=>
(if
(eq ?MS_Evaluate_Event on)
then
(undefrule *)
(InterpretationIndex "´úÐ»×ÛºÏÕ÷ÆÀ¹À")
(load (str-cat ?filepath "MS_MSEvaluate_Instance_2.clp"))))
