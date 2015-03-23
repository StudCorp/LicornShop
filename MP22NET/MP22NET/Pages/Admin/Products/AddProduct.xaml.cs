using FirstFloor.ModernUI.Windows.Controls;
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

namespace MP22NET.Pages.Admin.Products
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : ModernDialog
    {
        public AddProduct()
        {
            InitializeComponent();

            Button ok = this.OkButton;
            ok.Content = "Valider";

            ok.Click += OkClick;

            Button cancel = this.CancelButton;
            cancel.Content = "Annuler";

            this.Buttons = new Button[] { ok, cancel };

            PopulateSections();
            PopulateCategories();
        }

        public void OkClick(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                Product created = ctx.Products.Create();

                created.Name = ProductName.Text;
                created.Brand = ProductBrand.Text;
                created.Quantity = int.Parse(ProductQuantity.Text);
                created.Price = int.Parse(ProductPrice.Text);

                created.Section = ctx.Sections.Where(s => s.Name == section_list.SelectedValue.ToString()).First();
                created.Category = ctx.Categories.Where(s => s.Name == category_list.SelectedValue.ToString()).First();


                ctx.Products.Add(created);

                ctx.SaveChanges();
            }
        }

        public void PopulateSections()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<String> list = ctx.Sections.Select(s => s.Name).ToList();

                section_list.ItemsSource = list;
            }
        }
        public void PopulateCategories()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<String> list = ctx.Categories.Select(s => s.Name).ToList();

                category_list.ItemsSource = list;
            }
        }
    }
}
