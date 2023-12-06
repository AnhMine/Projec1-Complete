using OfficeOpenXml.Style;
using Projec1_Complete.BUS;
using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Donhang.xaml
    /// </summary>
    public partial class Donhang : Page
        
    {
        public ObservableCollection<Category> categories { get; set; }
        public ObservableCollection<Person> persons { get; set; }
        private CategoryBUS categoryBUS;
        private PersonBUS personBUS;
        private ProductBUS productBUS;
        private OrderBUS orderBUS;
        public Donhang()
        {
            InitializeComponent();
            DataContext = this;
            categories = new ObservableCollection<Category>();
            persons = new ObservableCollection<Person>();
            personBUS = new PersonBUS();
            categoryBUS = new CategoryBUS();
            productBUS = new ProductBUS();
            orderBUS = new OrderBUS();
            LoadCategory();
            LoadPersonsByStatus(null);
        }
        private void LoadOrderDTG(int id)
        {
            List<OrdersAndCustomers> orders = orderBUS.GetOrdersByPersonId(id);
            DTGOrder.ItemsSource = orders;
    
        }

        private void LoadCategory()
        {
            List<Category> categorieslist = categoryBUS.GetCategories();
            itctCate.ItemsSource = categorieslist;
            
        }
        private void LoadAllProduct()
        {
            List<Product> productList = productBUS.GetProducts();
            itctCate3.ItemsSource = productList;
        }
        private void LoadProductByIDCategory(int id)
        {
            List<Product> prdList = categoryBUS.GetProductByID(id);
            itctCate3.ItemsSource = prdList;
        }
        //private void LoadPerson()
        //{
        //    List<Person> perList = personBUS.GetListCustomer();
        //    List<PersonOrder> personOrderlist = new List<PersonOrder>();

        //    foreach (Person personItem in perList)
        //    {
        //        bool status = orderBUS.GetStatus(personItem.PersonID);
        //        string statusText = status ? "Đã Thanh Toán" : "Chưa Thanh Toán";

        //        PersonOrder personOrder = new PersonOrder
        //        {
        //            PerSon = personItem,
        //            DisplayStatus = statusText,
        //        };
        //        personOrderlist.Add(personOrder);
        //    }

        //    itctPeople.ItemsSource = personOrderlist;
        //}
        private void LoadPersonsByStatus(bool? status)
        {
            List<Person> perList = personBUS.GetListCustomer();
            List<PersonOrder> personOrderlist = new List<PersonOrder>();

            foreach (Person personItem in perList)
            {
                bool personStatus = orderBUS.GetStatus(personItem.PersonID);
                int OrderID = orderBUS.GetOrder(personItem.PersonID);
                decimal totalAmount = orderBUS.GetTotalAmount(OrderID);
                
                string statusText = personStatus == true ? "Đã Thanh Toán" : personStatus == false ? "Chưa Thanh Toán" : "";
                statusText = statusText ?? "";

                PersonOrder personOrder = new PersonOrder
                {
                    PerSon = personItem,
                    DisplayStatus = statusText,
                    TotalAmount = totalAmount,
                };
                personOrderlist.Add(personOrder);
            }

            // Sắp xếp danh sách theo trạng thái (status)
            if (status.HasValue)
            {
                personOrderlist = personOrderlist.Where(o => orderBUS.GetStatus(o.PerSon.PersonID) == status.Value).ToList();
            }

            itctPeople.ItemsSource = personOrderlist;
        }






        private void CategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Button cateBtn = (Button)sender;
            Category cate = (Category)cateBtn.DataContext;
            int categoryID = cate.CategoryID;
            LoadProductByIDCategory(categoryID);
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

        private void btnAllCateGory_Click(object sender, RoutedEventArgs e)
        {
            LoadAllProduct();
        }

        private void btnRetailCustomers_Click(object sender, RoutedEventArgs e)
        {
               LoadOrderDTG(0);
        }
        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            PersonOrder person = (PersonOrder)btn.DataContext;
            int personID = person.PerSon.PersonID;
           
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
