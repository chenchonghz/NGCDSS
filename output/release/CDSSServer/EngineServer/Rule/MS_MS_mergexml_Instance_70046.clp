(defrule MS_MS_mergexml_Instance_70046_0
(filepath ?filepath)
(TG_First_Drug ?TG_First_Drug)
(TC_First_Drug ?TC_First_Drug)
(HDLch_First_Drug ?HDLch_First_Drug)
(LDLch_First_Drug ?LDLch_First_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?TG_First_Drug YES TG_First_Drug))
(bind ?CIL021 (Leaf equals ?TC_First_Drug YES TC_First_Drug))
(bind ?CIL022 (Leaf equals ?HDLch_First_Drug YES HDLch_First_Drug))
(bind ?CIL023 (Leaf equals ?LDLch_First_Drug YES LDLch_First_Drug))
(if
(or (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022)  (Transform ?CIL023) )
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_70046)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "自我监测管理：
加服调脂药后2-4周内必须到医院复查肝功能和血脂，了解治疗效果和肝功能变化。")
(FactUsed "TG_First_Drug" "TC_First_Drug" "HDLch_First_Drug" "LDLch_First_Drug")
)
)


(defrule MS_MS_mergexml_Instance_70046_1
(filepath ?filepath)
(TG_First_Drug ?TG_First_Drug)
(TC_First_Drug ?TC_First_Drug)
(HDLch_First_Drug ?HDLch_First_Drug)
(LDLch_First_Drug ?LDLch_First_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf equals ?TG_First_Drug YES TG_First_Drug))
(bind ?CIL021 (Leaf equals ?TC_First_Drug YES TC_First_Drug))
(bind ?CIL022 (Leaf equals ?HDLch_First_Drug YES HDLch_First_Drug))
(bind ?CIL023 (Leaf equals ?LDLch_First_Drug YES LDLch_First_Drug))
(if
(or (Transform ?CIL020)  (Transform ?CIL021)  (Transform ?CIL022)  (Transform ?CIL023) )
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_70046)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_70064.clp"))
(FactUsed "TG_First_Drug" "TC_First_Drug" "HDLch_First_Drug" "LDLch_First_Drug")
)
)
