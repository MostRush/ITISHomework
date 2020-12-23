using System;

namespace Vector3DClass
{
	class Vector3D
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public Vector3D(double x = 0, double y = 0, double z = 0)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Vector3D Add(Vector3D vector) => new Vector3D(X + vector.X, Y + vector.Y, Z + vector.Z);

		public void Add2(Vector3D vector)
		{
			X += vector.X;
			Y += vector.Y;
			Z += vector.Z;
		}

		public Vector3D Sub(Vector3D vector) => new Vector3D(X - vector.X, Y - vector.Y, Z - vector.Z);

		public void Sub2(Vector3D vector)
		{
			X -= vector.X;
			Y -= vector.Y;
			Z -= vector.Z;
		}

		public Vector3D Multiply(double n) => new Vector3D(X * n, Y * n, Z * n);

		public void Multiply2(double n)
		{
			X *= n;
			Y *= n;
			Z *= n;
		}

		public override string ToString() => $"[{X} {Y} {Z}]";

		public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);

		public double ScalarProduct(Vector3D vector) => X * vector.X + Y * vector.Y + Z * vector.Z;
	}
}
