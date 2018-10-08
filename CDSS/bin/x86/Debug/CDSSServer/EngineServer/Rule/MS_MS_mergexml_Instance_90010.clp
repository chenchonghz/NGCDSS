(defrule MS_MS_mergexml_Instance_90010_0
(filepath ?filepath)
(statins_Drug ?statins_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?statins_Drug YES statins_Drug))
(bind ?RI0 ?CIL010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_90010)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "可选方案：
1.调强效他汀；（瑞舒伐他汀；阿托伐他汀）
2.增加药物剂量；
3.加胆固醇吸收抑制剂")
(Recommendation "TC已服药物未达指标")
(OperateFact "TC_Reach_Standard" "NO")
(FactUsed "statins_Drug")
)
)


(defrule MS_MS_mergexml_Instance_90010_1
(filepath ?filepath)
(statins_Drug ?statins_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?statins_Drug YES statins_Drug))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_90010)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "可选方案：
1.加用他汀类药物；
2.如不能耐受他汀类药物，换用普伐他汀,氟伐他汀,加胆固醇吸收抑制剂。")
(Recommendation "TC初次服药（贝特类或他汀类）")
(OperateFact "TC_First_Drug" "YES")
(FactUsed "statins_Drug")
)
)
