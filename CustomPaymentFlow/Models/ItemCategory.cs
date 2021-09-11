using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPaymentFlow.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Price { get; set; }
        public bool Selected { get; set; }
    }

    public class ViewModel {
        public List<Item> Items { get; set; } = new List<Item>
        {
            new Item{
                Name = "HP Laptop",
                Price = 60000,
                Id = "1",
            },
            new Item{
                Name = "Dell Laptop",
                Price = 40000,
                Id = "2",
            },
            new Item{
                Name = "lenovo Laptop",
                Price = 100000,
                Id = "3",
            }
        };
    }
}
