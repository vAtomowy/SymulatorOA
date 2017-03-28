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
    /// Interaction logic for cw1.xaml
    /// </summary>
    public partial class cw1 : Page
    {

        private ImageSource b_clicked = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.g2994_clicked.GetHbitmap(),
                                                                                                        IntPtr.Zero,
                                                                                                        Int32Rect.Empty,
                                                                                                        BitmapSizeOptions.FromEmptyOptions());

        byte msg1 = 0;
        byte msg2 = 0;

        byte UI_state = 0;

        CancellationTokenSource task_token;

        bool tok = false;

        public cw1()
        {
            InitializeComponent();
            LampsChange(false, false, false, false);
            task_token = new CancellationTokenSource();
            Process(task_token.Token);
        }

        private async void Process(CancellationToken token)
        {
            try
            {
                await Task.Run(() =>
                {
                    for(;;)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => Update()));
                        Console.WriteLine("DONE");
                        Thread.Sleep(30);
                        if (tok)
                            break;
                    }
                });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LampsChange(bool lamp1, bool lamp2, bool lamp3, bool lamp4)
        {
            LampChange(this.Lamp1, lamp1, 1);
            LampChange(this.Lamp2, lamp2, 2);
            LampChange(this.Lamp3, lamp3, 0);
            LampChange(this.Lamp4, lamp4, 0);
        }

        private void LampChange(Controls.Lamp lamp, bool on, int num)
        {
            lamp.imge.BeginInit();

            if (on)
            {
                Console.WriteLine(num + " lamp on");
                if(num == 0)
                    lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_y_on.png", UriKind.RelativeOrAbsolute));
                else if(num == 1)
                    lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_g_on.png", UriKind.RelativeOrAbsolute));
                else if(num == 2)
                    lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_r_on.png", UriKind.RelativeOrAbsolute));

            }
            else
            {
                Console.WriteLine(num + " lamp off");
                if (num == 0)
                    lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_y_off.png", UriKind.RelativeOrAbsolute));
                else if (num == 1)
                    lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_g_off.png", UriKind.RelativeOrAbsolute));
                else if (num == 2)
                    lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_r_off.png", UriKind.RelativeOrAbsolute));

            }

            lamp.imge.EndInit();
        }

        private void Update()
        {
            Property.Device.Send_Data(msg1);
            Property.Device.Send_Data(msg2);
            
            UI_state = Property.Device.recived_data;

            Console.WriteLine("Buttons: " + msg1);
            Console.WriteLine("Slider: " + msg2);
            Console.WriteLine("Recived: " + UI_state);

            switch(UI_state)
            {
                case 0:
                    LampsChange(false, false, false, false);
                    break;
                case 1:
                    LampsChange(true, false, false, false);
                    break;
                case 2:
                    LampsChange(false, true, false, false);
                    break;
                case 3:
                    LampsChange(true, true, false, false);
                    break;
                case 4:
                    LampsChange(false, false, true, false);
                    break;
                case 5:
                    LampsChange(true, false, true, false);
                    break;
                case 6:
                    LampsChange(false, true, true, false);
                    break;
                case 7:
                    LampsChange(true, true, true, false);
                    break;
                case 8:
                    LampsChange(false, false, false, true);
                    break;
                case 9:
                    LampsChange(true, false, false, true);
                    break;
                case 10:
                    LampsChange(false, true, false, true);
                    break;
                case 11:
                    LampsChange(true, true, false, true);
                    break;
                case 12:
                    LampsChange(false, false, true, true);
                    break;
                case 13:
                    LampsChange(true, false, true, true);
                    break;
                case 14:
                    LampsChange(false, true, true, true);
                    break;
                case 15:
                    LampsChange(true, true, true, true);
                    break;
            }
        }

        private void pot_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            msg2 = (byte)this.pot.Value;
            this.pot_status.Content = this.pot.Value;
            
            //if(this.pot.Value == 255)
            //{
            //    this.Lamp1.imge.BeginInit();
            //    this.Lamp1.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_off.png", UriKind.RelativeOrAbsolute));
            //    this.Lamp1.imge.EndInit();
            //}
            //else
            //{
            //    this.Lamp1.imge.BeginInit();
            //    this.Lamp1.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_on.png", UriKind.RelativeOrAbsolute)); //Tools.BitmapToImage(Tools.BitmapToBitmapImage(Properties.Resources.Lamp_on));
            //    this.Lamp1.imge.EndInit();
            //}
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if (true == (sender as Controls.SwitchButton).IsChecked)
                msg1 += 1;
            else
                msg1 -= 1;
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            if (B2.IsChecked)
                msg1 += 2;
            else
                msg1 -= 2;
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            if (B3.IsChecked)
                msg1 += 4;
            else
                msg1 -= 4;
        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            if (B4.IsChecked)
                msg1 += 8;
            else
                msg1 -= 8;
        }

        private void B5_Click(object sender, RoutedEventArgs e)
        {
            if (B5.IsChecked)
                msg1 += 16;
            else
                msg1 -= 16;
        }

        private void B6_Click(object sender, RoutedEventArgs e)
        {
            if (B6.IsChecked)
                msg1 += 32;
            else
                msg1 -= 32;
        }

        private void B7_Click(object sender, RoutedEventArgs e)
        {
            if (B7.IsChecked)
                msg1 += 64;
            else
                msg1 -= 64;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            task_token.Cancel();
            tok = true;
            Console.WriteLine("Koniec");
        }
    }
}
