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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        private int currentOrderID;
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
            currentID = -1;
            currentCateID = -1;
            LoadCategory();
            LoadPersonsByStatus(null);
            GetProductInfo(1);

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
       
        private void LoadProductByPersonID(int id)
        {

            List<ProductAndOrderInfo> prdList = categoryBUS.GetProductsByPersonId(id);
            
            itctProduct.ItemsSource = prdList;
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

        }
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {

            RadioButton btn = (RadioButton)sender;
            ProductAndOrderInfo person = (ProductAndOrderInfo)btn.DataContext;
            OrderInfo info = orderInfoBUS.GetOrderInfoByOrderAndProduct(currentOrderID, person.Product.ProductID);
            //MessageBox.Show(info.Order.Status.ToString());
            if (info != null)
            {

                person.orderInfo.Quantity++;
                orderInfoBUS.UpdateOrderInfo(info);
                LoadProductByIDCategory(currentCateID, currentID);
                LoadProductByPersonID(currentID);
                LoadOrderDTG(currentID);
                LoadPersonsByStatus(null);
                
            }
            else
            {
                OrderInfo newOrderInfo = new OrderInfo
                {
                    OrderID = currentOrderID,
                    ProductID = person.Product.ProductID,
                    Quantity = 1,
                };
                orderInfoBUS.UpdateOrderInfo(newOrderInfo);
                LoadProductByIDCategory(currentCateID, currentID);
                LoadProductByPersonID(currentID);
                LoadOrderDTG(currentID);
                LoadPersonsByStatus(null);

            }




        }

        private void btnAllCateGory_Click(object sender, RoutedEventArgs e)
        {

            LoadProductByPersonID(currentID);
            LoadOrderDTG(currentID);
            currentCateID = -1;

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
            
            if (currentCateID == -1)
            {
                currentOrderID = orderBUS.ReturnOrderIdByPersonId(personID) ;
                LoadProductByPersonID(currentID);
                LoadOrderDTG(personID);
                LoadCustomerInfo(personID);


            }
            else
            {
                currentOrderID = orderBUS.ReturnOrderIdByPersonId(personID);

                LoadProductByIDCategory(currentCateID, personID);

                LoadOrderDTG(personID);
                LoadCustomerInfo(personID);


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

            if (currentCateID == -1)
            {
                LoadProductByPersonID(currentID);
                LoadOrderDTG(-1);

            }
            else
            {
                LoadProductByIDCategory(currentCateID, -1);
                LoadOrderDTG(-1);

            }
        }

     
       
    }
}
