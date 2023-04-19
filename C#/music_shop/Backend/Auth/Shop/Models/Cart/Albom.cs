using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.Cart
{
    public class Albom
    {   
        public int Albom_id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Img_path { get; set; }
        public int Price { get; set; }
    }
}
