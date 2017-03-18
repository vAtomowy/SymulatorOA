using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymulatorObiektow_WPF_.Controls
{
    /// <summary>
    /// Interaction logic for TestButton.xaml
    /// </summary>
    public partial class TestButton : UserControl
    {
        public TestButton()
        {
            InitializeComponent();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            TB.BorderBrush = Brushes.Transparent;
            ImageBrush ib = new ImageBrush();

            ib.ImageSource = Tools.BitmapToBitmapImage(Properties.Resources.g2994_clicked);

            TB.Background = ib;
        }
    }
}
