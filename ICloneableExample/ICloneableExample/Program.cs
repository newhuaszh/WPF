using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICloneableExample
{
    abstract class CloneableType
    {
        public abstract object Clone();
    }

    class CloneA : CloneableType, InterfaceCloneable
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }
        public int a { get; set; }
    }

    interface InterfaceCloneable
    {
        object Clone();
        int a { get; set; }
    }

    class CloneB : InterfaceCloneable
    {
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int a { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string myStr = "Hello";
            OperatingSystem unixOS = new OperatingSystem(PlatformID.Unix, new Version());
            System.Data.SqlClient.SqlConnection sqlCnn = new System.Data.SqlClient.SqlConnection();

            CloneMe(myStr);
            CloneMe(unixOS);
            CloneMe(sqlCnn);

            InterfaceCloneable[] ainterface = { new CloneA(), new CloneB() };

            var file = @"C:\Users\sn01396\Desktop\柱温箱发送方法失败.txt";
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    var num = 0;
                    while (!sr.EndOfStream)
                    {
                        num++;
                        var str = sr.ReadLine();
                        var key = "7F FE 00 01 00 ";
                        var len = key.Length;
                        var index = str.IndexOf(key);
                        if (index != -1 && index + len + 2 <= str.Length)
                        {
                            var next = str.Substring(index + len, 2);
                            Console.WriteLine("源地址：" + next);
                            if (next != "28")
                            {
                                Console.WriteLine("出错在：" + num);
                            }
                        }
                    }
                }
            }



            Console.ReadLine();
        }

        private static void CloneMe(ICloneable c)
        {
            object theClone = c.Clone();
            Console.WriteLine("Your clone os a: {0}",
                theClone.GetType().Name);
        }
    }
}
