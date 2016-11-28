namespace Pr03CollectionOfProductsUnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using Pr03CollectionOfProducts;

    [TestClass]
    public class UnitTests
    {
        //[TestInitialize]
        private ProductData InitializeCollectionOfProducts()
        {
            var product01 = new Product("11aa33", "Laptop Lenovo ThinkPad", "SuperTech", 1800);
            var product02 = new Product("bb33cc", "HP ProBook 450G1", "MegaTech", 1500);
            var product03 = new Product("dd00gg", "Nexus 5", "GigaTron", 550);
            var product04 = new Product("22ffbb", "Nexus 4", "GigaTron", 450);
            var product05 = new Product("99ffbb", "Smasung Galaxy S6", "GigaTron", 550);
            var product06 = new Product("zzss99", "Smasung Galaxy S4", "MegaBit", 450);
            var product07 = new Product("gg5588", "Smasung Galaxy S4", "MegaBit", 450);
            var product08 = new Product("yyAA11", "Smasung Galaxy S4", "GigaTron", 450);
            var product09 = new Product("01dd77", "Laptop Lenovo ThinkPad", "SecondHandTech", 600);
            var product10 = new Product("00dd77", "Laptop Lenovo ThinkPad", "SecondHandTech", 600);

            var products = new ProductData();
            var productList = new List<Product>
            {
                product01,
                product02,
                product03,
                product04,
                product05,
                product06,
                product07,
                product08,
                product09,
                product10
            };

            foreach (var product in productList)
            {
                products.Add(product.ID, product.Title, product.Supplier, product.Price);
            }

            return products;
        }

        [TestMethod]
        public void Add_Product_With_Existing_Id_Should_Replace_the_OldProduct()
        {
            var products = this.InitializeCollectionOfProducts();
            var newProduct = new Product("11aa33", "Laptop Lenovo ThinkPad", "SuperTech", 3600);
            products.Add(newProduct.ID, newProduct.Title, newProduct.Supplier, newProduct.Price);

            Assert.AreEqual(10, products.Count);
            Assert.AreEqual(3600, products["11aa33"].Price);
        }

        [TestMethod]
        public void Add_Product_With_Existing_Id_Should_Replace_the_OldProduct_And_Affects_ProductsByTitle()
        {
            var products = this.InitializeCollectionOfProducts();
            var newProduct = new Product("11aa33", "Laptop Lenovo Novo", "SuperTech", 3600);
            products.Add(newProduct.ID, newProduct.Title, newProduct.Supplier, newProduct.Price);

            var productsByTitle = products.FindProductsByTitle("Laptop Lenovo ThinkPad");

            Assert.AreEqual(10, products.Count);
            Assert.AreEqual(2, productsByTitle.Count());
            Assert.AreEqual("Laptop Lenovo Novo", products["11aa33"].Title);
        }

        [TestMethod]
        public void Add_Product_With_Existing_Id_Should_Replace_the_OldProduct_And_Affects_ProductsByPriceRange()
        {
            var products = this.InitializeCollectionOfProducts();
            var newProduct = new Product("11aa33", "Laptop Lenovo Novo", "SuperTech", 3600);
            products.Add(newProduct.ID, newProduct.Title, newProduct.Supplier, newProduct.Price);

            var productsByPriceRange1 = products.FindProductsInPriceRange(3600, 4000).ToList();
            var productsByPriceRange2 = products.FindProductsInPriceRange(1800, 1800);

            Assert.AreEqual(10, products.Count);
            Assert.AreEqual("11aa33", productsByPriceRange1[0].ID);
            Assert.AreEqual("Laptop Lenovo Novo", productsByPriceRange1[0].Title);
            Assert.AreEqual(0, productsByPriceRange2.Count());
        }

        [TestMethod]
        public void Add_Product_With_Existing_Id_Should_Replace_the_OldProduct_And_Affects_ProductsByTitleAndPriceRange()
        {
            var products = this.InitializeCollectionOfProducts();
            var newProduct = new Product("11aa33", "Laptop Lenovo Novo", "SuperTech", 3600);
            products.Add(newProduct.ID, newProduct.Title, newProduct.Supplier, newProduct.Price);

            var productsByTitlePriceRange = products.FindProductsByTitleInPriceRange("Laptop Lenovo Novo", 3600, 4000).ToList();

            Assert.AreEqual(10, products.Count);
            Assert.AreEqual("11aa33", productsByTitlePriceRange[0].ID);
            Assert.AreEqual("Laptop Lenovo Novo", productsByTitlePriceRange[0].Title);
        }

        [TestMethod]
        public void Test_Find_Products_by_Title()
        {
            var products = this.InitializeCollectionOfProducts();

            var productsByTitle1 = products.FindProductsByTitle("Smasung Galaxy S4").ToList();
            Assert.AreEqual(3, productsByTitle1.Count);
            Assert.AreEqual("gg5588", productsByTitle1[0].ID);
            Assert.AreEqual("yyAA11", productsByTitle1[1].ID);
            Assert.AreEqual("zzss99", productsByTitle1[2].ID);

            var productsByTitle2 = products.FindProductsByTitle("Nexus 4").ToList();
            Assert.AreEqual(1, productsByTitle2.Count);
            Assert.AreEqual("22ffbb", productsByTitle2[0].ID);

            var productsByTitle3 = products.FindProductsByTitle("Not existiong product");
            Assert.AreEqual(null, productsByTitle3);
        }

        [TestMethod]
        public void Test_Find_Products_by_Title_and_Price()
        {
            var products = this.InitializeCollectionOfProducts();

            var productsByTitleAndPrice1 = products
                .FindProductsByTitleAndPrice("Smasung Galaxy S4", 450).ToList();
            Assert.AreEqual(3, productsByTitleAndPrice1.Count);
            Assert.AreEqual("gg5588", productsByTitleAndPrice1[0].ID);
            Assert.AreEqual("yyAA11", productsByTitleAndPrice1[1].ID);
            Assert.AreEqual("zzss99", productsByTitleAndPrice1[2].ID);

            var productsByTitleAndPrice2 = products
                .FindProductsByTitleAndPrice("Laptop Lenovo ThinkPad", 1800).ToList();
            Assert.AreEqual(1, productsByTitleAndPrice2.Count);
            Assert.AreEqual("11aa33", productsByTitleAndPrice2[0].ID);

            var productsByTitleAndPrice3 = products
                .FindProductsByTitleAndPrice("Laptop Lenovo ThinkPad", 1100);
            Assert.AreEqual(null, productsByTitleAndPrice3);
        }

        [TestMethod]
        public void Test_Find_Product_by_Price_Range()
        {
            var products = this.InitializeCollectionOfProducts();

            var productsByPriceRange = products
                .FindProductsInPriceRange(450, 550).ToList();
            Assert.AreEqual(6, productsByPriceRange.Count);
            Assert.AreEqual("22ffbb", productsByPriceRange[0].ID);
            Assert.AreEqual("99ffbb", productsByPriceRange[1].ID);
            Assert.AreEqual("dd00gg", productsByPriceRange[2].ID);
            Assert.AreEqual("gg5588", productsByPriceRange[3].ID);
            Assert.AreEqual("yyAA11", productsByPriceRange[4].ID);
            Assert.AreEqual("zzss99", productsByPriceRange[5].ID);
        }

        [TestMethod]
        public void Test_Find_Product_by_Title_and_Price_Range()
        {
            var products = this.InitializeCollectionOfProducts();

            var productsByTitleAndPriceRange = products
                .FindProductsByTitleInPriceRange("Laptop Lenovo ThinkPad", 500, 2000).ToList();
            Assert.AreEqual(3, productsByTitleAndPriceRange.Count);
            Assert.AreEqual("00dd77", productsByTitleAndPriceRange[0].ID);
            Assert.AreEqual("01dd77", productsByTitleAndPriceRange[1].ID);
            Assert.AreEqual("11aa33", productsByTitleAndPriceRange[2].ID);

        }

        [TestMethod]
        public void Test_Find_Products_by_Supplier_and_Price()
        {
            var products = this.InitializeCollectionOfProducts();

            var productsbySupplierAndPrice1 = products
                .FindProductsBySupplierAndPrice("MegaBit", 450).ToList();

            Assert.AreEqual(2, productsbySupplierAndPrice1.Count);
            Assert.AreEqual("gg5588", productsbySupplierAndPrice1[0].ID);
            Assert.AreEqual("zzss99", productsbySupplierAndPrice1[1].ID);

            var productsbySupplierAndPrice2 = products
                .FindProductsBySupplierAndPrice("SecondHandTech", 600).ToList();

            Assert.AreEqual(2, productsbySupplierAndPrice2.Count);
            Assert.AreEqual("00dd77", productsbySupplierAndPrice2[0].ID);
            Assert.AreEqual("01dd77", productsbySupplierAndPrice2[1].ID);

            var productsbySupplierAndPrice3 = products
                .FindProductsBySupplierAndPrice("SecondHandTech", 123);
            Assert.AreEqual(null, productsbySupplierAndPrice3);
        }

        [TestMethod]
        public void Test_Find_Products_by_Supplier_and_Price_Range()
        {
            var products = this.InitializeCollectionOfProducts();

            var productsBySupplierAndPriceRange1 = products
                .FindProductsBySupplierInPriceRange("GigaTron", 400, 600).ToList();

            Assert.AreEqual(4, productsBySupplierAndPriceRange1.Count);

            var productsBySupplierAndPriceRange2 = products
                .FindProductsBySupplierInPriceRange("GigaTron", 460, 600).ToList();

            Assert.AreEqual(2, productsBySupplierAndPriceRange2.Count);
            Assert.AreEqual("99ffbb", productsBySupplierAndPriceRange2[0].ID);
            Assert.AreEqual("dd00gg", productsBySupplierAndPriceRange2[1].ID);

            var productsBySupplierAndPriceRange3 = products
                .FindProductsBySupplierInPriceRange("SuperTech", 1800, 2000).ToList();

            Assert.AreEqual(1, productsBySupplierAndPriceRange3.Count);
            Assert.AreEqual("11aa33", productsBySupplierAndPriceRange3[0].ID);
        }

        [TestMethod]
        public void Test_Count_After_Remove_Product_by_Id()
        {
            var products = this.InitializeCollectionOfProducts();

            products.Remove("gg5588");

            Assert.AreEqual(9, products.Count);
        }

        [TestMethod]
        public void Test_Remove_NonExisting_Product()
        {
            var products = this.InitializeCollectionOfProducts();

            bool removed = products.Remove("Non Existing Id");

            Assert.AreEqual(false, removed);
        }

        [TestMethod]
        public void Test_Count_After_Remove_And_Add_Product()
        {
            var products = this.InitializeCollectionOfProducts();

            products.Remove("11aa33");
            products.Add("someId", "title", "supplier", 1000);

            Assert.AreEqual(10, products.Count);
        }

        [TestMethod]
        public void Test_FindByPriceRange_After_Remove_Product()
        {
            var products = this.InitializeCollectionOfProducts();

            products.Remove("gg5588");

            var productsByPriceRange = products
                .FindProductsInPriceRange(400, 500).ToList();
            Assert.AreEqual(3, productsByPriceRange.Count);
            Assert.AreEqual("22ffbb", productsByPriceRange[0].ID);
            Assert.AreEqual("yyAA11", productsByPriceRange[1].ID);
            Assert.AreEqual("zzss99", productsByPriceRange[2].ID);
        }

        [TestMethod]
        public void Test_Find_by_Title_and_Price_Range_After_Remove_Product()
        {
            var products = this.InitializeCollectionOfProducts();

            products.Remove("gg5588");

            var productsByTitleAndPriceRange1 = products
                .FindProductsByTitleInPriceRange("Smasung Galaxy S4", 400, 500).ToList();
            Assert.AreEqual(2, productsByTitleAndPriceRange1.Count);

            var productsByTitleAndPriceRange2 = products
                .FindProductsByTitleInPriceRange("Laptop Lenovo ThinkPad", 600, 2000).ToList();
            Assert.AreEqual(3, productsByTitleAndPriceRange2.Count);

            products.Remove("11aa33");
            var productsByTitleAndPriceRange3 = products
                .FindProductsByTitleInPriceRange("Laptop Lenovo ThinkPad", 600, 2000).ToList();
            Assert.AreEqual(2, productsByTitleAndPriceRange3.Count);
            Assert.AreEqual("00dd77", productsByTitleAndPriceRange3[0].ID);
            Assert.AreEqual("01dd77", productsByTitleAndPriceRange3[1].ID);
        }
    }
}
