using System;
using System.Runtime.InteropServices.ComTypes;

namespace PointInSquare
{
    struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X} {Y}]";
        }
    }

    class Rectangle
    {
        public Point Angle1 { get; private set; }
        public Point Angle2 { get; private set; }
        public Point Angle3 { get; private set; }
        public Point Angle4 { get; private set; }

        public Rectangle(Point angle1, Point angle3)
        {
            Angle1 = angle1;
            Angle3 = angle3;

            CalulateAngles();
        }

        public void CalulateAngles()
        {
            Angle2 = new Point(Angle3.X, Angle1.Y);
            Angle4 = new Point(Angle1.X, Angle3.Y);
        }

        public override string ToString()
        {
            return $"({Angle1} {Angle2} {Angle3} {Angle4})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\n\tВведиете коодинаты угла прямоугольника: ");

            var randomPoint = new Point(4, 6);

            Console.WriteLine(new Rectangle(new Point(3, 4), new Point(10, 8)));

        }
    }
}
