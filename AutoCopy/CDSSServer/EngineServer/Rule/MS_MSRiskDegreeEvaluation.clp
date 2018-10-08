(deffunction FamilyHis (?FamilyHisItem ?FamilyHisValue ?FamilyHisNum)
(if(eq ?FamilyHisItem ?FamilyHisValue)
then
(bind ?FamilyHisNum (+ ?FamilyHisNum 0.5))
)
(return ?FamilyHisNum) 
)

(deffunction His (?HisItem ?HisValue ?HisNum)
(if(eq ?HisItem ?HisValue)
then
(bind ?HisNum (+ ?HisNum 1))
)
(return ?HisNum) 
)

(defrule MSRiskDegreeEvaluation
(DM_Family_History ?DM_Family_History)
(Hypertension_Family_History ?Hypertension_Family_History)
(Dyslipidemia_Family_History ?Dyslipidemia_Family_History)
(Hyperuricemia_Family_History ?Hyperuricemia_Family_History)
(Coronary_Heart_Disease_Family_History ?Coronary_Heart_Disease_Family_History)
(Cerebral_Infarct_Family_History ?Cerebral_Infarct_Family_History)

(DM_History ?DM_History)
(DM_Diagnose ?DM_Diagnose)
(Hypertension_Diagnose ?Hypertension_Diagnose)
(Fat_Diagnose ?Fat_Diagnose)
(HUA_Diagnose ?HUA_Diagnose)
(Cardiopathy_History ?Cardiopathy_History)
(Cerebral_Infarct ?Cerebral_Infarct)
(Lower_Extremity_Angiopathy ?Lower_Extremity_Angiopathy)
(smoke_history ?smoke_history)
(ACr_Variable ?ACr_Variable)
(Dyslipidemia_Diagnose_TG ?Dyslipidemia_Diagnose_TG)
(Dyslipidemia_Diagnose_HDLC ?Dyslipidemia_Diagnose_HDLC)
(Dyslipidemia_Diagnose_LDLC ?Dyslipidemia_Diagnose_LDLC)

=>
(bind ?FamilyHisNum 0)
(bind ?HisNum 0)
(bind ?FamilyHisNum (FamilyHis ?DM_Family_History YES ?FamilyHisNum))
(bind ?FamilyHisNum (FamilyHis ?Hypertension_Family_History YES ?FamilyHisNum))
(bind ?FamilyHisNum (FamilyHis ?Dyslipidemia_Family_History YES ?FamilyHisNum))
(bind ?FamilyHisNum (FamilyHis ?Hyperuricemia_Family_History YES ?FamilyHisNum))
(bind ?FamilyHisNum (FamilyHis ?Coronary_Heart_Disease_Family_History YES ?FamilyHisNum))
(bind ?FamilyHisNum (FamilyHis ?Cerebral_Infarct_Family_History YES ?FamilyHisNum))

(if (or (eq ?DM_History YES)(eq ?DM_Diagnose IGT) 
(eq ?DM_Diagnose IFG) (eq ?DM_Diagnose T1DM) 
(eq ?DM_Diagnose T2DM)(eq ?DM_Diagnose DM) 
(eq ?DM_Diagnose DM_NoneType)(eq ?DM_Diagnose IGR))
then
(bind ?HisNum (+ ?HisNum 1))
)

(if (or (eq ?Dyslipidemia_Diagnose_TG Dyslipidemia_TG) (eq ?Dyslipidemia_Diagnose_HDLC Dyslipidemia_HDLch))
then
(bind ?HisNum (+ ?HisNum 1))
)

(if (eq ?Dyslipidemia_Diagnose_LDLC Dyslipidemia_LDLch)
then
(bind ?HisNum (+ ?HisNum 1))
)

(bind ?HisNum (His ?Hypertension_Diagnose Hypertension ?HisNum))
(bind ?HisNum (His ?Fat_Diagnose Fat ?HisNum))
(bind ?HisNum (His ?HUA_Diagnose Hyperuricaemia ?HisNum))
(bind ?HisNum (His ?Cardiopathy_History YES ?HisNum))
(bind ?HisNum (His ?Cerebral_Infarct YES ?HisNum))
(bind ?HisNum (His ?Lower_Extremity_Angiopathy YES ?HisNum))
(bind ?HisNum (His ?smoke_history YES ?HisNum))
(bind ?HisNum (His ?ACr_Variable YES ?HisNum))

(bind ?risk_score (+ ?FamilyHisNum ?HisNum))
;(assert (risk_score ?risk_score)) 
(OperateNumberFact "risk_score" ?risk_score)

(
if(< ?risk_score 2)
	then
	(bind ?MSRiskDegree LOW)
	else
	(
		if(and (< ?risk_score 4) (>= ?risk_score 2))
			then
			(bind ?MSRiskDegree MIDDLE)
			else
			(
				if(and (< ?risk_score 6) (>= ?risk_score 4))
					then
					(bind ?MSRiskDegree HIGH)
					else
					(bind ?MSRiskDegree VERYHIGH)
			)
	)
)
;(assert (MSRiskDegree ?MSRiskDegree))
(OperateFact "MSRiskDegree" ?MSRiskDegree)
)

