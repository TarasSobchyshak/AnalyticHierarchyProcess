using AHP.BL.Models;

namespace AHP.App
{
    public class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            MyProperty = Matrix.IdentityMatrix(5);
        }

        private Matrix _myProperty;

        public Matrix MyProperty
        {
            get { return _myProperty; }
            set { SetProperty(ref _myProperty, value); }
        }

    }
}
