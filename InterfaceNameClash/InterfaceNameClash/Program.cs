using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceNameClash
{
    public interface IDrawToForm
    {
        void Draw();
    }

    public interface IDrawToMemory
    {
        void Draw();
    }

    public interface IDrawToPrinter
    {
        void Draw();
    }

    class Octagon : IDrawToForm, IDrawToMemory, IDrawToPrinter
    {
        void IDrawToPrinter.Draw()
        {
            Console.WriteLine("Drawing the Printer");
        }

        void IDrawToMemory.Draw()
        {
            Console.WriteLine("Drawing the memory");
        }

        void IDrawToForm.Draw()
        {
            Console.WriteLine("Drawing the form");
        }

        internal void Draw()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Octagon oct = new Octagon();
            //oct.Draw();
            IDrawToForm itfForm = (IDrawToForm)oct;
            itfForm.Draw();

            IDrawToMemory itfMemory = (IDrawToMemory)oct;
            itfMemory.Draw();

            IDrawToPrinter itfPrinter = (IDrawToPrinter)oct;
            itfPrinter.Draw();

            Console.ReadLine();
        }
    }
}
