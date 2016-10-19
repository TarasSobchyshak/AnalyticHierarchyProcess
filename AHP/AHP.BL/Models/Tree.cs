using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AHP.BL.Models
{
    public class Tree : ObservableObject
    {
        private Goal _goal;
        private ObservableCollection<Criterion> _criteria;
        private ObservableCollection<Alternative> _alternatives;

		public Tree(Goal goal = null, IEnumerable<Criterion> criteria = null, IEnumerable<Alternative> alternatives = null)
		{
			_goal = goal;
			_criteria = criteria == null ? new ObservableCollection<Criterion>() : new ObservableCollection<Criterion>(criteria);
			_alternatives = alternatives == null ? new ObservableCollection<Alternative>() : new ObservableCollection<Alternative>(alternatives);
		}

        public int AlternativesLevel => Criteria.Max(x => x.Level) + 1;

        public Goal Goal
        {
            get { return _goal; }
            set { SetProperty(ref _goal, value); }
        }

        public ObservableCollection<Criterion> Criteria
        {
            get { return _criteria; }
            set { SetProperty(ref _criteria, value); }
        }

        public ObservableCollection<Alternative> Alternatives
        {
            get { return _alternatives; }
            set { SetProperty(ref _alternatives, value); }
        }
    }
}
