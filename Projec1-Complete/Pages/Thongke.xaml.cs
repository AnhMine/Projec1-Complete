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
using LiveCharts;
using LiveCharts.Wpf;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Thongke.xaml
    /// </summary>
    public partial class Thongke : Page
    {
        public Thongke()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Tháng 10 2023",
                    Values= new ChartValues<double> {20,10,36,90}
                }

            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Tháng 11 2023",
                Values = new ChartValues<double> { 12, 18, 15 }
            });

            SeriesCollection[1].Values.Add(48d);
            Labels = new[] { "Chinh", " Châm", "Huy", "Duy" };
            Values = value => value.ToString("N");

            DataContext = this;



        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values { get; set; }


        private void btn_thongke_Click(object sender, RoutedEventArgs e)
        {
            stpn_history.Visibility = Visibility.Collapsed;
            bd_thongke.Visibility = Visibility.Visible;
            btn_tktheongay.Visibility = Visibility.Visible;
            btn_tktheothang.Visibility = Visibility.Visible;
            btn_tksanpham.Visibility = Visibility.Visible;
            btn_quayve.Visibility = Visibility.Collapsed;

        }

        private void btn_lichsu_Click_1(object sender, RoutedEventArgs e)
        {
            stpn_history.Visibility = Visibility.Visible;
            bd_thongke.Visibility = Visibility.Collapsed;
            btn_tktheongay.Visibility = Visibility.Collapsed;
            btn_tktheothang.Visibility = Visibility.Collapsed;
            btn_tksanpham.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Visible;

        }
    }
    
}
