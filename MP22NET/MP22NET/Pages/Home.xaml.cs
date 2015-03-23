﻿using System;
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
using MP22NET.Pages.Map;

namespace MP22NET.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, IContent
    {
        private Button button = null;

        public Home()
        {
            InitializeComponent();

            InitMap();
        }
        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            InitMap();
        }
        public void InitMap()
        {
            canvas1.Children.Clear();
            using (var db = new MP22NETEntities1())
            {
                //affichage des Sections
                List<Section> sections = db.Sections.ToList();
                foreach (Section x in sections)
                {
                    button = new Button { Content = x.Name, Width = 50, Height = 50 };
                    button.Click += ClickSection;
                    Canvas.SetLeft(button, x.s_left);
                    Canvas.SetTop(button, x.s_top);
                    canvas1.Children.Add(button);
                }
                //Affichage des Checkouts
                List<Checkout> checkouts = db.Checkouts.ToList();
                foreach (Checkout x in checkouts)
                {
                    button = new Button { Content = x.Name, Width = 50, Height = 50 };
                    button.Click += ClickCheckout;
                    Canvas.SetLeft(button, x.c_left);
                    Canvas.SetTop(button, x.c_top);
                    canvas1.Children.Add(button);
                }
            }
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }
        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        public void ClickSection(object sender, RoutedEventArgs e)
        {
            String s_name = (sender as Button).Content.ToString();

            ModifSection d = null;

            using (var ctx = new MP22NETEntities1())
            {
                Section section_clicked = ctx.Sections.Where(s => s.Name == s_name).First();
                d = new ModifSection(section_clicked.Id);
            }


            bool? r = d.ShowDialog();

        }

        public void ClickCheckout(object sender, RoutedEventArgs e)
        {
            var d = new CheckoutPopup();

            bool? r = d.ShowDialog();

        }
    }
}
