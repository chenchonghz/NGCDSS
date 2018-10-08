(defrule MS_MS_mergexml_Instance_170104_0
(filepath ?filepath)
(DM_Drug ?DM_Drug)
=>
(bind ?Threshhold 0)

(bind ?COL010 (Leaf equals ?DM_Drug YES DM_Drug))
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
(if(NotifyOrNot equals ?Threshhold 0 ?ShortData ?filepath MS_mergexml_Instance_170104)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "二甲双胍+磺脲类口服降糖药或二甲双胍+皮下胰岛素治疗")
(Recommendation "皮下注射胰岛素治疗+二甲双胍（方案3+方案8）：
1.TDI估算：0.2-0.3U/kg.d；
2. CSII: ①可用胰岛素：生物合成中性人胰岛素或赖脯胰岛素或门冬胰岛素；②基础量：TDI４０％～５０％;③５０％～６０％用作餐前大剂量，可平均分配于三餐前或按４：３：３的比例分别分配于三餐前；也可按４：２：３：１分别分配于三餐前及睡前加餐前.

3.MDI:每日四次法：
 <1>.早餐前RI/赖脯胰岛素/门冬胰岛素(30%TDI)，午餐前和晚餐前RI/赖脯胰岛素/门冬胰岛素(22.5%TDI)，睡前NPH/determir/甘精胰岛素(25%TDI);
<2>.早餐前RI+NPH，中餐和晚餐前RI，睡前NPH；
2.每日3次法：
<1>.早餐前 RI/赖脯胰岛素/门冬胰岛素 +NPH/determir/PZI (短效：长效=2:1,52.5% TDI)，  晚餐RI/赖脯胰岛素/门冬胰岛素(22.5% TDI),   晚睡前NPH/determir/PZI/甘精胰岛素(25%  TDI) ;  
<2>.早餐前 RI (30% TDI) , 午餐前RI  (22.5% TDI), 晚餐前 RI+甘精胰岛素/NPH/determir/PZI  (47.5% TDI)；
<3>.早餐前RI+PZI ，午餐前 RI ，晚餐前 RI+PZI
<4>.RI+NPH 分别于三餐前
注：早餐前RI(多) 25-30%; 中餐前RI(少) 15-20%；晚餐RI(中量)20-25%; 睡前NPH(小) 20%；RI+NPH和RI于早餐前15-30分钟注射
；门冬胰岛素或赖脯胰岛素餐前15分钟或餐前即刻或餐后注射；
二甲双胍0.5 3次/d 或0.85, 2次/d")
(FactUsed "DM_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170104_1
(filepath ?filepath)
(DM_Drug ?DM_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL010 (Leaf equals ?DM_Drug YES DM_Drug))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170104)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "皮下注射胰岛素治疗+二甲双胍（方案3+方案8）：
1.TDI估算：0.2-0.3U/kg.d；
2. CSII: ①可用胰岛素：生物合成中性人胰岛素或赖脯胰岛素或门冬胰岛素；②基础量：TDI４０％～５０％;③５０％～６０％用作餐前大剂量，可平均分配于三餐前或按４：３：３的比例分别分配于三餐前；也可按４：２：３：１分别分配于三餐前及睡前加餐前.

3.MDI:每日四次法：
 <1>.早餐前RI/赖脯胰岛素/门冬胰岛素(30%TDI)，午餐前和晚餐前RI/赖脯胰岛素/门冬胰岛素(22.5%TDI)，睡前NPH/determir/甘精胰岛素(25%TDI);
<2>.早餐前RI+NPH，中餐和晚餐前RI，睡前NPH；
2.每日3次法：
<1>.早餐前 RI/赖脯胰岛素/门冬胰岛素 +NPH/determir/PZI (短效：长效=2:1,52.5% TDI)，  晚餐RI/赖脯胰岛素/门冬胰岛素(22.5% TDI),   晚睡前NPH/determir/PZI/甘精胰岛素(25%  TDI) ;  
<2>.早餐前 RI (30% TDI) , 午餐前RI  (22.5% TDI), 晚餐前 RI+甘精胰岛素/NPH/determir/PZI  (47.5% TDI)；
<3>.早餐前RI+PZI ，午餐前 RI ，晚餐前 RI+PZI
<4>.RI+NPH 分别于三餐前
注：早餐前RI(多) 25-30%; 中餐前RI(少) 15-20%；晚餐RI(中量)20-25%; 睡前NPH(小) 20%；RI+NPH和RI于早餐前15-30分钟注射
；门冬胰岛素或赖脯胰岛素餐前15分钟或餐前即刻或餐后注射；
二甲双胍0.5 3次/d 或0.85, 2次/d")
(FactUsed "DM_Drug")
)
)
