using AHP.BL.Interfaces;

namespace AHP.BL.Models
{
    public class Alternative : ObservableObject, IVertex
    {
        private int _level;
        private string _value;
        private double _weight;
        private PairwiseComparisonMatrix _pcm;
        private double _index;

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

        public PairwiseComparisonMatrix PCM
        {
            get { return _pcm; }
            set
            {
                SetProperty(ref _pcm, value);
                if (_pcm != null)
                {
                    _pcm.RefreshLocalPriorityVector();
                    Index = _pcm.Index;
                }
                else Index = -1;
            }
        }

        public double Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }
    }
}
