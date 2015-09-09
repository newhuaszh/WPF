using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEnumerator
{
    public class Car
    {
        public string PetName { get; set; } = "szh";
        public string CurrentSpeed { get; set; } = "100";

        public Car(string name, string speed)
        {
            PetName = name;
            CurrentSpeed = speed;
        }

        public Car()
        { }
    }

    public class Garage
    {
        private Car[] carArray = new Car[4];

        public Garage()
        {
            carArray[0] = new Car("a", "100");
            carArray[1] = new Car("b", "200");
            carArray[2] = new Car("c", "300");
            carArray[3] = new Car("d", "400");
        }

        //public IEnumerator GetEnumerator()
        //{
        //    return carArray.GetEnumerator();
        //}

        public IEnumerator GetEnumerator()
        {
            foreach(Car c in carArray)
            {
                yield return c;
            }
        }

        public IEnumerable GetTheCars(bool ReturnRevesed)
        {
            if (ReturnRevesed)
            {
                for (int i = carArray.Length; i != 0; i--)
                {
                    yield return carArray[i];
                }
            }
            else
            {
                foreach(Car c in carArray)
                {
                    yield return c;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Garage g = new Garage();
            
            foreach(Car c in g.GetTheCars(true))
            {
                
            }
        }
    }
}
