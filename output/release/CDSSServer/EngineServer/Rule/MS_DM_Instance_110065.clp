(defrule MS_DM_Instance_110065_0
(filepath ?filepath)
(HbA1c ?HbA1c)
=>
(bind ?Threshhold 0)

(bind ?CIL020 (Leaf < ?HbA1c 7.0 HbA1c))
(bind ?CIL021 (Leaf >= ?HbA1c 6.5 HbA1c))
(if
(and (Transform ?CIL020)  (Transform ?CIL021) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIL021 ?CIN010))
(if(eq ?CIN010 NULL)
then
(bind ?CIN010 FALSE)
)
)
(bind ?RI0 ?CIN010)
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
(if(NotifyOrNot equals ?Threshhold 1 ?ShortData ?filepath DM_Instance_110065)
then
(undefrule *)
(InterpretationIndex "6.5%<=HbA1c<7%£¬¼ÌÐøÅÐ¶ÏÊÇ·ñBMI>23¡£")

(FactUsed "HbA1c")
)
)
