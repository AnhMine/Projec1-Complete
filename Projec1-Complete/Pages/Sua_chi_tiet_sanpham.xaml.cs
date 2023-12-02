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
using System.Windows.Shapes;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Sua_chi_tiet_sanpham.xaml
    /// </summary>
    public partial class Sua_chi_tiet_sanpham : Window
    {
        public Sua_chi_tiet_sanpham()
        {
            InitializeComponent();
        }
        private void btn_huysuasp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


        }



        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            txtName.IsEnabled = true;
        }

        private void btnPriceSell_Click(object sender, RoutedEventArgs e)
        {
            txtPriceSell.IsEnabled = true;
        }

        private void btnPrice_Click(object sender, RoutedEventArgs e)
        {
            txtPrice.IsEnabled = true;
        }

        private void btnDes_Click(object sender, RoutedEventArgs e)
        {
            txtDes.IsEnabled = true;
        }

        private void btnQuantity_Click(object sender, RoutedEventArgs e)
        {
            txtQuantity.IsEnabled = true;
        }
    }
}
