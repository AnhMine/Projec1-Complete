using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Security.Cryptography;

namespace Projec1_Complete
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        private string storedOtpHash;
        private string storedEmail;
        private DateTime storedOtpCreationTime;
        public ForgotPassword()
        {
            InitializeComponent();
        }
        private void SendOTPByEmail(string email, string otp)
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("quanaovnqas@gmail.com");
            mail.To.Add(email);
            mail.Subject = "OTP từ Shop Quần Áo VNQAS";
            mail.Body =otp.ToString();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            System.Net.NetworkCredential ntcd = new NetworkCredential();
            ntcd.UserName = "quanaovnqas@gmail.com";
            ntcd.Password = "bnbp rvaq ceje iddj";
            smtpServer.Credentials = ntcd;
            smtpServer.Port = 587;

            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);
            MessageBox.Show("OTP đã gửi thành công! Vui lòng kiểm tra email");


        }

        private string GenerateOTPAndSendEmail(string email)
        {
            Random random = new Random();
            string otp = random.Next(0, 999999).ToString();
            SendOTPByEmail(email, otp);
            return otp;
            
        }
       
        private void SaveOtpSecurely(string email, string otp)
        {
            string hashedOtp = HashOtp(otp);

            storedOtpHash = hashedOtp;
            storedEmail = email;
            storedOtpCreationTime = DateTime.Now;
        }

        private string HashOtp(string otp)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] otpBytes = Encoding.UTF8.GetBytes(otp);
                byte[] hashBytes = sha256.ComputeHash(otpBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
        private void btn_XacNhan_Click(object sender, RoutedEventArgs e)
        {
            spnl_thayDoiMatKhau.Visibility = Visibility.Visible;
            spnl_QuaenMatKhau.Visibility = Visibility.Collapsed;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void btnSendOTP_Click(object sender, RoutedEventArgs e)
        {
            string email = txt_Email.Text;
           string otp = GenerateOTPAndSendEmail(email);
            SaveOtpSecurely(email,otp);

            txt_MaOTP.Visibility = Visibility.Visible;
            ((Button)sender).Visibility = Visibility.Collapsed;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
