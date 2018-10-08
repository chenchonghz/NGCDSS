(defrule MS_Dyslipidemia_Instance_120006_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Dyslipidemia_Instance_120006)
then
(undefrule *)
(InterpretationIndex "已经服过调脂药，继续判断是否已经采用贝特类药物。")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_120016.clp"))
(FactUsed "Dyslipidemia_Drug")
)
)


(defrule MS_Dyslipidemia_Instance_120006_1
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Dyslipidemia_Instance_120006)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "在医生指导下选用贝特类药物")
(Recommendation "TG初次服药（贝特类或他汀类）")
(OperateFact "TG_First_Drug" "YES")
(FactUsed "Dyslipidemia_Drug")
)
)
