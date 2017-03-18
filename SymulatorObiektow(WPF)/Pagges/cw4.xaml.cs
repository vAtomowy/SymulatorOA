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

namespace SymulatorObiektow_WPF_.Pagges
{
    /// <summary>
    /// Interaction logic for cw4.xaml
    /// </summary>
    public partial class cw4 : Page
    {

        int index = 0;
        List<string> imgs = new List<string>();

        public cw4()
        {
            InitializeComponent();

            imgs.Add("pack://siteoforigin:,,,/Resources/1.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/2.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/3.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/4.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/5.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/6.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/7.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/8.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/9.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/10.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/11.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/12.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/13.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/14.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/15.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/16.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/17.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/18.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/19.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/20.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/21.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/22.png");

        }
        public void changeBg(BitmapImage img)
        {
            Tasma.BeginInit();
            Tasma.Source = img;
            Tasma.EndInit();
        }

        public void nextBg()
        {
            if (index < imgs.Count() - 1)
                index++;
            else
                index = 0;

            changeBg(new BitmapImage(new Uri(imgs[index])));
        }

        private void b_Start_Click(object sender, RoutedEventArgs e)
        {
            nextBg();
        }
    }
}
