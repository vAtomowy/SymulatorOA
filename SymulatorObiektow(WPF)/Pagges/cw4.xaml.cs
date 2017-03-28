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
    /// Interaction logic for cw4.xaml
    /// </summary>
    public partial class cw4 : Page
    {

        int index = 0;
        List<string> imgs = new List<string>();
        CancellationTokenSource task_token;
        enum stages { Get_Bottles, Fill_Bottles1, Next_Bottle1, Fill_Bottles2, Next_Bottle2, Continues };
        bool tok = false;

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

        public void changeBg(int x)
        {
            changeBg(new BitmapImage(new Uri(imgs[x])));
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
            //nextBg();
            this.b_Start.Background = Brushes.Green;
            this.b_Stop.Background = Brushes.Silver;
            task_token = new CancellationTokenSource();
            Process(task_token.Token);
        }

        private async void Process(CancellationToken token)
        {

            try
            {
                await Task.Run(() =>
                {
                    byte msg = 0x01, anwser = 0x0;

                    bool abort = false;

                    stages Stage = stages.Get_Bottles;

                    Console.WriteLine("Next step");

                    for(;;)
                    {
                        Property.Device.Send_Data(msg, msg);
                        anwser = Property.Device.recived_data;

                        switch(anwser)
                        {
                            case 0x08:
                            case 0x18:
                            case 0x28:
                            case 0x38:
                                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => { b_Start.Background = Brushes.Red; }));
                                abort = true;
                                break;
                            case 0x01:
                                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => B_Second()));
                                Stage = stages.Continues;
                                msg = 0x31;
                                break;
                            case 0x06:
                                if (Stage == stages.Fill_Bottles1)
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,new Action(() =>A_First()));
                                    Stage = stages.Next_Bottle1;
                                    msg = 0x11;
                                }
                                else if(Stage == stages.Fill_Bottles2)
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => A_Second()));
                                    Stage = stages.Next_Bottle2;
                                    msg = 0x11;
                                }
                                else
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => A_Final()));
                                    msg = 0x11;
                                }
                                break;
                            case 0x05:
                                if(Stage == stages.Get_Bottles)
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => Ani_Init()));
                                    Stage = stages.Fill_Bottles1;
                                    msg = 0x01;
                                }
                                else if(Stage == stages.Next_Bottle1)
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => B_First()));
                                    Stage = stages.Fill_Bottles2;
                                    msg = 0x31;
                                }
                                else
                                {
                                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => B_Final()));
                                    msg = 0x31;
                                }
                                break;
                        }

                        if (abort || tok)
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void b_Stop_Click(object sender, RoutedEventArgs e)
        {
            task_token.Cancel();
            this.b_Start.Background = Brushes.Silver;
            this.b_Stop.Background = Brushes.Green;
            changeBg(0);
        }

        #region Animation

        private void Ani_Init()
        {
            changeBg(0);
            Thread.Sleep(500);
            changeBg(1);
            Thread.Sleep(500);
            changeBg(2);
            Thread.Sleep(500);
            changeBg(3);
            Thread.Sleep(500);
        }

        private void A_First()
        {
            changeBg(4);
            Thread.Sleep(500);
            changeBg(5);
            Thread.Sleep(500);
            changeBg(6);
            Thread.Sleep(500);
            changeBg(7);
            Thread.Sleep(500);
        }

        private void B_First()
        {
            changeBg(8);
            Thread.Sleep(500);
        }

        private void A_Second()
        {
            changeBg(9);
            Thread.Sleep(500);
            changeBg(10);
            Thread.Sleep(500);
            changeBg(11);
            Thread.Sleep(500);
            changeBg(12);
            Thread.Sleep(500);
            changeBg(13);
            Thread.Sleep(500);
        }

        private void B_Second()
        {
            changeBg(14);
            Thread.Sleep(500);
        }

        private void A_Final()
        {
            changeBg(15);
            Thread.Sleep(500);
            changeBg(16);
            Thread.Sleep(500);
            changeBg(17);
            Thread.Sleep(500);
            changeBg(18);
            Thread.Sleep(500);
        }

        private void B_Final()
        {
            changeBg(19);
            Thread.Sleep(500);
        }
        #endregion

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            task_token.Cancel();
            tok = true;
        }
    }
}
