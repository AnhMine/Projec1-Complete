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
using System.IO;
using Projec1_Complete.Themes;
using Projec1_Complete.Pages;

namespace Projec1_Complete
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowKhoHang();
        }
        private void ShowKhoHang()
        {
            Khohang khohang = new Khohang();
            frameContent.Content = khohang;
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {

                WindowState = WindowState.Maximized;
                bdr_Main.CornerRadius = new CornerRadius(0);

            }


            else
            {

                WindowState = WindowState.Normal;
                bdr_Main.CornerRadius = new CornerRadius(20);

            }


        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void rd_khohang_Checked(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Khohang());
        }

        private void rd_khachhang_Checked(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Khachhang());
        }

        private void rd_donhang_Checked(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Donhang());
        }

        private void rd_thongke_Checked(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Thongke());
        }

        private void frameContent_Navigated(object sender, NavigationEventArgs e)
        {

        }




        private void rdLogOut_Click(object sender, RoutedEventArgs e)
        {
            /*if (File.Exists("account.txt"))
            {
                // Xóa tệp tin lưu trữ thông tin tài khoản
                File.Delete("account.txt");
            }

            this.Close();
            Login.Instance.Visibility = Visibility.Visible;*/

        }

        private void home_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
