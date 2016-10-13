using static AHP.BL.Models.Vector;
using static AHP.BL.Models.Matrix;

namespace AHP.BL.Models
{
    public class PairwiseComparisonMatrix : ObservableObject
    {
        private Matrix _m;
        private Matrix _x;
        private int _index;
        private int _level;

        public PairwiseComparisonMatrix(Matrix m, int index, int level)
        {
            Index = index;
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
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }
        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
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
            X = new Matrix(GetLocalPriorityVector(M));
        }
    }
}
