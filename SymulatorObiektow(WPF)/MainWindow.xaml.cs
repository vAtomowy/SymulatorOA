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

namespace SymulatorObiektow_WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum cwiczenie { cw1, cw2, cw3, cw4};
        string[] COMports = System.IO.Ports.SerialPort.GetPortNames();
        // static Prezentation pr = new Prezentation();

        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
            Buttons_Disable();
            this.Connect_button.IsEnabled = false;
            this.COMBox.ItemsSource = COMports;
            Property.Device = new Hardware();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Property.Device.Disconnect();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Connect_button_Click(object sender, RoutedEventArgs e)
        {
            //_Status.Content = "Connected";
            //Property.isConnected = true;
            if(Property.Device.isConnected())
            {
                Buttons_Enable();
                _Status.Foreground = Brushes.Green;
                _Status.Content = "Connected";
            }
            else
            {
                System.Windows.MessageBox.Show("Nie można połączyć");
            }
        }

        private void Cw1_Click(object sender, RoutedEventArgs e)
        {
            //Prezentation pr = new Prezentation();
            //pr.MainFrame.Content = new Pagges.cw1();
            //pr.Title = "Ćwiczenie 1 - Moduł Testowy";
            //pr.ShowDialog();
            prez("Ćwiczenie 1 - Moduł testowy", cwiczenie.cw1);
        }

        private void Cw2_Click(object sender, RoutedEventArgs e)
        {
            //Prezentation pr = new Prezentation();
            //pr.MainFrame.Content = new Pagges.cw2();
            //pr.Title = "Ćwiczenie 2 - Uje muje";
            //pr.ShowDialog();
            prez("Ćwiczenie 2 - uje muje", cwiczenie.cw2);
        }

        private void Cw3_Click(object sender, RoutedEventArgs e)
        {
            Prezentation pr = new Prezentation();
            pr.MainFrame.Content = new Pagges.cw3();
            pr.Title = "Ćwiczenie 3 - Uje muje";
            pr.ShowDialog();
        }

        private void Cw4_Click(object sender, RoutedEventArgs e)
        {
            Prezentation pr = new Prezentation();
            pr.MainFrame.Content = new Pagges.cw4();
            pr.Title = "Ćwiczenie 4 - Uje muje";
            pr.ShowDialog();
        }

        private void Buttons_Disable()
        {
            this.Cw1.IsEnabled = false;
            this.Cw2.IsEnabled = false;
            this.Cw3.IsEnabled = false;
            this.Cw4.IsEnabled = false;
        }

        private void Buttons_Enable()
        {
            this.Cw1.IsEnabled = true;
            this.Cw2.IsEnabled = true;
            this.Cw3.IsEnabled = true;
            this.Cw4.IsEnabled = true;
        }

        private void prez(string title, cwiczenie cwicz)
        {
            //Prezentation pr = new Prezentation();

            Prezentation pr = new Prezentation();

            pr.Title = title;
            switch(cwicz)
            {
                case cwiczenie.cw1:
                    pr.Content = new Pagges.cw1();
                    break;
                case cwiczenie.cw2:
                    pr.Content = new Pagges.cw2();
                    break;
                case cwiczenie.cw3:
                    pr.Content = new Pagges.cw3();
                    break;
                case cwiczenie.cw4:
                    pr.Content = new Pagges.cw4();
                    break;
            }

            pr.ShowDialog();
        }

        private void COMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.COMBox.SelectedItem != null)
            {
                this.Connect_button.IsEnabled = true;
                Console.WriteLine(COMBox.SelectedItem.ToString());
                Property.Device.setPort((string)COMBox.SelectedValue);
                
            }
        }
    }
}
