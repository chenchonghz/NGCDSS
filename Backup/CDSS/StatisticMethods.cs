using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using CDSSDBAccess;
using System.Resources;
using System.Reflection;


namespace CDSS
{
    public struct DignoCountPair 
    {
        public int CurrentCount;
        public int PreviousCount;
    }
    public class StatisticMethods
    {
#region �ֶζ���

        private List<string> ValueProperties = new List<string>();
        private List<string> DignosedProperties = new List<string>();

        private static System.Resources.ResourceManager resourceManager = new ResourceManager("CDSS.Resource.StatisticsForm", Assembly.GetExecutingAssembly());
        private static string everyyear = resourceManager.GetString("EveryYear");
        private static string everymonth = resourceManager.GetString("EveryMonth");
        private static string everyseason = resourceManager.GetString("EverySeason");
        private static string defineown = resourceManager.GetString("DefinedOwn");

        private string TimeSlot = resourceManager.GetString("period");
        private string Group = resourceManager.GetString("group");
        private string Values = resourceManager.GetString("valueParas");
        private string Dignos = resourceManager.GetString("dignoParas");
        
#endregion


        public StatisticMethods()
        {
            
        }
#region ��ֵ����ͳ��

        public DataTable GetValueParaStatistics(List<Period> TotalPeriods, string[] childConditionQuery, string[] valueFiedls, List<ValuePropertyType> ValueProperties, string IntervalType)
        {
            List<DataTable> ValueParaCollection = new List<DataTable>();
            List<List<double>> averageOfValuePara = new List<List<double>>();
            List<List<double>> standardOfValuePara = new List<List<double>>();
            List<List<double>> maxOfValuePara = new List<List<double>>();
            List<List<double>> minOfValuePara = new List<List<double>>();
            List<List<double>> medianOfValuePara = new List<List<double>>();
            List<List<double>> quarOfValuePara = new List<List<double>>();
           
            ValueParaCollection=DBAccess.GetValueParaDataSet(childConditionQuery, TotalPeriods, valueFiedls);

#region ������ֵ������ͳ������

            for (int i = 0; i < ValueProperties.Count;i++ )
            {

                switch (ValueProperties[i])
                {
                    case ValuePropertyType.Average:
                        foreach (DataTable table in ValueParaCollection)
                        {
                            averageOfValuePara.Add(GetAverage(table));
                        }
                        break;
                    case ValuePropertyType.StandardDeviation:
                        foreach (DataTable table in ValueParaCollection)
                        {
                            standardOfValuePara.Add(GetStandard(table));
                        }
                        break;
                    case ValuePropertyType.Max:
                        foreach (DataTable table in ValueParaCollection)
                        {
                            List<List<double>> result = GetSortedArray(table);
                            maxOfValuePara.Add(GetMax(result));
                        }
                        break;
                    case ValuePropertyType.Min:
                        foreach (DataTable table in ValueParaCollection)
                        {
                            List<List<double>> result = GetSortedArray(table);
                            minOfValuePara.Add(GetMin(result));
                        }
                        break;
                    case ValuePropertyType.Median:
                        foreach (DataTable table in ValueParaCollection)
                        {
                            List<List<double>> result = GetSortedArray(table);
                            medianOfValuePara.Add(GetMedian(result));
                        }
                        break;
                    case ValuePropertyType.Quar:
                        foreach (DataTable table in ValueParaCollection)
                        {
                            List<List<double>> result = GetSortedArray(table);
                            quarOfValuePara.Add(GetQuar(result));
                        }
                        break;
                    default:
                        break;
                }
            }
#endregion

            #region ���õ��ĸ�������ϳ�һ��Datatable���أ���Datagridview��ʾ
            
            DataRow drow;
            int countOfGroup = 0;
            List<double> averageGroup = new List<double>();
            if (averageOfValuePara.Count != 0)
            {
                countOfGroup = averageOfValuePara.Count;
                foreach (List<double> list in averageOfValuePara)
                {
                    averageGroup.AddRange(list);
                }
            }
            List<double> standardGroup = new List<double>();
            if (standardOfValuePara.Count != 0)
            {
                countOfGroup = standardOfValuePara.Count;
                foreach (List<double> list in standardOfValuePara)
                {
                    standardGroup.AddRange(list);
                }
            }

            List<double> maxGroup = new List<double>();
            if (maxOfValuePara.Count != 0)
            {
                countOfGroup = maxOfValuePara.Count;
                foreach (List<double> list in maxOfValuePara)
                {
                    maxGroup.AddRange(list);
                }
            }
            List<double> minGroup = new List<double>();
            if (minOfValuePara.Count != 0)
            {
                countOfGroup = minOfValuePara.Count;
                foreach (List<double> list in minOfValuePara)
                {
                    minGroup.AddRange(list);
                }
            }
            List<double> medianGroup = new List<double>();
            if (medianOfValuePara.Count != 0)
            {
                countOfGroup = medianOfValuePara.Count;
                foreach (List<double> list in medianOfValuePara)
                {
                    medianGroup.AddRange(list);
                }
            }
            List<double> quarGroup = new List<double>();
            if (quarOfValuePara.Count != 0)
            {
                countOfGroup = quarOfValuePara.Count;
                foreach (List<double> list in quarOfValuePara)
                {
                    quarGroup.AddRange(list);
                }
            }
            DataTable dataTableResult = new DataTable();
            
            dataTableResult.Columns.Add(TimeSlot);
            dataTableResult.Columns.Add(Group);
            dataTableResult.Columns.Add(Values);
            foreach (ValuePropertyType property in ValueProperties)
            {
                dataTableResult.Columns.Add(resourceManager.GetString(property.ToString()));
            }
            //��������Ŀ
            int fieldsLength = valueFiedls.Length;
            //ʱ��ε���Ŀ
            int intervalLength = TotalPeriods.Count / 2;
            //��������ϳ�һ�ű��������
            for (int i = 0; i < countOfGroup*valueFiedls.Length; i++)
            {
                drow = dataTableResult.NewRow();
                //ÿ�����������������е�����i�������Ƶó���������ʱ��Ρ�ͳ���������飬�������ĸ���ֵ������ֵ
                //t��ʾ��t��ʱ��Σ���Ϊʱ�����������ѭ����2�ǽ�����ѭ����ͳ����Ͷ������������Ŀ��
                //fieldsLength��������ѭ����Ŀ��i���ԣ�2*fieldsLength��,��������t��ѭ��
                int t=i/(2*fieldsLength);
                //���������Ƶ�ԭ��c�����м���ѭ���Ĵ���
                int c=(i-t*(2*fieldsLength))/fieldsLength;
                //ͬ��v�ǵڼ�������
                int v=i%fieldsLength;
                //����ǵڼ���ʱ��κ󣬴�ʱ��������ȡ����Ӧ���ַ���
                string timeMark = TotalPeriods[t].StartTime.ToShortDateString() + "-" + TotalPeriods[t].EndTime.ToShortDateString();
                drow[TimeSlot] = timeMark;
                if (c == 0)
                {
                    drow[Group] = resourceManager.GetString("StatisticGroup");
                }
                else
                    drow[Group] = resourceManager.GetString("ContrastGroup");
                System.Resources.ResourceManager resourceManagerValue = new ResourceManager("CDSS.Resource.ValueParas", Assembly.GetExecutingAssembly());
                drow[Values] = resourceManagerValue.GetString(valueFiedls[v]);
                if (averageGroup.Count != 0)
                {
                    drow[resourceManager.GetString(ValuePropertyType.Average.ToString())] = Math.Round(averageGroup[i], 2);
                }
                if (standardGroup.Count != 0)
                {
                    drow[resourceManager.GetString(ValuePropertyType.StandardDeviation.ToString())] = Math.Round(standardGroup[i], 2);
                }
                if (maxGroup.Count != 0)
                {
                    drow[resourceManager.GetString(ValuePropertyType.Max.ToString())] = Math.Round(maxGroup[i], 2);
                }
                if (minGroup.Count != 0)
                {
                    drow[resourceManager.GetString(ValuePropertyType.Min.ToString())] = Math.Round(minGroup[i], 2);
                }
                if (medianGroup.Count != 0)
                {
                    drow[resourceManager.GetString(ValuePropertyType.Median.ToString())] = Math.Round(medianGroup[i], 2);
                }
                if (quarGroup.Count != 0)
                {
                    drow[resourceManager.GetString(ValuePropertyType.Quar.ToString())] = Math.Round(quarGroup[i], 2);
                }
                dataTableResult.Rows.Add(drow);
            }
            #endregion
            return dataTableResult;
        }

#region ��ֵ����ͳ��ʱ���ӷ���

