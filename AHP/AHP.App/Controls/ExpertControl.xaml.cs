using AHP.App.DependencyPropertyExtensions;
using AHP.BL.Models;
using System.Windows;
using System.Windows.Controls;

namespace AHP.App.Controls
{
    /// <summary>
    /// Interaction logic for ExpertControl.xaml
    /// </summary>
    public partial class ExpertControl : UserControl
    {
        public ExpertControl()
        {
            InitializeComponent();
        }

        public Expert Expert
        {
            get { return (Expert)GetValue(ExpertProperty); }
            set { SetValue(ExpertProperty, value); }
        }

        public static readonly DependencyProperty ExpertProperty = DependencyProperty<ExpertControl>
            .Register(x => x.Expert, new Expert("Unnamed", null, null, 0), x => x.OnExpertPropertyChanged);

        private void OnExpertPropertyChanged(DependencyPropertyChangedEventArgs<Expert> e)
        {
        }
    }
}
