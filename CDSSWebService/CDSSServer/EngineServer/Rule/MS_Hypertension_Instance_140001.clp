(defrule MS_Hypertension_Instance_140001_0
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
