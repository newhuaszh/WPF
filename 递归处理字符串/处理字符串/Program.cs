using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 处理字符串
{
    class Program
    {
        static Int32 s32Count = 0;
        static void Main(string[] args)
        {
            string strCmd = "1|2, 我们|的|爱, 6|7|8|9, a|b";
            string[] astrRows = strCmd.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            List<string[]> lstTotal = new List<string[]>();
            foreach(string str in astrRows)
            {
                lstTotal.Add(str.Trim().Split('|'));
            }
            DoStr(0, "", lstTotal);
            Console.WriteLine("共{0}个", s32Count);
            Console.ReadLine();
        }

        static void DoStr(Int32 s32Index, string strCur, List<string[]> lstTotal)
        {
            if (s32Index < lstTotal.Count)
            {
                for(int i = 0; i < lstTotal[s32Index].Length; i++)
                {
                    DoStr(s32Index + 1, strCur + lstTotal[s32Index][i], lstTotal);
                }
            }
            else
            {
                Console.WriteLine(strCur);
                s32Count++;
            }
        }
    }
}