        private List<double> GetAverage(DataTable table)
        {
            List<double> list=new List<double>();
            for (int i = 0; i < table.Columns.Count;i++ )
            {
                double sum = 0;
                int c = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row[i].ToString()!="")
                    {
                        sum += Convert.ToDouble(row[i]);
                        c++;
                    }
                }
                if (c!=0)
                {
                    sum = sum / c;
                }
                list.Add(sum);
            }
            return list;
        }
        private List<double> GetStandard(DataTable table)
        {
            List<double> aver = GetAverage(table);
            List<double> list = new List<double>();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                double s = 0;
                int c = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row[i].ToString() != "")
                    {
                        s += Math.Pow(Convert.ToDouble(row[i]) - aver[i], 2);
                        c++;
                    }                    
                }
                if (c != 0)
                {
                    s = s / c;
                    s = Math.Sqrt(s);
                }
                list.Add(s);
            }
            return list;
        }
        private List<List<double>> GetSortedArray(DataTable table)
        {
            List<List<double>> listOfUnsorted = new List<List<double>>();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                List<double> list = new List<double>();
                foreach (DataRow row in table.Rows)
                {
                    if (row[i].ToString() != "")
                    {
                        double d=Convert.ToDouble(row[i]);
                        list.Add(d);
                    }
                }
                listOfUnsorted.Add(list);
            }
            //ð������
            double temp;
            foreach(List<double> list in listOfUnsorted)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    for (int j = 0; j < list.Count - i; j++)
                        if (list[j] > list[j + 1])
                        {
                            temp = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = temp;
                        }
                }
            }
           return listOfUnsorted;
        }
        private List<double> GetMax(List<List<double>> array)
        {
            List<double> result=new List<double>();
            foreach (List<double> list in array)
            {
                if (list.Count != 0)
                {
                    int n = list.Count;
                    double m = list[n - 1];
                    result.Add(m);
                }
                else
                    result.Add(0);  
            }
            return result;
        }
        private List<double> GetMin(List<List<double>> array)
        {
            List<double> result=new List<double>();
            foreach (List<double> list in array)
            {
                if (list.Count != 0)
                {
                    double m = list[0];
                    result.Add(m);
                }
                else
                    result.Add(0);                
            }
            return result;
        }
        private List<double> GetMedian(List<List<double>> array)
        {
            List<double> result=new List<double>();
            foreach (List<double> list in array)
            {
                if (list.Count != 0)
                {
                    int n = list.Count;
                    double m = list[n / 2];
                    result.Add(m);
                }
                else
                    result.Add(0); 
            }
            return result;
        }
        private List<double> GetQuar(List<List<double>> array)
        {
            List<double> result=new List<double>();
            foreach (List<double> list in array)
            {
                if (list.Count != 0)
                {
                    int n = list.Count;
                    double m = list[3*n / 4] - list[n / 4];
                    result.Add(m);
                }
                else
                    result.Add(0); 
            }
            return result;
        }
