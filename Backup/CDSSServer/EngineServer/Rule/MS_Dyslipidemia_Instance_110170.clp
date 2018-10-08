(defrule MS_Dyslipidemia_Instance_110170_0
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_110170)
then
(undefrule *)
(InterpretationIndex "没有服用他汀类药物，继续判断是否服用贝特类药物。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110179.clp"))
(FactUsed "statins_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_110170_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110170)
then
(undefrule *)
(InterpretationIndex "已经服用过他汀类药物，可选方案为：调升HDL药，如瑞舒伐他汀，加烟酸，并1个月定期复查。")
(Recommendation "可选方案：
调升HDL药，如瑞舒伐他汀，加烟酸。")
(Recommendation "HDLch已服药物未达指标")
(OperateFact "HDLch_Reach_Standard" "NO")
(FactUsed "statins_Drug")
)
)
