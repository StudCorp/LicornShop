﻿using FirstFloor.ModernUI.Windows.Controls;
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
    public partial class DeleteEmployee : ModernDialog
    {
        public DeleteEmployee()
        {
            InitializeComponent();

            Button cancel = CancelButton;
            cancel.Content = "Annuler";

            Button ok = OkButton;
            ok.Content = "Supprimé";

            // define the dialog buttons
            this.Buttons = new Button[] { ok, cancel };
        }
    }
}