#endregion
#endregion
        #region ��ϲ���ͳ��
        private List<List<DignoCountPair>> GetDignosedParaDataSet(string[] childConditionQuery,List<Period> TotalPeriods, string[] dignosedFields,string IntervalType)
        {
            List<List<DignoCountPair>> dignosedList = new List<List<DignoCountPair>>();
            for (int i = 0; i < TotalPeriods.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        List<DignoCountPair> listPair = new List<DignoCountPair>();
                        foreach (string s in dignosedFields)
                        {
                            int c = DBAccess.GetDignosedCount(TotalPeriods[i].StartTime, TotalPeriods[i].EndTime, childConditionQuery[j], s);
                            DignoCountPair countPair;
                            countPair.CurrentCount=c;   
                            if(IntervalType==everyyear)
                            {
                                c = DBAccess.GetDignosedCount(TotalPeriods[i].StartTime.AddYears(-1), TotalPeriods[i].EndTime.AddYears(-1), childConditionQuery[j], s);
                                countPair.PreviousCount = c;
                                listPair.Add(countPair);
                            }
                            if (IntervalType == everyseason)
                            {
                                c = DBAccess.GetDignosedCount(TotalPeriods[i].StartTime.AddMonths(-3), TotalPeriods[i].EndTime.AddMonths(-3), childConditionQuery[j], s);
                                countPair.PreviousCount = c;
                                listPair.Add(countPair);
                            }
                            if (IntervalType == everymonth)
                            {
                                c = DBAccess.GetDignosedCount(TotalPeriods[i].StartTime.AddMonths(-1), TotalPeriods[i].EndTime.AddMonths(-1), childConditionQuery[j], s);
                                countPair.PreviousCount = c;
                                listPair.Add(countPair);
                            }
                            if (IntervalType == defineown)
                            {
                                TimeSpan timeSpan = TotalPeriods[i].EndTime - TotalPeriods[i].StartTime;
                                c = DBAccess.GetDignosedCount(TotalPeriods[i].StartTime.Subtract(timeSpan), TotalPeriods[i].EndTime.Subtract(timeSpan), childConditionQuery[j], s);
                                countPair.PreviousCount = c;
                                listPair.Add(countPair);
                            }
                        }
                        dignosedList.Add(listPair);
                    }
                }
                return dignosedList;
        }
        public DataTable GetDignosedParaStatistics(List<Period> TotalPeriods, string[] childConditionQuery, string[] dignosedFields,List<DignosedPropertyType> DignosedProperties, string IntervalType)
        {
            List<List<DignoCountPair>> DignoParaCollection = new List<List<DignoCountPair>>();
            List<int> totalPatients=new List<int>();            
            List<List<int>> resultOfNewMorbid = new List<List<int>>();
            List<List<double>> resultOfNewMorbidRate = new List<List<double>>();
            List<List<int>> resultOfMorbid = new List<List<int>>();
            List<List<double>> resultOfMorbidRate = new List<List<double>>();

            DignoParaCollection=GetDignosedParaDataSet(childConditionQuery, TotalPeriods, dignosedFields, IntervalType);
            totalPatients=DBAccess.GetPeriodTotalCountDataTable(TotalPeriods);
#region ������ϲ����������飬�������ͳ�Ʋ�������            
            for (int i = 0; i < DignosedProperties.Count;i++ )
            {
                switch (DignosedProperties[i])
                {
                    case DignosedPropertyType.NewMorbid:
                        foreach (List<DignoCountPair> list in DignoParaCollection)
                        {
                            List<int> result = GetNewMorbid(list);
                            resultOfNewMorbid.Add(result);
                        }
                        break;
                    case DignosedPropertyType.NewMorbidRate:
                        int k = 0;
                        foreach (List<DignoCountPair> list in DignoParaCollection)
                        {
                            List<double> result = GetNewMorbidRate(list, totalPatients[k / 2]);
                            resultOfNewMorbidRate.Add(result);
                            k++;
                        }
                        break;
                    case DignosedPropertyType.Morbid:
                        foreach (List<DignoCountPair> list in DignoParaCollection)
                        {
                            List<int> result = GetMorbid(list);
                            resultOfMorbid.Add(result);
                        }
                        break;
                    case DignosedPropertyType.MorbidRate:
                        int g = 0;
                        foreach (List<DignoCountPair> list in DignoParaCollection)
                        {
                            List<double> result = GetMorbidRate(list, totalPatients[g / 2]);
                            resultOfMorbidRate.Add(result);
                            g++;
                        }
                        break;
                    case DignosedPropertyType.Qulified:
                        foreach (List<DignoCountPair> table in DignoParaCollection)
                        {
                            //��Ҫ�޸ģ������������һ��������
                            //averageOfValuePara.Add(GetAverage(table));
                        }
                        break;
                    case DignosedPropertyType.QulifiedRate:
                        foreach (List<DignoCountPair> table in DignoParaCollection)
                        {
                            //��Ҫ�޸ģ������������һ��������
                            //averageOfValuePara.Add(GetAverage(table));
                        }
                        break;
                    default:
                        break;
                }
            }
#endregion

#region ���õ��ĸ�������ϳ�һ��Datatable���أ���Datagridview��ʾ

           
            
            DataRow drow;
            int countOfGroup=0;
            List<int> newMorbidGroup = new List<int>();
            if (resultOfNewMorbid.Count!=0)
            {
                countOfGroup = resultOfNewMorbid.Count;
                foreach (List<int> list in resultOfNewMorbid)
                {                  
                    newMorbidGroup.AddRange(list);
                }
            }
            List<double> newMorbidRateGroup = new List<double>();
            if (resultOfNewMorbidRate.Count != 0)
            {
                countOfGroup = resultOfNewMorbidRate.Count;
                foreach (List<double> list in resultOfNewMorbidRate)
                {
                    newMorbidRateGroup.AddRange(list);
                }
            }
            List<int> morbidGroup = new List<int>();
            if (resultOfMorbid.Count != 0)
            {
                countOfGroup = resultOfMorbid.Count;
                foreach (List<int> list in resultOfMorbid)
                {
                    morbidGroup.AddRange(list);
                }
            }
            List<double> morbidRateGroup = new List<double>();
            if (resultOfMorbidRate.Count != 0)
            {
                countOfGroup = resultOfMorbidRate.Count;
                foreach (List<double> list in resultOfMorbidRate)
                {
                    morbidRateGroup.AddRange(list);
                }
            }
            DataTable dataTableResult = new DataTable();
            dataTableResult.Columns.Add(TimeSlot);
            dataTableResult.Columns.Add(Group);
            dataTableResult.Columns.Add(Dignos);
            foreach (DignosedPropertyType property in DignosedProperties)
            {
                dataTableResult.Columns.Add(resourceManager.GetString(property.ToString()));
            }
            int fieldsLength = dignosedFields.Length;
            int intervalLength = TotalPeriods.Count / 2;
            for (int i = 0; i < countOfGroup * dignosedFields.Length; i++)
            {
                drow = dataTableResult.NewRow();
                int t = i / (2 * fieldsLength);
                int c = (i - t * (2 * fieldsLength)) / fieldsLength;
                int v = i % fieldsLength;
                string timeMark = TotalPeriods[t].StartTime.Date.ToShortDateString() + "-" + TotalPeriods[t].EndTime.Date.ToShortDateString();
                drow[TimeSlot] = timeMark;
                if (c == 0)
                {
                    drow[Group] = resourceManager.GetString("StatisticGroup");
                }
                else
                    drow[Group] = resourceManager.GetString("ContrastGroup");
                drow[Dignos] = dignosedFields[v];
           
                if (newMorbidGroup.Count!=0)
                {
                    drow[resourceManager.GetString(DignosedPropertyType.NewMorbid.ToString())] = newMorbidGroup[i];
                }
                if (newMorbidRateGroup.Count != 0)
                {
                    drow[resourceManager.GetString(DignosedPropertyType.NewMorbidRate.ToString())] = Math.Round(newMorbidRateGroup[i], 2);
                }
                if (morbidGroup.Count != 0)
                {
                    drow[resourceManager.GetString(DignosedPropertyType.Morbid.ToString())] = morbidGroup[i];
                }
                if (morbidRateGroup.Count != 0)
                {
                    drow[resourceManager.GetString(DignosedPropertyType.MorbidRate.ToString())] = Math.Round(morbidRateGroup[i], 2);
                }
                dataTableResult.Rows.Add(drow);
            }
#endregion
            return dataTableResult;
        }
#region ��ϲ���ͳ��ʱ���ӷ���

        private List<int> GetNewMorbid(List<DignoCountPair> list)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < list.Count;i++ )
            {
                result.Add(list[i].CurrentCount-list[i].PreviousCount);
            }
            return result;
        }
        private List<double> GetNewMorbidRate(List<DignoCountPair> list, int totalPatients)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < list.Count; i ++)
            {
                double diff = (double)(list[i].CurrentCount - list[i].PreviousCount);
                result.Add((double)diff / (double)totalPatients);   
            }
            return result;
        }
        private List<int> GetMorbid(List<DignoCountPair> list)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < list.Count; i ++)
            {
                result.Add(list[i].CurrentCount);
            }
            return result;
        }
        private List<double> GetMorbidRate(List<DignoCountPair> list, int totalPatients)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < list.Count; i ++)
            {
                result.Add((double)list[i].CurrentCount / (double)totalPatients);
            }
            return result;
        }
#endregion
        #endregion
    }
}
