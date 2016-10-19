using AHP.BL.Models;
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

                return tree;
            }
        }


    }
}
