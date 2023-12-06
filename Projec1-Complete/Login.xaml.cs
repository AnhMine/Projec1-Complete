using MaterialDesignThemes.Wpf;
using Projec1_Complete.BUS;
using Projec1_Complete.Pages;
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

namespace Projec1_Complete
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static Login Instance { get; private set; }
        public AccountBUS accountBUS;
        private MainWindow mainWindow;
        public Login()
        {
            InitializeComponent();
            accountBUS = new AccountBUS();
            Instance = this;
        }
        private void LoginForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_thoatForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_anForm_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_DangNhap_Click(object sender, RoutedEventArgs e)
        {
            string username = txt_Email.Text;
            string password = psw_MatKhau.Password;
            if (cbRememberpsw.IsChecked == true)
            {
                Properties.Settings.Default.username = username;
                Properties.Settings.Default.password = password.ToString();
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
            }

            bool isValid = accountBUS.CheckAccount(username, password);
            string type = accountBUS.CheckType(username);

            if (isValid)
            {
                if(type == "Admin")
                {
                    mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Hide();
                }    
                else
                {
                
                }    
                
            }
            else
            {
                MessageBox.Show("Sai Tên Tài Khoản Hoặc Mật Khẩu");
            }
        }

        private void btn_QuenMatKhau_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            this.Hide();
            forgotPassword.ShowDialog();
        }

        private void btnShowPsw_Click(object sender, RoutedEventArgs e)
        {
            if(psw_MatKhau.Visibility == Visibility.Visible)
            {
                txtShowPass.Text = psw_MatKhau.Password;
                psw_MatKhau.Visibility = Visibility.Collapsed;
                txtShowPass.Visibility = Visibility.Visible;
            }    
            else
            {
                psw_MatKhau.Password = txtShowPass.Text;
                txtShowPass.Visibility = Visibility.Collapsed;
                psw_MatKhau.Visibility = Visibility.Visible;
            }
        }
        private void FormLogin_Loaded(object sender, RoutedEventArgs e)
        {
            txt_Email.Text = Properties.Settings.Default.username;
            psw_MatKhau.Password = Properties.Settings.Default.password;
            if(txt_Email.Text != "" && psw_MatKhau.Password != "")
            {
                cbRememberpsw.IsChecked = true;
            }
            else
            {
                cbRememberpsw.IsChecked = false;
            }
        }
        private void cbRememberpsw_Checked(object sender, RoutedEventArgs e)
        {
            if(cbRememberpsw.IsChecked == true)
            {
                Properties.Settings.Default.username = txt_Email.Text;
                Properties.Settings.Default.password = psw_MatKhau.Password.ToString();
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
            }
        }

       
    }
}
