(deffunction DataShortJudge(?InputData ?DataName)
(if (eq ?InputData NULL)
then
(DataNotify ?DataName)
(return FALSE)
else
(return TRUE)
)
)

(defrule SportDataJudge
(filepath ?filepath)
(BMI ?BMI)
(HbA1c ?HbA1c)
(Sex ?Sex)
(Age ?Age)
(Weight ?Weight)
(LimbDyskinesia ?LimbDyskinesia)
(SportType ?SportType)
=>
;(bind ?R1 (DataShortJudge ?SportType SportType))
(bind ?R2 (DataShortJudge ?BMI BMI))
(bind ?R3 (DataShortJudge ?Weight Weight))
(bind ?R4 (DataShortJudge ?Sex Sex))
(bind ?R5 (DataShortJudge ?Age Age))
(bind ?R6 (DataShortJudge ?LimbDyskinesia LimbDyskinesia))
(bind ?R7 (DataShortJudge ?HbA1c HbA1c))
(if (eq ?SportType NULL)
then
(OperateFact "SportType" FREEDEGREE)
)
;revised by wbf 081229

(if (and ?R2 ?R3 ?R4 ?R5 ?R6 ?R7)
;revised by wbf 081229 
then
(load (str-cat ?filepath "MS_SportCalc.clp"))
)
)
