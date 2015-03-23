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
    public partial class AddCategory : ModernDialog
    {
        public AddCategory()
        {
            InitializeComponent();

            Button ok = this.OkButton;
            ok.Content = "Valider";

            ok.Click += OkClick;

            Button cancel = this.CancelButton;
            cancel.Content = "Annuler";

            this.Buttons = new Button[] { ok, cancel };
        }

        public void OkClick(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                Category created = ctx.Categories.Create();

                created.Name = CategoryName.Text;

                ctx.Categories.Add(created);

                ctx.SaveChanges();
            }
        }
    }
}
