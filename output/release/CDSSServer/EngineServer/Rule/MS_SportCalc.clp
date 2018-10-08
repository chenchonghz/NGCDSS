(deffunction ConfirmGlucoseLevel(?Coeff ?Lower ?Upper)
	(if(< ?Coeff ?Lower) 
		then
		(bind ?GlucoseLevel GOOD)
		else
		(if(and (>= ?Coeff ?Lower) (<= ?Coeff ?Upper))
			then
			(bind ?GlucoseLevel NEUTER)
			else
			(bind ?GlucoseLevel BAD)
		)
	)
	;(assert (GlucoseLevel ?GlucoseLevel))
	(OperateFact "GlucoseLevel" ?GlucoseLevel)
)

(defrule GlucoseLevelConfirmByHbA1c
(HbA1c ?HbA1c)
=>
(if(eq ?HbA1c NULL)
	then
	;(assert (GLByFBGA TRUE))
	(OperateFact "GLByFBGA" "TRUE")
	else
	(ConfirmGlucoseLevel ?HbA1c 6.5 7.5)
)
)

(defrule GlucoseLevelConfirmByFBGA
(FBG_Variable ?FBG_Variable)
(GLByFBGA TRUE)
=>
(if(eq ?FBG_Variable NULL)
	then
	;(assert (GLByRBGA TRUE))
	(OperateFact "GLByRBGA" "TRUE")
	else
	(ConfirmGlucoseLevel ?FBG_Variable 6.2 7.0)
)
)

(defrule GlucoseLevelConfirmByRBGA
(BG_Variable ?BG_Variable)
(GLByRBGA TRUE)
=>
(if(not (eq ?BG_Variable NULL))
	then
	(ConfirmGlucoseLevel ?BG_Variable 6.5 7.5)
	;else
	;(printout t "无法判断血糖水平" crlf)
)
)

(defrule SexCoeffConfirm
(Sex ?Sex)
=>
(if(eq ?Sex male)
then
(bind ?SexCoeff 1)
)

(if(eq ?Sex female)
then
(bind ?SexCoeff 0.95)
)
;(assert (SexCoeff ?SexCoeff))
(OperateNumberFact "SexCoeff" ?SexCoeff)
)

(defrule AgeCoeffConfirm
(Age ?Age)
=>
(if(< ?Age 25)
then
(bind ?AgeCoeff 1.05)
)

(if(and (>= ?Age 25) (< ?Age 55))
then
(bind ?AgeCoeff 1.00)
)

(if(and (>= ?Age 55) (< ?Age 65))
then
(bind ?AgeCoeff 0.95)
)

(if(and (>= ?Age 65) (< ?Age 75))
then
(bind ?AgeCoeff 0.90)
)

(if(and (>= ?Age 75) (< ?Age 85))
then
(bind ?AgeCoeff 0.85)
)

(if(>= ?Age 85)
then
(bind ?AgeCoeff 0.80)
)
;(assert (AgeCoeff ?AgeCoeff))
(OperateNumberFact "AgeCoeff" ?AgeCoeff)
)

(defrule ReduceWeight
(BMI ?BMI)
=>
(if(>= ?BMI 25)
then
;(assert (ReduceWeight YES))
(OperateFact "ReduceWeight" "YES")
;(printout t "减轻体重" crlf)
)
)

(defrule ReduceBG
(GlucoseLevel ?GlucoseLevel)
=>
(if(not (eq ?GlucoseLevel GOOD))
then
;(assert (ReduceBG YES))
(OperateFact "ReduceBG" "YES")
;(printout t "降低餐后血糖" crlf)
)
)

(defrule StrengthenPhysique
(BMI ?BMI)
(GlucoseLevel ?GlucoseLevel)
=>
(if(and (< ?BMI 25) (eq ?GlucoseLevel GOOD))
then
;(assert (StrengthenPhysique YES))
(OperateFact "StrengthenPhysique" "YES")
;(printout t "增强体质" crlf)
else
(OperateFact "StrengthenPhysique" "NO")
)
)

(defrule ReduceBGCal
(GlucoseLevel ?GlucoseLevel)
(ReduceBG YES)
=>
(if(eq ?GlucoseLevel BAD)
then
(OperateNumberFact "ReduceBGCal" 150)
else
(OperateNumberFact "ReduceBGCal" 100)
)
)

(defrule ReduceWeightCal
(BMI ?BMI)
(ReduceWeight YES)
=>
(if(>= ?BMI 30)
then
(OperateNumberFact "ReduceWeightCal" 600)
else
(OperateNumberFact "ReduceWeightCal" 450)
)
)

(defrule SportType
(Age ?Age)
(LimbDyskinesia ?LimbDyskinesia)
(SportType ?SportType)
(StrengthenPhysique ?StrengthenPhysique)
=>
(bind ?SportTypeLow YES)
(bind ?SportTypeMiddle YES)
(bind ?SportTypeHigh YES)

(if(or (> ?Age 60) (eq ?LimbDyskinesia YES) (eq ?SportType SICKBED))
then
(bind ?SportTypeHigh NO)
)

(if(eq ?SportType SICKBED)
then
(bind ?SportTypeMiddle NO)
)

(if(eq ?StrengthenPhysique YES)
then
(bind ?SportTypeHigh NO)
)

;(assert (SportTypeLow ?SportTypeLow))
;(assert (SportTypeMiddle ?SportTypeMiddle))
;(assert (SportTypeHigh ?SportTypeHigh))
(OperateFact "SportTypeLow" ?SportTypeLow)
(OperateFact "SportTypeMiddle" ?SportTypeMiddle)
(OperateFact "SportTypeHigh" ?SportTypeHigh)
(OperateNumberFact "LEnergyConsumption" 0.07)
(OperateNumberFact "MEnergyConsumption" 0.14)
(OperateNumberFact "HEnergyConsumption" 0.23)
)


