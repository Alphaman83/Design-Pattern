using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public Rectangle()
        {

        }
        public Rectangle(int _width, int _height)
        {
            Width = _width;
            Height = _height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)}:{Width},{nameof(Height)}:{Height}";
        }
    }
    public class Square : Rectangle
    {
        public override int Width
        { set { base.Width = base.Height = value; } }

        public override int Height
        { set { base.Width = base.Height = value; } }
    }
    public class LiskovSubstitutionPrinciple
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2,3);
            WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            WriteLine($"{sq} has area {Area(sq)}");

            ReadLine();
        }
    }
}
