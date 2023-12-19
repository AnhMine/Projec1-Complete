using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
using OfficeOpenXml;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using MessageBox = System.Windows.Forms.MessageBox;
using Binding = System.Windows.Data.Binding;
using System.Windows.Controls.Primitives;
using CheckBox = System.Windows.Controls.CheckBox;
using Projec1_Complete.BUS;
using Projec1_Complete.DAL;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Khohang.xaml
    /// </summary>
    public partial class Khohang : Page
    {
        public ProductBUS productBUS;

        public Khohang()
        {
            InitializeComponent();
            productBUS = new ProductBUS();
            LoadPage();
        }

        #region Method

        public void LoadPage()
        {
            DTGProduct.ItemsSource = null;

            ObservableCollection<Product> products = new ObservableCollection<Product>();
            List<Product> productlist = productBUS.GetProducts();
            foreach (Product product in productlist)
            {
                products.Add(product);
            }

            DTGProduct.ItemsSource = products;

        }
        private bool ShowConfirmationMessageBox(string message, DependencyObject parentWindow)
        {
            Window window = Window.GetWindow(parentWindow);
            MessageBoxResult result = System.Windows.MessageBox.Show(window, message, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
        private void AttachButtonEvent(string buttonName, RoutedEventHandler eventHandler)
        {
            var column = DTGProduct.Columns.FirstOrDefault(c => c.Header != null && c.Header.ToString() == buttonName);
            if (column is DataGridTemplateColumn)
            {
                var templateColumn = (DataGridTemplateColumn)column;
                var cellStyle = templateColumn.CellStyle;
                if (cellStyle != null)
                {
                    var setter = cellStyle.Setters.OfType<Setter>().FirstOrDefault(s => s.Property == Button.NameProperty && s.Value.ToString() == buttonName);
                    if (setter != null)
                    {
                        var button = (Button)setter.Value;
                        button.Click += eventHandler;
                    }
                }
            }
        }
        #endregion Event


        private void btn_themsp_Click(object sender, RoutedEventArgs e)
        {
            themmoi_sanpham add = new themmoi_sanpham();
            add.Show();

        }
       
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtSearch.Text;
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            List<Product> productlist = productBUS.SearchPrd(search);
            foreach (Product product in productlist)
            {
                products.Add(product);
            }

            DTGProduct.ItemsSource = products;
        }

        private void DTGProduct_Loaded(object sender, RoutedEventArgs e)
        {
            AttachButtonEvent("RemoveButton", RemoveButton_Click);
            AttachButtonEvent("UpdateButton", Update_Click);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = (Button)sender;
            Product prd = (Product)removeButton.DataContext;
            string PrdID = prd.ProductID;


            bool confirmed = ShowConfirmationMessageBox("Bạn có chắc chắn muốn xóa dữ liệu này?", this);
            if (confirmed)
            {
                productBUS.DelProduct(PrdID);
                LoadPage();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            Product selectedProduct = (Product)DTGProduct.SelectedItem;
            Category cate = selectedProduct.Category;
            if (selectedProduct != null)
            {
                string cateName = cate.CategoryName;
                Sua_chi_tiet_sanpham detailForm = new Sua_chi_tiet_sanpham(selectedProduct, this, cateName);
                detailForm.ShowDialog();
                LoadPage();

            }


        }

        private void KhoHang_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void btnImportFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = DTGProduct; // Đảm bảo rằng DTGProduct là tên chính xác của DataGrid

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
    }
}
