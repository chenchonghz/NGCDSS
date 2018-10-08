(defrule MS_HUA_Instance_20004_0
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_20004)
then
(undefrule *)
(InterpretationIndex "���ڼ���ʹ���Թؽ��׷����ڣ�ȷ��Ϊʹ�硣")
(Recommendation "ʹ��:��")
(OperateFact "HUA_Diagnose_Gouty" "Gouty")
(load (str-cat ?filepath "MS_HUA_Instance_100018.clp"))
(FactUsed "Acute_Gouty_Arthritis_Period")
)
)


(defrule MS_HUA_Instance_20004_1
(filepath ?filepath)
(Acute_Gouty_Arthritis_Period ?Acute_Gouty_Arthritis_Period)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Acute_Gouty_Arthritis_Period YES Acute_Gouty_Arthritis_Period))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_20004)
then
(undefrule *)
(InterpretationIndex "�����ڼ���ʹ���Թؽ��׷����ڣ��ж��Ƿ��и�����Ѫ֢��ʷ��")
(load (str-cat ?filepath "MS_HUA_Instance_20007.clp"))
(FactUsed "Acute_Gouty_Arthritis_Period")
)
)
