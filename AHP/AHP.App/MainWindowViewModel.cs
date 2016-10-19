﻿using AHP.BL.GraphModels;
using AHP.BL.Interfaces;
using AHP.BL.Models;
using GraphSharp.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            layoutAlgorithmTypes = new List<string>();
            Graph = new Graph(true);
            Experts = new ObservableCollection<Expert>();
            Experts.Add(new Expert("Taras"));

            foreach (var x in Experts)
            {
                x.Tree = App.Tree;
            }

            SelectedExpert = Experts[0];

            Graph.AddVertex(SelectedExpert.Tree.Goal); // replace SelectedExpert with selectedExpert.Tree (from combobox) in private method
            Graph.AddVertexRange(SelectedExpert.Tree.Criteria);
            Graph.AddVertexRange(SelectedExpert.Tree.Alternatives);

            AddNewGraphEdges(SelectedExpert.Tree.Goal, SelectedExpert.Tree.Criteria.Where(x => x.Level == 1));
            AddNewGraphEdges(SelectedExpert.Tree.Criteria.Where(x => x.Level == SelectedExpert.Tree.AlternativesLevel - 1), SelectedExpert.Tree.Alternatives);

            for (int i = 1; i < SelectedExpert.Tree.AlternativesLevel - 1; ++i)
            {
                AddNewGraphEdges(SelectedExpert.Tree.Criteria.Where(x => x.Level == i), SelectedExpert.Tree.Criteria.Where(x => x.Level == i + 1));
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
            RaisePropertyChanged(nameof(Graph));
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
            RaisePropertyChanged(nameof(Graph));
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
            RaisePropertyChanged(nameof(Graph));
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
            RaisePropertyChanged(nameof(Graph));
            return res;
        }
        #endregion
    }
}