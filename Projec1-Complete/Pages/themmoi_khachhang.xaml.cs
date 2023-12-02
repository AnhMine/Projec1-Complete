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
    /// Interaction logic for themmoi_khachhang.xaml
    /// </summary>
    public partial class themmoi_khachhang : Window
    {
        public themmoi_khachhang()
        {
            InitializeComponent();
        }
        private void btn_huythemkhachhang_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
