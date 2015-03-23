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
using FirstFloor.ModernUI.Windows;
using Microsoft.Win32;


namespace MP22NET.Pages.Admin
{
    /// <summary>
    /// Interaction logic for SplitPage3.xaml
    /// </summary>
    public partial class EmployeesPage : UserControl
    {
        public Employee selectedEmployee;
        public EmployeesPage()
        {
            InitializeComponent();

            UpdateList();

            //this.DataContext = new EmployeesViewModel();
            selectedEmployee = null;


            e_list.SelectionChanged += e_list_SelectionChanged;


        }

        public void UpdateList()
        {
            List<Employee> list = new List<Employee>();

            using (var ctx = new MP22NETEntities1())
            {
                list = ctx.Employees.ToList();

            }

            e_list.ItemsSource = list;

        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            var d = new AddEmployee();

            bool? r = d.ShowDialog();

            if (r == true)
            {
                UpdateList();
            }
        }


        private void e_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedValue == null)
            {
                splitter.Visibility = Visibility.Hidden;
                modif.Visibility = Visibility.Hidden;
                return;
            }

            Employee selected = ((sender as ListBox).SelectedValue as Employee);

            this.selectedEmployee = selected;

            Firstname.Text = selected.Firstname;
            Lastname.Text = selected.Lastname;

            Image.Source = new BitmapImage(new Uri(@selected.Icon));
            AvatarButton.Content = "Changer l'avatar...";

            splitter.Visibility = Visibility.Visible;
            modif.Visibility = Visibility.Visible;
        }

        private void AvatarButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg ";

            if (fd.ShowDialog() == true)
            {
                AvatarButton.Content = fd.FileName;
                Image.Source = new BitmapImage(new Uri(@fd.FileName));
            }
                
        }

        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {
                Employee to_update = ctx.Employees.Where(s => s.Id == selectedEmployee.Id).First();

                if (to_update != null)
                {
                    to_update.Lastname = Lastname.Text;
                    to_update.Firstname = Firstname.Text;

                    String icon_path = AvatarButton.Content.ToString();

                    to_update.Icon = icon_path.Replace("\\", "\\\\");

                    ctx.Entry(to_update).State = System.Data.Entity.EntityState.Modified;

                    ctx.SaveChanges();


                }
            }

            UpdateList();
        }
        private void CancelModif(object sender, RoutedEventArgs e)
        {
            Firstname.Text = selectedEmployee.Firstname;
            Lastname.Text = selectedEmployee.Lastname;
            Image.Source = new BitmapImage(new Uri(selectedEmployee.Icon));
            AvatarButton.Content = "Changer l'avatar...";
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            var d = new DeleteEmployee();

            // On demande la confirmation
            bool? r = d.ShowDialog();

            if (r == true) // Suppression confirmée
            {
                using (var ctx = new MP22NETEntities1())
                {
                    Employee to_delete = ctx.Employees.Where(s => s.Id == selectedEmployee.Id).First();

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
