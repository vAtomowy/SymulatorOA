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
    /// Interaction logic for SwitchButton.xaml
    /// </summary>
    public partial class SwitchButton : UserControl
    {
        public SwitchButton()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;
        public event RoutedEventHandler Checked;
        public event RoutedEventHandler UnChecked;

        public static readonly DependencyProperty EnabledUncheckedProperty = DependencyProperty.Register(
            "EnabledUnchecked",
            typeof(ImageSource),
            typeof(SwitchButton),
            new PropertyMetadata(onEnabledUncheckedChangedCallback));

        public ImageSource EnabledUnchecked
        {
            get { return (ImageSource)GetValue(EnabledUncheckedProperty); }
            set { SetValue(EnabledUncheckedProperty, value);  }
        }

        static void onEnabledUncheckedChangedCallback(
            DependencyObject dobj,
            DependencyPropertyChangedEventArgs args)
        {
            //do something if needed
        }

        public static readonly DependencyProperty DisabledUncheckedProperty =
            DependencyProperty.Register(
            "DisabledUnchecked",
            typeof(ImageSource),
            typeof(SwitchButton),
            new PropertyMetadata(onDisabledUncheckedChangedCallback));

        public ImageSource DisabledUnchecked
        {
            get { return (ImageSource)GetValue(DisabledUncheckedProperty); }
            set { SetValue(DisabledUncheckedProperty, value); }
        }

        static void onDisabledUncheckedChangedCallback(
            DependencyObject dobj,
            DependencyPropertyChangedEventArgs args)
        {
            //do something if needed
        }


        public static readonly DependencyProperty EnabledCheckedProperty =
            DependencyProperty.Register(
            "EnabledChecked",
            typeof(ImageSource),
            typeof(SwitchButton),
            new PropertyMetadata(onEnabledCheckedChangedCallback));

        public ImageSource EnabledChecked
        {
            get { return (ImageSource)GetValue(EnabledCheckedProperty); }
            set { SetValue(EnabledCheckedProperty, value); }
        }

        static void onEnabledCheckedChangedCallback(
            DependencyObject dobj,
            DependencyPropertyChangedEventArgs args)
        {
            //do something if needed
        }


        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
            "IsChecked",
            typeof(Boolean),
            typeof(SwitchButton),
            new PropertyMetadata(onCheckedChangedCallback));

        public Boolean IsChecked
        {
            get { return (Boolean)GetValue(IsCheckedProperty); }
            set { if (value != IsChecked) SetValue(IsCheckedProperty, value); }
        }

        static void onCheckedChangedCallback(
            DependencyObject dobj,
            DependencyPropertyChangedEventArgs args)
        {
            //do something, if needed
        }

        private void TB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Checked != null)
            {
                this.Checked(this, e);
                Console.WriteLine("CHECKED");
            }
        }

        private void TB_Click(object sender, RoutedEventArgs e)
        {
            if(this.Click != null)
            {
                if (true == TB.IsChecked)
                    this.IsChecked = true;
                else
                    this.IsChecked = false;

                this.Click(this, e);
            }
        }

        
        private void TB_Unchecked(object sender, RoutedEventArgs e)
        {
            if(this.UnChecked != null)
            {
                this.UnChecked(this, e);
                Console.WriteLine("UNCHECKED");
                
            }
        }
    }
}
