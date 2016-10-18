using AHP.App.DependencyPropertyExtensions;
using AHP.BL.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Globalization;

namespace AHP.App.Controls
{
    public partial class MatrixGrid : UserControl
    {
        public MatrixGrid()
        {
            InitializeComponent();
        }

        public Matrix Matrix
        {
            get { return (Matrix)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public static readonly DependencyProperty MatrixProperty = DependencyProperty<MatrixGrid>
            .Register(x => x.Matrix,
                      Matrix.IdentityMatrix(1),
                      x => x.OnMatrixPropertyChanged);

        private void OnMatrixPropertyChanged(DependencyPropertyChangedEventArgs<Matrix> e)
        {
        }
    }
}
