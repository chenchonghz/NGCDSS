(defrule MS_Dyslipidemia_Instance_90028_0
(filepath ?filepath)
(Dyslipidemia_Drug ?Dyslipidemia_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_Drug YES Dyslipidemia_Drug))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_90028)
then
(undefrule *)
(InterpretationIndex "已服用过调脂药，继续判断是否服用他汀类药物。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_100010.clp"))
(FactUsed "Dyslipidemia_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_90028_1
(filepath ?filepath)
(Dyslipidemia_Drug ?Dyslipidemia_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Dyslipidemia_Drug YES Dyslipidemia_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_90028)
then
(undefrule *)
(InterpretationIndex "没有服用过调脂药，建议采用他汀类药物：普伐他汀；氟伐他汀；洛伐他汀；辛伐他汀；匹他伐他汀；阿托伐他汀；瑞舒伐他汀，并1个月定期复查。")
(Recommendation "在医生指导下选用贝特类药物")
(Recommendation "LDLch初次服药（贝特类或他汀类）")
(OperateFact "LDLch_First_Drug" "YES")
(FactUsed "Dyslipidemia_Drug")
)
)
