(defrule MS_Dyslipidemia_Instance_80013_0
(filepath ?filepath)
(Dyslipidemia_HDLch_His ?Dyslipidemia_HDLch_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_HDLch_His YES Dyslipidemia_HDLch_His))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_80013)
then
(undefrule *)
(InterpretationIndex "有低高密度脂蛋白血症病史，确诊为低高密度脂蛋白血症病史。并继续判断是否有高低密度脂蛋白血症病史。")
(Recommendation "低高密度脂蛋白血症")
(OperateFact "Dyslipidemia_Diagnose_HDLC" "Dyslipidemia_HDLch")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_80024.clp"))
(FactUsed "Dyslipidemia_HDLch_His")
)
)


(defrule MS_Dyslipidemia_Instance_80013_1
(filepath ?filepath)
(Dyslipidemia_HDLch_His ?Dyslipidemia_HDLch_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_HDLch_His YES Dyslipidemia_HDLch_His))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_80013)
then
(undefrule *)
(InterpretationIndex "无低高密度脂蛋白血症病史，继续判断是否有高低密度脂蛋白血症病史。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_80024.clp"))
(FactUsed "Dyslipidemia_HDLch_His")
)
)
