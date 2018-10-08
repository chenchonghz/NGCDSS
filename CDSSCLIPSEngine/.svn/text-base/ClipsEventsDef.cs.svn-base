using System;
using System.Collections.Generic;
using System.Text;
using CDSSvMRDataDef;

namespace CDSSCLIPSEngine
{
    class ClipsEventsDef
    {
        //ÊÂ¼þÓ¢ÎÄÃû¶¨Òå
        public const string CONST_EVENTENNAME_DM = "DM_Diagnose_EVENT";
        public const string CONST_EVENTENNAME_Dyslipidemia = "Dyslipidemia_Diagnose_EVENT";
        public const string CONST_EVENTENNAME_Hypertension = "Hypertension_Diagnose_EVENT";
        public const string CONST_EVENTENNAME_HUA = "Hyperuricaemia_Diagnose_EVENT";
        public const string CONST_EVENTENNAME_Fat = "Fat_Diagnose_EVENT";
        public const string CONST_EVENTENNAME_MSEvaluate = "MS_Evaluate_Event";

        public const string CONST_EVENTENNAME_MS_RiskDegree_Evaluation = "MS_RiskDegree_Evaluation_EVENT";
        public const string CONST_EVENTENNAME_SportCalc = "SportCalc_EVENT";
        public const string CONST_EVENTENNAME_DietCalc = "DietCalc_EVENT";

        public const string CONST_EVENTENNAME_DM_Therapy = "DM_Therapy_EVENT";
        public const string CONST_EVENTENNAME_Hypertension_Therapy = "Hypertension_Therapy_EVENT";
        public const string CONST_EVENTENNAME_HUA_Therapy = "Hyperuricaemia_Therapy_EVENT";
        public const string CONST_EVENTENNAME_TC_Therapy = "Dyslipidemia_TC_EVENT";
        public const string CONST_EVENTENNAME_TG_Therapy = "Dyslipidemia_TG_EVENT";
        public const string CONST_EVENTENNAME_HDLC_Therapy = "Dyslipidemia_HDLC_EVENT";
        public const string CONST_EVENTENNAME_LDLC_Therapy = "Dyslipidemia_LDLC_EVENT";
        public const string CONST_EVENTENNAME_Antiplatelet_Drug_Therapy = "Antiplatelet_Drug_Use_EVENT";
        public const string CONST_EVENTENNAME_Fat_Therapy = "Fat_Therapy_EVENT";

        public const string CONST_EVENTENNAME_DM_Therapy_Lack = "DM_Therapy_LACK";
        public const string CONST_EVENTENNAME_Hypertension_Therapy_Lack = "Hypertension_Therapy_LACK";
        public const string CONST_EVENTENNAME_HUA_Therapy_Lack = "Hyperuricaemia_Therapy_LACK";
        public const string CONST_EVENTENNAME_TC_Therapy_Lack = "Dyslipidemia_TC_LACK";
        public const string CONST_EVENTENNAME_TG_Therapy_Lack = "Dyslipidemia_TG_LACK";
        public const string CONST_EVENTENNAME_HDLC_Therapy_Lack = "Dyslipidemia_HDLC_LACK";
        public const string CONST_EVENTENNAME_LDLC_Therapy_Lack = "Dyslipidemia_LDLC_LACK";


        public const string CONST_EVENTENNAME_DM_SelfMonitor = "DM_SelfMonitor_Event";
        public const string CONST_EVENTENNAME_Dyslipidemia_SelfMonitor = "Dyslipidemia_SelfMonitor_EVENT";
        public const string CONST_EVENTENNAME_HUA_SelfMonitor = "HUA_SelfMonitor_EVENT";
        public const string CONST_EVENTENNAME_Hypertension_Therapy_Suggestion = "Hypertension_Therapy_Suggestion_EVENT";



        public static void MapEvent(string strEventName, ref vMRClsDef.TriggeringEvent oTriggeringEvent)
        {
            switch(strEventName)
            {
                case ClipsEventsDef.CONST_EVENTENNAME_DM:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÌÇ´úÐ»";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.DIAGNOSIS;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_DM_Therapy:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÌÇ´úÐ»";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.THERAPY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_DM_SelfMonitor:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÌÇ´úÐ»";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.SELFMONITOR;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Fat:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "·ÊÅÖ¶È";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.DIAGNOSIS;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Dyslipidemia:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "Ö¬´úÐ»";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.DIAGNOSIS;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_HDLC_Therapy:
                case ClipsEventsDef.CONST_EVENTENNAME_LDLC_Therapy:
                case ClipsEventsDef.CONST_EVENTENNAME_TC_Therapy:
                case ClipsEventsDef.CONST_EVENTENNAME_TG_Therapy:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "Ö¬´úÐ»";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.THERAPY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Dyslipidemia_SelfMonitor:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "Ö¬´úÐ»";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.SELFMONITOR;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Hypertension:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÑªÑ¹";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.DIAGNOSIS;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Hypertension_Therapy:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÑªÑ¹";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.THERAPY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Hypertension_Therapy_Suggestion:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÑªÑ¹";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.SELFMONITOR;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_HUA:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÑªÄòËá";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.DIAGNOSIS;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_HUA_Therapy:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÑªÄòËá";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.THERAPY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_HUA_SelfMonitor:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÑªÄòËá";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.SELFMONITOR;
                    return;

                case ClipsEventsDef.CONST_EVENTENNAME_MSEvaluate:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "´úÐ»×ÛºÏÕ÷";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.MSEVALUATION;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_MS_RiskDegree_Evaluation:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "´úÐ»×ÛºÏÕ÷Î£ÏÕ¶ÈÆÀ¹À";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.RISKEVALUATION;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_DietCalc:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "´úÐ»×ÛºÏÕ÷";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.DIETARY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_SportCalc:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "´úÐ»×ÛºÏÕ÷";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.PHYSICALACTIVITY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Antiplatelet_Drug_Therapy:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "ÆäËû";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.THERAPY;
                    return;
                case ClipsEventsDef.CONST_EVENTENNAME_Fat_Therapy:
                    oTriggeringEvent.oDisease.strDiseaseCNName = "·ÊÅÖ¶È";
                    oTriggeringEvent.m_emInferenceResultType = vMRClsDef.EnumInferenceResultType.THERAPY;
                    return;
                default:
                    return;
            }
          
        }
    }
}
