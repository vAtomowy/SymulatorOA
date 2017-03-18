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
    /// Interaction logic for cw3.xaml
    /// </summary>
    public partial class cw3 : Page
    {
        List<string> imgs = new List<string>();
        int index = 0;

        public cw3()
        {
            InitializeComponent();
            imgs.Add("pack://siteoforigin:,,,/Resources/D.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DD[1].png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DD[2]-4.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DD[2]-D.png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DD[3].png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DG[1].png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DG[2].png");
            imgs.Add("pack://siteoforigin:,,,/Resources/DG[3].png");
            imgs.Add("pack://siteoforigin:,,,/Resources/G[opcja1].png");
            imgs.Add("pack://siteoforigin:,,,/Resources/G[opcja2].png");
        }

        public void changeBg(BitmapImage img)
        {
            //this.Background = new ImageBrush(Tools.BitmapToBitmapImage((System.Drawing.Bitmap)imgs[3]));
            // ImageBrush bg = new ImageBrush(img);
            //bg.ImageSource = img;

            // this.Background = bg;
            // Container.Background = Brushes.AliceBlue;
            // Console.WriteLine("Zmieniono tło");
            // Container.Background = bg;

            BG_Image.BeginInit();
            BG_Image.Source = img;
            BG_Image.EndInit();

        }

        public void nextBg()
        {
            // string bg_str = this.Background;
            //Console.WriteLine(bg_str);
            // imgBG = Tools.BitmapToBitmapImage(Properties.Resources.DD_1_);
            if (index < imgs.Count() - 1)
                index++;
            else
                index = 0;

            changeBg(new BitmapImage(new Uri(imgs[index])));
        }

        private void b_BigButton_Click(object sender, RoutedEventArgs e)
        {

            nextBg();
        }

        private void b_info_Click(object sender, RoutedEventArgs e)
        {
            Container.Background = Brushes.Aqua;
        }
    }
}
