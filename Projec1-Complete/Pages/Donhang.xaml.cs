using OfficeOpenXml.Style;
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
using static Projec1_Complete.DAL.CategoryDAL;

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
            currentID = 0;
            currentCateID = 1;
            LoadCategory();
            LoadPersonsByStatus(null);
            GetProductInfo(1);

        }

        private int GetCurrentPersonID()
        {
            return currentID;
        }
        private int GetCateID()
        {
            return currentCateID;
        }
        private void LoadOrderDTG(int id)
        {
            List<OrdersAndCustomers> orders = orderBUS.GetOrdersByPersonId(id);
            DTGOrder.ItemsSource = orders;
    
        }

        private void LoadCategory()
        {
            List<Category> categorieslist = categoryBUS.GetCategories();
            //List<ProductAndOrderInfo> categorieslist = categoryBUS.GetCateGory();
            itctCate.ItemsSource = categorieslist;

        }
   
        private void LoadProductByIDCategory(int categoryId,int personId)
        {
            List<ProductAndOrderInfo> prdList = categoryBUS.GetProductByCateAndPersonId(categoryId,personId);
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
            List<ProductAndOrderInfo> personOrderlist = new List<ProductAndOrderInfo>();

            foreach (Person personItem in perList)
            {
                bool personStatus = orderBUS.GetStatus(personItem.PersonID);
                int OrderID = orderBUS.GetOrder(personItem.PersonID);
                decimal totalAmount = orderBUS.GetTotalAmount(OrderID);
                
                string statusText = personStatus == true ? "Đã Thanh Toán" : personStatus == false ? "Chưa Thanh Toán" : "";
                statusText = statusText ?? "";

                ProductAndOrderInfo personOrder = new ProductAndOrderInfo
                {
                    person = personItem,
                    DisplayStatus = statusText,
                    TotalAmount = totalAmount,
                };
                personOrderlist.Add(personOrder);
            }

            // Sắp xếp danh sách theo trạng thái (status)
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
       
        private void  btnProductOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            
            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;

            Order order = person.order;
          

            OrderInfo info = orderInfoBUS.GetOrderInfoByOrderAndProduct(order.OrderID, person.Product.ProductID);
            if (info != null)
            {
                info.Quantity++;
                orderInfoBUS.UpdateOrderInfo(info);
            }
            else
            {
                OrderInfo newOrderInfo = new OrderInfo
                {
                    OrderID = order.OrderID,
                    ProductID = person.Product.ProductID,
                    Quantity = 1,
                };
                orderBUS.AddProductToOrder(order, newOrderInfo, (short)newOrderInfo.Quantity);
            }



            LoadProductByIDCategory(currentID, currentID);
            LoadOrderDTG(currentID);
        }
        
        private void btnAllCateGory_Click(object sender, RoutedEventArgs e)
        {

            ///*LoadAllProduct*/();

        }

        private void btnRetailCustomers_Click(object sender, RoutedEventArgs e)
        {
               LoadOrderDTG(0);
        }
        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            int personID = person.person.PersonID;
            currentID = personID;
            LoadProductByIDCategory(currentCateID, personID);
            LoadOrderDTG(personID);


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
    }
}
