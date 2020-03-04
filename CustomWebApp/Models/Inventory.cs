using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApp.Models {
    public static class Inventory {
        public static Item[] Items { get; set; } //might as well store this list globally and only once
        static Inventory() { //not the best way to handle inventory
            Items = new Item[] { new Item { Name = "Lamp", Price = (decimal)3.50, Quantity = 10000 }, new Item { Quantity = 12, Name = "Metal Switch Box", Price = (decimal)5.50 }, new Item { Quantity = 32, Name = "Bulb", Price = (decimal)2.01 }, new Item { Quantity = 32, Name = "LED Light", Price = (decimal)3.01 }, new Item { Quantity = 50, Name = "Headband LED Light", Price = (decimal)12.01 }, new Item { Quantity = 7, Name = "Wrist LED Light", Price = (decimal)4.31 }, new Item { Quantity = 12, Name = "Ethernet", Price = (decimal)99.99, AvalibleUntil = new DateTime(2025, 10, 25) } };
        }
        public static List<Item> Search(string query) {
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("SELECT ID, NAME, PRICE, QUANTITY, LISTINGEXPIRATION FROM PRODUCT WHERE LOWER(NAME) LIKE @Terms OR LOWER(DESCRIPTION) LIKE @Terms", con)) { //I assume everything is stored in one table here, whereas it would be better for content to be better normalized
                    cmd.Parameters.Add("@Terms", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@Terms"].Value = $"%{query.ToLower().Trim('%')}%"; //should check with whitespace, to prevent some over matching
                    try {
                        var ItemList = new LinkedList<Item>();
                        con.Open();
                        using(var reader = cmd.ExecuteReader()) {
                            while(reader.Read()) {
                                var tempItem = new Item();
                                for(int i = 0; i < reader.FieldCount; i++)
                                    switch(reader.GetName(i).ToLower()) {
                                        case "id":
                                            tempItem.Id = reader.GetInt32(i);
                                            break;
                                        case "name":
                                            tempItem.Name = reader.GetString(i);
                                            break;
                                        case "price":
                                            tempItem.Price = reader.GetDecimal(i);
                                            break;
                                        case "quantity":
                                            tempItem.Quantity = reader.GetInt16(i); //we will assume it was input as a short and doesn't need error handling/etc here
                                            break;
                                        case "listingexpiration":
                                            tempItem.AvalibleUntil = reader.GetDateTime(i); //normally should validate
                                            break;
                                    }
                                ItemList.AddLast(tempItem); //doubly linked list, preforms quickly
                            }
                        }
                        con.Close();
                        return ItemList.ToList(); //converts LinkedList to a generic List
                    } catch { //Usually you want specific exception's listed here for different handling, I know that it will likely not have a DB connection
                        return Items.Where(item => item.Name.ToLower().Contains(query.ToLower())).ToList(); //LINQ is not the fastest means, but convenient
                    }
                }
            }
        }
    }
}
