namespace AHP.BL.Models
{
    public class Alternative: ObservableObject
    {
        private int _index;
        private double _weight;

        public Alternative(int index, int level, double weight)
        {
            _index = index;
            _weight = weight;
        }

        public int Value
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }
        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
    }
}
