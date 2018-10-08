(defrule MS_Hypertension_Instance_30000_0
(filepath ?filepath)
(Ccr ?Ccr)
(irenal_artery_Bistenosis ?irenal_artery_Bistenosis)
(DM_kidney ?DM_kidney)
(renal_artery_stenosis ?renal_artery_stenosis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?Ccr 30.0 Ccr))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?irenal_artery_Bistenosis YES irenal_artery_Bistenosis))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf equals ?DM_kidney YES DM_kidney))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf equals ?renal_artery_stenosis YES renal_artery_stenosis))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_30000)
then
(undefrule *)
(InterpretationIndex "有继发性因素(Ccr<30，或肾动脉狭窄（单）或肾动脉狭窄（双）或糖尿病肾病)，诊断为继发性高血压，并进行危险度评估。")
(Recommendation "有继发因素")
(OperateFact "Hypertension_Diagnose_PS" "secondary")
(load (str-cat ?filepath "MS_Hypertension_Instance_40003.clp"))
(FactUsed "Ccr" "irenal_artery_Bistenosis" "DM_kidney" "renal_artery_stenosis")
)
)


(defrule MS_Hypertension_Instance_30000_1
(filepath ?filepath)
(irenal_artery_Bistenosis ?irenal_artery_Bistenosis)
(renal_artery_stenosis ?renal_artery_stenosis)
(Ccr ?Ccr)
(DM_kidney ?DM_kidney)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?irenal_artery_Bistenosis YES irenal_artery_Bistenosis))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?renal_artery_stenosis YES renal_artery_stenosis))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf < ?Ccr 30.0 Ccr))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf equals ?DM_kidney YES DM_kidney))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_30000)
then
(undefrule *)
(InterpretationIndex "无继发因素，诊断为原发性高血压，并进行危险度评估。")
(Recommendation "无继发因素")
(OperateFact "Hypertension_Diagnose_PS" "primary")
(load (str-cat ?filepath "MS_Hypertension_Instance_40003.clp"))
(FactUsed "irenal_artery_Bistenosis" "renal_artery_stenosis" "Ccr" "DM_kidney")
)
)
