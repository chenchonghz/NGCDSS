(defrule MS_HUA_Instance_100083_0
(filepath ?filepath)
(UPh ?UPh)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?UPh 6.0 UPh))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath HUA_Instance_100083)
then
(undefrule *)
(InterpretationIndex "��Ph<6.0������1.�ӷ�̼�����ɣ�
2.���ڸ��飬������pH6~6.5��")
(Recommendation "�ӷ�̼������")
(FactUsed "UPh")
)
)
