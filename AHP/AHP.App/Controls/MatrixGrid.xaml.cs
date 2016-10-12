using AHP.App.DependencyPropertyExtensions;
using AHP.BL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AHP.BL.Models.Matrix;

namespace AHP.App.Controls
{
    /// <summary>
    /// Interaction logic for MatrixGrid.xaml
    /// </summary>
    public partial class MatrixGrid : UserControl, INotifyPropertyChanged
    {
        private DataTable _table;
        public MatrixGrid()
        {
            InitializeComponent();
            _table = new DataTable();
            Binding b = new Binding("Table")
            {
                Source = this
            };
            dataGrid.SetBinding(DataGrid.ItemsSourceProperty, b);

            Matrix = IdentityMatrix(8);
        }

        public Matrix Matrix
        {
            get { return (Matrix)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public int M
        {
            get { return (int)GetValue(MProperty); }
            set { SetValue(MProperty, value); }
        }
        public int N
        {
            get { return (int)GetValue(MProperty); }
            set { SetValue(MProperty, value); }
        }
        public DataTable Table
        {
            get { return _table; }
            set { SetProperty(ref _table, value); }
        }

        public static readonly DependencyProperty MProperty =
            DependencyProperty<MatrixGrid>.Register(x => x.M, 1, x => x.OnMPropertyChanged);

        public static readonly DependencyProperty NProperty =
            DependencyProperty<MatrixGrid>.Register(x => x.N, 1, x => x.OnNPropertyChanged);

        public static readonly DependencyProperty MatrixProperty =
            DependencyProperty<MatrixGrid>.Register(x => x.Matrix, IdentityMatrix(1, 1), x => x.OnMatrixPropertyChanged);

        private void OnMatrixPropertyChanged(DependencyPropertyChangedEventArgs<Matrix> e)
        {
            MatrixToGrid();
        }

        private void OnMPropertyChanged(DependencyPropertyChangedEventArgs<int> e)
        {
        }

        private void OnNPropertyChanged(DependencyPropertyChangedEventArgs<int> e)
        {
        }

        private void MatrixToGrid()
        {
            dataGrid.CanUserAddRows = true;
            _table = new DataTable();
            for (int i = 0; i < Matrix.M; ++i) { Table.Columns.Add(""); Table.Columns[i].DataType = typeof(double); } 

            for (int i = 0; i < Matrix.N; ++i)
            {
                var row = Table.NewRow();
                Table.Rows.Add(row);
                Table.Rows[i].ItemArray = Matrix.GetRow(i).Select(x => (object)x).ToArray();
            }
            dataGrid.ItemsSource = Table.DefaultView;
            dataGrid.CanUserAddRows = false;
        }

        private void GridToMatrix()
        {
            for (int i = 0; i < Table.Rows.Count; ++i)
            {
                for (int j = 0; j < Table.Columns.Count; ++j)
                {
                    Matrix[i, j] = (double)Table.Rows[i].ItemArray[j];
                }
            }
        }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            var changed = !EqualityComparer<T>.Default.Equals(backingField, value);

            if (changed)
            {
                backingField = value;
                RaisePropertyChanged(propertyName);
            }

            return changed;
        }
        #endregion

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            GridToMatrix();
        }
    }
}
