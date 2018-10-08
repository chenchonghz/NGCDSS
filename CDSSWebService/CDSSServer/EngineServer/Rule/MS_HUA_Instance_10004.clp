(defrule MS_HUA_Instance_10004_0
(filepath ?filepath)
(Acute_Gouty_Arthritis_Period ?Acute_Gouty_Arthritis_Period)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Acute_Gouty_Arthritis_Period YES Acute_Gouty_Arthritis_Period))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_10004)
then
(undefrule *)
(InterpretationIndex "处于发作期，诊断为急性痛风性关节炎发作期。")
(Recommendation "急性痛风关节炎")
(OperateFact "HUA_Diagnose_Acute" "HUAcute")
(Recommendation "高尿酸血症:有")
(OperateFact "HUA_Diagnose" "Hyperuricaemia")
(load (str-cat ?filepath "MS_HUA_Instance_120015.clp"))
(FactUsed "Acute_Gouty_Arthritis_Period")
)
)
