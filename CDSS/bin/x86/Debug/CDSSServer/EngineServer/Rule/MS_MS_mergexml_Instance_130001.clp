(defrule MS_MS_mergexml_Instance_130001_0
(filepath ?filepath)
(Antiplatelet_Drug_Use_EVENT ?Antiplatelet_Drug_Use_EVENT)
=>
(if
(eq ?Antiplatelet_Drug_Use_EVENT on)
then
(undefrule *)
(InterpretationIndex "��ѪС��ҩ������")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_130002.clp"))))
