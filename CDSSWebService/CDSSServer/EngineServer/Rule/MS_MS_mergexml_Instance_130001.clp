(defrule MS_MS_mergexml_Instance_130001_0
(filepath ?filepath)
(Antiplatelet_Drug_Use_EVENT ?Antiplatelet_Drug_Use_EVENT)
=>
(if
(eq ?Antiplatelet_Drug_Use_EVENT on)
then
(undefrule *)
(InterpretationIndex "¿¹ÑªĞ¡°åÒ©ÎïÖÎÁÆ")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_130002.clp"))))
