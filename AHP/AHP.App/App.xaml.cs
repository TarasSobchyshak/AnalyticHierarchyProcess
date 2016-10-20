using AHP.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
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
			var settings = new Dictionary<string, object>();
			settings.Add(key, JsonConvert.SerializeObject(tree));
			BinaryFormatter formatter = new BinaryFormatter();
			var store = IsolatedStorageFile.GetUserStoreForAssembly();

			// Save
			using (var stream = store.OpenFile("settings.cfg", FileMode.OpenOrCreate, FileAccess.Write))
			{
				formatter.Serialize(stream, settings);
			}
		}

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


        public static Tree LoadTree(string key)
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
				return JsonConvert.DeserializeObject<Tree>((string)settings[key]);
			}
			catch (Exception ex)
			{
				return null;
			}
		}

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
			catch (Exception ex)
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
			foreach(var exp in settings)
			{
				builder.Append((string)(exp.Value));
				builder.Append(',');
			}
			builder.Remove(builder.Length - 1, 1);
			builder.Append(']');
			return JsonConvert.DeserializeObject<IEnumerable<Expert>>(builder.ToString());
		}
    }
}
