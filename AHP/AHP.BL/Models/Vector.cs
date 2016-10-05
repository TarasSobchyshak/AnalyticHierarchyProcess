using System.Collections;
using System.Collections.Generic;
using static System.Math;

namespace AHP.BL.Models
{
    public class Vector : IEnumerable<double>
    {
        public Vector(int n)
        {
            X = new double[n];
        }
        public Vector(params double[] x)
        {
            X = new double[x.Length];
            for (int i = 0; i < x.Length; X[i] = x[i], ++i) ;
        }

        public double[] X { set; get; }
        public int Length => X.Length;
        
        public static double Sum(params double[] x)
        {
            double res = 0.0;
            for (int i = 0; i < x.Length; res += x[i], ++i) ;
            return res;
        }

        public static double GeometricMean(Vector x)
        {
            double res = 1.0;
            for (int i = 0; i < x.Length; ++i)
            {
                res *= x[i];
            }
            return Pow(res, 1.0 / x.Length);
        }

        public double this[int i]
        {
            get { return X[i]; }
            set { X[i] = value; }
        }
        public static explicit operator double[] (Vector a)
        {
            double[] c = new double[a.Length];
            for (int i = 0; i < c.Length; c[i] = a[i], ++i) ;
            return c;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            if (a.Length != b.Length) throw new System.Exception("Different vectors length");
            Vector c = new Vector(a.Length);
            for (int i = 0; i < c.Length; c[i] = a[i] + b[i], ++i) ;
            return c;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            if (a.Length != b.Length) throw new System.Exception("Different vectors length");
            Vector c = new Vector(a.Length);
            for (int i = 0; i < c.Length; c[i] = a[i] - b[i], ++i) ;
            return c;
        }
        public static Vector operator -(Vector a)
        {
            Vector c = new Vector(a.Length);
            for (int i = 0; i < c.Length; c[i] = -a[i], ++i) ;
            return c;
        }
        public static double operator *(Vector a, Vector b)
        {
            if (a.Length != b.Length) throw new System.Exception("Different vectors length");
            double res = 0.0;
            for (int i = 0; i < a.Length; res += a[i] * b[i], ++i) ;
            return res;
        }
        public static Vector operator *(double a, Vector b)
        {
            Vector c = new Vector(b.Length);
            for (int i = 0; i < b.Length; c[i] = a * b[i], ++i) ;
            return c;
        }
        public static Matrix operator ^(Vector a, Vector b)
        {
            Matrix res = new Matrix(a.Length);
            for (int i = 0; i < res.Dim; ++i)
            {
                for (int j = 0; j < res.Dim; ++j)
                {
                    res[i, j] = a[i] * b[j];
                }
            }
            return res;
        }
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)X).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)X).GetEnumerator();
        }
        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder("");
            for (int i = 0; i < X.Length; ++i) str.Append($"x[{i + 1}]: {X[i]:F9}\t");
            return str.ToString();
        }
    }
}
