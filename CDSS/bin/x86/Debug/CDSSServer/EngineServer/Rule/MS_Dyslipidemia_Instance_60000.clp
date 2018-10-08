(defrule MS_Dyslipidemia_Instance_60000_0
(filepath ?filepath)
(Dyslipidemia_History ?Dyslipidemia_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Dyslipidemia_History YES Dyslipidemia_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_60000)
then
(undefrule *)
(InterpretationIndex "有血脂紊乱疾病史，继续判断是否有高甘油三酯病史。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_70002.clp"))
(FactUsed "Dyslipidemia_History")
)
)


(defrule MS_Dyslipidemia_Instance_60000_1
(filepath ?filepath)
(Dyslipidemia_History ?Dyslipidemia_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Dyslipidemia_History YES Dyslipidemia_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_60000)
then
(undefrule *)
(InterpretationIndex "无血脂紊乱病史，继续进行指标诊断血脂异常分类。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_20010.clp"))
(FactUsed "Dyslipidemia_History")
)
)
