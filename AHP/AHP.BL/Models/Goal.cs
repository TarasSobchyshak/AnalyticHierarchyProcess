using System;
using AHP.BL.Interfaces;

namespace AHP.BL.Models
{
    public class Goal : ObservableObject, IVertex
    {
        private string _value;
        private double _weight;
        private PairwiseComparisonMatrix _pcm;
        private double _index;

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
        public PairwiseComparisonMatrix PCM
        {
            get { return _pcm; }
            set
            {
                SetProperty(ref _pcm, value);
                _pcm.RefreshLocalPriorityVector();
                Index = _pcm == null ? -1 : _pcm.Index;
            }
        }

        public double Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }
    }
}
