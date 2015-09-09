using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace InterfaceHierarchy
{
    public interface IDrawable
    {
        void Draw();
    }

    public interface IAdvancedDraw : IDrawable
    {
        void DrawInBoundingBox(int top, int left, int bottom, int right);
        void DrawUpsideDown();
    }

    public class BitmapImage : IAdvancedDraw
    {
        public void Draw()
        {
            WriteLine("Draw");
        }

        public void DrawInBoundingBox(int top, int left, int bottom, int right)
        {
            WriteLine("Draw in box");
        }

        public void DrawUpsideDown()
        {
            WriteLine("Draw upsidedown");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BitmapImage bi = new BitmapImage();
            bi.Draw();
            bi.DrawInBoundingBox(10, 10, 100, 150);
            bi.DrawUpsideDown();
            ReadLine();
        }
    }
}
