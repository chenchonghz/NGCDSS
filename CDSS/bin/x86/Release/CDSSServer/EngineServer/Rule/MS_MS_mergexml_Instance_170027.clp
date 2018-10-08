(defrule MS_MS_mergexml_Instance_170027_0
(filepath ?filepath)
(Insolin ?Insolin)
(Shuanggua_Drug ?Shuanggua_Drug)
(Gelienai_Drug ?Gelienai_Drug)
(Gelietong_Drug ?Gelietong_Drug)
(Huangniao_Drug ?Huangniao_Drug)
(AGI_Drug ?AGI_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL040 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(bind ?CIL041 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL042 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL043 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL044 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(or (Transform ?CIL040)  (Transform ?CIL041)  (Transform ?CIL042)  (Transform ?CIL043)  (Transform ?CIL044) )
then
(bind ?CIN030 TRUE)
else
(bind ?CIN030 NULL)
(bind ?CIN030 (AddOrNot ?CIL040 ?CIN030))
(bind ?CIN030 (AddOrNot ?CIL041 ?CIN030))
(bind ?CIN030 (AddOrNot ?CIL042 ?CIN030))
(bind ?CIN030 (AddOrNot ?CIL043 ?CIN030))
(bind ?CIN030 (AddOrNot ?CIL044 ?CIN030))
(if(eq ?CIN030 NULL)
then
(bind ?CIN030 FALSE)
)
)
(bind ?CIL030 (Leaf equals ?Insolin YES Insolin))
(if
(and (Transform ?CIL030)  (Transform ?CIN030) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIN030 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL020 (Leaf equals ?Insolin YES Insolin))
(if
(or (Transform ?CIL020)  (Transform ?CIN020) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIL020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170027)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "调整胰岛素用量或种类")
(FactUsed "Insolin" "Shuanggua_Drug" "Gelienai_Drug" "Gelietong_Drug" "Huangniao_Drug" "AGI_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170027_1
(filepath ?filepath)
(Huangniao_Drug ?Huangniao_Drug)
(Shuanggua_Drug ?Shuanggua_Drug)
(Gelietong_Drug ?Gelietong_Drug)
(AGI_Drug ?AGI_Drug)
(Gelienai_Drug ?Gelienai_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL031 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(if
(and (Transform ?CIL030)  (Transform ?CIL031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIL031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL032 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL033 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(if
(and (Transform ?CIL032)  (Transform ?CIL033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIL032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIL033 ?CIN021))
(if(eq ?CIN021 NULL)
then
(bind ?CIN021 FALSE)
)
)
(bind ?CIL034 (Leaf equals ?Huangniao_Drug YES Huangniao_Drug))
(bind ?CIL035 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL034)  (Transform ?CIL035) )
then
(bind ?CIN022 TRUE)
else
(bind ?CIN022 NULL)
(bind ?CIN022 (AddOrNot ?CIL034 ?CIN022))
(bind ?CIN022 (AddOrNot ?CIL035 ?CIN022))
(if(eq ?CIN022 NULL)
then
(bind ?CIN022 FALSE)
)
)
(bind ?CIL036 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL037 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(if
(and (Transform ?CIL036)  (Transform ?CIL037) )
then
(bind ?CIN023 TRUE)
else
(bind ?CIN023 NULL)
(bind ?CIN023 (AddOrNot ?CIL036 ?CIN023))
(bind ?CIN023 (AddOrNot ?CIL037 ?CIN023))
(if(eq ?CIN023 NULL)
then
(bind ?CIN023 FALSE)
)
)
(bind ?CIL038 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL039 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(if
(and (Transform ?CIL038)  (Transform ?CIL039) )
then
(bind ?CIN024 TRUE)
else
(bind ?CIN024 NULL)
(bind ?CIN024 (AddOrNot ?CIL038 ?CIN024))
(bind ?CIN024 (AddOrNot ?CIL039 ?CIN024))
(if(eq ?CIN024 NULL)
then
(bind ?CIN024 FALSE)
)
)
(bind ?CIL0310 (Leaf equals ?Gelienai_Drug YES Gelienai_Drug))
(bind ?CIL0311 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL0310)  (Transform ?CIL0311) )
then
(bind ?CIN025 TRUE)
else
(bind ?CIN025 NULL)
(bind ?CIN025 (AddOrNot ?CIL0310 ?CIN025))
(bind ?CIN025 (AddOrNot ?CIL0311 ?CIN025))
(if(eq ?CIN025 NULL)
then
(bind ?CIN025 FALSE)
)
)
(if
(or (Transform ?CIN020)  (Transform ?CIN021)  (Transform ?CIN022)  (Transform ?CIN023)  (Transform ?CIN024)  (Transform ?CIN025) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN022 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN023 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN024 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN025 ?CIN010))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170027)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "1.胰岛素+糖苷酶抑制剂+磺脲类或格列奈类；2.噻唑烷二酮类或糖苷酶抑制剂+调整磺脲类或格列奈类剂量或种类；3.胰岛素治疗")
(FactUsed "Huangniao_Drug" "Shuanggua_Drug" "Gelietong_Drug" "AGI_Drug" "Gelienai_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170027_2
(filepath ?filepath)
(Gelietong_Drug ?Gelietong_Drug)
(AGI_Drug ?AGI_Drug)
(Shuanggua_Drug ?Shuanggua_Drug)
=>
(bind ?Threshhold 0)

(bind ?CIL030 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL031 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL030)  (Transform ?CIL031) )
then
(bind ?CIN020 TRUE)
else
(bind ?CIN020 NULL)
(bind ?CIN020 (AddOrNot ?CIL030 ?CIN020))
(bind ?CIN020 (AddOrNot ?CIL031 ?CIN020))
(if(eq ?CIN020 NULL)
then
(bind ?CIN020 FALSE)
)
)
(bind ?CIL032 (Leaf equals ?Gelietong_Drug YES Gelietong_Drug))
(bind ?CIL033 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(if
(and (Transform ?CIL032)  (Transform ?CIL033) )
then
(bind ?CIN021 TRUE)
else
(bind ?CIN021 NULL)
(bind ?CIN021 (AddOrNot ?CIL032 ?CIN021))
(bind ?CIN021 (AddOrNot ?CIL033 ?CIN021))
(if(eq ?CIN021 NULL)
then
(bind ?CIN021 FALSE)
)
)
(bind ?CIL034 (Leaf equals ?Shuanggua_Drug YES Shuanggua_Drug))
(bind ?CIL035 (Leaf equals ?AGI_Drug YES AGI_Drug))
(if
(and (Transform ?CIL034)  (Transform ?CIL035) )
then
(bind ?CIN022 TRUE)
else
(bind ?CIN022 NULL)
(bind ?CIN022 (AddOrNot ?CIL034 ?CIN022))
(bind ?CIN022 (AddOrNot ?CIL035 ?CIN022))
(if(eq ?CIN022 NULL)
then
(bind ?CIN022 FALSE)
)
)
(if
(or (Transform ?CIN020)  (Transform ?CIN021)  (Transform ?CIN022) )
then
(bind ?CIN010 TRUE)
else
(bind ?CIN010 NULL)
(bind ?CIN010 (AddOrNot ?CIN020 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN021 ?CIN010))
(bind ?CIN010 (AddOrNot ?CIN022 ?CIN010))
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
(if(NotifyOrNot >= ?Threshhold 1 ?ShortData ?filepath MS_mergexml_Instance_170027)
then
(undefrule *)
(InterpretationIndex "NO_VALUE")
(Recommendation "1.磺脲类或格列奈类；2.糖苷酶抑制剂或噻唑烷二酮类+磺脲类或格列奈类")
(FactUsed "Gelietong_Drug" "AGI_Drug" "Shuanggua_Drug")
)
)


(defrule MS_MS_mergexml_Instance_170027_3
(filepath ?filepath)
=>

)


(defrule MS_MS_mergexml_Instance_170027_4
(filepath ?filepath)
=>

)
