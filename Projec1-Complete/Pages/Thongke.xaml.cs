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
using Projec1_Complete.DAL;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Thongke.xaml
    /// </summary>
    public partial class Thongke : Page
    {
        private ThongKeDataBUS thongKeDataBUS;
        public SeriesCollection seriesCollection;
        public ProductBUS productBUS;
        public AccountBUS accountBUS;
        public HistoryBUS historyBUS;

        public List<string> Labels;
        public Thongke()
        {
            InitializeComponent();
            thongKeDataBUS = new ThongKeDataBUS();
            historyBUS = new HistoryBUS();
            accountBUS = new AccountBUS();
            productBUS = new ProductBUS();

        }
        public void LoadHis()
        {
            List<History> hisList = historyBUS.GetHistories();
            List<HisWithUsername> historyWithUsernameList = new List<HisWithUsername>();

            foreach (History historyItem in hisList)
            {
                Account account = accountBUS.GetAccountID((int)historyItem.OrderID);
                Product prd = productBUS.GetProduct(historyItem.ProductID);
                string employeeName = account != null ? account.UserName : "N/A";
                string prdname = prd != null ? prd.ProductName : "Tên Sản Phẩm";


                HisWithUsername historyWithUsername = new HisWithUsername
                {
                    History = historyItem,
                    EmployeeName = employeeName,
                    ProductName = prdname,
                };

                historyWithUsernameList.Add(historyWithUsername);
            }

            DTGHistory.ItemsSource = historyWithUsernameList;
        }
        private bool ShowConfirmationMessageBox(string message, DependencyObject parentWindow)
        {
            Window window = Window.GetWindow(parentWindow);
            MessageBoxResult result = System.Windows.MessageBox.Show(window, message, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
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

        private void btn_lichsu_Click(object sender, RoutedEventArgs e)
        {
            stpn_history.Visibility = Visibility.Visible;
            bd_thongke.Visibility = Visibility.Collapsed;
            btnDay.Visibility = Visibility.Collapsed;
            btnMonth.Visibility = Visibility.Collapsed;
            btnPrd.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Visible;
            LoadHis();

        }

        private void btnDay_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection = thongKeDataBUS.GetChartData(out var labels, "Date");
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

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection = thongKeDataBUS.GetChartData(out var labels, "Month");
            ChartDT.AxisX.Clear();
            ChartDT.AxisX.Add(new Axis
            {
                Title = "Tháng",
                Labels = labels,
            });
            ChartDT.AxisY.Clear();
            ChartDT.AxisY.Add(new Axis
            {
                Title = "Doanh thu"
            });

            ChartDT.Series = seriesCollection;
        }

        private void btnPrd_Click(object sender, RoutedEventArgs e)
        {
            seriesCollection = thongKeDataBUS.GetChartData(out var labels, "Product");
            ChartDT.AxisX.Clear();
            ChartDT.AxisX.Add(new Axis
            {
                Title = "Sản Phẩm",
                Labels = labels,
            });
            ChartDT.AxisY.Clear();
            ChartDT.AxisY.Add(new Axis
            {
                Title = "Doanh thu"
            });

            ChartDT.Series = seriesCollection;
        }

        private void DTGHistory_Loaded(object sender, RoutedEventArgs e)
        {
            var removeButtonColumn = DTGHistory.Columns.FirstOrDefault(c => c.Header != null && c.Header.ToString() == "");
            if (removeButtonColumn is DataGridTemplateColumn)
            {
                var templateColumn = (DataGridTemplateColumn)removeButtonColumn;
                var removeButtonCellStyle = templateColumn.CellStyle;
                if (removeButtonCellStyle != null)
                {
                    var setter = removeButtonCellStyle.Setters.OfType<Setter>().FirstOrDefault(s => s.Property == Button.NameProperty && s.Value.ToString() == "RemoveButton");
                    if (setter != null)
                    {
                        var removeButton = (Button)setter.Value;
                        removeButton.Click += btnRemoveHis_Click;
                    }
                }
            }
        }
        private void btnRemoveHis_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            HisWithUsername his = (HisWithUsername)removeButton.DataContext;
            int HisID = his.History.HistoryID;

            bool confirmed = ShowConfirmationMessageBox("Bạn có chắc chắn muốn xóa dữ liệu này?", this);
            if (confirmed)
            {
                historyBUS.DelHistory(HisID);
                LoadHis();
            }
        }
    }
    
}
