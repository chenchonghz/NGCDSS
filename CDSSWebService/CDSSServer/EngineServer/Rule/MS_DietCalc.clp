(deffunction FourInFiveOut (?InputNum)
(bind ?Integer (div ?InputNum 1))
(bind ?BelowPoint (- ?InputNum ?Integer))
(if
	(<= ?BelowPoint 0.25)
	then
	(bind ?Result ?Integer)
	else
	(if (and (> ?BelowPoint 0.25) (<= ?BelowPoint 0.75))
		then
		(bind ?Result (+ ?Integer 0.5))
		else
		(bind ?Result (+ ?Integer 1))
	)
)
(return ?Result)
)
;总热卡参数确定
(defrule SportLevelConfirm
(SportType ?SportType)
(BMI ?BMI)
=>
(if(or(eq ?SportType NO) (eq ?SportType SICKBED))
then
(bind ?SportLevel 25)
)

(if(or(eq ?SportType FREEDEGREE) (eq ?SportType LOWDEGREE))
then
(bind ?SportLevel 30)
)

(if(or(eq ?SportType MIDDLEDEGREE) (eq ?SportType HIGHDEGREE))
then
(bind ?SportLevel 35)
)

(if(<= ?BMI 20)
then
(bind ?SportLevel (+ ?SportLevel 5))
)

(if(>= ?BMI 25)
then
(bind ?SportLevel (- ?SportLevel 5))
)

; (printout t "SportLevel = " ?SportLevel crlf)
;(assert (SportLevel ?SportLevel))
(OperateNumberFact "SportLevel" ?SportLevel)
)

(defrule IdealBMIConfirm
(Height ?Height)
=>
(bind ?Height (/ ?Height 100))
(if(< ?Height 1.50)
then
(bind ?IdealBMI 21)
)

(if(and (>= ?Height 1.50) (< ?Height 1.65))
then
(bind ?IdealBMI 22)
)

(if(and (>= ?Height 1.65) (< ?Height 1.85))
then
(bind ?IdealBMI 23)
)

(if(>= ?Height 1.85)
then
(bind ?IdealBMI 24)
)
;(printout t "IdealBMI = " ?IdealBMI crlf)

;(assert (IdealBMI ?IdealBMI))
(OperateNumberFact "IdealBMI" ?IdealBMI)
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
(printout t "SexCoeff = " ?SexCoeff crlf)
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
;(printout t "AgeCoeff = " ?AgeCoeff crlf)
;(assert (AgeCoeff ?AgeCoeff))
(OperateNumberFact "AgeCoeff" ?AgeCoeff)
)

(defrule DiseaseStatusCoeffConfirm
(DiseaseStatus ?DiseaseStatus)
=>
(if(eq ?DiseaseStatus COMMON)
then
(bind ?DiseaseStatusCoeff 1.00)
)

(if(eq ?DiseaseStatus SPECIAL)
then
(bind ?DiseaseStatusCoeff 1.05)
)
;(printout t "DiseaseStatusCoeff = " ?DiseaseStatusCoeff crlf)
;(assert (DiseaseStatusCoeff ?DiseaseStatusCoeff))
(OperateNumberFact "DiseaseStatusCoeff" ?DiseaseStatusCoeff)
)

(defrule TKcalCalc
(SportLevel ?SportLevel)
(IdealBMI ?IdealBMI)
(Height ?Height)
(SexCoeff ?SexCoeff)
(AgeCoeff ?AgeCoeff)
(DiseaseStatusCoeff ?DiseaseStatusCoeff)
=>
(bind ?Height (/ ?Height 100))
(bind ?TKcal (* (* (* (* (* (* ?SportLevel ?IdealBMI) ?Height) ?Height) ?SexCoeff) ?AgeCoeff) ?DiseaseStatusCoeff) )
(bind ?TKcal (* (div ?TKcal 5) 5))
;(printout t "总热卡为 = " ?TKcal crlf)
;(assert (TKcal ?TKcal))
(OperateNumberFact "TKcal" ?TKcal)
)

