using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository;
namespace bestpricedaily.Models
{
    public class Item : BaseEntity
    {
        //public Guid sku { get; set; }
        public float price { get; set; }
        public string name { get; set; }
        public string des { get; set; }
        public string pix { get; set; }
        //public int quantityAvailable { get; set; }
    }

}
