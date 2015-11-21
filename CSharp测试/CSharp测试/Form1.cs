using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int s32a = 3;
        public int s32b = 4;
        public void doSomeThing()
        {
            B b = new B();
            Form1 f = this;
            b.DoSomeThingWith(f);
        }
    }

    public class B
    {
        public void DoSomeThingWith(Form1 f1)
        {
            Console.WriteLine(f1.s32a + f1.s32b);
                        
        }
    }
}