;食物类别分配
(defrule Protein_Carbo_Fat_AmountCalc
(Dyslipidemia_Diagnose_TG ?Dyslipidemia_Diagnose_TG)
(Dyslipidemia_Diagnose_TC ?Dyslipidemia_Diagnose_TC)
(Sex ?Sex)
(Age ?Age)
(Weight ?Weight)
(Cr_Variable ?Cr_Variable)
(ACr_Variable ?ACr_Variable)
(IdealBMI ?IdealBMI)
(Height ?Height)
(TKcal ?TKcal)
(UrineProtein ?UrineProtein)
(HUA_Diagnose ?HUA_Diagnose)
(DM_History ?DM_History)
(DM_Diagnose ?DM_Diagnose)
(HUA_Diagnose ?HUA_Diagnose)
(Hypertension_Diagnose ?Hypertension_Diagnose)
=>
(bind ?DietaryType "F")
(if (or (eq ?DM_History YES)(eq ?DM_Diagnose IGT) 
(eq ?DM_Diagnose IFG) (eq ?DM_Diagnose T1DM) 
(eq ?DM_Diagnose T2DM)(eq ?DM_Diagnose DM) 
(eq ?DM_Diagnose DM_NoneType)(eq ?DM_Diagnose IGR))
then
(bind ?DietaryType(str-cat ?DietaryType "1"))
)

(if (eq ?HUA_Diagnose Hyperuricaemia)
then
(bind ?DietaryType(str-cat ?DietaryType "4"))
)

(if (eq ?Hypertension_Diagnose Hypertension)
then
(bind ?DietaryType(str-cat ?DietaryType "3"))
)

(bind ?Height (/ ?Height 100))
(if(eq ?Dyslipidemia_Diagnose_TG Dyslipidemia_TG)
then
(bind ?FatAmount_Temp 20)
(bind ?CarboRatioLimit 55)
;(Recommendation "忌干果")
(OperateFact "Nut" YES)
(bind ?DietaryType(str-cat ?DietaryType "2"))
else
(bind ?FatAmount_Temp 30)
(bind ?CarboRatioLimit 50)
(OperateFact "Nut" NO)
)

(if(eq ?Dyslipidemia_Diagnose_TC Dyslipidemia_TC)
then
;(printout t "忌食动物内脏" crlf)
(Recommendation "忌食动物内脏")
(bind ?DietaryType(str-cat ?DietaryType "5"))
)

(OperateFact "DietaryType" ?DietaryType)

(bind ?BaseProteinAmount 1.2)
(if(eq ?Sex male)
then
(bind ?Ccr (div (* (* (- 140 ?Age) ?Weight) 1.228) ?Cr_Variable))
)

(if(eq ?Sex female)
then
(bind ?Ccr (div (* (* (- 140 ?Age) ?Weight) 1.04) ?Cr_Variable))
)
;(assert (Ccr ?Ccr))
;(printout t "Ccr = " ?Ccr crlf)
(OperateNumberFact "Ccr" ?Ccr)

(if(< ?Ccr 80)
then
(bind ?BaseProteinAmount (- ?BaseProteinAmount 0.2))
)

(if(or (eq ?HUA_Diagnose Hyperuricaemia) (and(> ?ACr_Variable 30)  (< ?ACr_Variable 300)) (and (> ?UrineProtein 0.15) (< ?UrineProtein 3.5)) )
;reivsed by wbf 081229 尿蛋白定量单位由mg/日转换为g/日
then
(bind ?BaseProteinAmount (- ?BaseProteinAmount 0.2))
)

(if(or (< ?Ccr 50) (>= ?ACr_Variable 300) (>= ?UrineProtein 3.5))
;reivsed by wbf 081229 尿蛋白定量单位由mg/日转换为g/日
then
(bind ?BaseProteinAmount (- ?BaseProteinAmount 0.2))
)

(if(< ?Ccr 10)
then
(bind ?BaseProteinAmount (- ?BaseProteinAmount 0.2))
)
;(printout t "BaseProteinAmount = " ?BaseProteinAmount crlf)

(if(eq ?HUA_Diagnose Hyperuricaemia) 
then
;(printout t "低嘌呤饮食（忌植物蛋白）" crlf)
(Recommendation "低嘌呤饮食（忌植物蛋白）")
)

(if(< ?Ccr 50) 
then
;(printout t "宜优质蛋白" crlf)
(Recommendation "宜优质蛋白")
)

(if(< ?Ccr 10) 
then
;(printout t "补充a酮酸" crlf)
(Recommendation "补充a酮酸")
)
(assert (BaseProteinAmount ?BaseProteinAmount))
(bind ?ProteinAmount_Temp (* (* (* ?BaseProteinAmount ?IdealBMI) ?Height) ?Height))
(assert (ProteinAmount_Temp ?ProteinAmount_Temp))
(bind ?ProteinRatio_Temp (div (* ?ProteinAmount_Temp 1000) ?TKcal)) 
(assert (ProteinRatio_Temp ?ProteinRatio_Temp))

(if(<= ?ProteinRatio_Temp 30)
then
(bind ?ProteinRatio ?ProteinRatio_Temp)
(bind ?ProteinAmount ?ProteinAmount_Temp)
else
(bind ?ProteinRatio 30)
(bind ?ProteinAmount (* ?TKcal 0.03))
)
(bind ?TKProtein (div (* ?ProteinRatio ?TKcal) 100))

;(printout t "蛋白质比例 = " ?ProteinRatio crlf)
;(assert (ProteinRatio ?ProteinRatio))
(OperateNumberFact "ProteinRatio" ?ProteinRatio)
;(bind ?ProteinAmount (div ?ProteinAmount 1))
;(printout t "蛋白质可食入量 = " ?ProteinAmount crlf)
(OperateNumberFact "ProteinAmount" ?ProteinAmount)

(bind ?CarboRatio_Temp (- (- 100 ?ProteinRatio) (div (* ?FatAmount_Temp 9) ?TKcal)))

(if(<= ?CarboRatio_Temp ?CarboRatioLimit)
then
(bind ?CarboRatio ?CarboRatio_Temp)
(bind ?CarboAmount (div (- (- ?TKcal (div (* ?ProteinRatio ?TKcal) 100)) (* ?FatAmount_Temp 9)) 4))
else
(bind ?CarboRatio ?CarboRatioLimit)
(bind ?CarboAmount (div (* ?CarboRatioLimit ?TKcal) 400))
)
(bind ?TKCarbo (div (* ?CarboRatio ?TKcal) 100))

;(printout t "糖类比例 = " ?CarboRatio crlf)
;(assert (CarboRatio ?CarboRatio))
(OperateNumberFact "CarboRatio" ?CarboRatio)
;(bind ?CarboAmount (* 5 (div ?CarboAmount 5)))
;(printout t "糖类可食入量 = " ?CarboAmount crlf)
;(assert (CarboAmount ?CarboAmount))
(OperateNumberFact "CarboAmount" ?CarboAmount)

(bind ?FatRatio (- (- 100 ?CarboRatio) ?ProteinRatio))
(bind ?FatAmount (div (* ?FatRatio ?TKcal) 900))
(bind ?TKFat (div (* ?FatRatio ?TKcal) 100))
;(printout t "脂肪比例 = " ?FatRatio crlf)
;(assert (FatRatio ?FatRatio))
(OperateNumberFact "FatRatio" ?FatRatio)
;(printout t "脂肪可食入量 = " ?FatAmount crlf)
;(assert (FatAmount ?FatAmount))
(OperateNumberFact "FatAmount" ?FatAmount)

(bind ?MeatAmount (* (- (- ?ProteinAmount (* ?CarboAmount 0.07)) 15.75) 5.556))
(bind ?MeatAmount (* 5 (div ?MeatAmount 5)))
;(printout t "瘦肉可食入量 = " ?MeatAmount crlf)
;(assert (MeatAmount ?MeatAmount))
(OperateNumberFact "MeatAmount" ?MeatAmount)

;(assert (MilkAmount 250))
;(printout t "牛奶食入量 = " 250 crlf)
(OperateNumberFact "MilkAmount" 250)

;(assert (EggAmount 1))
;(printout t "鸡蛋个数 = " 1 crlf)
(OperateNumberFact "EggAmount" 1)

(bind ?TotalCarboWithoutFruit (- (/ (* ?CarboAmount 4) 90) 1) )
(bind ?MainFood ?TotalCarboWithoutFruit)
;(printout t "主食份 = " ?MainFood crlf)
(bind ?MainFood (FourInFiveOut ?MainFood))
;(assert (MainFood ?MainFood))
(OperateNumberFact "MainFood" ?MainFood)

(bind ?Fruit 1)
;(printout t "水果份 = " ?Fruit crlf)
;(bind ?Fruit (FourInFiveOut ?Fruit))
;(assert (Fruit ?Fruit))
(OperateNumberFact "Fruit" ?Fruit)

;(bind ?Vegetable (* ?MainFood 0.25))
;(if(< ?Vegetable 1)
;then
;(bind ?Vegetable 1)
;)
;(printout t "蔬菜份 = " ?Vegetable crlf)
;(assert (Vegetable ?Vegetable))
(bind ?Vegetable (FourInFiveOut 1))
(OperateNumberFact "Vegetable" ?Vegetable)

(bind ?TotalCarbo (+ (+ ?MainFood ?Fruit) ?Vegetable))
(OperateNumberFact "TotalCarbo" ?TotalCarbo)

(bind ?Meat (/ ?MeatAmount 50) )
;(printout t "瘦肉份 = " ?Meat crlf)
;(assert (Meat ?Meat))
(bind ?Meat (FourInFiveOut ?Meat))
(OperateNumberFact "Meat" ?Meat)

(bind ?TotalProtein (+ ?Meat 2.5))
(OperateNumberFact "TotalProtein" ?TotalProtein)

(bind ?TotalFat (/ ?FatAmount 10))
(bind ?TotalFat (FourInFiveOut ?TotalFat))
;(printout t "脂肪份 = " ?TotalFat crlf)
;(assert (TotalFat ?TotalFat))
(OperateNumberFact "TotalFat" ?TotalFat)

;(printout t "食用油份 = " ?TotalFat crlf)
;(assert (Oil ?TotalFat))
(OperateNumberFact "Oil" ?TotalFat)


)

