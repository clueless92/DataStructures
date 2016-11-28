namespace Pr03CollectionOfProducts
{
    using System;

    public class Product : IComparable<Product>
    {
        public Product(string id, string title, string supplier, decimal price)
        {
            this.ID = id;
            this.Title = title;
            this.Supplier = supplier;
            this.Price = price;
        }

        public string ID { get; set; }

        public string Title { get; set; }

        public string Supplier { get; set; }

        public decimal Price { get; set; }
        public int CompareTo(Product other)
        {
            return String.Compare(this.ID, other.ID, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            Product other = obj as Product;
            return String.Equals(this.ID, other.ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
