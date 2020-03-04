using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApp.Models {
    public class Item {
        private long? _Id; //could be a "SKU" type value easily, just a unique identifier
        public long? Id { //nullable long because you have to cast to it and it "future-proofs" the design (int should work fine in most cases)
            get { return _Id ?? 0; }
            set {
                if(value < 0 || value > long.MaxValue) throw new ArgumentOutOfRangeException($"A item ID [{nameof(value)}] must be between 0 and {long.MaxValue} (recieved: '{value}').");
                _Id = value;
            }
        }
        public string Name { get; set; }
        public decimal Price { get; set; } = 5; //int could work as well (but not ideal)
        public short? Quantity { get; set; }
        public DateTime? AvalibleUntil { get; set; }
        public override string ToString() {
            var temp = GetAvalibleUntilRaw();
            if(temp != null) temp = $", Listing Expires: {temp}";
            return $"Product: {GetName()}, Price {GetPrice()}, Number Avalible: {GetQuantity()}" + temp;
        }
        public string ToTableRow() {
            return $"<tr><td>{GetName()}{GetAvalibleUntil()}</td><td>{GetPrice()}</td><td>{GetQuantity()}</td><td><button>Add To Cart</button></td></tr>{Environment.NewLine}"; //could easily adjust date to pop up on a certain number of days in advance of expiration and such
        }
        public string GetNameRaw() {
            return Name ?? String.Empty;
        }
        public string GetName() {
            return Name ?? "Product Name Unavalible";
        }
        public string GetPrice() {
            return $"{Math.Abs(Price):C}"; //String.Format
        }
        public string GetQuantity() {
            return (Quantity ?? 0).ToString();
        }
        public string GetAvalibleUntilRaw() {
            return AvalibleUntil.HasValue ? AvalibleUntil.Value.ToString() : String.Empty;
        }
        public string GetAvalibleUntil() {
            return AvalibleUntil.HasValue ? $" [Expires: {AvalibleUntil.Value}]" : String.Empty;
        }
    }

}
