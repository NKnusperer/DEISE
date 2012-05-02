using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Delta.InputSystem;
using System.Collections.ObjectModel;

namespace DEISE.Controls
{
    public partial class TriggerControl : UserControl
    {
        public ObservableCollection<Modifier> ModifierCollection = new ObservableCollection<Modifier>();

        public TriggerControl()
        {
            InitializeComponent();
            cbxTriggerButton.ItemsSource = Enum.GetNames(typeof(InputButton));
            cbxState.ItemsSource = Enum.GetNames(typeof(InputState));
            cbxModifierButtons.ItemsSource = Enum.GetNames(typeof(InputButton));
            lvModifierButtons.ItemsSource = ModifierCollection;
        }

        private void cbxModifierButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbxModifierButtons.SelectedItem;
            if (item != null)
            {
                ModifierCollection.Add(new Modifier() { Items = Enum.GetNames(typeof(InputButton)), Selected = item });
                cbxModifierButtons.SelectedItem = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var modifier = button.DataContext as Modifier;
            ModifierCollection.Remove(modifier);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbx = sender as ComboBox;
            var modifier = cbx.DataContext as Modifier;

            if (cbx.SelectedItem != null)
            {
                modifier.Selected = cbx.SelectedItem;
            }
        }
    }

    public class Modifier
    {
        public IEnumerable<object> Items { get; set; }
        public object Selected { get; set; }
    }
}
