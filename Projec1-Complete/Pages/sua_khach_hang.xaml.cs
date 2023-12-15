﻿using Projec1_Complete.BUS;
using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for sua_khach_hang.xaml
    /// </summary>
    public partial class sua_khach_hang : Window
    {
        private PersonBUS personBUS;
        public sua_khach_hang()
        {
            InitializeComponent();
        }
        public sua_khach_hang(Person person, Khachhang  kh)
        {
            InitializeComponent();
            personBUS = new PersonBUS();
            DataContext = person;
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsNumeric(string value)
        {
            return value.All(char.IsDigit);
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_suatenkh_Click(object sender, RoutedEventArgs e)
        {
            txtB_suakh_TenKH.IsEnabled = true;
        }

        private void btn_suasdtkh_Click(object sender, RoutedEventArgs e)
        {
            txtB_suakh_sdt.IsEnabled = true;
        }

        private void btn_suaemailkh_Click(object sender, RoutedEventArgs e)
        {
            txtB_suakh_email.IsEnabled = true;
        }

        private void btn_suadiachikh_Click(object sender, RoutedEventArgs e)
        {
            txtB_suakh_Diachi.IsEnabled = true;
        }

        private void btnSavePerson_Click(object sender, RoutedEventArgs e)
        {
            Person selectedCustomer = (Person)DataContext;

            if (selectedCustomer != null)
            {
                if (selectedCustomer.PersonName.Length > 255)
                {
                    MessageBox.Show("Tên khách hàng không được vượt quá 255 kí tự.");
                    return;
                }

                if (!IsValidEmail(selectedCustomer.Email))
                {
                    MessageBox.Show("Địa chỉ email không hợp lệ.");
                    return;
                }

                if (selectedCustomer.Phone.Length != 10 || !IsNumeric(selectedCustomer.Phone))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số.");
                    return;
                }

                if (selectedCustomer.Address.Length > 255)
                {
                    MessageBox.Show("Địa chỉ không được vượt quá 255 kí tự.");
                    return;
                }

                personBUS.UpdateCustomer(selectedCustomer);
                MessageBox.Show("Đã Cập Nhật Thành Công");
            }
        }
    }
}
