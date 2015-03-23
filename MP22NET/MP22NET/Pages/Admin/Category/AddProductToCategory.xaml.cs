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

namespace MP22NET.Pages.Admin
{
    /// <summary>
    /// Interaction logic for ModernDialog1.xaml
    /// </summary>
    public partial class AddPtoC : ModernDialog
    {
        public AddPtoC()
        {
            InitializeComponent();

            Button ok = this.OkButton;
            ok.Content = "Valider";

            ok.Click += OkClick;

            Button cancel = this.CancelButton;
            cancel.Content = "Annuler";

            this.Buttons = new Button[] { ok, cancel };

            using (var ctx = new MP22NETEntities1())
            {
                List<String> list = ctx.Products.Select(s=> s.Name).ToList();

                combo.ItemsSource = list;
            }
        }

        public void OkClick(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                String nameSelected = combo.SelectedValue.ToString();


                Product to_modif = ctx.Products.Where(s => s.Name == nameSelected).First();
                to_modif.Category = ctx.Categories.Where(s=> s.Id == c_id).First();

                ctx.Entry(to_modif).State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();

            }
        }

        public int c_id
        {
            get;
            set;
        }
    }
}
