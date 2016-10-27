using AHP.App.DependencyPropertyExtensions;
using AHP.BL.Models;
using System.Windows;
using System.Windows.Controls;


namespace AHP.App.Controls
{
    public partial class StrMatrixControl : UserControl
    {
        public StrMatrixControl()
        {
            InitializeComponent();
        }

        public Matrix Matrix
        {
            get { return (Matrix)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public static readonly DependencyProperty MatrixProperty = DependencyProperty<StrMatrixControl>
            .Register(x => x.Matrix,
                      new Matrix(""),
                      x => x.OnMatrixPropertyChanged);

        private void OnMatrixPropertyChanged(DependencyPropertyChangedEventArgs<Matrix> e)
        {
        }
    }
}
