using AHP.BL.Models;
using System.Collections.Generic;

namespace AHP.App
{
    public class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            MyProperty = Matrix.IdentityMatrix(5);
            Expert = new Expert("Shaurma",
                                new System.Collections.ObjectModel.ObservableCollection<PairwiseComparisonMatrix>(
                                    new List<PairwiseComparisonMatrix>()
                                    {
                                        new PairwiseComparisonMatrix(new BL.Models.Matrix() { A = new double[,]
                                                                                                {
                                                                                                    {2,1,1,1 },
                                                                                                    {1,1,1,1 },
                                                                                                    {1,1,1,1 }
                                                                                                }
                                                                                            },
                                                                     1,
                                                                     2),
                                        new PairwiseComparisonMatrix(new BL.Models.Matrix() { A = new double[,]
                                                                                                {
                                                                                                    {212,17,91,1 },
                                                                                                    {21,17,16,1 },
                                                                                                    {21,17,16,1 },
                                                                                                    {21,17,16,1 },
                                                                                                    {21,17,16,1 },
                                                                                                    {51,51,41,1 }
                                                                                                }
                                                                                            },
                                                                     1,
                                                                     1),
                                        new PairwiseComparisonMatrix(new BL.Models.Matrix() { A = new double[,]
                                                                                                {
                                                                                                    {2,2,2,2,1,1,13 },
                                                                                                    {1,34,76,1,56,1,1 },
                                                                                                    {23,65,1,1,871,41,1 }
                                                                                                }
                                                                                            },
                                                                     2,
                                                                     1),

                                    }),
                                new PairwiseComparisonMatrix(BL.Models.Matrix.IdentityMatrix(7), 1, 1),
                                0.74);
        }

        private Matrix _myProperty;

        public Matrix MyProperty
        {
            get { return _myProperty; }
            set { SetProperty(ref _myProperty, value); }
        }

        private Expert _expert;

        public Expert Expert
        {
            get { return _expert; }
            set { SetProperty(ref _expert, value); }
        }
    }
}
