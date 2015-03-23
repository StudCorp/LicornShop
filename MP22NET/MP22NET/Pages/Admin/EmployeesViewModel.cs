using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MP22NET.Pages.Admin
{
    
    class EmployeesViewModel 
        : NotifyPropertyChanged
    {
        public string edition;
        public Employee selectedEmployee;
        

        public EmployeesViewModel()
        {
            edition = "Visible";
        }

        public string editionVisibility
        {
            get { return this.edition; }
            set { this.edition = value; }
        }

        public Employee SelectedEmployee
        {
            get { return this.selectedEmployee; }
            set { 
                this.selectedEmployee = value;
                editionVisibility = "Hidden";
            }
        }
    }
}
