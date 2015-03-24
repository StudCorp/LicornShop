using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP22NET.Pages.Admin.Stats
{
    class MoneySectionModel
    {
        public ObservableCollection<DataPoint> Errors { get; private set; }

        public MoneySectionModel()
        {
            Errors = new ObservableCollection<DataPoint>();
            Errors.Add(new DataPoint() { Category = "Globalization", Number = 75 });
            Errors.Add(new DataPoint() { Category = "Features", Number = 2 });
            Errors.Add(new DataPoint() { Category = "ContentTypes", Number = 12 });
            Errors.Add(new DataPoint() { Category = "Correctness", Number = 83});
            Errors.Add(new DataPoint() { Category = "Best Practices", Number = 29 });
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                // selected item has changed
                selectedItem = value;                
            }
        }
    }

     // class which represent a data point in the chart
    public class DataPoint
    {
        public string Category { get; set; }

        public int Number  { get; set; }        
    }
}
