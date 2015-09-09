using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace 委托11._2
{
    public delegate int BinaryOp(int x, int y);

    public class SimpleMath
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
        public int Subtract(int x, int y)
        {
            return x - y;
        }
    }

    public class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        private bool carIsDead;

        public Car() { MaxSpeed = 100; }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }

        public delegate void CarEngineHandler(string msgForCaller);
        private CarEngineHandler listOfHandlers;
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }

        public void Accelerate(int delta)
        {
            if (carIsDead)
            {
                if (listOfHandlers != null)
                {
                    listOfHandlers("Sorry, this cai is dead");
                }
            }
            else
            {
                CurrentSpeed += delta;
                if (10 == (MaxSpeed - CurrentSpeed)
                    && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy! Gonna blow!");
                }
                if (CurrentSpeed >= MaxSpeed)
                {
                    carIsDead = true;
                }
                else
                {
                    WriteLine("CurrentSpeed = {0}", CurrentSpeed);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Name = {0} MaxSpeed = {1}", PetName, MaxSpeed);
        }
    }

    public class SportCar : Car
    {
        public SportCar(string name, int maxSp, int currSp) : base(name, maxSp, currSp)
        {
        }
    }

    class Program
    {
        static void DisplayDelegateInfo(Delegate delObj)
        {
            foreach(Delegate d in delObj.GetInvocationList())
            {
                WriteLine("Method Name: {0}", d.Method);
                WriteLine("Type Name: {0}", d.Target);
            }
        }

        public delegate Car ObtainCarDelegate();

        public delegate void MyGenericDelegate<T>(T arg);

        static void Main(string[] args)
        {
            //SimpleMath s = new SimpleMath();
            //BinaryOp b = new BinaryOp(s.Add);

            //WriteLine("10 + 10 = {0}", b(10, 10));

            //DisplayDelegateInfo(b);

            Car c1 = new Car("SlugBug", 100, 10);
            c1.RegisterWithCarEngine(OnCarEngineEvent);
            //c1.RegisterWithCarEngine(OnCarEngineEvent2);
            WriteLine("Speeding up");
            
            for (int i = 0; i < 8; i++)
            {
                c1.Accelerate(20);
            }

            ObtainCarDelegate targetA = new ObtainCarDelegate(GetBasicCar);
            Car c = targetA();
            WriteLine("Obtained a {0}", c);

            ObtainCarDelegate targetB = new ObtainCarDelegate(GetBasicCar2);
            SportCar d = (SportCar)targetB();
            WriteLine("Obtained a {0}", d);

            MyGenericDelegate<string> strTarget = new MyGenericDelegate<string>(StringTarget);
            strTarget("Some string data");

            MyGenericDelegate<int> intTarget = new MyGenericDelegate<int>(IntTarget);
            intTarget(9);

            ReadLine();
        }

        public static void OnCarEngineEvent(string msg)
        {
            WriteLine("Message from Car object => {0}", msg);
        }

        public static void OnCarEngineEvent2(string msg)
        {
            WriteLine("Message from Car object => {0}", msg.ToUpper());
        }

        public static Car GetBasicCar()
        {
            return new Car("Zippy", 100, 55);
        }

        public static SportCar GetBasicCar2()
        {
            return new SportCar("dog", 50, 10);
        }

        public static void StringTarget(string arg)
        {
            WriteLine("arg in uppercase is: {0}", arg.ToUpper());
        }

        public static void IntTarget(int arg)
        {
            WriteLine("++arg is: {0}", ++arg);
        }
    }
}
