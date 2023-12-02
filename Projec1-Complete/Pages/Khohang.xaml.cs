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
    /// Interaction logic for Khohang.xaml
    /// </summary>
    public partial class Khohang : Page
    {
        public Khohang()
        {
            InitializeComponent();
        }
        private void btn_themsp_Click(object sender, RoutedEventArgs e)
        {
            themmoi_sanpham add = new themmoi_sanpham();
            add.Show();

        }



        private void btn_sanpham_Click(object sender, RoutedEventArgs e)
        {
            stpn_history.Visibility = Visibility.Collapsed;
            stpn_product.Visibility = Visibility.Visible;
            txtbl_khohang.Text = "KHO HÀNG";

            btnAddPrd.Visibility = Visibility.Visible;
            btnImportFile.Visibility = Visibility.Visible;
        }

    }
}
