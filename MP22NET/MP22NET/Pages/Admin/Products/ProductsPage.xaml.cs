using MP22NET.Pages.Admin.Products;
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
using System.Data.Entity;

namespace MP22NET.Pages.Admin
{
    /// <summary>
    /// Interaction logic for SplitPage1.xaml
    /// </summary>
    public partial class ProductsPage : UserControl
    {

        public Product selectedProduct;
        public ProductsPage()
        {
            InitializeComponent();

            UpdateList();

            selectedProduct = null;

            p_list.SelectionChanged += p_list_SelectionChanged;
        }

        public void UpdateList()
        {
            

            using (var ctx = new MP22NETEntities1())
            {
                List<Product> list = ctx.Products.Include(s=> s.Category).Include(s=> s.Section).ToList();

                p_list.ItemsSource = list;

            }

            

        }

        public void UpdateRelatedLists()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<String> c_list = ctx.Categories.Select(s => s.Name).ToList();
                List<String> s_list = ctx.Sections.Select(s => s.Name).ToList();


                ProductSection.ItemsSource = s_list;
                ProductCategory.ItemsSource = c_list;

                ProductCategory.SelectedValue = ctx.Categories.Where(s => s.Id == selectedProduct.Category.Id).Select(s => s.Name).First();
                ProductSection.SelectedValue = ctx.Sections.Where(s => s.Id == selectedProduct.Section.Id).Select(s => s.Name).First();

                
            }

            
        }

        private void AddProduct_click(object sender, RoutedEventArgs e)
        {
            var d = new AddProduct();

            bool? r = d.ShowDialog();

            if (r == true)
            {
                UpdateList();
            }
        }


        private void p_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedValue == null)
            {
                splitter.Visibility = Visibility.Hidden;
                modif.Visibility = Visibility.Hidden;
                return;
            }

            Product selected = ((sender as ListBox).SelectedValue as Product);

            this.selectedProduct = selected;

            ProductName.Text = selected.Name;
            ProductBrand.Text = selected.Brand;
            ProductPrice.Text = selected.Price.ToString();
            ProductQuantity.Text = selected.Quantity.ToString();

            splitter.Visibility = Visibility.Visible;
            modif.Visibility = Visibility.Visible;

            UpdateRelatedLists();
        }

        private void UpdateProduct(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                Product to_update = ctx.Products.Where(s => s.Id == selectedProduct.Id).First();

                if (to_update != null)
                {
                    to_update.Name = ProductName.Text;
                    to_update.Brand = ProductBrand.Text;
                    to_update.Quantity = int.Parse(ProductQuantity.Text);
                    to_update.Price = int.Parse(ProductPrice.Text);

                    to_update.Category = ctx.Categories.Where(s => s.Name == ProductCategory.SelectedValue.ToString()).First();
                    to_update.Section = ctx.Sections.Where(s => s.Name == ProductSection.SelectedValue.ToString()).First();


                    ctx.Entry(to_update).State = System.Data.Entity.EntityState.Modified;

                    ctx.SaveChanges();


                }
            }

            UpdateList();
        }
        private void CancelModif(object sender, RoutedEventArgs e)
        {
            ProductName.Text = selectedProduct.Name;
            ProductBrand.Text = selectedProduct.Brand;
            ProductPrice.Text = selectedProduct.Price.ToString();
            ProductQuantity.Text = selectedProduct.Quantity.ToString();
            using (var ctx = new MP22NETEntities1())
            {
                ProductCategory.SelectedValue = ctx.Categories.Where(s => s.Id == selectedProduct.Category.Id).Select(s => s.Name).First();
                ProductSection.SelectedValue = ctx.Sections.Where(s => s.Id == selectedProduct.Section.Id).Select(s => s.Name).First();
            }
            
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var d = new DeleteProduct();

            // On demande la confirmation
            bool? r = d.ShowDialog();

            if (r == true) // Suppression confirmée
            {
                using (var ctx = new MP22NETEntities1())
                {
                    Product to_delete = ctx.Products.Where(s => s.Id == selectedProduct.Id).First();

                    if (to_delete != null)
                    {
                        ctx.Entry(to_delete).State = System.Data.Entity.EntityState.Deleted;

                        ctx.SaveChanges();
                    }
                }

                UpdateList();
            }
        }
    }
}
