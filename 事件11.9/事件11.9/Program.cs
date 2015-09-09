using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace 事件11._9
{
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

        //public delegate void CarEngineHandler(string msgForCaller);
        public delegate void CarEngineHandler(object sender, CarEventArgs e);
        public event CarEngineHandler Exploded;
        public event CarEngineHandler AboutToBlow;

        //private CarEngineHandler listOfHandlers;
        //public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        //{
        //    listOfHandlers += methodToCall;
        //}

        public void Accelerate(int delta)
        {
            if (carIsDead)
            {
                if (Exploded != null)
                {
                    Exploded(this, new CarEventArgs("Sorry, this cai is dead"));
                }
            }
            else
            {
                CurrentSpeed += delta;
                if (10 == (MaxSpeed - CurrentSpeed)
                    && AboutToBlow != null)
                {
                    AboutToBlow(this, new CarEventArgs("Careful buddy! Gonna blow!"));
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

    public class CarEventArgs : EventArgs
    {
        public readonly string msg;
        public CarEventArgs(string message)
        {
            msg = message;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car("zippy", 100, 10);
            c.Exploded += CarAboutToBlow;
            c.AboutToBlow += CarAboutToBlow;
            for (int i = 0; i < 8; i++)
            {
                c.Accelerate(20);
            }

            ReadLine();
        }

        public static void OnCarEngineEvent(string msg)
        {
            WriteLine("Message from Car object => {0}", msg);
        }

        public static void CarAboutToBlow(object sender, CarEventArgs e)
        {
            if (sender is Car)
            {
                Car c = (Car)sender;
                WriteLine("Critical Message form {0}: {1}", c.PetName, e.msg);
            }
        }
    }
}
