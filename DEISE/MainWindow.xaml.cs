using System.Windows;
using System.Windows.Input;
using System.IO;

namespace DEISE
{
    public partial class MainWindow : Window
    {
        private bool doAddCommand { get; set; }
        private bool doAddTrigger { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddCommand_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            doAddCommand = true;
        }

        private void btnAddTrigger_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            doAddTrigger = true;
        }

        private void designArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (doAddCommand || doAddTrigger)
            {
                var position = e.GetPosition(null);
                DesignerItem item;
                if (doAddCommand)
                {
                    item = DesignerItemFactory.CreateItem(ItemType.Command);
                }
                else
                {
                    item = DesignerItemFactory.CreateItem(ItemType.Trigger);

                }
                DesignerCanvas.SetLeft(item, position.X);
                DesignerCanvas.SetTop(item, position.Y);
                designArea.Children.Add(item);

                doAddCommand = false;
                doAddTrigger = false;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void btnGenerateXml_Click(object sender, RoutedEventArgs e)
        {
            var graph = DependencyBuilder.GetGraph(designArea);
            var xml = XmlBuilder.BuildInputSettings(graph);

            tbxXml.Text = xml.ToString();
            tabControl.SelectedIndex = 1;
        }
    }
}
