(defrule MS_Hypertension_Instance_130046_0
(filepath ?filepath)
(irenal_artery_Bistenosis ?irenal_artery_Bistenosis)
(abnormal_renal_function ?abnormal_renal_function)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?irenal_artery_Bistenosis YES irenal_artery_Bistenosis))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?abnormal_renal_function YES abnormal_renal_function))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_130046)
then
(undefrule *)
(InterpretationIndex "肾动脉狭窄(双)或肾功能异常，建议首选CCB或BB。")
(Recommendation "首选二氢吡啶类钙离子拮抗剂或贝特受体阻滞剂")
(FactUsed "irenal_artery_Bistenosis" "abnormal_renal_function")
)
)


(defrule MS_Hypertension_Instance_130046_1
(filepath ?filepath)
(irenal_artery_Bistenosis ?irenal_artery_Bistenosis)
(abnormal_renal_function ?abnormal_renal_function)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?irenal_artery_Bistenosis YES irenal_artery_Bistenosis))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?abnormal_renal_function YES abnormal_renal_function))
(bind ?RI1 ?CIL110)
(if
(eq ?RI1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RI0 ?ShortData))
(bind ?ShortData (AddOrNot ?RI1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_130046)
then
(undefrule *)
(InterpretationIndex "不是肾动脉狭窄(双)和肾功能异常，建议首选ARB或ACE-I或CCB。")
(Recommendation "首选血管紧张素受体阻滞剂或血管紧张素转移酶抑制剂或二氢吡啶类钙离子拮抗剂")
(FactUsed "irenal_artery_Bistenosis" "abnormal_renal_function")
)
)
