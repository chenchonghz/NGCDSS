(defrule MS_Hypertension_Instance_4_0
(filepath ?filepath)
(DBP_Top_Variable ?DBP_Top_Variable)
(SBP_Top_Variable ?SBP_Top_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?DBP_Top_Variable 110.0 DBP_Top_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?SBP_Top_Variable 180.0 SBP_Top_Variable))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_4)
then
(undefrule *)
(InterpretationIndex "舒张压>=110mmHg或收缩压>=180mmHg，诊断为高血压3级，并继续判断有无继发性因素。")
(Recommendation "高血压:3级")
(OperateFact "hypertension_Diagnose_Stage" "Third_Stage")
(Recommendation "高血压:有")
(OperateFact "Hypertension_Diagnose" "Hypertension")
(load (str-cat ?filepath "MS_Hypertension_Instance_30000.clp"))
(FactUsed "DBP_Top_Variable" "SBP_Top_Variable")
)
)


(defrule MS_Hypertension_Instance_4_1
(filepath ?filepath)
(DBP_Top_Variable ?DBP_Top_Variable)
(SBP_Top_Variable ?SBP_Top_Variable)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf >= ?DBP_Top_Variable 110.0 DBP_Top_Variable))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf >= ?SBP_Top_Variable 180.0 SBP_Top_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_4)
then
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?DBP_Top_Variable 100.0 DBP_Top_Variable))
(bind ?CIL021 (Leaf <= ?DBP_Top_Variable 109.0 DBP_Top_Variable))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL120 (Leaf >= ?SBP_Top_Variable 160.0 SBP_Top_Variable))
(bind ?CIL121 (Leaf <= ?SBP_Top_Variable 179.0 SBP_Top_Variable))
(if
(and (Transform ?CIL120)  (Transform ?CIL121) )
then
(bind ?CIN110 TRUE)
else
(bind ?CIN110 NULL)
(bind ?CIN110 (AddOrNot ?CIL120 ?CIN110))
(bind ?CIN110 (AddOrNot ?CIL121 ?CIN110))
(if(eq ?CIN110 NULL)
then
(bind ?CIN110 FALSE)
)
)
(bind ?RI1 ?CIN110)
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_4)
then
(undefrule *)
(InterpretationIndex "舒张压100～109mmHg或收缩压160～179mmHg，诊断为高血压2级，并继续判断有无继发性因素。")
(Recommendation "高血压:2级")
(OperateFact "hypertension_Diagnose_Stage" "Second_Stage")
(Recommendation "高血压:有")
(OperateFact "Hypertension_Diagnose" "Hypertension")
(load (str-cat ?filepath "MS_Hypertension_Instance_30000.clp"))
(FactUsed "DBP_Top_Variable" "SBP_Top_Variable")
)
)
)


(defrule MS_Hypertension_Instance_4_2
(filepath ?filepath)
(DBP_Top_Variable ?DBP_Top_Variable)
(SBP_Top_Variable ?SBP_Top_Variable)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf >= ?DBP_Top_Variable 100.0 DBP_Top_Variable))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf >= ?SBP_Top_Variable 160.0 SBP_Top_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_4)
then
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?DBP_Top_Variable 90.0 DBP_Top_Variable))
(bind ?CIL021 (Leaf <= ?DBP_Top_Variable 99.0 DBP_Top_Variable))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL120 (Leaf >= ?SBP_Top_Variable 140.0 SBP_Top_Variable))
(bind ?CIL121 (Leaf <= ?SBP_Top_Variable 159.0 SBP_Top_Variable))
(if
(and (Transform ?CIL120)  (Transform ?CIL121) )
then
(bind ?CIN110 TRUE)
else
(bind ?CIN110 NULL)
(bind ?CIN110 (AddOrNot ?CIL120 ?CIN110))
(bind ?CIN110 (AddOrNot ?CIL121 ?CIN110))
(if(eq ?CIN110 NULL)
then
(bind ?CIN110 FALSE)
)
)
(bind ?RI1 ?CIN110)
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_4)
then
(undefrule *)
(InterpretationIndex "舒张压90～99mmHg或收缩压140～159mmHg，诊断为高血压1级，并继续判断有无继发性因素。")
(Recommendation "高血压:1级")
(OperateFact "hypertension_Diagnose_Stage" "First_Stage")
(Recommendation "高血压:有")
(OperateFact "Hypertension_Diagnose" "Hypertension")
(load (str-cat ?filepath "MS_Hypertension_Instance_30000.clp"))
(FactUsed "DBP_Top_Variable" "SBP_Top_Variable")
)
)
)


(defrule MS_Hypertension_Instance_4_3
(filepath ?filepath)
(DBP_Top_Variable ?DBP_Top_Variable)
(SBP_Top_Variable ?SBP_Top_Variable)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf >= ?DBP_Top_Variable 90.0 DBP_Top_Variable))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf >= ?SBP_Top_Variable 140.0 SBP_Top_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_4)
then
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf >= ?DBP_Top_Variable 85.0 DBP_Top_Variable))
(bind ?CIL021 (Leaf <= ?DBP_Top_Variable 89.0 DBP_Top_Variable))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL120 (Leaf >= ?SBP_Top_Variable 130.0 SBP_Top_Variable))
(bind ?CIL121 (Leaf <= ?SBP_Top_Variable 139.0 SBP_Top_Variable))
(if
(and (Transform ?CIL120)  (Transform ?CIL121) )
then
(bind ?CIN110 TRUE)
else
(bind ?CIN110 NULL)
(bind ?CIN110 (AddOrNot ?CIL120 ?CIN110))
(bind ?CIN110 (AddOrNot ?CIL121 ?CIN110))
(if(eq ?CIN110 NULL)
then
(bind ?CIN110 FALSE)
)
)
(bind ?RI1 ?CIN110)
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_4)
then
(undefrule *)
(InterpretationIndex "舒张压85～89mmHg或收缩压130～139mmHg，诊断为高血压前期，并继续判断有无继发性因素。")
(Recommendation "高血压:前期")
(OperateFact "hypertension_Diagnose_Stage" "pre_Stage")
(Recommendation "高血压:有")
(OperateFact "Hypertension_Diagnose" "Hypertension")
(load (str-cat ?filepath "MS_Hypertension_Instance_30000.clp"))
(FactUsed "DBP_Top_Variable" "SBP_Top_Variable")
)
)
)


(defrule MS_Hypertension_Instance_4_4
(filepath ?filepath)
(DBP_Top_Variable ?DBP_Top_Variable)
(SBP_Top_Variable ?SBP_Top_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?DBP_Top_Variable 85.0 DBP_Top_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?SBP_Top_Variable 130.0 SBP_Top_Variable))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_4)
then
(undefrule *)
(InterpretationIndex "舒张压<85mmHg且收缩压<130mmHg，诊断为无高血压。")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_80000.clp"))
(FactUsed "DBP_Top_Variable" "SBP_Top_Variable")
)
)
