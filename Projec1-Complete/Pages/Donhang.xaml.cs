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

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Donhang.xaml
    /// </summary>
    public partial class Donhang : Page
    {
        public Donhang()
        {
            InitializeComponent();
        }
        private void btn_packages_Click(object sender, RoutedEventArgs e)
        {
            stpn_packages.Visibility = Visibility.Visible;
            stpn_invoices.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Visible;

        }

        

        private void btn_thanhtoan_Click(object sender, RoutedEventArgs e)
        {
            stpn_packages.Visibility = Visibility.Visible;
            stpn_invoices.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Visible;
            btn_quayve.Visibility = Visibility.Visible;

        }
        private Button lastClickedButton;
        private void btn_khdh2_Click(object sender, RoutedEventArgs e)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = Brushes.White;
            }
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khdh1_Click(object sender, RoutedEventArgs e)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = Brushes.White;
            }
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khdh3_Click(object sender, RoutedEventArgs e)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = Brushes.White;
            }
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khdh4_Click(object sender, RoutedEventArgs e)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = Brushes.White;
            }
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khdh5_Click(object sender, RoutedEventArgs e)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = Brushes.White;
            }
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khdh6_Click(object sender, RoutedEventArgs e)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.Background = Brushes.White;
            }
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp1_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp2_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp3_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;

        }

        private void btn_sp4_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;

        }

        private void btn_sp5_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp6_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp7_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp8_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khohang_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_quayve_Click(object sender, RoutedEventArgs e)
        {
            stpn_invoices.Visibility = Visibility.Visible;
            stpn_packages.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Collapsed;
        }
    }
}
