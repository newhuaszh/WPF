using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lst = new List<string> { "s", "z", "h"};
            DoStr(lst, 2, 0, "");

            Console.ReadLine();
        }

        static void DoStr(List<string> lst, Int32 s32Num, Int32 s32Index, string str)
        {
            if (s32Index < lst.Count)
            {
                for (int i = s32Index; i < lst.Count; i++)
                {
                    if (str.Length < s32Num)
                    {
                        string strCur = lst[i];
                        DoStr(lst, s32Num, i + 1, str + strCur);
                    }
                    else
                    {
                        Console.WriteLine(str);
                        break;
                    }
                }
            }
            else
            {
                if (str.Length == s32Num)
                    Console.WriteLine(str);
            }  
        }
    }
}
