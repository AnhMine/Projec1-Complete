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
using Projec1_Complete.BUS;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Thongke.xaml
    /// </summary>
    public partial class Thongke : Page
    {
        private ThongKeDataBUS thongKeDataBUS;
        public SeriesCollection seriesCollection;
        public List<string> Labels;
        public Thongke()
        {
            InitializeComponent();
            thongKeDataBUS = new ThongKeDataBUS();
        }


        private void btn_thongke_Click(object sender, RoutedEventArgs e)
        {
            stpn_history.Visibility = Visibility.Collapsed;
            bd_thongke.Visibility = Visibility.Visible;
            btnDay.Visibility = Visibility.Visible;
            btnMonth.Visibility = Visibility.Visible;
            btnPrd.Visibility = Visibility.Visible;
            btn_quayve.Visibility = Visibility.Collapsed;

        }

        private void btn_lichsu_Click_1(object sender, RoutedEventArgs e)
        {
            stpn_history.Visibility = Visibility.Visible;
            bd_thongke.Visibility = Visibility.Collapsed;
            btnDay.Visibility = Visibility.Collapsed;
            btnMonth.Visibility = Visibility.Collapsed;
            btnPrd.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Visible;

        }

        private void btnDay_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection = thongKeDataBUS.GetChartData(out var labels, "Ngày");
            ChartDT.AxisX.Clear();
            ChartDT.AxisX.Add(new Axis
            {
                Title = "Ngày",
                Labels = labels,
            });
            ChartDT.AxisY.Clear();
            ChartDT.AxisY.Add(new Axis
            {
                Title = "Doanh thu"
            });

            ChartDT.Series = seriesCollection;
        }
    }
    
}
