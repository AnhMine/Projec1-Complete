using Projec1_Complete.BUS;
using Projec1_Complete.DAL;
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
using System.Windows.Shapes;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for themmoi_khachhang.xaml
    /// </summary>
    public partial class themmoi_khachhang : Window
    {
        private PersonBUS personBUS;
        public themmoi_khachhang()
        {
            InitializeComponent();
            personBUS = new PersonBUS();
        }
        private void btn_huythemkhachhang_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            Person customer = new Person
            {

                PersonName = txt_TenKH.Text.Trim(),
                Email = txt_email.Text.Trim(),
                Phone = txt_sdt.Text.Trim(),
                Address = txt_Diachi.Text.Trim(),
                DateSave = DateTime.Now,
                Type = "Khách Hàng",
                PersonImage = "/Assets/Icons/WL.jpg",
            };
            if (string.IsNullOrEmpty(customer.PersonName))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!");
                return;
            }
            personBUS.AddNewCustomer(customer);
            MessageBox.Show("Thêm khách hàng mới thành công!");
            TextBox[] txt = new TextBox[] { txt_TenKH, txt_email, txt_sdt, txt_Diachi };
            foreach (TextBox t in txt)
            {
                t.Text = string.Empty;
            }
        }
    }
}
