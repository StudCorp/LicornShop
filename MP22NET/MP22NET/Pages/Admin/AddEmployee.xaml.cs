using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.IO;
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

using Microsoft.Win32;

namespace MP22NET.Pages.Admin
{
    /// <summary>
    /// Interaction logic for ModernDialog1.xaml
    /// </summary>
    public partial class AddEmployee : ModernDialog
    {
        public AddEmployee()
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
                Employee created = ctx.Employees.Create();

                created.Lastname = Lastname.Text;
                created.Firstname = Firstname.Text;

                String icon_path = AvatarButton.Content.ToString();

                created.Icon = icon_path.Replace("\\", "\\\\");

                ctx.Employees.Add(created);

                ctx.SaveChanges();
            }
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg ";

            if (fd.ShowDialog() == true)
                AvatarButton.Content = fd.FileName;
        }
    }
}
