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
using System.Threading;

namespace SymulatorObiektow_WPF_.Pagges
{
    /// <summary>
    /// Interaction logic for cw3.xaml
    /// </summary>
    public partial class cw3 : Page
    {
        List<string> imgs = new List<string>();
        int index = 0;
        CancellationTokenSource task_token;
        byte msg = 0x80;
        bool tok = false;

        enum states { state_0, state_1 };

        public cw3()
        {
            InitializeComponent();
           /*0*/ imgs.Add("pack://siteoforigin:,,,/Resources/D.png");
           /*1*/ imgs.Add("pack://siteoforigin:,,,/Resources/DD[1].png");
           /*2*/ imgs.Add("pack://siteoforigin:,,,/Resources/DD[2]-4.png");
           /*3*/ imgs.Add("pack://siteoforigin:,,,/Resources/DD[2]-D.png");
           /*4*/ imgs.Add("pack://siteoforigin:,,,/Resources/DD[3].png");
           /*5*/ imgs.Add("pack://siteoforigin:,,,/Resources/DG[1].png");
           /*6*/ imgs.Add("pack://siteoforigin:,,,/Resources/DG[2].png");
           /*7*/ imgs.Add("pack://siteoforigin:,,,/Resources/DG[3].png");
           /*8*/ imgs.Add("pack://siteoforigin:,,,/Resources/G[opcja1].png");
           /*9*/ imgs.Add("pack://siteoforigin:,,,/Resources/G[opcja2].png");

            b_brumbrum.IsEnabled = false;
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

        public void changeBg(int x)
        {
            changeBg(new BitmapImage(new Uri(imgs[x])));
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
            //nextBg();
            task_token = new CancellationTokenSource();
            Process(task_token.Token);
        }

        private void b_info_Click(object sender, RoutedEventArgs e)
        {
            Container.Background = Brushes.Aqua;
        }

        private async void Process(CancellationToken token)
        {
            try
            {
                await Task.Run(() =>
                {
                byte anwser;
                bool abort = false;

                    states State = states.state_0;

                for (;;)
                {
                    Property.Device.Send_Data(msg, msg);
                    anwser = Property.Device.recived_data;

                    switch (anwser)
                    {
                        case 0xF0:
                        case 0x30:
                        case 0x70:
                            abort = true;
                            break;
                        case 0x0D:
                            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => DG1_3()));
                            msg = 0x80;
                            break;
                        case 0x01:
                                if (State == states.state_0)
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => G()));
                                    msg = 0x20;
                                    State = states.state_1;
                                }
                                else
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => DD2D()));
                                    msg = 0x40;
                                    State = states.state_0;
                                }
                                break;
                        case 0x0B:
                            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { b_brumbrum.IsEnabled = true; D1_3(); }));
                            msg = 0x00;
                            break;
                        case 0x00:
                            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => changeBg(0)));
                            msg = 0x50;
                            break;
                        }

                        if (abort || tok)
                            break;
                    }

                

                });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DG1_3()
        {
            changeBg(5);
            Thread.Sleep(500);
            changeBg(6);
            Thread.Sleep(500);
            changeBg(7);
            Thread.Sleep(500);
        }
        private void G()
        {
            changeBg(8);
            Thread.Sleep(500);
            changeBg(9);
            Thread.Sleep(500);
        }

        private void D1_3()
        {
            changeBg(1);
            Thread.Sleep(500);
            changeBg(2);
            Thread.Sleep(500);
            changeBg(4);
            Thread.Sleep(500);
        }

        private void DD2D()
        {
            changeBg(3);
            Thread.Sleep(500);
        }

        private void b_brumbrum_Click(object sender, RoutedEventArgs e)
        {
            this.b_brumbrum.IsEnabled = false;
            msg = 0x40;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            task_token.Cancel();
            tok = true;
        }
    }
}
