(defrule MS_Dyslipidemia_Instance_70002_0
(filepath ?filepath)
(Dyslipidemia_TG_His ?Dyslipidemia_TG_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_TG_His YES Dyslipidemia_TG_His))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_70002)
then
(undefrule *)
(InterpretationIndex "有高甘油三酯病史，确诊为高甘油三酯症。")
(Recommendation "高甘油三酯症")
(OperateFact "Dyslipidemia_Diagnose_TG" "Dyslipidemia_TG")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_80002.clp"))
(FactUsed "Dyslipidemia_TG_His")
)
)


(defrule MS_Dyslipidemia_Instance_70002_1
(filepath ?filepath)
(Dyslipidemia_TG_His ?Dyslipidemia_TG_His)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_TG_His YES Dyslipidemia_TG_His))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_70002)
then
(undefrule *)
(InterpretationIndex "无高甘油三酯病史，继续判断是否有高胆固醇病史")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_80002.clp"))
(FactUsed "Dyslipidemia_TG_His")
)
)
