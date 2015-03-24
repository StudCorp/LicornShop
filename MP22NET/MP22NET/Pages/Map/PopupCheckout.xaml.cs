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

namespace MP22NET.Pages.Map
{
    /// <summary>
    /// Interaction logic for ModernDialog1.xaml
    /// </summary>
    public partial class CheckoutPopup : ModernDialog
    {
        public int totalCost;

        public Checkout checkout;

        public Product selectedProduct;
        public Employee selectedEmployee;
        public List<CartItem> cart { get; set; }
        public CheckoutPopup(Checkout c)
        {
            InitializeComponent();

            checkout = c;
            

            ShowListProducts();

            totalCost = 0;

            cart = new List<CartItem>();

            txtNum.Text = _numValue.ToString();

            p_list.SelectionChanged += p_list_SelectionChanged;

            Button ok = OkButton;
            ok.Content = "Valider les achats";
            ok.Click += okClick;

            Button cancel = CancelButton;
            cancel.Content = "Annuler";

            // define the dialog buttons
            this.Buttons = new Button[] { ok, cancel };

            PopulateEmployees();

        }


        public void okClick(object sender, RoutedEventArgs e)
        {
            using (var ctx = new MP22NETEntities1())
            {

                String selected = e_list.SelectedValue.ToString();
                String[] r = selected.Split('.');
                int id = int.Parse(selected[0].ToString());

                // Mise à jour de l'employé

                Employee to_update = ctx.Employees.Where(s => s.Id == id).First();

                to_update.Checkouts.Add(ctx.Checkouts.Where(s => s.Id == checkout.Id).First());

                ctx.Entry(to_update).State = System.Data.Entity.EntityState.Modified;

                // Mise a jour des produits

                foreach(var x in cart)
                {
                    Product e_to_update = ctx.Products.Where(s => s.Id == x.p.Id).First();
                    e_to_update.Quantity -= x.Quantity;
                    e_to_update.Section.Benefit += int.Parse(x.Cost);
                    ctx.Entry(e_to_update).State = System.Data.Entity.EntityState.Modified;
                }

                // Mise a jour des bénéfices
                Checkout c_to_update = ctx.Checkouts.Where(s => s.Id == checkout.Id).First();
                c_to_update.Benefit += totalCost;
                ctx.Entry(c_to_update).State = System.Data.Entity.EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        public void PopulateEmployees()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<String> list = ctx.Employees.Select(s => s.Id + ". " + s.Firstname + " " + s.Lastname).ToList();

                e_list.ItemsSource = list;

                e_list.SelectedValue = checkout.Employee.Id + ". " + checkout.Employee.Firstname + " " + checkout.Employee.Lastname;
            }
        }

        public void ShowListProducts()
        {
            using (var ctx = new MP22NETEntities1())
            {
                List<Product> list = ctx.Products.Where(s=>s.Quantity > 0).ToList();

                p_list.ItemsSource = list;
            }
        }

        public void ShowCart()
        {
            cart_list.Items.Clear();
            foreach (var x in cart)
            {
                cart_list.Items.Add(x);
            }
        }

        private void p_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedValue == null)
            {
                return;
            }

            Product selected = ((sender as ListBox).SelectedValue as Product);

            this.selectedProduct = selected;

            NumValue = 0;

        }


        // Minus/Plus Control

        private int _numValue = 0;
        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
                BuyCost.Text = (value * selectedProduct.Price).ToString();
            }
        }

        public int QuantityLeft()
        {
            int left = selectedProduct.Quantity;

            foreach(var x in cart)
            {
                if (x.Name == selectedProduct.Name)
                {
                    left -= x.Quantity;
                }
            }

            return left;
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            if (NumValue + 1 > QuantityLeft())
                return;
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            if (NumValue > 0)
                NumValue--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out _numValue))
                txtNum.Text = _numValue.ToString();
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            if (QuantityLeft() <= 0)
            {
                NumValue = 0;
                return;
            }

            CartItem i = new CartItem { p = selectedProduct, Quantity = NumValue };
            CartItem temp = exists(i);

            if (temp == null)
            {
                cart.Add(i);
            }
            else
            {
                int index = cart.IndexOf(temp);
                cart.ElementAt(index).Quantity += i.Quantity;
            }

            NumValue = 0;

            CalculateTotalCost();

            ShowCart();
        }

        public void CalculateTotalCost()
        {
            totalCost = 0;

            foreach(var x in cart)
            {
                totalCost += int.Parse(x.Cost);
            }

            TotalCost.Text = totalCost.ToString();
        }

        private CartItem exists(CartItem i)
        {
            foreach (var item in cart)
            {
                if (item.Name == i.Name)
                    return item;
            }

            return null;
        }

    }

    public class CartItem
    {
        public Product p { get; set; }
        public int Quantity { get; set; }

        public String Cost { get { return (Quantity * p.Price).ToString(); } set { Cost = value; } }

        public String Name { get { return p.Name; } set { Name = value; } }
    }



}
