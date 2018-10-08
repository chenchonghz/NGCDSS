(defrule Entrance_1
(filepath ?filepath)
(DM_Diagnose_EVENT ?DM_Diagnose_EVENT)
=>
(if
(eq ?DM_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "糖尿病诊断")
(load (str-cat ?filepath "MS_DM_Instance_60000.clp"))))

(defrule Entrance_2
(filepath ?filepath)
(DM_Therapy_EVENT ?DM_Therapy_EVENT)
=>
(if
(eq ?DM_Therapy_EVENT on)
then
(undefrule *)
(InterpretationIndex "糖尿病治疗")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_10000.clp"))))

(defrule Entrance_3
(filepath ?filepath)
(Dyslipidemia_Diagnose_EVENT ?Dyslipidemia_Diagnose_EVENT)
=>
(if
(eq ?Dyslipidemia_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "血脂异常诊断")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_60000.clp"))))

(defrule Entrance_4
(filepath ?filepath)
(Dyslipidemia_TC_EVENT ?Dyslipidemia_TC_EVENT)
=>
(if
(eq ?Dyslipidemia_TC_EVENT on)
then
(undefrule *)
(InterpretationIndex "TC治疗")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110040.clp"))))

(defrule Entrance_5
(filepath ?filepath)
(Dyslipidemia_TG_EVENT ?Dyslipidemia_TG_EVENT)
=>
(if
(eq ?Dyslipidemia_TG_EVENT on)
then
(undefrule *)
(InterpretationIndex "TG治疗")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_120000.clp"))))

(defrule Entrance_6
(filepath ?filepath)
(Dyslipidemia_LDLC_EVENT ?Dyslipidemia_LDLC_EVENT)
=>
(if
(eq ?Dyslipidemia_LDLC_EVENT on)
then
(undefrule *)
(InterpretationIndex "LDLC治疗")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110102.clp"))))

(defrule Entrance_7
(filepath ?filepath)
(Dyslipidemia_HDLC_EVENT ?Dyslipidemia_HDLC_EVENT)
=>
(if
(eq ?Dyslipidemia_HDLC_EVENT on)
then
(undefrule *)
(InterpretationIndex "HDLC治疗")
(load (str-cat ?filepath "MS_Dyslipidemia_Instance_110153.clp"))))

(defrule Entrance_8
(filepath ?filepath)
(Fat_Diagnose_EVENT ?Fat_Diagnose_EVENT)
=>
(if
(eq ?Fat_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "肥胖诊断")
(load (str-cat ?filepath "MS_Fat_Instance_50000.clp"))))

(defrule Entrance_9
(filepath ?filepath)
(Hyperuricaemia_Diagnose_EVENT ?Hyperuricaemia_Diagnose_EVENT)
=>
(if
(eq ?Hyperuricaemia_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "高尿酸血症诊断")
(load (str-cat ?filepath "MS_HUA_Instance_100004.clp"))))

(defrule Entrance_10
(filepath ?filepath)
(Hyperuricaemia_Therapy_EVENT ?Hyperuricaemia_Therapy_EVENT)
=>
(if
(eq ?Hyperuricaemia_Therapy_EVENT on)
then
(undefrule *)
(InterpretationIndex "高尿酸血症治疗")
(load (str-cat ?filepath "MS_HUA_Instance_100020.clp"))))

(defrule Entrance_11
(filepath ?filepath)
(Hypertension_Diagnose_EVENT ?Hypertension_Diagnose_EVENT)
=>
(if
(eq ?Hypertension_Diagnose_EVENT on)
then
(undefrule *)
(InterpretationIndex "高血压诊断")
(load (str-cat ?filepath "MS_Hypertension_Instance_80000.clp"))))

(defrule Entrance_12
(filepath ?filepath)
(Hypertension_Therapy_Suggestion_EVENT ?Hypertension_Therapy_Suggestion_EVENT)
=>
(if
(eq ?Hypertension_Therapy_Suggestion_EVENT on)
then
(undefrule *)
(InterpretationIndex "高血压治疗生活方式调整与自我监测")
(Recommendation "生活方式管理：
1.饮食、运动详见后附专栏
2.保持平衡心态，避免精神紧张
3.保证睡眠6-8h/日（成人）
4.避免过度劳累")
(Recommendation "自我监测管理：
1.血压测定 >=2次/日
2.脉率测定>=2次/日
3.体重测定，1次每周")))

(defrule Entrance_13
(filepath ?filepath)
(Hypertension_Therapy_EVENT ?Hypertension_Therapy_EVENT)
=>
(if
(eq ?Hypertension_Therapy_EVENT on)
then
(undefrule *)
(InterpretationIndex "高血压治疗")
(load (str-cat ?filepath "MS_Hypertension_Instance_100003.clp"))))

(defrule Entrance_14
(filepath ?filepath)
(MS_Evaluate_Event ?MS_Evaluate_Event)
=>
(if
(eq ?MS_Evaluate_Event on)
then
(undefrule *)
(InterpretationIndex "代谢综合征评估")
(load (str-cat ?filepath "MS_MSEvaluate_Instance_2.clp"))))

(defrule Entrance_15
(filepath ?filepath)
(DM_SelfMonitor_Event ?DM_SelfMonitor_Event)
=>
(if
(eq ?DM_SelfMonitor_Event on)
then
(undefrule *)
(InterpretationIndex "糖尿病自我监测")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_20005.clp"))))

(defrule Entrance_16
(filepath ?filepath)
(Dyslipidemia_SelfMonitor_EVENT ?Dyslipidemia_SelfMonitor_EVENT)
=>
(if
(eq ?Dyslipidemia_SelfMonitor_EVENT on)
then
(undefrule *)
(InterpretationIndex "血脂紊乱随访建议")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_70055.clp"))))

(defrule Entrance_17
(filepath ?filepath)
(HUA_SelfMonitor_EVENT ?HUA_SelfMonitor_EVENT)
=>
(if
(eq ?HUA_SelfMonitor_EVENT on)
then
(undefrule *)
(InterpretationIndex "高尿酸血症随访建议")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_20057.clp"))))

(defrule Entrance_18
(filepath ?filepath)
(Antiplatelet_Drug_Use_EVENT ?Antiplatelet_Drug_Use_EVENT)
=>
(if
(eq ?Antiplatelet_Drug_Use_EVENT on)
then
(undefrule *)
(InterpretationIndex "抗血小板药物治疗")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_130002.clp"))))

(defrule Entrance_19
(filepath ?filepath)
(Fat_Therapy_EVENT ?Fat_Therapy_EVENT)
=>
(if
(eq ?Fat_Therapy_EVENT on)
then
(undefrule *)
(InterpretationIndex "肥胖治疗")
(load (str-cat ?filepath "MS_MS_mergexml_Instance_190000.clp"))))
