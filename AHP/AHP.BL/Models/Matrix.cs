using System;
using static System.Math;

namespace AHP.BL.Models
{
    public class Matrix
    {
        public Matrix(int n)
        {
            A = new double[n, n];
        }

        /*          PROPERTIES           */

        public double[,] A { get; set; }
        public int Dim => (int)Sqrt(A.Length);

        public static class LinearEquationSolver
        {
            /// <summary>Computes the solution of a linear equation system.</summary>
            /// <param name="M">
            /// The system of linear equations as an augmented matrix[row, col] where (rows + 1 == cols).
            /// It will contain the solution in "row canonical form" if the function returns "true".
            /// </param>
            /// <returns>Returns whether the matrix has a unique solution or not.</returns>
            public static bool Solve(double[,] M)
            {
                // input checks
                int rowCount = M.GetUpperBound(0) + 1;
                if (M == null || M.Length != rowCount * (rowCount + 1))
                    throw new ArgumentException("The algorithm must be provided with a (n x n+1) matrix.");
                if (rowCount < 1)
                    throw new ArgumentException("The matrix must at least have one row.");

                // pivoting
                for (int col = 0; col + 1 < rowCount; col++)
                    if (M[col, col] == 0)
                    // check for zero coefficients
                    {
                        // find non-zero coefficient
                        int swapRow = col + 1;
                        for (; swapRow < rowCount; swapRow++) if (M[swapRow, col] != 0) break;

                        if (M[swapRow, col] != 0) // found a non-zero coefficient?
                        {
                            // yes, then swap it with the above
                            double[] tmp = new double[rowCount + 1];
                            for (int i = 0; i < rowCount + 1; i++)
                            { tmp[i] = M[swapRow, i]; M[swapRow, i] = M[col, i]; M[col, i] = tmp[i]; }
                        }
                        else return false; // no, then the matrix has no unique solution
                    }

                // elimination
                for (int sourceRow = 0; sourceRow + 1 < rowCount; sourceRow++)
                {
                    for (int destRow = sourceRow + 1; destRow < rowCount; destRow++)
                    {
                        double df = M[sourceRow, sourceRow];
                        double sf = M[destRow, sourceRow];
                        for (int i = 0; i < rowCount + 1; i++)
                            M[destRow, i] = M[destRow, i] * df - M[sourceRow, i] * sf;
                    }
                }

                // back-insertion
                for (int row = rowCount - 1; row >= 0; row--)
                {
                    double f = M[row, row];
                    if (f == 0) return false;

                    for (int i = 0; i < rowCount + 1; i++) M[row, i] /= f;
                    for (int destRow = 0; destRow < row; destRow++)
                    { M[destRow, rowCount] -= M[destRow, row] * M[row, rowCount]; M[destRow, row] = 0; }
                }
                return true;
            }
        }

        public static Vector Gauss(Matrix A, Vector b, ref int p)
        {
            Vector x = new Vector(b.Length);

            for (int i = 0; i < b.Length - 1; i++)
            {
                for (int j = i + 1; j < b.Length; j++)
                {
                    double s = A[j, i] / A[i, i];
                    for (int k = i; k < b.Length; k++)
                    {
                        A[j, k] -= A[i, k] * s;
                    }
                    b[j] -= b[i] * s;
                    p += 2;
                }
            }

            for (int i = b.Length - 1; i >= 0; i--)
            {
                b[i] /= A[i, i];
                A[i, i] /= A[i, i];
                for (int j = i - 1; j >= 0; j--)
                {
                    double s = A[j, i] / A[i, i];
                    A[j, i] -= s;
                    b[j] -= b[i] * s;
                    p += 3;
                }
            }

            for (int i = 0; i < b.Length; i++)
            {
                x[i] = b[i] / A[i, i];
                p += 2;
            }

            return x;
        }

        public Matrix Inversion()
        {
            int n = Dim;
            Matrix res = new Matrix(n);
            Matrix E = IdentityMatrix(n);
            Matrix AA = (Matrix)A;
            double temp;
            for (int k = 0; k < n; k++)
            {
                temp = AA[k, k];
                for (int j = 0; j < n; j++)
                {
                    AA[k, j] /= temp;
                    E[k, j] /= temp;
                }
                for (int i = k + 1; i < n; i++)
                {
                    temp = AA[i, k];
                    for (int j = 0; j < n; j++)
                    {
                        AA[i, j] -= AA[k, j] * temp;
                        E[i, j] -= E[k, j] * temp;
                    }
                }
            }
            return E;
        }

        public static Matrix IdentityMatrix(int n)
        {
            Matrix res = new Matrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = i == j ? 1 : 0;
            return res;
        }

        /*          OPERATORS           */

        public static Vector operator *(Matrix a, Vector b)
        {
            if (b.Length != a.Dim) throw new System.Exception("Different vector and matrix dimensions");
            Vector c = new Vector(b.Length);
            double temp;
            for (int i = 0; i < a.Dim; ++i)
            {
                temp = 0;
                for (int j = 0; j < a.Dim; ++j) temp += a[i, j] * b[j];
                c[i] = temp;
            }
            return c;
        }
        public static Matrix operator *(double a, Matrix b)
        {
            Matrix c = new Matrix(b.Dim);
            for (int i = 0; i < c.Dim; ++i)
                for (int j = 0; j < c.Dim; ++j)
                    c[i, j] = a * b[i, j];
            return c;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(b.Dim);
            for (int i = 0; i < c.Dim; ++i)
            {
                for (int j = 0; j < c.Dim; ++j)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < c.Dim; ++k)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return c;
        }
        public static Matrix operator *(Matrix b, double a) => a * b;
        public static Matrix operator /(Matrix b, double a) => (1.0 / a) * b;
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(b.Dim);
            for (int i = 0; i < c.Dim; ++i)
                for (int j = 0; j < c.Dim; ++j)
                    c[i, j] = a[i, j] + b[i, j];
            return c;
        }
        public static Matrix operator -(Matrix a, Matrix b) => a + -b;
        public static Matrix operator -(Matrix a)
        {
            Matrix c = new Matrix(a.Dim);
            for (int i = 0; i < c.Dim; ++i)
                for (int j = 0; j < c.Dim; ++j)
                    c[i, j] = -a[i, j];
            return c;
        }
        public static explicit operator Matrix(double[,] a)
        {
            Matrix c = new Matrix((int)Math.Sqrt(a.Length));
            for (int i = 0; i < c.Dim; ++i)
                for (int j = 0; j < c.Dim; ++j)
                    c[i, j] = a[i, j];
            return c;
        }
        public double this[int i, int j]
        {
            get { return A[i, j]; }
            set { A[i, j] = value; }
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Dim; ++i)
            {
                for (int j = 0; j < Dim; ++j)
                {
                    str += $"{A[i, j]:0.00}\t";
                }
                str += "\r\n";
            }
            return str;
        }
    }
}