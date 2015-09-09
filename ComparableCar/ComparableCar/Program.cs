using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparableCar
{
    public class Car : IComparable
    {

        public int CarID { get; set; }
        public int CurrentSpeed { get; private set; }
        public string PetName { get; private set; }

        public Car(string name, int currSp, int id)
        {
            CurrentSpeed = currSp;
            PetName = name;
            CarID = id;
        }

        public static IComparer PetNameComparer
        {
            get
            {
                return (IComparer)new PetNameComparer();
            }
        }
        int IComparable.CompareTo(object obj)
        {
            Car temp = obj as Car;
            if (temp != null)
            {
                //if (this.CarID > temp.CarID)
                //{
                //    return 1;
                //}
                //else if (this.CarID < temp.CarID)
                //{
                //    return -1;
                //}
                //else
                //{
                //    return 0;
                //}
                return this.CarID.CompareTo(temp.CarID);
            }
            else
            {
                throw new ArgumentException("not a car");
            }
        }
    }

    public class PetNameComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Car c1 = x as Car;
            Car c2 = y as Car;
            if (c1 != null && c2 != null)
            {
                return string.Compare(c1.PetName, c2.PetName);
            }
            else
            {
                throw new ArgumentException("not a car");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car[] myAutos = new Car[5];
            myAutos[0] = new Car("Rusty", 80, 1);
            myAutos[1] = new Car("Mary", 40, 234);
            myAutos[2] = new Car("Viper", 40, 34);
            myAutos[3] = new Car("Mel", 40, 4);
            myAutos[4] = new Car("Chucky", 40, 5);

            // Display current array.
            Console.WriteLine("Here is the unordered set of cars:");
            foreach (Car c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.PetName);
            // Now, sort them using IComparable!
            Array.Sort(myAutos);
            Console.WriteLine();
            // Display sorted array.
            Console.WriteLine("Here is the ordered set of cars:");
            foreach (Car c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.PetName);

            // sorted by petname
            Console.WriteLine();
            //Array.Sort(myAutos, new PetNameComparer());
            Array.Sort(myAutos, Car.PetNameComparer);
            foreach (Car c in myAutos)
                Console.WriteLine("{0} {1}", c.CarID, c.PetName);

            Console.ReadLine();
        }
    }
}
