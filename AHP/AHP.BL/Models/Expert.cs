using System.Collections.Generic;
using System.Linq;

namespace AHP.BL.Models
{
    public class Expert : ObservableObject
    {
        private string _name;
        private double _weight;
        private string _imageKey;
        private PairwiseComparisonMatrix _pcm;
        private Tree _tree;

        public Expert(string name, double weight = 1.0)
        {
            _name = name;
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

        public Tree Tree
        {
            get { return _tree; }
            set
            {
                SetProperty(ref _tree, value);
                InitPCM();
            }
        }

        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        public Vector GetGlobalProorityVector()
        {
            return PCM == null ? null : PCM.M * PCM.X.GetColumn(0);
        }

        private void InitPCM()
        {
            PCM = new PairwiseComparisonMatrix() { X = Tree.Goal.PCM.X, M = new Matrix(new List<Vector>(Tree.Criteria.Select(c => c.PCM.X.GetColumn(0)))) };
        }
    }
}
