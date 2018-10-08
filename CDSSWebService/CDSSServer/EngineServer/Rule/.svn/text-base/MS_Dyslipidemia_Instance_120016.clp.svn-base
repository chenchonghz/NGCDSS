(defrule MS_Dyslipidemia_Instance_120016_0
(filepath ?filepath)
(fibrates_Drug ?fibrates_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?fibrates_Drug YES fibrates_Drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_120016)
then
(undefrule *)
(InterpretationIndex "已服过贝特类药物，建议加服赛钶，亚油酸，并1个月定期复查。")
(Recommendation "加服赛尼可，亚油酸")
(Recommendation "TG已服药物未达指标")
(OperateFact "TG_Reach_Standard" "NO")
(FactUsed "fibrates_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_120016_1
(filepath ?filepath)
(fibrates_Drug ?fibrates_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?fibrates_Drug YES fibrates_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_120016)
then
(undefrule *)
(InterpretationIndex "没有服过贝特类药物，建议使用贝特类药物或烟酸、亚油酸，并1个月定期复查。")
(Recommendation "使用贝特类药物或烟酸、亚油酸")
(Recommendation "TG已服药物未达指标")
(OperateFact "TG_Reach_Standard" "NO")
(FactUsed "fibrates_Drug")
)
)