;水盐定量
(defrule WaterAndSaltConfirm
(Fat_Diagnose ?Fat_Diagnose)
(Weight ?Weight)
(Hypertension_Diagnose ?Hypertension_Diagnose)
(Ccr ?Ccr)
=>
(bind ?MaxWater (* ?Weight 40))
(bind ?MinWater (* ?Weight 30))

(if(<= ?Ccr 30)
then
(bind ?MaxWater (* ?MaxWater 0.8))
(bind ?MaxWater (* 100 (div ?MaxWater 100)))
(bind ?MinWater (* ?MinWater 0.8))
(bind ?MinWater (* 100 (div ?MinWater 100)))
)
;(printout t "食用水" ?MinWater"~" ?MaxWater "ml/日" crlf)
;(assert (MaxWater ?MaxWater))
;(assert (MinWater ?MinWater))
(OperateNumberFact "MaxWater" ?MaxWater)
(OperateNumberFact "MinWater" ?MinWater)

(if(<= ?Ccr 30)
then
(bind ?Salt 5)
else
(if(or (eq ?Hypertension_Diagnose Hypertension) (eq ?Fat_Diagnose Fat))
then
(bind ?Salt 6)
else
(bind ?Salt 8)
)
)

;(printout t "食用盐" ?Salt "g/日" crlf)
;(assert (Salt ?Salt))
(OperateNumberFact "Salt" ?Salt)
(undefrule *)
;add by wbf 081230防止规则被重复触发
)