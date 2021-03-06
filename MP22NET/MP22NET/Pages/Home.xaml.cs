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
using System.Drawing;
using FirstFloor.ModernUI.Windows.Controls;

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
                    button = new Button { Content = x.Name, Tag = x.Name, Width = 200, Height = 50 };
                    button.Background = System.Windows.Media.Brushes.YellowGreen;
                    button.BorderBrush = System.Windows.Media.Brushes.YellowGreen;
                    button.Click += ClickSection;
                    Canvas.SetLeft(button, x.s_left);
                    Canvas.SetTop(button, x.s_top);
                    canvas1.Children.Add(button);
                    RotateTransform rotateTransform1 =
                    new RotateTransform(90);
                    button.RenderTransform = rotateTransform1;
                }
                //Affichage des Checkouts
                List<Checkout> checkouts = db.Checkouts.ToList();
                foreach (Checkout x in checkouts)
                {
                    var streamGeometry = StreamGeometry.Parse("F1 M 39.9317,58.3617L 39.9317,53.1984C 42.9656,52.6781 45.2439,51.5757 46.7665,49.8912C 48.2891,48.2067 49.0508,46.2045 49.0517,43.8847C 49.0819,41.5743 48.4428,39.652 47.1343,38.1177C 45.8259,36.5835 43.6669,35.2898 40.6573,34.2367C 38.4191,33.4616 36.8047,32.741 35.8143,32.0749C 34.8239,31.4088 34.3385,30.6754 34.3583,29.8746C 34.3347,29.124 34.6508,28.4567 35.3066,27.8726C 35.9625,27.2885 37.0998,26.9804 38.7187,26.9483C 40.6959,26.9847 42.2936,27.2073 43.5116,27.616C 44.7296,28.0248 45.6405,28.4013 46.2442,28.7456L 47.7221,23.0015C 46.8926,22.5841 45.8837,22.2172 44.6955,21.901C 43.5073,21.5847 42.082,21.395 40.4195,21.3317L 40.4195,16.8783L 35.4538,16.8783L 35.4538,21.6859C 32.6538,22.2213 30.5179,23.2883 29.046,24.8868C 27.5742,26.4853 26.8327,28.4387 26.8217,30.747C 26.8607,33.2007 27.6817,35.1573 29.2847,36.6167C 30.8876,38.076 33.0384,39.25 35.7369,40.1384C 37.636,40.7639 39.0728,41.4246 40.0474,42.1206C 41.0219,42.8167 41.5111,43.6442 41.515,44.6032C 41.4785,45.6114 40.9661,46.3919 39.9779,46.9446C 38.9896,47.4974 37.7444,47.7775 36.2423,47.785C 34.6009,47.7647 33.0786,47.5359 31.6755,47.0986C 30.2723,46.6613 29.0485,46.1374 28.004,45.527L 26.5176,51.4439C 27.4094,51.9849 28.6021,52.4569 30.0958,52.8601C 31.5894,53.2632 33.2003,53.5096 34.9282,53.5994L 34.9282,58.3617L 39.9317,58.3617 Z ");
                    button = new ModernButton { Content = x.Name, Tag = x.Name, IconData = streamGeometry, IconWidth = 35, IconHeight = 35, EllipseDiameter = 45, FontSize = 20, Height = 50 };
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
            String s_name = (sender as Button).Tag.ToString();

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
            CheckoutPopup d = null;

            String s_name = (sender as ModernButton).Tag.ToString();

            using (var ctx = new MP22NETEntities1())
            {
                Checkout checkout_clicked = ctx.Checkouts.Where(s => s.Name == s_name).First();
                d = new CheckoutPopup(checkout_clicked);
            }

            bool? r = d.ShowDialog();

        }
    }
}