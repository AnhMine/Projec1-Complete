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
    /// Interaction logic for themmoi_sanpham.xaml
    /// </summary>
    public partial class themmoi_sanpham : Window
    {

        public ProductBUS productBUS;
        public CategoryBUS categoryBUS;
        public string selectedImagePath;
        public themmoi_sanpham()
        {
            InitializeComponent();
            productBUS = new ProductBUS();
            categoryBUS = new CategoryBUS();
            LoadCombobox();
        }
        void LoadCombobox()
        {
            List<Category> cate = categoryBUS.GetCategories();
            cboCate.ItemsSource = cate;
            cboCate.DisplayMemberPath = "CategoryName";
            cboCate.SelectedIndex = 0;
            List<string> activeOptions = new List<string>
             {
               "Mới",
               "Đã qua sử dụng",
             };
            cboStatus.ItemsSource = activeOptions;
            cboStatus.SelectedIndex = 0;
        }
        private void btn_huythemspmoi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = cboCate.Text;
            Category selectedCategory = cboCate.Items.Cast<Category>().FirstOrDefault(x => x.CategoryName == categoryName);

            string status = cboStatus.SelectedValue?.ToString();

            if (selectedImagePath == null)
            {
                selectedImagePath = "/Assets/Icons/anhpic-removebg-preview.png";
            }

            Product prd = new Product
            {
                CategoryID = selectedCategory.CategoryID,
                ProductName = txtNamePrd.Text,
                Quantity = short.TryParse(txtQuantity.Text, out short quantity) ? quantity : (short)0,
                Price = decimal.TryParse(txtPrice.Text, out decimal price) ? price : 0,
                PriceSell = decimal.TryParse(txtPriceSell.Text, out decimal priceSell) ? priceSell : 0,
                Status = status,
                DateImport = DateTime.Now,
                Description = "...",

                ImageLink = selectedImagePath
            };

            if (status == "Mới")
            {
                prd.ProductID = GetNewProductID();
            }
            else
            {
                prd.ProductID = GetOldProductID();
            }
            productBUS.AddProduct(prd);
            MessageBox.Show("Thêm sản phẩm mới thành công!");

            txtNamePrd.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtPriceSell.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }
        private string GetNewProductID()
        {
            string latestProductID = productBUS.GetLastestProductID();

            if (string.IsNullOrEmpty(latestProductID))
            {
                return "NEW0001";
            }
            else
            {
                int currentNumber = int.Parse(latestProductID.Substring(3));
                int nextNumber = currentNumber + 1;
                string nextNumberString = nextNumber.ToString().PadLeft(4, '0');

                string newProductID = "NEW" + nextNumberString;


                while (productBUS.ProductIDExists(newProductID))
                {
                    nextNumber++;
                    nextNumberString = nextNumber.ToString().PadLeft(4, '0');

                    newProductID = "NEW" + nextNumberString;
                }

                return newProductID;
            }
        }
        private string GetOldProductID()
        {
            string latestProductID = productBUS.GetLastestProductID();

            if (string.IsNullOrEmpty(latestProductID))
            {
                return "OLD0001";
            }
            else
            {
                int currentNumber = int.Parse(latestProductID.Substring(3));
                int nextNumber = currentNumber + 1;
                string nextNumberString = nextNumber.ToString().PadLeft(4, '0');
                string oldProductID = "OLD" + nextNumberString;
                while (productBUS.ProductIDExists(oldProductID))
                {
                    nextNumber++;
                    nextNumberString = nextNumber.ToString().PadLeft(4, '0');
                    oldProductID = "OLD" + nextNumberString;
                }
                return oldProductID;
            }
        }

        private void imgPrd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                imgPrd.Source = new BitmapImage(new Uri(selectedImagePath));
            }
        }
    }
}
