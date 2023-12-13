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
    }
}
