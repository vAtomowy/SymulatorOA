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

        public cw1()
        {
            InitializeComponent();
        }

        private void LampChange(Controls.Lamp lamp, bool on)
        {
            lamp.imge.BeginInit();
            
            if(on)
                lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_on.png", UriKind.RelativeOrAbsolute));
            else
                lamp.imge.Source = new BitmapImage(new Uri("/Resources/Lamp_off.png", UriKind.RelativeOrAbsolute));

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
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, false);
                    break;
                case 1:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, false);
                    break;

                case 2:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, false);
                    break;
                case 3:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, false);
                    break;
                case 4:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, false);
                    break;
                case 5:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, false);
                    break;
                case 6:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, false);
                    break;
                case 7:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, false);
                    break;
                case 8:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, true);
                    break;
                case 9:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, true);
                    break;
                case 10:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, true);
                    break;
                case 11:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, false);
                    LampChange(this.Lamp4, true);
                    break;
                case 12:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, true);
                    break;
                case 13:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, false);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, true);
                    break;
                case 14:
                    LampChange(this.Lamp1, false);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, true);
                    break;
                case 15:
                    LampChange(this.Lamp1, true);
                    LampChange(this.Lamp2, true);
                    LampChange(this.Lamp3, true);
                    LampChange(this.Lamp4, true);
                    break;
            }
        }

        private void pot_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            msg2 = (byte)this.pot.Value;
            this.pot_status.Content = this.pot.Value;
            Update();
            
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
            Update();
        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            if (B2.IsChecked)
                msg1 += 2;
            else
                msg1 -= 2;
            Update();
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            if (B3.IsChecked)
                msg1 += 4;
            else
                msg1 -= 4;
            Update();
        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            if (B4.IsChecked)
                msg1 += 8;
            else
                msg1 -= 8;
            Update();
        }

        private void B5_Click(object sender, RoutedEventArgs e)
        {
            if (B5.IsChecked)
                msg1 += 16;
            else
                msg1 -= 16;
            Update();
        }

        private void B6_Click(object sender, RoutedEventArgs e)
        {
            if (B6.IsChecked)
                msg1 += 32;
            else
                msg1 -= 32;
            Update();
        }

        private void B7_Click(object sender, RoutedEventArgs e)
        {
            if (B7.IsChecked)
                msg1 += 64;
            else
                msg1 -= 64;
            Update();
        }
    }
}
