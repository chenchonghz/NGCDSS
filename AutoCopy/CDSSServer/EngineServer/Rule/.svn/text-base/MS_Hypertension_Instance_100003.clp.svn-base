(defrule MS_Hypertension_Instance_100003_0
(filepath ?filepath)
(Cerebrovascular_Disease_History ?Cerebrovascular_Disease_History)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?Cerebrovascular_Disease_History YES Cerebrovascular_Disease_History))
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_100003)
then
(undefrule *)
(InterpretationIndex "ÓÐÄÔ¹£Èû£¬¼ÌÐøÅÐ¶ÏÊÇ·ñSBP<150mmHgÇÒDBP<80mmHg¡£")
(load (str-cat ?filepath "MS_Hypertension_Instance_110006.clp"))
(FactUsed "Cerebrovascular_Disease_History")
)
)


(defrule MS_Hypertension_Instance_100003_1
(filepath ?filepath)
(Cerebrovascular_Disease_History ?Cerebrovascular_Disease_History)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?Cerebrovascular_Disease_History YES Cerebrovascular_Disease_History))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_100003)
then
(undefrule *)
(InterpretationIndex "²»ÊÇÄÔ¹£Èû£¬¼ÌÐøÅÐ¶ÏÊÇ·ñ´úÐ»×ÛºÏÕ÷Î£ÏÕ¶ÈµÍÎ£¡£")
(load (str-cat ?filepath "MS_Hypertension_Instance_110009.clp"))
(FactUsed "Cerebrovascular_Disease_History")
)
)
