using static AHP.BL.Models.Vector;

namespace AHP.BL.Models
{
    public class PairwiseComparisonMatrix : ObservableObject
    {
        private Matrix _m;
        private Vector _x;
        private int _index;
        private int _level;

        public PairwiseComparisonMatrix(Matrix m, int index, int level)
        {
            Index = index;
            Level = level;
            M = m;
            _x = GetLocalPriorityVector(M);
        }

        public Matrix M
        {
            get { return _m; }
            set { SetProperty(ref _m, value); }
        }
        public Vector X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
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
            }
        }
        public double this[int i]
        {
            get { return _x[i]; }
            set
            {
                X[i] = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        public static Vector GetLocalPriorityVector(Matrix a)
        {
            double[] x = new double[a.M];
            for(int i = 0; i < a.N; ++i)
            {
                x[i] = GeometricMean(a.GetRow(i));
            }
            return new Vector(x);
        }
    }
}
