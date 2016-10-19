using AHP.BL.Interfaces;

namespace AHP.BL.Models
{
    public class Сriterion : ObservableObject, IVertex
    {
        private int _level;
        private string _value;
        private double _weight;

        public Сriterion(string value, int level = 1, double weight = 0.0)
        {
            _level = level;
            _weight = weight;
            _value = value;
        }

        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

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
