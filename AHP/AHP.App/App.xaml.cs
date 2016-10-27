using AHP.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
                  new Goal() { Value = "Вибір теми" },
                  new List<Criterion>()
                  {
                        new Criterion(){ Value = "Актуальність", Level = 1 },
                        new Criterion(){ Value = "Оригінальність", Level = 1 },
                        new Criterion(){ Value = "Перспективність", Level = 1 },
                        new Criterion(){ Value = "Ефективність", Level = 1 },
                        new Criterion(){ Value = "Популярність", Level = 2 },
                        new Criterion(){ Value = "Складність", Level = 2 },
                  }
              );
                tree.Alternatives.Add(new Alternative() { Value = "Розмальовка писанок", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Вибір технології", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Вибір машини", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Вибір книги", Level = tree.AlternativesLevel });
                tree.Alternatives.Add(new Alternative() { Value = "Вибір чогось", Level = tree.AlternativesLevel });

                tree.Goal.PCM = new PairwiseComparisonMatrix(new Matrix(tree.Criteria.Where(c => c.Level == 1).Count()), tree.Goal.Level);
                for (int i = 0; i < tree.Criteria.Count; ++i)
                {
                    int n;
                    if (tree.Criteria[i].Level + 1 == tree.AlternativesLevel)
                        tree.Criteria[i].PCM = new PairwiseComparisonMatrix(Matrix.IdentityMatrix(tree.Alternatives.Count()), tree.AlternativesLevel);
                    else
                    {
                        n = tree.Criteria.Where(t => t.Level == tree.Criteria[i].Level + 1).Count();
                        tree.Criteria[i].PCM = new PairwiseComparisonMatrix(Matrix.IdentityMatrix(n), i + 1);
                    }
                }

                return tree;
            }
        }

        //public static void SaveTree(string key, Tree tree)
        //{
        //    var settings = new Dictionary<string, object>();
        //    settings.Add(key, JsonConvert.SerializeObject(tree));
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    var store = IsolatedStorageFile.GetUserStoreForAssembly();

        //    // Save
        //    using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Write))
        //    {
        //        formatter.Serialize(stream, settings);
        //    }
        //}

        public static void SaveExpert(Expert expert)
        {
            string key = expert.Name;
            var settings = new Dictionary<string, object>();
            settings.Add(key, JsonConvert.SerializeObject(expert));
            BinaryFormatter formatter = new BinaryFormatter();
            var store = IsolatedStorageFile.GetUserStoreForAssembly();

            // Save
            using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(stream, settings);
            }
        }

        public static void SaveExperts(IEnumerable<Expert> experts)
        {
			var settings = new Dictionary<string, object>();
			BinaryFormatter formatter = new BinaryFormatter();
			var store = IsolatedStorageFile.GetUserStoreForAssembly();
			// Load
			using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Read))
			{
				var x = formatter.Deserialize(stream);
				settings = (Dictionary<string, object>)x;
			}
			foreach(var exp in experts)
			{
				if (settings.ContainsKey(exp.Name)) settings.Remove(exp.Name);
				settings.Add(exp.Name, JsonConvert.SerializeObject(exp));
			}

			using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Write))
			{
				formatter.Serialize(stream, settings);
			}
		}
        
        //public static Tree LoadTree(string key)
        //{
        //    var settings = new Dictionary<string, object>();
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    var store = IsolatedStorageFile.GetUserStoreForAssembly();
        //    // Load
        //    using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        var x = formatter.Deserialize(stream);
        //        settings = (Dictionary<string, object>)x;
        //    }
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<Tree>((string)settings[key]);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public static Expert LoadExpert(string key)
        {
            var settings = new Dictionary<string, object>();
            BinaryFormatter formatter = new BinaryFormatter();
            var store = IsolatedStorageFile.GetUserStoreForAssembly();
            // Load
            using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var x = formatter.Deserialize(stream);
                settings = (Dictionary<string, object>)x;
            }
            try
            {
                return JsonConvert.DeserializeObject<Expert>((string)settings[key]);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<Expert> LoadExperts()
        {
            var settings = new Dictionary<string, object>();
            BinaryFormatter formatter = new BinaryFormatter();
            var store = IsolatedStorageFile.GetUserStoreForAssembly();
            // Load
            using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Read))
            {
                var x = formatter.Deserialize(stream);
                settings = (Dictionary<string, object>)x;
            }
            var builder = new StringBuilder("[");
            foreach (var exp in settings)
            {
                builder.Append((string)(exp.Value));
                builder.Append(',');
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(']');
            var q = JsonConvert.DeserializeObject<IEnumerable<Expert>>(builder.ToString());
            var ex = new ObservableCollection<Expert>(
                q.Select(c => new Expert(c.Name)
                {
                    Tree = c.Tree,
                    ImageKey = c.ImageKey,
                    Weight = c.Weight
                }));
            return ex;
        }
    }
}
