(defrule MS_HUA_Instance_10000_0
(filepath ?filepath)
(Acute_Gouty_Arthritis_History ?Acute_Gouty_Arthritis_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Acute_Gouty_Arthritis_History YES Acute_Gouty_Arthritis_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_10000)
then
(undefrule *)
(InterpretationIndex "�м���ʹ���Թؽ��׷���ʷ���ж��Ƿ��ڷ����ڡ�")
(load (str-cat ?filepath "MS_HUA_Instance_10004.clp"))
(FactUsed "Acute_Gouty_Arthritis_History")
)
)


(defrule MS_HUA_Instance_10000_1
(filepath ?filepath)
(Acute_Gouty_Arthritis_History ?Acute_Gouty_Arthritis_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Acute_Gouty_Arthritis_History YES Acute_Gouty_Arthritis_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath HUA_Instance_10000)
then
(undefrule *)
(InterpretationIndex "û�м���ʹ���Թؽ��׷���ʷ���ж��Ƿ���ʹ��ʯ�������ʯ����·��ʯ��")
(load (str-cat ?filepath "MS_HUA_Instance_20018.clp"))
(FactUsed "Acute_Gouty_Arthritis_History")
)
)
