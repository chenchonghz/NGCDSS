(defrule MS_Hypertension_Instance_110009_0
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf < ?risk_score 2.0 risk_score))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_110009)
then
(undefrule *)
(InterpretationIndex "��л�ۺ���Σ�նȵ�Σ������<2���������ж��Ƿ�SBP<130mmHg��DBP<85mmHg��")
(load (str-cat ?filepath "MS_Hypertension_Instance_140004.clp"))
(FactUsed "risk_score")
)
)


(defrule MS_Hypertension_Instance_110009_1
(filepath ?filepath)
(risk_score ?risk_score)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf < ?risk_score 2.0 risk_score))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_110009)
then
(undefrule *)
(InterpretationIndex "��л�ۺ���Σ�նȲ��ǵ�Σ�������ж��Ƿ�SBP<120mmHg��DBP<80mmHg��")
(load (str-cat ?filepath "MS_Hypertension_Instance_140008.clp"))
(FactUsed "risk_score")
)
)
