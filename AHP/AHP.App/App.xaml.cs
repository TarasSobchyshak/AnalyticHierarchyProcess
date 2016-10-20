﻿using AHP.BL.Models;
using System.Collections.Generic;
using System.Windows;

namespace AHP.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Tree Tree
        {
            get
            {
                var tree = new Tree(
                    new Goal() { Value = "Goal" },
                    new List<Criterion>()
                    {
                        new Criterion(){ Value = "Criterion 1", Level = 1 },
                        new Criterion(){ Value = "Criterion 2", Level = 2 },
                        new Criterion(){ Value = "Criterion 4", Level = 1 },
                        new Criterion(){ Value = "Criterion 6", Level = 2 },
                        new Criterion(){ Value = "Criterion 3", Level = 4 },
                        new Criterion(){ Value = "Criterion 5", Level = 3 },
                        new Criterion(){ Value = "Criterion 7", Level = 1 }
                    }
                );
                tree.Alternatives.Add(new Alternative() { Value = "Alternative 1", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Alternative 2", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Alternative 3", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Alternative 4", Level = tree.AlternativesLevel });

                tree.Goal.PCM = new PairwiseComparisonMatrix(new Matrix(tree.Criteria.Count), tree.Goal.Level);
                for (int i = 0; i < tree.Criteria.Count; ++i)
                {
                    tree.Criteria[i].PCM = new PairwiseComparisonMatrix(Matrix.IdentityMatrix(tree.Alternatives.Count), tree.Goal.Level);
                }

                return tree;
            }
        }

        public static void SaveTree(string key, Tree tree)
        {
            // iso storage 
        }

        public static void SaveExpert(Expert expert)
        {
            // iso storage 
            string key = expert.Name;
        }


        public static Tree LoadTree(string key)
        {
            // iso storage 
            return null;
        }

        public static Expert LoadExpert(string key)
        {
            // iso storage 
            return null;
        }

        public static IEnumerable<Expert> LoadExperts(string[] keys)
        {
            // iso storage 
            return null;
        }
    }
}
