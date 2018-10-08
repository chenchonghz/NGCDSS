(defrule MS_HUA_Instance_120015_0
(filepath ?filepath)
(Ccr ?Ccr)
(Malignant_Tumour ?Malignant_Tumour)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?Ccr 30.0 Ccr))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?Malignant_Tumour YES Malignant_Tumour))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath HUA_Instance_120015)
then
(undefrule *)
(InterpretationIndex "肾脏功能不全Ccr<30ml/min或恶性肿瘤，诊断为继发性高尿酸血症。")
(Recommendation "高尿酸血症原因:继发")
(OperateFact "HUA_Diagnose_PS" "secondary")
(FactUsed "Ccr" "Malignant_Tumour")
)
)


(defrule MS_HUA_Instance_120015_1
(filepath ?filepath)
(Malignant_Tumour ?Malignant_Tumour)
(Ccr ?Ccr)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Malignant_Tumour YES Malignant_Tumour))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf < ?Ccr 30.0 Ccr))
(bind ?RO1 ?COL110)
(if
(eq ?RO1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(bind ?ShortData (AddOrNot ?RO1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_120015)
then
(undefrule *)
(InterpretationIndex "Cr>=30且无恶性肿瘤，诊断为原发性高尿酸血症。")
(Recommendation "高尿酸血症原因:原发")
(OperateFact "HUA_Diagnose_PS" "primary")
(load (str-cat ?filepath "MS_HUA_Instance_10000.clp"))
(FactUsed "Malignant_Tumour" "Ccr")
)
)
