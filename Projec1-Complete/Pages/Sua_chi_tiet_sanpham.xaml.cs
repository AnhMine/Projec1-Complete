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
using System.Windows.Shapes;

namespace Projec1_Complete.Pages
{
    /// <summary>
    /// Interaction logic for Sua_chi_tiet_sanpham.xaml
    /// </summary>
    public partial class Sua_chi_tiet_sanpham : Window
    {

        public CategoryBUS categoryBUS;
        public ProductBUS productBUS;
        private Khohang kh;
        private string selectedImagePath;
        public Sua_chi_tiet_sanpham()
        {
            InitializeComponent();
        }
        public Sua_chi_tiet_sanpham(Product product, Khohang khohang, string cate)
        {
            InitializeComponent();
            categoryBUS = new CategoryBUS();
            productBUS = new ProductBUS();
            kh = khohang;
            txtCategory.Text = cate;
            DataContext = product;
            kh.LoadPage();
            imagePrd.Loaded += btnImage_Loaded;

        }

        private bool IsNumeric(string value)
        {
            return value.All(char.IsDigit);
        }

        private void btnImage_Loaded(object sender, RoutedEventArgs e)
        {

            if (imagePrd.Source != null)
            {
                btnImage.Visibility = Visibility.Hidden;
                bdImg.Visibility = Visibility.Hidden;
            }
        }
        private void btn_huysuasp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

            kh.LoadPage();

        }



        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            txtName.IsEnabled = true;
        }

        private void btnPriceSell_Click(object sender, RoutedEventArgs e)
        {
            txtPriceSell.IsEnabled = true;
        }

        private void btnPrice_Click(object sender, RoutedEventArgs e)
        {
            txtPrice.IsEnabled = true;
        }

        private void btnDes_Click(object sender, RoutedEventArgs e)
        {
            txtDes.IsEnabled = true;
        }

        private void btnQuantity_Click(object sender, RoutedEventArgs e)
        {
            txtQuantity.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Product selectedProduct = (Product)DataContext;
            selectedProduct.ImageLink = selectedImagePath;

            if (selectedProduct != null)
            {
                if (selectedProduct.ProductName.Length > 50)
                {
                    MessageBox.Show("Tên sản phẩm không được vượt quá 50 kí tự.");
                    return;
                }

                if (selectedProduct.Quantity > 100000)
                {
                    MessageBox.Show("Số lượng sản phẩm không được vượt quá 100,000.");
                    return;
                }

                if (selectedProduct.Price > 10000000)
                {
                    MessageBox.Show("Giá sản phẩm không được vượt quá 10,000,000.");
                    return;
                }

                if (selectedProduct.PriceSell > 10000000)
                {
                    MessageBox.Show("Giá bán sản phẩm không được vượt quá 10,000,000.");
                    return;
                }


                productBUS.UpdateProduct(selectedProduct);
                MessageBox.Show("Lưu Thành Công");
                this.Close();
                kh.LoadPage();
            }
        }

        private void imagePrd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                imagePrd.Source = new BitmapImage(new Uri(selectedImagePath));
            }
            else
            {
                imagePrd.Source = new BitmapImage(new Uri("/Assets/Icons/anhpic-removebg-preview.png", UriKind.Relative));
            }
        }

        private void txtPriceSell_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
