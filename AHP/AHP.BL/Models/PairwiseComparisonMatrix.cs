using static AHP.BL.Models.Vector;
using static AHP.BL.Models.Matrix;
using System.Windows.Input;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace AHP.BL.Models
{
    public class PairwiseComparisonMatrix : ObservableObject
    {
        private Matrix _m;
        private Matrix _x;
        private int _level;
        private double _index;

        [JsonIgnore]
        public ICommand RefVector
        {
            get { return new DelegateCommand(new Action(() => RefreshLocalPriorityVector())); }
        }
        public PairwiseComparisonMatrix()
        {

        }
        public PairwiseComparisonMatrix(Matrix m, int level)
        {
            Level = level;
            M = m;
            RefreshLocalPriorityVector();

        }
        public Matrix X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }
        public Matrix M
        {
            get { return _m; }
            set { SetProperty(ref _m, value); }
        }
        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        public double Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        public double this[int i, int j]
        {
            get { return _m[i, j]; }
            set
            {
                M[i, j] = value;
                RaisePropertyChanged(nameof(M));
                RefreshLocalPriorityVector();
            }
        }
        public double this[int i]
        {
            get { return X[i, 1]; }
            set
            {
                X[i, 1] = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        public static Vector GetLocalPriorityVector(Matrix a)
        {
            double[] x = new double[a.N];
            double m = GeometricMean(a);
            for (int i = 0; i < a.N; ++i)
            {
                x[i] = GeometricMean(a.GetRow(i)) / m;
            }
            return new Vector(x);
        }

        public void RefreshLocalPriorityVector()
        {
            for (int i = 0; i < M.N; ++i)
            {
                for (int j = i; j < M.M; ++j)
                {
                    M[i, j] = i == j ? 1 : 1.0 / M[j, i];
                }
            }

            X = new Matrix(GetLocalPriorityVector(M));

            double lam = 0.0;
            for (int i = 0; i < X.GetColumn(0).Length; ++i)
            {
                lam += X.GetColumn(0)[i] * M.GetColumn(i).X.Sum();
            }
            Index = (lam - M.N) / (M.N - 1);
        }
    }
}
