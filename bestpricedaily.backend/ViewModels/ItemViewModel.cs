using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestpricedaily.ViewModels
{
    public class ItemViewModel
    {
        public Guid id { get; set; }
        public float price { get; set; }
        public string name { get; set; }
        public string des { get; set; }
        public string pix { get; set; }
    }
}
