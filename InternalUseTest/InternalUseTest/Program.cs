using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalUseTest
{
    public class Class1
    {
        protected internal string OtherDll1 = "This is Other dll protected internal";
        protected string Otherstr = "This is other dll protected";
        internal string OtherDll = "This is Other dll internal";
    }
    public class Class2 : Class1
    {
        public string GetInternal()
        {
            return this.OtherDll;
        }
        public string GetProtectedInternal()
        {
            return this.OtherDll1;
        }
        public string GetProtected()
        {
            return this.Otherstr;
        }
    }
    internal class Class3
    {
        protected string str1 = "this is a test internal class protected attribute";
        public string str2 = "This is a test internal class public sttribute";
        internal string str3 = "This is a test internal class internal attribute";
    }
    class Class4 : Class3
    {
        public string GetInternal()
        {
            return this.str3;
        }
        public string GetProtected()
        {
            return this.str1;
        }
        public string Getpublic()
        {
            return this.str2;
        }
    }

    public class Class5
    {
        Class1 cla1 = new Class1();
        Class3 cls3 = new Class3();
        public string GetInternal()
        {
            return cla1.OtherDll;
        }
        public string GetProtectedInternal()
        {
            return cla1.OtherDll1;
        }
        public string Getpublic()
        {
            return cls3.str2;
        }
        public string GetInternal1()
        {
            return cls3.str3;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
