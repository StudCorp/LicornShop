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
using System.Data.Entity;

namespace MP22NET.Pages.Map
{
    /// <summary>
    /// Interaction logic for ModifSection.xaml
    /// </summary>
    public partial class ModifSection : ModernDialog
    {
        public ModifSection(int section_id)
        {
            InitializeComponent();

            sectionId = section_id;

            Button ok = this.OkButton;
            ok.Content = "Valider";

            ok.Click += OkClick;

            Button cancel = this.CancelButton;
            cancel.Content = "Annuler";

            this.Buttons = new Button[] { ok, cancel };

            InitEmployeeList();
            InitProductsList();
        }

        public void InitEmployeeList()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<String> list = ctx.Employees.Select(s=> s.Id +". " + s.Firstname + " " + s.Lastname).ToList();

                Employee manager = ctx.Sections.Where(x => x.Id == sectionId).First().Employee;

                e_list.ItemsSource = list;

                e_list.SelectedValue = manager.Id + ". " + manager.Firstname + " " + manager.Lastname; 
            }
        }

        public void InitProductsList()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<Product> list_dispo = ctx.Products.Where(s => s.Quantity != 0).ToList();
                List<Product> list_notdispo = ctx.Products.Where(s => s.Quantity == 0).ToList();

                dispo_list.ItemsSource = list_dispo;
                notdispo_list.ItemsSource = list_notdispo;
            }
        }

        public void OkClick(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                Product created = ctx.Products.Create();

                String selected = e_list.SelectedValue.ToString();
                String[] r = selected.Split('.');
                int id = int.Parse(selected[0].ToString());

                Employee to_update = ctx.Employees.Where(s => s.Id == id).First();

                //to_update.Sections.Clear();
                to_update.Sections.Add(ctx.Sections.Where(s => s.Id == sectionId).First());

                ctx.Entry(to_update).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        public int sectionId
        {
            get;
            set;
        }
    }
}
