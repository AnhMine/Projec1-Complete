﻿using Projec1_Complete.BUS;
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
        public Donhang()
        {
            InitializeComponent();
            DataContext = this;
            categories = new ObservableCollection<Category>();
            persons = new ObservableCollection<Person>();
            personBUS = new PersonBUS();
            categoryBUS = new CategoryBUS();
            LoadCategory();
            LoadPerson();
        }
        private void LoadCategory()
        {
            List<Category> categorieslist = categoryBUS.GetCategories();
            itctCate.ItemsSource = categorieslist;
            
        }
        private void LoadProductByIDCategory(int id)
        {
            List<Product> prdList = categoryBUS.GetProductByID(id);
            itctCate3.ItemsSource = prdList;
        }
        private void LoadPerson()
        {
            List<Person> perList = personBUS.GetListCustomer();
            itctPeople.ItemsSource = perList;
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
        private Button lastClickedButton;
      

        private void btn_sp1_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp2_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp3_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;

        }

        private void btn_sp4_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;

        }

        private void btn_sp5_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp6_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp7_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_sp8_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6741d9"));
            lastClickedButton = button;


        }

        private void btn_khohang_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_quayve_Click(object sender, RoutedEventArgs e)
        {
            stpn_invoices.Visibility = Visibility.Visible;
            stpn_packages.Visibility = Visibility.Collapsed;
            spr_duonggach.Visibility = Visibility.Collapsed;
            btn_quayve.Visibility = Visibility.Collapsed;
        }

    }
}
