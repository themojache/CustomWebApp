using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApp.Models {
    public static class Inventory { //not the best way to handle inventory
        public static Item[] Items { get; set; }
        static Inventory() {
            Items = new Item[] { new Item { Name = "Lamp", Price = (decimal)3.50, Quantity = 10000 }, new Item { Quantity = 12, Name = "Metal Switch Box", Price = (decimal)5.50 }, new Item { Quantity = 32, Name = "Bulb", Price = (decimal)2.01 }, new Item { Quantity = 32, Name = "LED Light", Price = (decimal)3.01 }, new Item { Quantity = 50, Name = "Headband LED Light", Price = (decimal)12.01 }, new Item { Quantity = 7, Name = "Wrist LED Light", Price = (decimal)4.31 }, new Item { Quantity = 12, Name = "Ethernet", Price = (decimal)99.99, AvalibleUntil = new DateTime(2025, 10, 25) } };
        }
        public static List<Item> Search(string query) {
            return Items.Where(item => item.Name.ToLower().Contains(query.ToLower())).ToList(); //LINQ is not the fastest means, but convenient
        }
    }
}
