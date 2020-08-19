using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDemo.StringComparison
{
    /// <summary>
    /// 比较2个字符串的相似度(使用余弦相似度)
    /// </summary>
    internal static class StringSimilarity
    {
        public static double Get(string str1, string str2)
        {
            str1 = str1.Trim();
            str2 = str2.Trim();
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return 0;

            List<string> lstr1 = SimpParticiple(str1);
            List<string> lstr2 = SimpParticiple(str2);
            //求并集
            var strUnion = lstr1.Union(lstr2);
            //求向量
            List<int> int1 = new List<int>();
            List<int> int2 = new List<int>();
            foreach (var item in strUnion)
            {
                int1.Add(lstr1.Count(o => o == item));
                int2.Add(lstr2.Count(o => o == item));
            }

            double s = 0;
            double den1 = 0;
            double den2 = 0;
            for (int i = 0; i < int1.Count(); i++)
            {
                //求分子
                s += int1[i] * int2[i];
                //求分母（1）
                den1 += Math.Pow(int1[i], 2);
                //求分母（2）
                den2 += Math.Pow(int2[i], 2);
            }

            return s / (Math.Sqrt(den1) * Math.Sqrt(den2));
        }

        /// <summary>
        /// 简单分词（需要更好的效果，需要这里优化，比如把：【今天天气很好】，分成【今天，天气，很好】，同时可以做同义词优化，【今天】=【今日】效果更好）
        /// </summary>
        private static List<string> SimpParticiple(string str)
        {
            List<string> vs = new List<string>();
            foreach (var item in str)
            {
                vs.Add(item.ToString());
            }
            return vs;
        }
    }
}