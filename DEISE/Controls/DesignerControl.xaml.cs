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
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace DEISE.Controls
{
    public partial class DesignerControl : UserControl
    {
        public string Titel
        {
            get { return (string)GetValue(TitelProperty); }
            set { SetValue(TitelProperty, value); }
        }
        public static readonly DependencyProperty TitelProperty = DependencyProperty.Register("Titel", typeof(string), typeof(DesignerControl));


        public DesignerControl()
        {
            InitializeComponent();
        }
    }
}
