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

namespace MP22NET.Pages.Admin
{
    /// <summary>
    /// Interaction logic for SplitPage2.xaml
    /// </summary>
    /// 


    public partial class CategoryPage : UserControl
    {
        public Category selectedCategory;

        public CategoryPage()
        {
            InitializeComponent();

            c_list.SelectionChanged += c_list_SelectionChanged;


            UpdateList();
        }

        public void UpdateList()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<Category> list = ctx.Categories.ToList();

                c_list.ItemsSource = list;
            }
        }

        public void UpdateProductsList()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<Product> products = ctx.Products.Where(s => s.Category.Id == selectedCategory.Id).ToList();

                p_list.ItemsSource = products;
            }
        }

        public void AddCategory (object sender, RoutedEventArgs e)
        {
            var d = new AddCategory();

            bool? r = d.ShowDialog();

            if (r == true)
            {
                UpdateList();
            }
        }

        public void AddProductToCategory(object sender, RoutedEventArgs e)
        {
            var d = new AddPtoC();
            d.c_id = selectedCategory.Id;

            bool? r = d.ShowDialog();

            if (r == true)
            {
                UpdateProductsList();
            }
        }

        public void UpdateCategory(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                Category to_update = selectedCategory;
                to_update.Name = CategoryName.Text;

                ctx.Entry(to_update).State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();
            }

            UpdateList();
        }

        public void CancelModif(object sender, RoutedEventArgs e)
        {
            CategoryName.Text = selectedCategory.Name;
        }

        public void DeleteCategory(object sender, RoutedEventArgs e)
        {
            var d = new DeleteCategory();

            // On demande la confirmation
            bool? r = d.ShowDialog();

            if (r == true) // Suppression confirmée
            {
                using (var ctx = new MP22NETEntities1())
                {
                    Category to_delete = ctx.Categories.Where(s => s.Id == selectedCategory.Id).First();

                    if (to_delete != null)
                    {
                        Category other = ctx.Categories.Where(s => s.Id != selectedCategory.Id).First();

                        List<Product> products = ctx.Products.Where(s => s.Category.Id == to_delete.Id).ToList();

                        foreach (Product p in products )
                        {
                            p.Category = other;
                            ctx.Entry(p).State = System.Data.Entity.EntityState.Modified;
                        }

                        ctx.SaveChanges();

                        ctx.Entry(to_delete).State = System.Data.Entity.EntityState.Deleted;

                        ctx.SaveChanges();
                    }
                }

                UpdateList();
            }
        }

        private void c_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedValue == null)
            {
                modif.Visibility = Visibility.Hidden;
                splitter.Visibility = Visibility.Hidden;
                return;
            }

            Category selected = ((sender as ListBox).SelectedValue as Category);

            this.selectedCategory = selected;

            CategoryName.Text = selected.Name;

            UpdateProductsList();

            modif.Visibility = Visibility.Visible;
            splitter.Visibility = Visibility.Visible;
        }

        public Category SelectedCategory
        {
            get;
            set;
        }
    }
}
