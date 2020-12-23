using System;

namespace ClassMatrix3x3
{
    class Matrix3x3
    {
        public double[][] values;

        public Matrix3x3()
        {
            for (int x = 0; x < 3; x++)
                values[x] = new double[3];
        }

        public Matrix3x3(double n)
        {
            for (int x = 0; x < 3; x++)
                values[x] = new double[] { n, n, n };
        }

        public Matrix3x3(double[][] matrix)
        {
            values = matrix;
        }

        public Matrix3x3(double a, double b, double c, double d, double e, double f, double g, double h, double i)
        {
            values[0] = new double[3] { a, b, c };
            values[1] = new double[3] { d, e, f };
            values[2] = new double[3] { g, h, i };
        }

        public Matrix3x3 Add(Matrix3x3 matrix)
        {
            double[][] temp = new double[3][];

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    temp[x][y] = matrix.values[x][y] + values[x][y];

            return new Matrix3x3(temp);
        }

        public void Add3(Matrix3x3 matrix)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    values[x][y] += matrix.values[x][y];
        }

        public Matrix3x3 Sub(Matrix3x3 matrix)
        {
            double[][] temp = new double[3][];

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    temp[x][y] = values[x][y] - matrix.values[x][y];

            return new Matrix3x3(temp);
        }

        public void Sub3(Matrix3x3 matrix)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    values[x][y] -= matrix.values[x][y];
        }
        public Matrix3x3 MultiplyNumber(double n)
        {
            double[][] temp = new double[3][] { new double[3], new double[3], new double[3] };

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    temp[x][y] = values[x][y] * n;

            return new Matrix3x3(temp);
        }

        public void MultiplyNumber3(double n)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    values[x][y] *= n;
        }

        public Matrix3x3 Multiply(Matrix3x3 matrix)
        {
            double[][] temp = new double[][] { new double[3], new double[3], new double[3] };

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    double num = 0;

                    for (int x = 0; x < 3; x++)
                        num += values[row][x] * matrix.values[x][col];

                    temp[row][col] = num;
                }
            }

            return new Matrix3x3(temp);
        }

        public void Multiply3(Matrix3x3 matrix)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    double num = 0;

                    for (int x = 0; x < 3; x++)
                        num += values[row][x] * matrix.values[x][col];

                    values[row][col] = num;
                }
            }
        }

        public double Det()
        {
            return values[0][0] * values[1][1] * values[2][2]
                 + values[0][1] * values[1][2] * values[2][0]
                 + values[0][2] * values[1][0] * values[2][1]
                 - values[0][2] * values[1][1] * values[2][0]
                 - values[0][0] * values[2][1] * values[1][2]
                 - values[2][2] * values[1][0] * values[0][1];
        }

        public void Transpon()
        {
            Matrix3x3 temp = new Matrix3x3();

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    temp.values[x][y] = values[y][x];

            values = temp.values;
        }

        public Matrix3x3 InverseMatrix()
        {
            if (Det() != 0)
            {
                double[] tempDet = new double[4];
                int counter = 0;

                Matrix3x3 temp = new Matrix3x3();

                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (a != x && b != y)
                                {
                                    tempDet[counter] = values[a][b];
                                    counter++;
                                }
                            }
                        }

                        counter = 0;

                        temp.values[x][y] = Math.Pow(-1, x + y) * (tempDet[0] * tempDet[3] - tempDet[1] * tempDet[2]);
                    }
                }

                temp.Transpon();

                return temp.MultiplyNumber(1 / Det());
            }
            else
                return new Matrix3x3();
        }

        public override string ToString()
        {
            return $"{values[0][0]}\t{values[0][1]}\t{values[0][2]}\n{values[1][0]}\t{values[1][1]}\t{values[1][2]}\n{values[2][0]}\t{values[2][1]}\t{values[2][2]}";
        }

        public Vector3D MultiplyVector(Vector3D vector)
        {
            var temp = new Vector3D();

            temp.x = vector.x * values[0][0] + vector.y * values[0][1] + vector.z * values[0][2];
            temp.y = vector.x * values[1][0] + vector.y * values[1][1] + vector.z * values[1][2];
            temp.z = vector.x * values[2][0] + vector.y * values[2][1] + vector.z * values[2][2];

            return temp;
        }
    }
}
