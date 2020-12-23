using System;
using System.Collections.Generic;
using System.Text;

namespace ComplexNumber
{
	class ComplexNumber
	{
		public double a { get; set; }
		public double b { get; set; }

		public ComplexNumber(double a = 0, double b = 0)
		{
			this.a = a;
			this.b = b;
		}

		public ComplexNumber Add(ComplexNumber number) => new ComplexNumber(a + number.a, b + number.b);

		public void Add2(ComplexNumber number)
		{
			a += number.a;
			b += number.b;
		}

		public ComplexNumber Sub(ComplexNumber number) => new ComplexNumber(a - number.a, b - number.b);

		public void Sub2(ComplexNumber number)
		{
			a -= number.a;
			b -= number.b;
		}

		public ComplexNumber Multiply(ComplexNumber number) => new ComplexNumber(a * number.a - b * number.b, a * number.b + b * number.a);

		public void Multiply2(ComplexNumber number)
		{
			a = a * number.a - b * number.b;
			b = a * number.b + b * number.a;
		}

		public ComplexNumber MultiplyNumber(double n) => new ComplexNumber(a * n, b * n);

		public void MultiplyNumber2(double n)
		{
			a *= n;
			b *= n;
		}

		public ComplexNumber Divide(ComplexNumber number)
		{
			return new ComplexNumber((a * number.a + b * number.b) / (number.a * number.a + number.b * number.b),
				(b * number.a - a * number.b) / (number.a * number.a + number.b * number.b));
		}

		public void Divide2(ComplexNumber number)
		{
			a = (a * number.a + b * number.b) / (number.a * number.a + number.b * number.b);
			b = (b * number.a - a * number.b) / (number.a * number.a + number.b * number.b);
		}

		public double Length() => Math.Sqrt(a * a + b * b);

		public override string ToString() => this.a + (b < 0 ? " - " : " + ") + Math.Abs(this.b) + " * i";

		public double Arg() => a < 0 ? Math.Atan(b / a + Math.PI) : Math.Atan(b / a);

		public ComplexNumber Pow(double n)
		{
			return new ComplexNumber(Math.Round(Math.Pow(this.Length(), n) * Math.Cos(this.Arg() * n)),
				Math.Round(Math.Pow(this.Length(), n) * Math.Sin(this.Arg() * n)));
		}

		public bool Equals(ComplexNumber z) => a == z.a && b == z.b;
	}
}
