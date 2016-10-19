using AHP.BL.GraphModels;
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

            Graph.AddVertex(Experts[0].Tree.Goal); // replace Experts[0] with selectedExpert.Tree (from combobox) in private method
            Graph.AddVertexRange(Experts[0].Tree.Criteria);
            Graph.AddVertexRange(Experts[0].Tree.Alternatives);

            AddNewGraphEdges(Experts[0].Tree.Goal, Experts[0].Tree.Criteria.Where(x => x.Level == 1));
            AddNewGraphEdges(Experts[0].Tree.Criteria.Where(x => x.Level == Experts[0].Tree.AlternativesLevel - 1), Experts[0].Tree.Alternatives);

            // Join criteria


            //var splitted = criteria
            //    .GroupBy(x => x)
            //    .SelectMany(x => x
            //                        .OrderBy(y => y.Value)
            //                        .Select((y, i) => new Сriterion(y.Value, y.Level -i, y.Weight))
            //                        .GroupBy(y => y.Level)
            //                        .Select(y => y.Select(z => z))
            //    );
            //int n = splitted.Count() - 1;
            //for (int i = 0; i < n; ++i)
            //{
            //    var a = splitted.ElementAt(i);
            //    var b = splitted.ElementAt(i + 1);
            //    AddNewGraphEdges(a, b);
            //}


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






//MyProperty = Matrix.IdentityMatrix(5);
//Expert = new Expert("Shaurma",
//                    new System.Collections.ObjectModel.ObservableCollection<PairwiseComparisonMatrix>(
//                        new List<PairwiseComparisonMatrix>()
//                        {
//                            new PairwiseComparisonMatrix(new BL.Models.Matrix() { A = new double[,]
//                                                                                    {
//                                                                                        {2,1,1,1 },
//                                                                                        {1,1,1,1 },
//                                                                                        {1,1,1,1 }
//                                                                                    }
//                                                                                },
//                                                         1,
//                                                         2),
//                            new PairwiseComparisonMatrix(new BL.Models.Matrix() { A = new double[,]
//                                                                                    {
//                                                                                        {212,17,91,1 },
//                                                                                        {21,17,16,1 },
//                                                                                        {21,17,16,1 },
//                                                                                        {21,17,16,1 },
//                                                                                        {21,17,16,1 },
//                                                                                        {51,51,41,1 }
//                                                                                    }
//                                                                                },
//                                                         1,
//                                                         1),
//                            new PairwiseComparisonMatrix(new BL.Models.Matrix() { A = new double[,]
//                                                                                    {
//                                                                                        {2,2,2,2,1,1,13 },
//                                                                                        {1,34,76,1,56,1,1 },
//                                                                                        {23,65,1,1,871,41,1 }
//                                                                                    }
//                                                                                },
//                                                         2,
//                                                         1),

//                        }),
//                    new PairwiseComparisonMatrix(BL.Models.Matrix.IdentityMatrix(7), 1, 1),
//                    0.74);