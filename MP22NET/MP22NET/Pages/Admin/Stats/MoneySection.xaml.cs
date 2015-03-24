using MP22NET.Pages.Admin.Stats;
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

namespace MP22NET.Pages.Stats
{
    /// <summary>
    /// Interaction logic for MoneySection.xaml
    /// </summary>
    public partial class MoneySection : UserControl
    {
        public MoneySection()
        {
            InitializeComponent();

            this.DataContext = new MoneySectionModel();
        }
    }
}
