using AHP.BL.Interfaces;

namespace AHP.BL.Models
{
    public class Goal : ObservableObject, IVertex
    {
        private string _value;
        private double _weight;

        public Goal(string value)
        {
            _value = value;
            Weight = 1.0;
        }

        public int Level => 0;

        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
    }
}
