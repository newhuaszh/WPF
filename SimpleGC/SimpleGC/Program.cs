using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SimpleGC
{
    public class Car : IDisposable
    {
        public int CurrentSpeed { get; set; }
        public string Petname { get; set; }

        public Car() { }
        public Car(string name, int speed)
        {
            Petname = name;
            CurrentSpeed = speed;
        }

        public override string ToString()
        {
            return string.Format("{0} is going {1} MPH", Petname, CurrentSpeed);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~Car()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** Fun with System.GC *****");

            WriteLine("Estimated bytes on heep: {0}",
                GC.GetTotalMemory(false));

            WriteLine("This OS has {0} object generations.\n",
                (GC.MaxGeneration + 1));

            Car refToMyCar = new Car("Zippy", 100);
            WriteLine(refToMyCar.ToString());
            refToMyCar.Dispose();

            WriteLine("Generation of refToMyCar is: {0}",
                GC.GetGeneration(refToMyCar));

            object[] tonsOfObjects = new object[50000];
            for (int i = 0; i < 50000; i++)
                tonsOfObjects[i] = new object();

            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            WriteLine("Generation of refToMyCar is: {0}",
                GC.GetGeneration(refToMyCar));

            if (tonsOfObjects[9000] != null)
            {
                WriteLine("Generation of tonsOfObjects[9000] is: {0}",
                    GC.GetGeneration(tonsOfObjects[9000]));
            }
            else
            {
                WriteLine("tonsOfObjects[9000] is on longer alive.");
            }

            WriteLine("\nGen 0 has been swept {0} times",
                GC.CollectionCount(0));
            WriteLine("\nGen 1 has been swept {0} times",
                GC.CollectionCount(1));
            WriteLine("\nGen 2 has been swept {0} times",
                GC.CollectionCount(2));

            ReadLine();
        }
    }
}
