(defrule MS_Dyslipidemia_Instance_110179_0
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_110179)
then
(undefrule *)
(InterpretationIndex "没有服用贝特类药物，进行贝特类药物治疗，并1个月定期复查。")
(Recommendation "在医生指导下选用贝特类药物")
(Recommendation "HDLch初次服药（贝特类或他汀类）")
(OperateFact "HDLch_First_Drug" "YES")
(FactUsed "fibrates_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_110179_1
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_110179)
then
(undefrule *)
(InterpretationIndex "已服用贝特类药物，治疗建议，加烟酸，并1个月定期复查。")
(Recommendation "加烟酸")
(Recommendation "HDLch已服药物未达指标")
(OperateFact "HDLch_Reach_Standard" "NO")
(FactUsed "fibrates_Drug")
)
)
