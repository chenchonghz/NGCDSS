(defrule MS_Hypertension_Instance_130048_0
(filepath ?filepath)
(abnormal_renal_function ?abnormal_renal_function)
(irenal_artery_Bistenosis ?irenal_artery_Bistenosis)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?abnormal_renal_function YES abnormal_renal_function))
(bind ?RI0 ?CIL010)
(if
(eq ?RI0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?CIL110 (Leaf equals ?irenal_artery_Bistenosis YES irenal_artery_Bistenosis))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath Hypertension_Instance_130048)
then
(undefrule *)
(InterpretationIndex "肾动脉狭窄(双)或肾功能异常，建议首选CCB+BB或CCB+利尿剂。")
(Recommendation "首选二氢吡啶类钙离子拮抗剂+贝特受体阻滞剂或二氢吡啶类钙离子拮抗剂+利尿剂")
(FactUsed "abnormal_renal_function" "irenal_artery_Bistenosis")
)
)


(defrule MS_Hypertension_Instance_130048_1
(filepath ?filepath)
(abnormal_renal_function ?abnormal_renal_function)
(irenal_artery_Bistenosis ?irenal_artery_Bistenosis)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?abnormal_renal_function YES abnormal_renal_function))
(bind ?RO0 ?COL010)
(if
(eq ?RO0 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)

(bind ?COL110 (Leaf equals ?irenal_artery_Bistenosis YES irenal_artery_Bistenosis))
(bind ?RO1 ?COL110)
(if
(eq ?RO1 TRUE)
then
(bind ?Threshhold (+ ?Threshhold 1))
)
(bind ?ShortData NULL)
(bind ?ShortData (AddOrNot ?RO0 ?ShortData))
(bind ?ShortData (AddOrNot ?RO1 ?ShortData))
(if
(eq ?ShortData NULL)
then
(bind ?ShortData FALSE)
)
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath Hypertension_Instance_130048)
then
(undefrule *)
(InterpretationIndex "不是肾动脉狭窄(双)或肾功能异常，建议首选ARB+CCB或ACE-I+利尿剂或CCB+ACE-I。")
(Recommendation "首选血管紧张素受体阻滞剂+二氢吡啶类钙离子拮抗剂或血管紧张素转移酶抑制剂+利尿剂或二氢吡啶类钙离子拮抗剂+血管紧张素转移酶抑制剂")
(FactUsed "abnormal_renal_function" "irenal_artery_Bistenosis")
)
)
