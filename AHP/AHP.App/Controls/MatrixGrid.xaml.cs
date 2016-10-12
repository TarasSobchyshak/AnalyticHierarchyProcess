using AHP.App.DependencyPropertyExtensions;
using System.Windows;
using System.Windows.Controls;

namespace AHP.App.Controls
{
    public partial class MatrixGrid : UserControl
    {
        public MatrixGrid()
        {
            InitializeComponent();
        }

        public double[,] Matrix
        {
            get { return (double[,])GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public static readonly DependencyProperty MatrixProperty = DependencyProperty<MatrixGrid>
            .Register(x => x.Matrix,
                      new double[1, 1] { { 1 } },
                      x => x.OnMatrixPropertyChanged);

        private void OnMatrixPropertyChanged(DependencyPropertyChangedEventArgs<double[,]> e)
        {
        }
    }
}
