namespace Pr03CollectionOfProducts
{
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class ProductData
    {
        private readonly Dictionary<string, Product> productByID;
        private readonly OrderedDictionary<decimal, HashSet<Product>> productsByPrice;
        public readonly Dictionary<string, SortedSet<Product>> productsByTitle;
        private readonly Dictionary<string, OrderedDictionary<decimal, HashSet<Product>>> productsByTitleAndPrice;
        private readonly Dictionary<string, OrderedDictionary<decimal, HashSet<Product>>> productsBySupplierAndPrice;

        public ProductData()
        {
            this.productByID = new Dictionary<string, Product>();
            this.productsByPrice = new OrderedDictionary<decimal, HashSet<Product>>();
            this.productsByTitle = new Dictionary<string, SortedSet<Product>>();
            this.productsBySupplierAndPrice = new Dictionary<string, OrderedDictionary<decimal, HashSet<Product>>>();
            this.productsByTitleAndPrice = new Dictionary<string, OrderedDictionary<decimal, HashSet<Product>>>();
        }

        public int Count
        {
            get { return this.productByID.Count; }
        }

        public Product this[string id]
        {
            get
            {
                if (!this.productByID.ContainsKey(id))
                {
                    return null;
                }

                return this.productByID[id];
            }
        }

        public void Add(string id, string title, string supplier, decimal price)
        {
            Product newProduct = new Product(id, title, supplier, price);
            if (this.productByID.ContainsKey(id))
            {
                this.Remove(id);
            }

            this.productByID.Add(id, newProduct);

            if (!this.productsByPrice.ContainsKey(price))
            {
                this.productsByPrice.Add(price, new HashSet<Product>());
            }

            this.productsByPrice[price].Add(newProduct);

            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                this.productsBySupplierAndPrice.Add(supplier, new OrderedDictionary<decimal, HashSet<Product>>());
            }

            if (!this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                this.productsBySupplierAndPrice[supplier].Add(price, new HashSet<Product>());
            }

            this.productsBySupplierAndPrice[supplier][price].Add(newProduct);

            if (!this.productsByTitle.ContainsKey(title))
            {
                this.productsByTitle.Add(title, new SortedSet<Product>());
                this.productsByTitleAndPrice.Add(title, new OrderedDictionary<decimal, HashSet<Product>>());
            }

            if (!this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                this.productsByTitleAndPrice[title].Add(price, new HashSet<Product>());
            }

            this.productsByTitle[title].Add(newProduct);
            this.productsByTitleAndPrice[title][price].Add(newProduct);
        }

        public bool Remove(string id)
        {
            if (!this.productByID.ContainsKey(id))
            {
                return false;
            }

            Product product = this.productByID[id];
            this.productByID.Remove(id);
            this.productsByPrice[product.Price].Remove(product);
            this.productsBySupplierAndPrice[product.Supplier][product.Price].Remove(product);
            this.productsByTitle[product.Title].Remove(product);
            this.productsByTitleAndPrice[product.Title][product.Price].Remove(product);

            return true;
        }

        public IEnumerable<Product> FindProductsInPriceRange(decimal from, decimal to)
        {
            SortedSet<Product> output = new SortedSet<Product>();
            var productsInRange = this.productsByPrice.Range(from, true, to, true);
            foreach (var products in productsInRange)
            {
                foreach (Product product in products.Value)
                {
                    output.Add(product);
                }
            }

            return output;
        }

        public IEnumerable<Product> FindProductsByTitle(string title)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                return null;
            }

            return this.productsByTitle[title];
        }

        public IEnumerable<Product> FindProductsByTitleAndPrice(string title, decimal price)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                return null;
            }

            if (!this.productsByTitleAndPrice[title].ContainsKey(price))
            {
                return null;
            }

            SortedSet<Product> output = new SortedSet<Product>();
            foreach (Product product in this.productsByTitleAndPrice[title][price])
            {
                output.Add(product);
            }

            return output;
        }

        public IEnumerable<Product> FindProductsByTitleInPriceRange(string title, decimal from, decimal to)
        {
            if (!this.productsByTitleAndPrice.ContainsKey(title))
            {
                return null;
            }

            SortedSet<Product> output = new SortedSet<Product>();
            var productsByTitleInRange = this.productsByTitleAndPrice[title].Range(from, true, to, true);
            foreach (var pair in productsByTitleInRange)
            {
                foreach (Product product in pair.Value)
                {
                    output.Add(product);
                }
            }

            return output;
        }

        public IEnumerable<Product> FindProductsBySupplierAndPrice(string supplier, decimal price)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                return null;
            }

            if (!this.productsBySupplierAndPrice[supplier].ContainsKey(price))
            {
                return null;
            }

            SortedSet<Product> output = new SortedSet<Product>();
            foreach (Product product in this.productsBySupplierAndPrice[supplier][price])
            {
                output.Add(product);
            }

            return output;
        }

        public IEnumerable<Product> FindProductsBySupplierInPriceRange(string supplier, decimal from, decimal to)
        {
            if (!this.productsBySupplierAndPrice.ContainsKey(supplier))
            {
                return null;
            }

            SortedSet<Product> output = new SortedSet<Product>();
            var productsBySupplierInRange = this.productsBySupplierAndPrice[supplier].Range(from, true, to, true);
            foreach (var pair in productsBySupplierInRange)
            {
                foreach (Product product in pair.Value)
                {
                    output.Add(product);
                }
            }

            return output;
        }
    }
}
