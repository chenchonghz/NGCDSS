(deffunction DataShortJudge(?InputData ?DataName)
(if (eq ?InputData NULL)
then
(DataNotify ?DataName)
(return FALSE)
else
(return TRUE)
)
)

(defrule DietDataJudge
(filepath ?filepath)
(SportType ?SportType)
(BMI ?BMI)
(Height ?Height)
(Sex ?Sex)
(Age ?Age)
(DiseaseStatus ?DiseaseStatus)
(Dyslipidemia_Diagnose_TG ?Dyslipidemia_Diagnose_TG)
(Dyslipidemia_Diagnose_TC ?Dyslipidemia_Diagnose_TC)
(Weight ?Weight)
(Cr_Variable ?Cr_Variable)
(ACr_Variable ?ACr_Variable)
(UrineProtein ?UrineProtein)
(HUA_Diagnose ?HUA_Diagnose)
(Fat_Diagnose ?Fat_Diagnose)
(Hypertension_Diagnose ?Hypertension_Diagnose)
=>
;(bind ?R1 (DataShortJudge ?SportType SportType))
(bind ?R2 (DataShortJudge ?BMI BMI))
(bind ?R3 (DataShortJudge ?Height Height))
(bind ?R4 (DataShortJudge ?Sex Sex))
(bind ?R5 (DataShortJudge ?Age Age))
;(bind ?R6 (DataShortJudge ?DiseaseStatus DiseaseStatus))
;revised by wbf 081229 Bug B94
(bind ?R7 (DataShortJudge ?Dyslipidemia_Diagnose_TG Dyslipidemia_Diagnose_TG))
(bind ?R8 (DataShortJudge ?Dyslipidemia_Diagnose_TC Dyslipidemia_Diagnose_TC))
(bind ?R9 (DataShortJudge ?Weight Weight))
(bind ?R10 (DataShortJudge ?Cr_Variable Cr_Variable))
;(bind ?R11 (DataShortJudge ?ACr_Variable ACr_Variable))
;(bind ?R12 (DataShortJudge ?UrineProtein UrineProtein))
;revised by wbf 081229 Bug B94
(bind ?R13 (DataShortJudge ?HUA_Diagnose HUA_Diagnose))
(bind ?R14 (DataShortJudge ?Fat_Diagnose Fat_Diagnose))
(bind ?R15 (DataShortJudge ?Hypertension_Diagnose Hypertension_Diagnose))

(if (eq ?SportType NULL)
then
(OperateFact "SportType" FREEDEGREE)
)
(if (eq ?DiseaseStatus NULL)
then
(OperateFact "DiseaseStatus" COMMON)
)
(if (eq ?UrineProtein NULL)
then
(OperateNumberFact "UrineProtein" 0)
)
;revised by wbf 081229 Bug B94

(if (eq ?ACr_Variable NULL)
then
(OperateNumberFact "ACr_Variable" 0)
)

(if (and ?R2 ?R3 ?R4 ?R5 ?R7 ?R8 ?R9 ?R10  ?R13 ?R14 ?R15)
;revised by wbf 081229 Bug B94
then
(load (str-cat ?filepath "MS_DietCalc.clp"))
)
)
