(defrule MS_MS_mergexml_Instance_70064_0
(filepath ?filepath)
(TG_Reach_Standard ?TG_Reach_Standard)
(TC_Reach_Standard ?TC_Reach_Standard)
(HDLch_Reach_Standard ?HDLch_Reach_Standard)
(LDLch_Reach_Standard ?LDLch_Reach_Standard)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?TG_Reach_Standard YES TG_Reach_Standard))
(bind ?CIL021 (Leaf equals ?TC_Reach_Standard YES TC_Reach_Standard))
(bind ?CIL022 (Leaf equals ?HDLch_Reach_Standard YES HDLch_Reach_Standard))
(bind ?CIL023 (Leaf equals ?LDLch_Reach_Standard YES LDLch_Reach_Standard))
(if
(and (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022)  (Transform ?CIL023) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL022 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL023 ?CIN010))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_70064)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "自我监测管理：
4-6月检查一次血脂4项。")
(FactUsed "TG_Reach_Standard" "TC_Reach_Standard" "HDLch_Reach_Standard" "LDLch_Reach_Standard")
)
)


(defrule MS_MS_mergexml_Instance_70064_1
(filepath ?filepath)
(TG_Reach_Standard ?TG_Reach_Standard)
(TC_Reach_Standard ?TC_Reach_Standard)
(HDLch_Reach_Standard ?HDLch_Reach_Standard)
(LDLch_Reach_Standard ?LDLch_Reach_Standard)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?TG_Reach_Standard YES TG_Reach_Standard))
(bind ?CIL021 (Leaf equals ?TC_Reach_Standard YES TC_Reach_Standard))
(bind ?CIL022 (Leaf equals ?HDLch_Reach_Standard YES HDLch_Reach_Standard))
(bind ?CIL023 (Leaf equals ?LDLch_Reach_Standard YES LDLch_Reach_Standard))
(if
(and (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022)  (Transform ?CIL023) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL022 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL023 ?CIN010))
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
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_70064)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "自我监测管理：
调整药物治疗后2-4周复查血脂4项，了解治疗后的效果。")
(Recommendation "自我监测管理：
4-6月检查一次血脂4项。")
(FactUsed "TG_Reach_Standard" "TC_Reach_Standard" "HDLch_Reach_Standard" "LDLch_Reach_Standard")
)
)
