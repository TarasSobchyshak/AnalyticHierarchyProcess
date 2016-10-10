namespace AHP.BL.Models
{
    public class Сriterion : ObservableObject
    {
        private int _index;
        private int _level;
        private double _weight;

        public Сriterion(int index, int level, double weight)
        {
            _index = index;
            _level = level;
            _weight = weight;
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
        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
    }
}
