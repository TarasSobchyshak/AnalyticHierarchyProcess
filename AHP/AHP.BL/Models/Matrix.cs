using System;
using System.Collections.Generic;
using System.Linq;

namespace AHP.BL.Models
{
    public class Matrix : ObservableObject
    {
        private int _n;
        private int _m;
        public double[,] _a;
        public string[,] _aStr;

        public Matrix()
        {
            N = 0;
            M = 0;
            A = null;
        }

        public Matrix(int n)
        {
            N = n;
            M = n;
            A = new double[_n, _m];
        }

        public Matrix(int n, int m)
        {
            N = n;
            M = m;
            A = new double[_n, _m];
        }

        public Matrix(Vector x)
        {
            N = x.Length;
            M = 1;
            A = new double[N, M];
            for (int i = 0; i < N; ++i) A[i, 0] = x[i];
        }

        public Matrix(params string[] x)
        {
            N = x.Length;
            M = 1;
            AStr = new string[N, M];
            for (int i = 0; i < N; ++i) AStr[i, 0] = x[i];
        }

        public Matrix(List<Vector> x)
        {
            N = x.FirstOrDefault().Length;
            M = x.Count();
            A = new double[N, M];
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    A[i, j] = x[j][i];
                }
            }
        }
        public Matrix(Matrix a)
        {
            M = a.M;
            N = a.N;
            A = a.A;
        }

        public int N
        {
            get { return _n; }
            set { _n = value; }
        }
        public int M
        {
            get { return _m; }
            set { SetProperty(ref _m, value); }
        }
        public double[,] A
        {
            get { return _a; }
            set
            {
                if (value != null)
                {
                    N = value.GetLength(0);
                    M = value.GetLength(1);
                }
                else
                {
                    N = M = 0;
                }
                SetProperty(ref _a, value);
            }
        }

        public string[,] AStr
        {
            get { return _aStr; }
            set
            {
                if (value != null)
                {
                    N = value.GetLength(0);
                    M = value.GetLength(1);
                }
                else
                {
                    N = M = 0;
                }
                SetProperty(ref _aStr, value);
            }
        }
        public Vector GetRow(int index)
        {
            if (index > N || index < 0) throw new IndexOutOfRangeException();
            double[] res = new double[M];
            for (int i = 0; i < M; ++i)
            {
                res[i] = A[index, i];
            }
            return new Vector(res);
        }

        public Vector GetColumn(int index)
        {
            if (index >= M || index < 0) throw new IndexOutOfRangeException();
            double[] res = new double[N];
            for (int i = 0; i < N; ++i)
            {
                res[i] = A[i, index];
            }
            return new Vector(res);
        }

        public static double GeometricMean(Matrix a)
        {
            double res = 0.0;
            for (int i = 0; i < a.N; ++i)
            {
                res += Vector.GeometricMean(a.GetRow(i));
            }
            return res;
        }

        public static Matrix IdentityMatrix(int n)
        {
            Matrix res = new Matrix(n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    res[i, j] = i == j ? 1 : 0;
            return res;
        }

        public static Matrix IdentityMatrix(int n, int m)
        {
            Matrix res = new Matrix(n, m);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    res[i, j] = i == j ? 1 : 0;
            return res;
        }

        public static Vector operator *(Matrix a, Vector b)
        {
            if (b.Length != a.M) throw new System.Exception("Different vector and matrix dimensions");

            Vector c = new Vector(b.Length);
            double temp;
            for (int i = 0; i < a.N; ++i)
            {
                temp = 0;
                for (int j = 0; j < a.M; ++j) temp += a[i, j] * b[j];
                c[i] = temp;
            }
            return c;
        }

        public static Matrix operator *(double a, Matrix b)
        {
            Matrix c = new Matrix(b.N, b.M);
            for (int i = 0; i < c.N; ++i)
                for (int j = 0; j < c.M; ++j)
                    c[i, j] = a * b[i, j];
            return c;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.M != b.N) throw new RankException();

            Matrix c = new Matrix(a.N, b.M);
            for (int i = 0; i < a.N; ++i)
            {
                for (int j = 0; j < b.M; ++j)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < a.M; ++k)
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
            if ((a.M != b.M) || (a.N != b.N)) throw new RankException();

            Matrix c = new Matrix(a.N, a.M);
            for (int i = 0; i < c.N; ++i)
                for (int j = 0; j < c.M; ++j)
                    c[i, j] = a[i, j] + b[i, j];
            return c;
        }

        public static Matrix operator -(Matrix a, Matrix b) => a + -b;

        public static Matrix operator -(Matrix a)
        {
            Matrix c = new Matrix(a.N, a.M);
            for (int i = 0; i < c.N; ++i)
                for (int j = 0; j < c.M; ++j)
                    c[i, j] = -a[i, j];
            return c;
        }

        public static implicit operator Matrix(double[,] a)
        {
            Matrix c = new Matrix(a.GetLength(0), a.GetLength(1));
            for (int i = 0; i < c.N; ++i)
                for (int j = 0; j < c.M; ++j)
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
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    str += $"{A[i, j]:0.00}\t";
                }
                str += "\r\n";
            }
            return str;
        }
    }
}