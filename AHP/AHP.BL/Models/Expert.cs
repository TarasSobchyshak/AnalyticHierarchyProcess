using System.Collections.Generic;
using System.Linq;

namespace AHP.BL.Models
{
    public class Expert : ObservableObject
    {
        private string _name;
        private double _weight;
        private string _imageKey;
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

        public Tree Tree
        {
            get { return _tree; }
            set
            {
                SetProperty(ref _tree, value);
            }
        }

        public double Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }

        public Matrix GlobalPriorityVector => GetGlobalPriorityVector();

        public Matrix GetGlobalPriorityVector()
        {
            if (Tree == null) return new Matrix(1);
            var trees = new List<Matrix>();
            for (int i = Tree.AlternativesLevel - 2; i >= 0; --i)
            {
                var vect = Tree.Criteria
                    .Where(c => c.Level == i + 1)
                    .Select(v => v.PCM.X.GetColumn(0))
                    .ToList();
                var temp = new Matrix(vect);

                trees.Add(temp);
            }

            for (int i = 0; i < trees.Count - 1; ++i)
            {
                trees[i + 1] = trees[i] * trees[i + 1];
            }

            var result = trees.Last() * Tree.Goal.PCM.X;
            return result;
        }


        public void RefreshGlobalPriorityVector()
        {
            RaisePropertyChanged(nameof(GlobalPriorityVector));
        }
    }
}
