using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace GenericPoint
{
    public struct Point<T>
    {
        private T xPos;
        private T yPos;

        public Point(T xval, T yval)
        {
            xPos = xval;
            yPos = yval;
        }
        public T X
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public T Y
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", xPos, yPos);
        }

        public void ResetPoint()
        {
            xPos = default(T); 
            yPos = default(T);
        }
    }

    public class MyList<T, K> where T: new()
        where K : new()
    {
        private List<T> listOfData = new List<T>();

        public virtual void Insert(T Data)
        {
            listOfData.Add(Data);
        }

        public virtual void OutPut()
        {
            WriteLine(typeof(T).GetType().ToString() + ": "+ listOfData.Count);
        }
    }

    //public class MystringList : MyList<Test>
    //{

    //    //public override void OutPut()
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //}

    public class Test
    {
        public int a;
        public int b;

        private int T1()
        {
            return 1;
        }

        public int T2()
        {
            return 2;
        }
    }

    //public class BsicMath<T> where T : operator +, operator -, operator *, operator /
    //{
    //    public T Add(T a, T b)
    //    {
    //        return a + b;
    //    }

    //    public T Subtract(T a, T b)
    //    {
    //        return a - b;
    //    }

    //    public T Multiply(T a, T b)
    //    {
    //        return a * b;
    //    }

    //    public T Divide(T a, T b)
    //    {
    //        return a / b;
    //    }
    //}

    class Program
    {
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        static void Main(string[] args)
        {
            Test test = new Test();
            Type t = test.GetType();
            System.Reflection.MethodInfo[] am = t.GetMethods();

            Point<int> p = new Point<int>(1, 2);
            Point<string> p2 = new Point<string>("cc", "dd");
            Point<Test> p3 = new Point<Test>(new Test() { a = 1, b = 2 }, new Test() { a = 3, b = 4 });

            WriteLine("P.ToString() = {0}", p.ToString());
            p.ResetPoint();
            WriteLine("P.ToString() = {0}", p.ToString());
            WriteLine();

            WriteLine("P2.ToString() = {0}", p2.ToString());
            p2.ResetPoint();
            WriteLine("P2.ToString() = {0}", p2.ToString());
            WriteLine();

            WriteLine("p3.ToString() = {0}", p3.ToString());
            p2.ResetPoint();
            WriteLine("p3.ToString() = {0}", p3.ToString());
            WriteLine();

            //MystringList a = new MystringList();
            //a.Insert(new Test());
            //a.Insert(new Test());
            //a.OutPut();
            int a = 1;
            int b = 2;
            Swap<int>(ref a, ref b);
            WriteLine("a={0} b={1}", a, b);

            string stra = "a";
            string strb = "b";
            Swap<string>(ref stra, ref strb);
            WriteLine("stra={0} strb={1}", stra, strb);

            ReadLine();
        }
    }
}
