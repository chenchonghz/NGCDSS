(defrule MS_DM_Instance_40073_0
(filepath ?filepath)
(Sex ?Sex)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Sex male Sex))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_40073)
then
(undefrule *)
(InterpretationIndex "���Ի��ߣ��ж��Ƿ���֡������򲡼���ʷ���������30�ꡣ")
(load (str-cat ?filepath "MS_DM_Instance_40075.clp"))
(FactUsed "Sex")
)
)


(defrule MS_DM_Instance_40073_1
(filepath ?filepath)
(Sex ?Sex)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Sex female Sex))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_40073)
then
(undefrule *)
(InterpretationIndex "Ů�Ի��ߣ��ж��Ƿ���֡������򲡼���ʷ���������30�ꡢ��GDM��ʷ���о޴������ʷ��")
(load (str-cat ?filepath "MS_DM_Instance_40076.clp"))
(FactUsed "Sex")
)
)
