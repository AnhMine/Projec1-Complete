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
            LoadChart("Date");

        }
        public void LoadHis()
        {
            List<History> hisList = historyBUS.GetHistories();
            List<HisWithUsername> historyWithUsernameList = new List<HisWithUsername>();

            foreach (History historyItem in hisList)
            {

                if(historyItem.OrderID.HasValue)
                {
                    Account account = accountBUS.GetAccountID((int)historyItem.OrderID);
                    string person = accountBUS.GetPersonById(account.AccountID);
                    Product prd = productBUS.GetProduct(historyItem.ProductID);
                    string prdname = prd != null ? prd.ProductName : "Tên Sản Phẩm";


                    HisWithUsername historyWithUsername = new HisWithUsername
                    {
                        History = historyItem,
                        EmployeeName = person,
                        ProductName = prdname,
                    };

                    historyWithUsernameList.Add(historyWithUsername);
                }
               
             
                
                else
                {
                    Product prd = productBUS.GetProduct(historyItem.ProductID);

                    string person = "Admin";
                    string prdname = prd != null ? prd.ProductName : "Tên Sản Phẩm";

                    HisWithUsername historyWithUsername = new HisWithUsername
                    {
                        History = historyItem,
                        EmployeeName = person,
                        ProductName = prdname,
                    };
                    historyWithUsernameList.Add(historyWithUsername);
                }

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

        private void LoadChart(string GroupBy)
        {
            var seriesCollection = thongKeDataBUS.GetChartData(out var labels, GroupBy);

            // Clear the existing axes
            ChartDT.AxisX.Clear();
            ChartDT.AxisY.Clear();

            // Create new axes and set their properties
            var xAxis = new Axis { Labels = labels, FontSize = 18 };
            var yAxis = new Axis { FontSize = 18 };

            // Add the new axes to the chart
            ChartDT.AxisX.Add(xAxis);
            ChartDT.AxisY.Add(yAxis);

            // Set the series collection
            ChartDT.Series = seriesCollection;
        }

        private void btnDay_Click(object sender, RoutedEventArgs e)
        {
            LoadChart("Date");
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            LoadChart("Month");
        }

        private void btnPrd_Click(object sender, RoutedEventArgs e)
        {
            LoadChart("Product");
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
