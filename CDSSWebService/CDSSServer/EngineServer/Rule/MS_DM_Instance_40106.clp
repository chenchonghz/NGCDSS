(defrule MS_DM_Instance_40106_0
(filepath ?filepath)
(FBG_OGTT_Variable ?FBG_OGTT_Variable)
(twoHPBG_OGTT_Variable ?twoHPBG_OGTT_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_OGTT_Variable 7.0 FBG_OGTT_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?twoHPBG_OGTT_Variable 11.1 twoHPBG_OGTT_Variable))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath DM_Instance_40106)
then
(undefrule *)
(InterpretationIndex "¿Õ¸¹ÑªÌÇ>=7.0mmol/L¡¢²Íºó2Ð¡Ê±ÑªÌÇ>=11.1mmol/L£¬Õï¶ÏÎªÓÐÌÇÄò²¡£¬¼ÌÐø½øÐÐ·ÖÐÍ¡£")
(Recommendation "ÌÇÄò²¡:ÓÐ")
(OperateFact "DM_Diagnose" "DM")
(load (str-cat ?filepath "MS_DM_Instance_40073.clp"))
(FactUsed "FBG_OGTT_Variable" "twoHPBG_OGTT_Variable")
)
)


(defrule MS_DM_Instance_40106_1
(filepath ?filepath)
(FBG_OGTT_Variable ?FBG_OGTT_Variable)
(twoHPBG_OGTT_Variable ?twoHPBG_OGTT_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?FBG_OGTT_Variable 6.1 FBG_OGTT_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf >= ?twoHPBG_OGTT_Variable 7.8 twoHPBG_OGTT_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf < ?twoHPBG_OGTT_Variable 11.1 twoHPBG_OGTT_Variable))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 3 ?ShortData ?filepath DM_Instance_40106)
then
(undefrule *)
(InterpretationIndex "¿Õ¸¹ÑªÌÇ<6.1mmol/L¡¢²Íºó2Ð¡Ê±ÑªÌÇ7.8¡«11.1mmol/L£¬Õï¶ÏÎªIGT¡£")
(Recommendation "ÌÇÄò²¡:ÓÐ")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "ÌÇ´úÐ»Ë®Æ½:IGT")
(OperateFact "DM_Diagnose" "IGT")
(FactUsed "FBG_OGTT_Variable" "twoHPBG_OGTT_Variable")
)
)


(defrule MS_DM_Instance_40106_2
(filepath ?filepath)
(FBG_OGTT_Variable ?FBG_OGTT_Variable)
(twoHPBG_OGTT_Variable ?twoHPBG_OGTT_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_OGTT_Variable 6.1 FBG_OGTT_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf < ?FBG_OGTT_Variable 7.0 FBG_OGTT_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf < ?twoHPBG_OGTT_Variable 7.8 twoHPBG_OGTT_Variable))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 3 ?ShortData ?filepath DM_Instance_40106)
then
(undefrule *)
(InterpretationIndex "¿Õ¸¹ÑªÌÇ6.1¡«7.0mmol/L¡¢²Íºó2Ð¡Ê±ÑªÌÇ<7.8mmol/L£¬Õï¶ÏÎªIFG¡£")
(Recommendation "ÌÇÄò²¡:ÓÐ")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "ÌÇ´úÐ»Ë®Æ½:IFG")
(OperateFact "DM_Diagnose" "IFG")
(FactUsed "FBG_OGTT_Variable" "twoHPBG_OGTT_Variable")
)
)


(defrule MS_DM_Instance_40106_3
(filepath ?filepath)
(FBG_OGTT_Variable ?FBG_OGTT_Variable)
(twoHPBG_OGTT_Variable ?twoHPBG_OGTT_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_OGTT_Variable 6.1 FBG_OGTT_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf < ?FBG_OGTT_Variable 7.0 FBG_OGTT_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf >= ?twoHPBG_OGTT_Variable 7.8 twoHPBG_OGTT_Variable))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL310 (Leaf < ?twoHPBG_OGTT_Variable 11.1 twoHPBG_OGTT_Variable))
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
(if(NotifyOrNot equals ?Threshhold 4 ?ShortData ?filepath DM_Instance_40106)
then
(undefrule *)
(InterpretationIndex "¿Õ¸¹ÑªÌÇ6.1¡«7.0mmol/L¡¢²Íºó2Ð¡Ê±ÑªÌÇ7.8¡«11.1mmol/L£¬Õï¶ÏÎªIGR¡£")
(Recommendation "ÌÇÄò²¡:ÓÐ")
(OperateFact "DM_Diagnose" "DM")
(Recommendation "ÌÇ´úÐ»Ë®Æ½:IGR")
(OperateFact "DM_Diagnose" "IGR")
(FactUsed "FBG_OGTT_Variable" "twoHPBG_OGTT_Variable")
)
)


(defrule MS_DM_Instance_40106_4
(filepath ?filepath)
(FBG_OGTT_Variable ?FBG_OGTT_Variable)
(twoHPBG_OGTT_Variable ?twoHPBG_OGTT_Variable)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf >= ?FBG_OGTT_Variable 3.9 FBG_OGTT_Variable))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf < ?FBG_OGTT_Variable 6.1 FBG_OGTT_Variable))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL210 (Leaf < ?twoHPBG_OGTT_Variable 7.8 twoHPBG_OGTT_Variable))
(bind ?RI2 ?CIL210)
(if
(eq ?RI2 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(bind ?ShortData (AddOrNot ?RI2 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 3 ?ShortData ?filepath DM_Instance_40106)
then
(undefrule *)
(InterpretationIndex "¿Õ¸¹ÑªÌÇ3.9¡«6.1mmol/L¡¢²Íºó2Ð¡Ê±ÑªÌÇ<7.8mmol/L£¬Õï¶ÏÎªÎÞÌÇÄò²¡¡£")
(Recommendation "ÎÞÌÇÄò²¡")
(OperateFact "DM_Diagnose" "DM_Normal")
(FactUsed "FBG_OGTT_Variable" "twoHPBG_OGTT_Variable")
)
)
