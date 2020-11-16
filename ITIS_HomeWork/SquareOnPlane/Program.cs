using System;
using System.Threading;
using System.Transactions;

namespace SquareOnPlane
{
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, b.Y + b.Y);
        }

        public static double GetDistanceBetweenPoints(Point a, Point b)
        {
            return Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
        }

        public override string ToString() => $"[{X} {Y}]";
    }

    class Square
    {
        public Point pointA { get; private set; }
        public Point pointB { get; private set; }
        public Point pointC { get; private set; }
        public Point pointD { get; private set; }
        public bool IsSquare { get; private set; }

        public Square(Point a, Point b, Point c)
        {
            pointA = a;
            pointB = b;
            pointC = c;

            var AB = Point.GetDistanceBetweenPoints(pointA, pointB);
            var BC = Point.GetDistanceBetweenPoints(pointB, pointC);
            var CA = Point.GetDistanceBetweenPoints(pointC, pointA);

            var hipotenuseIsAB = BC == CA && AB == GetHipotenuseByLegs(BC, CA);
            var hipotenuseIsBC = CA == AB && BC == GetHipotenuseByLegs(CA, AB);
            var hipotenuseIsCA = AB == BC && CA == GetHipotenuseByLegs(AB, BC);

            IsSquare = hipotenuseIsAB || hipotenuseIsBC || hipotenuseIsCA;
        }

        public override string ToString()
        {
            return $"{pointA} {pointB} {pointC} {pointD}";
        }

        private double GetHipotenuseByLegs(double leg1, double leg2)
        {
            return Math.Sqrt(leg1 * leg1 + leg2 * leg2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(new Point(1, 4), new Point(3, 4), new Point(1, 2));

            Console.WriteLine(square);
            Console.WriteLine("\n\tЯвляется квадратом: " + square.IsSquare);
        }
    }
}
