using OfficeOpenXml;
using Projec1_Complete.BUS;
using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for Khachhang.xaml
    /// </summary>
    public partial class Khachhang : Page
    {
        public PersonBUS personBUS;
        public Khachhang()
        {
            InitializeComponent();
            personBUS = new PersonBUS();
            LoadData();
        }
        private void btn_themkhahhang_Click(object sender, RoutedEventArgs e)
        {
            themmoi_khachhang add = new themmoi_khachhang();
            add.Show();
        }
        private bool ShowConfirmationMessageBox(string message, DependencyObject parentWindow)
        {
            Window window = Window.GetWindow(parentWindow);
            MessageBoxResult result = System.Windows.MessageBox.Show(window, message, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
        void LoadData()
        {
            ObservableCollection<Person> customers = new ObservableCollection<Person>();
            ObservableCollection<Person> employees = new ObservableCollection<Person>();
            List<Person> list = personBUS.GetListCustomer();

            foreach (Person person in list)
            {
                if (person.Type == "Khách Hàng")
                {
                    customers.Add(person);
                }
                else if (person.Type == "Admin" || person.Type == "Nhân Viên")
                {
                    employees.Add(person);
                }
            }

            DTGCustomers.ItemsSource = customers;
            DTGEmployees.ItemsSource = employees;
        }


        private void btn_sanpham_Click(object sender, RoutedEventArgs e)
        {
            stpn_dsnv.Visibility = Visibility.Collapsed;
            stpn_dskh.Visibility = Visibility.Visible;
            LoadData();


        }

        private void btn_nhanvien_Click(object sender, RoutedEventArgs e)
        {
            stpn_dsnv.Visibility = Visibility.Visible;
            stpn_dskh.Visibility = Visibility.Collapsed;
            LoadData();

        }

        private void btnImportFile_customer_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = DTGCustomers; // Đảm bảo rằng DTGProduct là tên chính xác của DataGrid

            // Tạo dialog để chọn vị trí lưu file Excel
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "Excel Files|*.xlsx";
            if (saveDialog.ShowDialog() == true)
            {
                FileInfo newFile = new FileInfo(saveDialog.FileName);

                // Tạo một package mới cho file Excel
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (var excelPackage = new ExcelPackage(newFile))
                {
                    // Tạo một worksheet mới trong file Excel
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                    // Ghi tiêu đề cột
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
                    }

                    for (int row = 0; row < dataGrid.Items.Count; row++)
                    {
                        for (int col = 0; col < dataGrid.Columns.Count; col++)
                        {
                            var cellContent = dataGrid.Columns[col].GetCellContent(dataGrid.Items[row]);

                            if (cellContent is TextBlock textBlock)
                            {
                                var cellValue = textBlock.Text;
                                worksheet.Cells[row + 2, col + 1].Value = cellValue;
                            }
                            else if (cellContent is Image image)
                            {
                                var dataItem = dataGrid.Items[row] as Product;
                                var imagePath = dataItem.ImageLink;
                                worksheet.Cells[row + 2, col + 1].Value = imagePath;
                            }
                        }
                    }

                    excelPackage.Save();
                }

                MessageBox.Show("Xuất ra file Excel thành công!");
            }
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {

            Person selectedCustomers = (Person)DTGCustomers.SelectedItem;
            if (selectedCustomers != null)
            {
                sua_khach_hang detailForm = new sua_khach_hang(selectedCustomers, this);
                detailForm.ShowDialog();
                LoadData();

            }
        }

        private void btnDelCustomer_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            Person prd = (Person)removeButton.DataContext;
            int id = prd.PersonID;

            bool confirmed = ShowConfirmationMessageBox("Bạn có chắc chắn muốn xóa dữ liệu này?", this);
            if (confirmed)
            {
                personBUS.RemoveCustomer(id);
                LoadData();
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {

            Person selectedCustomers = (Person)DTGEmployees.SelectedItem;
            if (selectedCustomers != null)
            {
                sua_khach_hang detailForm = new sua_khach_hang(selectedCustomers, this);
                detailForm.ShowDialog();
                LoadData();

            }
        }

        private void btnDelEmployee_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            Person prd = (Person)removeButton.DataContext;
            int id = prd.PersonID;

            bool confirmed = ShowConfirmationMessageBox("Bạn có chắc chắn muốn xóa dữ liệu này?", this);
            if (confirmed)
            {
                personBUS.RemoveCustomer(id);
                LoadData();
            }
        }

        private void txtSerach_TextChanged(object sender, TextChangedEventArgs e)
        {

            string search = txtSerach.Text;
            ObservableCollection<Person> customers = new ObservableCollection<Person>();
            ObservableCollection<Person> employees = new ObservableCollection<Person>();

            List<Person> customerlist = personBUS.SearchPersonById(search);
            foreach (Person cus in customerlist)
            {
                if (cus.Type == "Khách Hàng")
                {
                    customers.Add(cus);

                }
                else
                {
                    employees.Add(cus);
                }    
            }

            DTGCustomers.ItemsSource = customers;
            DTGEmployees.ItemsSource = employees;
            if(search == "")
            {
                LoadData();
            }    
        }
    }
}
