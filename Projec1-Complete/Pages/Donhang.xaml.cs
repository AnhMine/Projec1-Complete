
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
using System.Diagnostics.Eventing.Reader;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Donhang.xaml
    /// </summary>
    public partial class Donhang : Page

    {
       bool flag = false;
        string search = "";
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
       
            personBUS = new PersonBUS();
            categoryBUS = new CategoryBUS();
            orderBUS = new OrderBUS();
            orderInfoBUS = new OrderInfoBUS();
            currentCateID = -1;
            //currentID = 0;
            txtTotalAmount.Text = orderBUS.GetTotalAmount(-1).ToString();
            LoadCategory();
            LoadPersonsByStatus(false);

        }
         bool convertStatus(string DisplayStatus)
        {
            
            if (DisplayStatus == "Đã Thanh Toán")
            {
                return true;
            }    
            else
            {
                return false;
            }    
        }
        void LoadTotalAmount(int orderID)
        {
            txtTotalAmount.Text = orderBUS.GetTotalAmount(orderID).ToString();
        }
      
     
        private void LoadCustomerInfo(int id)
        {
            List<Person> people = personBUS.GetInfoPerson(id);
            itctPersonId.ItemsSource = people;
        }
        private void LoadOrderDTG(int id, int orderId)
        {

            if(id == -10 && orderId ==-10)
            {

                DTGOrder.ItemsSource = null;
            }   
            else
            {
                ObservableCollection<ProductAndOrderInfo> listUnPaid = new ObservableCollection<ProductAndOrderInfo>();

                List<ProductAndOrderInfo> orders = orderBUS.GetOrdersByPersonId2(id, orderId);
                foreach (ProductAndOrderInfo oi in orders)
                {
                    if (oi.order.Status == false)
                    {

                        listUnPaid.Add(oi);
                    }

                }

                DTGOrder.ItemsSource = listUnPaid;
            }    
         


        }
        private void LoadOrderDTGPaid(int id, int orderid )
        {
            ObservableCollection<ProductAndOrderInfo> listPaid = new ObservableCollection<ProductAndOrderInfo>();

            List<ProductAndOrderInfo> orders = orderBUS.GetOrdersByPersonId2(id,orderid);
            foreach (ProductAndOrderInfo oi in orders)
            {
                if (oi.order.Status == true)
                {
                   
                    listPaid.Add(oi);
                }

            }

            DTGOrder.ItemsSource = listPaid;
        }

        private void LoadCategory()
        {
            List<Category> categorieslist = categoryBUS.GetCategories();
            itctCate.ItemsSource = categorieslist;

        }
       
       
        private void LoadProductByIDCategory(int categoryId, int personId,int orderid)
        {
            List<ProductAndOrderInfo> prdList = categoryBUS.GetProductByCateAndPersonId(categoryId, personId,orderid);
            itctProduct.ItemsSource = prdList;
        }

        private void LoadPersonsByStatus(bool status)
        {
            List<Order> orderList = orderBUS.GetOrderList(status);
            List<ProductAndOrderInfo> personOrderlist = new List<ProductAndOrderInfo>();

            foreach (Order order in orderList)
            {
                decimal totalAmount = orderBUS.GetTotalAmount(order.OrderID);
                string statusText = orderBUS.GetStatus(order.OrderID) == true ? "Đã Thanh Toán" : "Chưa Thanh Toán";

                ProductAndOrderInfo product = new ProductAndOrderInfo
                {
                    person = orderBUS.GetPersonByOrderId(order.OrderID),
                    TotalAmount = totalAmount,
                    DisplayStatus = statusText
                };

                personOrderlist.Add(product);
            }

            itctPeople.ItemsSource = personOrderlist;

        }
        private void LoadPeople(string search)
        {


            List<Order> orderList = orderBUS.GetListOrderBySearch(search);
            List<ProductAndOrderInfo> personOrderlist = new List<ProductAndOrderInfo>();

            foreach (Order order in orderList)
            {
                decimal totalAmount = orderBUS.GetTotalAmount(order.OrderID);
                string statusText = orderBUS.GetStatus(order.OrderID) == true ? "Đã Thanh Toán" : "Chưa Thanh Toán";

                Person person = personBUS.SerachPerson(search, order.OrderID);

                if (person != null)
                {
                    ProductAndOrderInfo product = new ProductAndOrderInfo
                    {
                        person = person,
                        TotalAmount = totalAmount,
                        DisplayStatus = statusText
                    };

                    personOrderlist.Add(product);
                }
            }

            personOrderlist = personOrderlist
                .OrderByDescending(p => p.person.PersonName == "Khách Lẻ")
                .ThenBy(p => p.person.PersonName)
                .ToList();

            itctPeople.ItemsSource = personOrderlist;




        }





        private void CategoryBtn_Click(object sender, RoutedEventArgs e)
        {

            if (currentOrderID != null)
            {
                RadioButton btn = (RadioButton)sender;
                Category prd = (Category)btn.DataContext;
                int categoryID = prd.CategoryID;
                int personID = currentID;
                currentCateID = categoryID;
                LoadProductByIDCategory(categoryID, personID, currentOrderID.OrderID);
                LoadOrderDTG(personID, currentOrderID.OrderID);

            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước <3");
                return;
            }    

        }

        

        private void btn_thanhtoan_Click(object sender, RoutedEventArgs e)
        {
            stpnDTG.Visibility = Visibility.Visible;
            stpn_sanpham.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Visible;
            btn_quayve.Visibility = Visibility.Visible;
          
                LoadOrderDTG(currentID, currentOrderID.OrderID);

        

        }

        private void btn_quayve_Click(object sender, RoutedEventArgs e)
        {
            stpn_sanpham.Visibility = Visibility.Visible;
            stpnDTG.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Collapsed;
            LoadProductByIDCategory(currentCateID,currentID,currentOrderID.OrderID);
        }

       
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {

            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            OrderInfo info = orderInfoBUS.GetOrderInfoByOrderAndProduct(currentOrderID.OrderID, person.Product.ProductID);
            if(info != null)
            {

                person.orderInfo.Quantity--;
                orderInfoBUS.RemoveProductFromOrder(info);
                LoadProductByIDCategory(currentCateID, currentID, currentOrderID.OrderID);
                LoadTotalAmount(currentOrderID.OrderID);
                if (search.Trim() == "")
                {
                    LoadPersonsByStatus(false);
                }
                else
                {
                    LoadPeople(search);
                }

               
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

            OrderInfo info = orderInfoBUS.GetOrderInfoByOrderAndProduct(currentOrderID.OrderID, person.Product.ProductID);

            if (info != null)
            {
                //person.orderInfo.Quantity++;
                info.Quantity++;
                orderInfoBUS.UpdateOrderInfo(info);
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
            }

            LoadProductByIDCategory(currentCateID, currentID, currentOrderID.OrderID);

            if (search.Trim() == "")
            {
                LoadPersonsByStatus(false);
            }
            else
            {
                LoadPeople(search);
            }

            LoadTotalAmount(currentOrderID.OrderID);
            LoadOrderDTG(currentID, currentOrderID.OrderID);
        }


        private void btnAllCateGory_Click(object sender, RoutedEventArgs e)
        {
            if(currentOrderID==null)
            {
                MessageBox.Show("Vui Lòng Chọn Khách Hàng Trước <3");
                return;
                
            } 
            else
            {
              
                    LoadProductByIDCategory(-1, currentID, currentOrderID.OrderID);
                    LoadOrderDTG(currentID, currentOrderID.OrderID);
                    currentCateID = -1;

                
            }
           



        }


        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            btn_thanhtoan.Visibility = Visibility.Visible;
            stpnDTG.Visibility = Visibility.Visible;
            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            int personID = person.person.PersonID;
            currentID = personID;
            currentOrderID = orderBUS.ReturnOrderIdByPersonId(personID, convertStatus(person.DisplayStatus));
            if (currentCateID == -1)
            {
               if(person.DisplayStatus == "Chưa Thanh Toán")
                {
                    LoadProductByIDCategory(-1, currentID, currentOrderID.OrderID);
                    LoadOrderDTG(personID, currentOrderID.OrderID);
                    LoadCustomerInfo(personID);
                    LoadTotalAmount(currentOrderID.OrderID);
                    txtTotalAmount.Text = Convert.ToString(person.TotalAmount);
                    txtDiscount.Text = Convert.ToString(currentOrderID.Discount);

                }
               else
                {
                    btn_quayve.Visibility = Visibility.Hidden;
                    LoadOrderDTGPaid(personID, currentOrderID.OrderID);

                    txtTotalAmount.Text = Convert.ToString(person.TotalAmount);
                    txtDiscount.Text = Convert.ToString(currentOrderID.Discount);


                }

            }
            else
            {
                if(person.DisplayStatus == "Chưa Thanh Toán")
                {
                    LoadProductByIDCategory(currentCateID, personID, currentOrderID.OrderID);

                    LoadOrderDTG(personID, currentOrderID.OrderID);
                    LoadCustomerInfo(personID);
                    LoadTotalAmount(currentOrderID.OrderID);
                                        txtTotalAmount.Text = Convert.ToString(person.TotalAmount);
                    txtDiscount.Text = Convert.ToString(currentOrderID.Discount);


                }

                else
                {
                    btn_quayve.Visibility = Visibility.Hidden;
                    LoadOrderDTGPaid(personID, currentOrderID.OrderID);
                    txtTotalAmount.Text = Convert.ToString(person.TotalAmount);
                    txtDiscount.Text = Convert.ToString(currentOrderID.Discount);


                }


            }


        }

     
        private void btnPaid_Click(object sender, RoutedEventArgs e)
        {
            LoadPersonsByStatus(true);
            stpn_sanpham.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Hidden;
            btnPay.Visibility = Visibility.Hidden;
            btnDiscount.Visibility = Visibility.Hidden;
            txtDiscount.IsEnabled = false;
            flag = true;
            LoadOrderDTG(-10, -10);
        }
        private void btnUnPaid_Click(object sender, RoutedEventArgs e)
        {
            LoadCategory();

            stpn_sanpham.Visibility = Visibility.Visible;
            stpnDTG.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Collapsed;
            btnPay.Visibility = Visibility.Visible;
            btnDiscount.Visibility = Visibility.Visible;
            txtDiscount.IsEnabled = true;
            LoadPersonsByStatus(false);
            flag = false;
            //LoadOrderDTG(-10, -10);

            //LoadOrderDTG(currentID, currentOrderID.OrderID);

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
                    LoadOrderDTG(currentID, currentOrderID.OrderID);
                    LoadTotalAmount(currentOrderID.OrderID);
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
            
            MessageBoxResult result = MessageBox.Show("Bạn Chắc Chắn Muốn Thanh Toán Cho Đơn Hàng Này ?", "Xác Nhận Thanh Toán", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
  
                    orderBUS.UpdateStatus(currentOrderID.OrderID);
                    LoadPersonsByStatus(false);
                

            }
            else
            {
                return;
            }
        }
        private void btnPlusOrder_Click(object sender, RoutedEventArgs e)
        {


            orderBUS.CreateNewOrder(currentID);


        }

        private void txtSearchCustomers_TextChanged(object sender, TextChangedEventArgs e)
        {
            
          search =   txtSearchCustomers.Text  ;
            if(search.Trim()== null)
            {
                LoadPersonsByStatus(false );
            }
            else
            {
                LoadPeople(search);

            }

        }

        private void txtSearchPrd_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtSearchPrd.Text;
            if(currentOrderID==null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng");
                return;
            }
            else
            {
                List<ProductAndOrderInfo> prdList = categoryBUS.SearchProductByText(search, currentID, currentOrderID.OrderID);
                itctProduct.ItemsSource = prdList;
            }
         
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ProductAndOrderInfo prd = (ProductAndOrderInfo)btn.DataContext;
            MessageBoxResult result = MessageBox.Show("Bạn Chắc Chắn Muốn Xóa Sản Phẩm Này Ra Khỏi Đơn Hàng ?", "Xác Nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                orderInfoBUS.DelProduct(prd.Product.ProductID,currentOrderID.OrderID);
                LoadOrderDTG(currentID, currentOrderID.OrderID);
                LoadTotalAmount(currentOrderID.OrderID);
                LoadPersonsByStatus(false);

            }
            else
            {
                return;
            }


        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            stpnDTG.Visibility = Visibility.Visible;
            stpn_sanpham.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Visible;
            btn_quayve.Visibility = Visibility.Visible;
            LoadOrderDTG(currentID, currentOrderID.OrderID);
        }
    }
}
