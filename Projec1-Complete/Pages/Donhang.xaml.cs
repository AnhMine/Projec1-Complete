
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Projec1_Complete.BUS;
using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.Win32;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.UI.Xaml.Grid.Converter;


namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Donhang.xaml
    /// </summary>
    public partial class Donhang : Page

    {
        public ObservableCollection<Category> categories { get; set; }
        public ObservableCollection<Person> persons { get; set; }
        private CategoryBUS categoryBUS;
        private OrderInfoBUS orderInfoBUS;
        private PersonBUS personBUS;
        private OrderBUS orderBUS;
        private int currentID;
        private int currentCateID;
        private Order currentOrderID;
        public Donhang()
        {
            InitializeComponent();
            DataContext = this;
            categories = new ObservableCollection<Category>();
            persons = new ObservableCollection<Person>();
            personBUS = new PersonBUS();
            categoryBUS = new CategoryBUS();
            orderBUS = new OrderBUS();
            orderInfoBUS = new OrderInfoBUS();
            currentCateID = -1;
            currentID = -1;
            txtTotalAmount.Text = orderBUS.GetTotalAmount(-1).ToString();
            LoadCategory();
            LoadPersonsByStatus(null);
            //GetProductInfo(1);

        }
      
        void LoadTotalAmount(int orderID)
        {
            txtTotalAmount.Text = orderBUS.GetTotalAmount(orderID).ToString();
        }
        private int GetCurrentPersonID()
        {
            return currentID;
        }
     
        private void LoadCustomerInfo(int id)
        {
            List<Person> people = personBUS.GetInfoPerson(id);
            itctPersonId.ItemsSource = people;
        }
        private void LoadOrderDTG(int id)
        {

            ObservableCollection<ProductAndOrderInfo> listUnPaid = new ObservableCollection<ProductAndOrderInfo>();

            List<ProductAndOrderInfo> orders = orderBUS.GetOrdersByPersonId2(id);
            foreach(ProductAndOrderInfo oi in orders)
            {
                if(oi.order.Status == false)
                {

                    listUnPaid.Add(oi);
                }

            }

            DTGOrder.ItemsSource = listUnPaid;
            



        }

        private void LoadCategory()
        {
            List<Category> categorieslist = categoryBUS.GetCategories();
            itctCate.ItemsSource = categorieslist;

        }
       
       
        private void LoadProductByIDCategory(int categoryId, int personId)
        {
            List<ProductAndOrderInfo> prdList = categoryBUS.GetProductByCateAndPersonId(categoryId, personId);
            itctProduct.ItemsSource = prdList;
        }
        private void GetProductInfo(int id)
        {
            List<ProductAndOrderInfo> list = categoryBUS.GetProductsWithOrderInfo(id);
            itctProduct.ItemsSource = list;
        }

        private void LoadPersonsByStatus(bool? status)
        {
            List<Person> perList = personBUS.GetListCustomer();

            List<ProductAndOrderInfo> personOrderlist = perList.Select(personItem =>
            {
                bool personStatus = orderBUS.GetStatus(personItem.PersonID);
                int OrderID = orderBUS.GetOrder(personItem.PersonID);
                decimal totalAmount = orderBUS.GetTotalAmount(OrderID);
                string statusText = personStatus == true ? "Đã Thanh Toán" : personStatus == false ? "Chưa Thanh Toán" : "";
                statusText = statusText ?? "";

                return new ProductAndOrderInfo
                {
                    person = personItem,
                    DisplayStatus = statusText,
                    TotalAmount = totalAmount

                };
            }).ToList();

            if (status.HasValue)
            {
                personOrderlist = personOrderlist.Where(o => orderBUS.GetStatus(o.person.PersonID) == status.Value).ToList();
            }


            itctPeople.ItemsSource = personOrderlist;
        }






        private void CategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            Category prd = (Category)btn.DataContext;
            int categoryID = prd.CategoryID;
            int personID = GetCurrentPersonID();
            currentCateID = categoryID;
            LoadProductByIDCategory(categoryID, personID);
            LoadOrderDTG(personID);

        }

        private void btn_packages_Click(object sender, RoutedEventArgs e)
        {
            stpn_packages.Visibility = Visibility.Visible;
            stpn_invoices.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Visible;

        }

        private void btn_thanhtoan_Click(object sender, RoutedEventArgs e)
        {
            stpn_packages.Visibility = Visibility.Visible;
            stpn_invoices.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Visible;
            btn_quayve.Visibility = Visibility.Visible;

        }

        private void btn_quayve_Click(object sender, RoutedEventArgs e)
        {
            stpn_invoices.Visibility = Visibility.Visible;
            stpn_packages.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Collapsed;
        }

        private void btnProductOrder_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {

            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            OrderInfo info = orderInfoBUS.GetOrderInfoByOrderAndProduct(currentOrderID.OrderID, person.Product.ProductID);
            if(info != null)
            {

                person.orderInfo.Quantity++;
                orderInfoBUS.RemoveProductFromOrder(info);
                LoadProductByIDCategory(currentCateID, currentID);
                LoadOrderDTG(currentID);
                LoadPersonsByStatus(false);
                LoadTotalAmount(currentOrderID.OrderID);
            }    
            else
            {
                MessageBox.Show("Không Thể Nhập Số Bé Hơn 0");
                return;
            }    
     

        }
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            
            RadioButton btn = (RadioButton)sender;
            
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            if(currentID ==-1 )
            {
                currentOrderID = orderBUS.ReturnOrderIdByPersonId(-1);
            }    
            OrderInfo info = orderInfoBUS.GetOrderInfoByOrderAndProduct(currentOrderID.OrderID, person.Product.ProductID);
            //MessageBox.Show(currentID.ToString());

            if (currentID == -1 && currentOrderID.Status == false)
            {
                person.orderInfo.Quantity++;
                orderInfoBUS.UpdateOrderInfo(info);
                LoadProductByIDCategory(currentCateID, -1);
                LoadOrderDTG(-1);
                LoadPersonsByStatus(false);
                LoadTotalAmount(currentOrderID.OrderID);


            }
            else if (currentID == -1 && currentOrderID.Status == true)
            {

                OrderInfo newOrderInfo = new OrderInfo
                {
                    OrderID = currentOrderID.OrderID,
                    ProductID = person.Product.ProductID,
                    Quantity = 1,
                };
                orderInfoBUS.UpdateOrderInfo(newOrderInfo);
                LoadProductByIDCategory(currentCateID, -1);
                LoadOrderDTG(-1);
                LoadPersonsByStatus(false);
                LoadTotalAmount(currentOrderID.OrderID);

            }
            else
            {
                if (info != null)
                {

                    person.orderInfo.Quantity++;
                    orderInfoBUS.UpdateOrderInfo(info);
                    LoadProductByIDCategory(currentCateID, currentID);
                    LoadOrderDTG(currentID);
                    LoadPersonsByStatus(false);
                    LoadTotalAmount(currentOrderID.OrderID);


                }
                else
                {
                    OrderInfo newOrderInfo = new OrderInfo
                    {
                        OrderID = currentOrderID.OrderID,
                        ProductID = person.Product.ProductID,
                        Quantity = 1,
                    };
                    orderInfoBUS.UpdateOrderInfo(newOrderInfo);
                    LoadProductByIDCategory(currentCateID, currentID);
                    LoadOrderDTG(currentID);
                    LoadPersonsByStatus(false);
                    LoadTotalAmount(currentOrderID.OrderID);


                }
            }





        }

        private void btnAllCateGory_Click(object sender, RoutedEventArgs e)
        {

            LoadProductByIDCategory(-1, currentID);
            LoadOrderDTG(currentID);
            currentCateID = -1;
            LoadTotalAmount(currentOrderID.OrderID);


        }


        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {

            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            int personID = person.person.PersonID;
      
        
            currentID = personID;
            
            if (currentCateID == -1)
            {
                currentOrderID = orderBUS.ReturnOrderIdByPersonId(personID) ;
                LoadProductByIDCategory(-1, currentID);
                LoadOrderDTG(personID);
                LoadCustomerInfo(personID);
                LoadTotalAmount(currentOrderID.OrderID);




            }
            else
            {
                currentOrderID = orderBUS.ReturnOrderIdByPersonId(personID);

                LoadProductByIDCategory(currentCateID, personID);

                LoadOrderDTG(personID);
                LoadCustomerInfo(personID);
                LoadTotalAmount(currentOrderID.OrderID);



            }


        }

        private void btnAllCustomer_Click(object sender, RoutedEventArgs e)
        {
            LoadPersonsByStatus(null);
        }
        private void btnPaid_Click(object sender, RoutedEventArgs e)
        {
            LoadPersonsByStatus(true);
        }
        private void btnUnPaid_Click(object sender, RoutedEventArgs e)
        {
            LoadPersonsByStatus(false);

        }

        private void btnACustomer_Click(object sender, RoutedEventArgs e)
        {
            currentID = -1;

          
                LoadProductByIDCategory(currentCateID, currentID);
                LoadOrderDTG(-1);

           
        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            int discount ;
            if (int.TryParse(txtDiscount.Text, out int discountValue))
            {
                discount = discountValue;
                if(discountValue > orderBUS.GetTotalAmount(currentOrderID.OrderID))
                {
                    MessageBox.Show("Không Được Phép Nhập Quá Tổng Tiền");
                    return;
                }   
                else
                {
                    orderBUS.UpdateDiscount(currentOrderID.OrderID, discount);
                }    
               
            }
       


        }


        private void btnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = DTGOrder; // Đảm bảo rằng DTGProduct là tên chính xác của DataGrid

            // Tạo dialog để chọn vị trí lưu file Excel
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "Excel Files|*.xlsx";
            if (saveDialog.ShowDialog() == true)
            {
                FileInfo newFile = new FileInfo(saveDialog.FileName);

                // Tạo một package mới cho file Excel
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
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

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
