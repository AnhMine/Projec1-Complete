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
            LoadPerson();
        }
        private void LoadOrderDTG(int id)
        {
            
           /* OrdersAndCustomers firstOrder = orders.FirstOrDefault();

            if (firstOrder != null)
            {
                if(firstOrder.order.Status == true && firstOrder.order.PersonID == 0)
                {
                    tblStatus.Text = "Đã Thanh Toán";
                    tblStatus.Foreground = new SolidColorBrush(Color.FromRgb(0, 204, 51)); ;
                }
                else if (firstOrder.order.Status == true && firstOrder.order.PersonID == 0)
                {
                    tblStatus.Text = "Chưa Thanh Toán";

                    tblStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
             
                
                tblTotalAmount.Text = firstOrder.TotalAmount.ToString(); 
            }
            else
            {
                tblStatus.Text = "Chưa Thanh Toán";
                tblTotalAmount.Text = "0";
            }*/
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
        private void LoadPerson()
        {
            List<Person> perList = personBUS.GetListCustomer();
            List<PersonOrder> personOrder = new List<PersonOrder>;
            foreach (Person personItem in perList)
            {

            }
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
            Person person = (Person)btn.DataContext;
            int personID = person.PersonID;

            LoadOrderDTG(personID);
        }
    }
}
