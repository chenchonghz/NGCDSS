(defrule MS_HUA_Instance_20030_0
(filepath ?filepath)
(Malignant_Tumour ?Malignant_Tumour)
(Chemotherapy ?Chemotherapy)
(Nephropathy ?Nephropathy)
(Dose_Affect_UA_Excretion ?Dose_Affect_UA_Excretion)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Malignant_Tumour YES Malignant_Tumour))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?Chemotherapy YES Chemotherapy))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf equals ?Nephropathy YES Nephropathy))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf equals ?Dose_Affect_UA_Excretion YES Dose_Affect_UA_Excretion))
(bind ?RI3 ?CIL310)
(if
(eq ?RI3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(bind ?ShortData (AddOrNot ?RI3 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath HUA_Instance_20030)
then
(undefrule *)
(InterpretationIndex "有继发因素（肿瘤病史、近期放化疗、有尿酸排泄过少疾病如肾脏疾病、使用影响尿酸排泄药物如利尿剂、尼麦角林 ），诊断为继发性高尿酸血症。")
(Recommendation "高尿酸血症原因:继发")
(OperateFact "HUA_Diagnose_PS" "secondary")
(FactUsed "Malignant_Tumour" "Chemotherapy" "Nephropathy" "Dose_Affect_UA_Excretion")
)
)


(defrule MS_HUA_Instance_20030_1
(filepath ?filepath)
(Malignant_Tumour ?Malignant_Tumour)
(Chemotherapy ?Chemotherapy)
(Nephropathy ?Nephropathy)
(Dose_Affect_UA_Excretion ?Dose_Affect_UA_Excretion)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Malignant_Tumour YES Malignant_Tumour))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf equals ?Chemotherapy YES Chemotherapy))
(bind ?RO1 ?COL110)
(if
(eq ?RO1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL210 (Leaf equals ?Nephropathy YES Nephropathy))
(bind ?RO2 ?COL210)
(if
(eq ?RO2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL310 (Leaf equals ?Dose_Affect_UA_Excretion YES Dose_Affect_UA_Excretion))
(bind ?RO3 ?COL310)
(if
(eq ?RO3 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(bind ?ShortData (AddOrNot ?RO1 ?ShortData))
(bind ?ShortData (AddOrNot ?RO2 ?ShortData))
(bind ?ShortData (AddOrNot ?RO3 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_20030)
then
(undefrule *)
(InterpretationIndex "没有继发因素（肿瘤病史、近期放化疗、有尿酸排泄过少疾病如肾脏疾病、使用影响尿酸排泄药物如利尿剂、尼麦角林 ），诊断为原发性高尿酸血症。")
(Recommendation "高尿酸血症原因:原发")
(OperateFact "HUA_Diagnose_PS" "primary")
(load (str-cat ?filepath "MS_HUA_Instance_10000.clp"))
(FactUsed "Malignant_Tumour" "Chemotherapy" "Nephropathy" "Dose_Affect_UA_Excretion")
)
)
