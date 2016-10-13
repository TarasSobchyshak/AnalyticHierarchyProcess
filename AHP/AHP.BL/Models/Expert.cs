using System.Collections.ObjectModel;

namespace AHP.BL.Models
{
    public class Expert : ObservableObject
    {
        private string _name;
        private string _imageKey;
        private ObservableCollection<PairwiseComparisonMatrix> _cpcm;
        private PairwiseComparisonMatrix _pcm;
        private double _weight;

        public Expert(string name, ObservableCollection<PairwiseComparisonMatrix> cpcm, PairwiseComparisonMatrix pcm, double weight = 1.0)
        {
            _name = name;
            _cpcm = cpcm;
            _pcm = pcm;
            _weight = weight;
            _imageKey = "/Images/ImageDefault.png";
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string ImageKey
        {
            get { return _imageKey; }
            set { SetProperty(ref _imageKey, value); }
        }

        public PairwiseComparisonMatrix PCM
        {
            get { return _pcm; }
            set { SetProperty(ref _pcm, value); }
        }
        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
        public ObservableCollection<PairwiseComparisonMatrix> CPCM
        {
            get { return _cpcm; }
            set { SetProperty(ref _cpcm, value); }
        }
    }
}
