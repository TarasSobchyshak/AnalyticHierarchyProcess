using AHP.BL.GraphModels;
using AHP.BL.Interfaces;
using AHP.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AHP.App
{
    public class MainWindowViewModel : ObservableObject
    {
        private bool _isGraphHidden;
        private string _layoutAlgorithmType;
        private Graph _graph;
        private List<string> layoutAlgorithmTypes;
        private ObservableCollection<Expert> _experts;
        private Expert _selectedExpert;
        private string _expertName;

        public string ExpertName
        {
            get { return _expertName; }
            set { SetProperty(ref _expertName, value); }
        }

        public Graph Graph
        {
            get { return _graph; }
            set { SetProperty(ref _graph, value); }
        }

        public ObservableCollection<Expert> Experts
        {
            get { return _experts; }
            set { SetProperty(ref _experts, value); }
        }

        public Expert SelectedExpert
        {
            get { return _selectedExpert; }
            set { SetProperty(ref _selectedExpert, value); }
        }

        public bool IsGraphHidden
        {
            get { return _isGraphHidden; }
            set { SetProperty(ref _isGraphHidden, value); }
        }

        public List<string> LayoutAlgorithmTypes
        {
            get { return layoutAlgorithmTypes; }
        }

        public string LayoutAlgorithmType
        {
            get { return _layoutAlgorithmType; }
            set { SetProperty(ref _layoutAlgorithmType, value); }
        }


        public MainWindowViewModel()
        {
            PropertyChanged += OnSelectedExpertChanged;
            layoutAlgorithmTypes = new List<string>();
            Graph = new Graph(true);
            Experts = new ObservableCollection<Expert>();
            Experts.Add(new Expert("Taras"));

            foreach (var x in Experts)
            {
                x.Tree = App.Tree;
            }



            //Add Layout Algorithm Types
            layoutAlgorithmTypes.Add("BoundedFR");
            layoutAlgorithmTypes.Add("Circular");
            layoutAlgorithmTypes.Add("CompoundFDP");
            layoutAlgorithmTypes.Add("EfficientSugiyama");
            layoutAlgorithmTypes.Add("FR");
            layoutAlgorithmTypes.Add("ISOM");
            layoutAlgorithmTypes.Add("KK");
            layoutAlgorithmTypes.Add("LinLog");
            layoutAlgorithmTypes.Add("Tree");

            //Pick a default Layout Algorithm Type
            LayoutAlgorithmType = "LinLog";
        }

        #region AddEdges
        private Edge AddNewGraphEdges(IVertex from, IVertex to)
        {
            var x = new Edge(from, to);
            Graph.AddEdge(x);
            return x;
        }

        private IEnumerable<Edge> AddNewGraphEdges(IVertex from, IEnumerable<IVertex> to)
        {
            List<Edge> res = new List<Edge>();
            Edge x;
            foreach (var t in to)
            {
                x = new Edge(from, t);
                res.Add(x);
                Graph.AddEdge(x);
            }
            return res;
        }


        private IEnumerable<Edge> AddNewGraphEdges(IEnumerable<IVertex> from, IVertex to)
        {
            List<Edge> res = new List<Edge>();
            Edge x;
            foreach (var f in from)
            {
                x = new Edge(f, to);
                res.Add(x);
                Graph.AddEdge(x);
            }
            return res;
        }

        private IEnumerable<Edge> AddNewGraphEdges(IEnumerable<IVertex> from, IEnumerable<IVertex> to)
        {
            List<Edge> res = new List<Edge>();
            Edge x;
            foreach (var f in from)
            {
                foreach (var t in to)
                {
                    x = new Edge(f, t);
                    res.Add(x);
                    Graph.AddEdge(x);
                }
            }
            return res;
        }
        #endregion

        #region Commands

        public ICommand SaveTreeCommand => new DelegateCommand(new Action(() => SaveTree()));
        public ICommand SaveExpertCommand => new DelegateCommand(new Action(() => SaveExpert()));
        public ICommand LoadTreeCommand => new DelegateCommand(new Action(() => LoadTree()));
        public ICommand LoadExpertCommand => new DelegateCommand(new Action(() => LoadExpert()));
        public ICommand LoadExpertSCommand => new DelegateCommand(new Action(() => LoadExperts()));

        private void OnSelectedExpertChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedExpert) || SelectedExpert == null) return;
			Graph = new Graph(true);
            
            Graph.AddVertex(SelectedExpert.Tree.Goal);
            Graph.AddVertexRange(SelectedExpert.Tree.Criteria);
            Graph.AddVertexRange(SelectedExpert.Tree.Alternatives);

            AddNewGraphEdges(SelectedExpert.Tree.Goal, SelectedExpert.Tree.Criteria.Where(x => x.Level == 1));
            AddNewGraphEdges(SelectedExpert.Tree.Criteria.Where(x => x.Level == SelectedExpert.Tree.AlternativesLevel - 1), SelectedExpert.Tree.Alternatives);

            for (int i = 1; i < SelectedExpert.Tree.AlternativesLevel - 1; ++i)
            {
                AddNewGraphEdges(SelectedExpert.Tree.Criteria.Where(x => x.Level == i), SelectedExpert.Tree.Criteria.Where(x => x.Level == i + 1));
            }

            RaisePropertyChanged(nameof(Graph));
        }

        private void SaveTree()
        {
            if (SelectedExpert != null)
                App.SaveTree(SelectedExpert.Name + "Tree", SelectedExpert.Tree);
        }

        private void LoadTree()
        {
            if (SelectedExpert != null)
                SelectedExpert.Tree = App.LoadTree(SelectedExpert.Name + "Tree");
        }

        private void SaveExpert()
        {
            if (SelectedExpert != null)
                App.SaveExpert(SelectedExpert);
        }

        private void LoadExpert()
        {
            var x = App.LoadExpert(ExpertName);
            if (!Experts.Contains(x))
                Experts.Add(App.LoadExpert(ExpertName));
        }


        private void LoadExperts()
        {
            Experts.Clear();
            var x = App.LoadExperts();
            foreach (var exp in x)
            {
                Experts.Add(exp);
            }
        }

        #endregion
    }
}