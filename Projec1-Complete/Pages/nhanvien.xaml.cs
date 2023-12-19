using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for nhanvien.xaml
    /// </summary>
    public partial class nhanvien : Page
    {
        private bool isPasswordVisible = false;
        private string oldPsw;
        private string oldName;
        private string oldAddress;
        private string oldEmail;
        private string oldPhone;
        private PersonBUS personBUS;
        private string selectedImagePath;

        //public nhanvien()
        //{
        //    InitializeComponent();
        //}
        public nhanvien(Employees employees)
        {
            InitializeComponent();
            personBUS = new PersonBUS();

            DataContext = employees;
           
            pswChangePass.Password = employees.account.Password;
            oldPsw = employees.account.Password;
            oldName = employees.person.PersonName;
            oldAddress = employees.person.Address;
            oldEmail = employees.person.Email;
            oldPhone = employees.person.Phone;
            
        }
        void checkTextChanged()
        {
            if (txtChangePass.Text != oldPsw ||txtName.Text != oldName|| txtAddress.Text != oldAddress || txtEmail.Text != oldEmail|| txtPhone.Text != oldPhone)
            {
                btnSave.IsEnabled = true;
                btnSave.Cursor = Cursors.Hand;
            }
       
        }
        void LoadPage()
        {
            Employees employees = (Employees)DataContext;
            txtName.Text = employees.person.PersonName;
            txtAddress.Text = employees.person.Address;
            txtEmail.Text = employees.person.Email;
            txtPhone.Text = employees.person.Phone;
            txtChangePass.Text = employees.account.Password;
            pswChangePass.Password = employees.account.Password;

        }
        private void btnShowPswnv_Click(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                pswChangePass.Password = txtChangePass.Text; 

                txtChangePass.Visibility = Visibility.Visible;
                pswChangePass.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtChangePass.Text = pswChangePass.Password;
                txtChangePass.Visibility = Visibility.Collapsed;
                pswChangePass.Visibility = Visibility.Visible;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Employees employees = (Employees)DataContext;
            employees.person.PersonImage = selectedImagePath;
            personBUS.UpdateEmployees(employees);
            MessageBox.Show("Đã Thay Đổi Thành Công");
            LoadPage();
              
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextChanged();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextChanged();
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextChanged();
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextChanged();
        }

        private void txtChangePass_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextChanged();

        }

        private void pswChangePass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkTextChanged();

        }

        //private void imgEmployee_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp|All Files (*.*)|*.*";

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        selectedImagePath = openFileDialog.FileName;
        //        imgEmployee.Source = new BitmapImage(new Uri(selectedImagePath));
        //    }
        //    else
        //    {
        //        imgEmployee.Source = new BitmapImage(new Uri("/Assets/Icons/WL.jpg", UriKind.Relative));
        //    }
        //}

        //private void imgEmployee_Loaded(object sender, RoutedEventArgs e)
        //{

        //    if (imgEmployee.Source != null)
        //    {
        //        btnImg.Visibility = Visibility.Hidden;
        //    }
        //}


        private void imgElEmployee_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                imgBrEmployee.ImageSource = new BitmapImage(new Uri(selectedImagePath));
            }
    
        }

        private void imgElEmployee_Loaded(object sender, RoutedEventArgs e)
        {

            if (imgBrEmployee.ImageSource != null)
            {
                btnImg.Visibility = Visibility.Hidden;
            }
        }
    }
}
