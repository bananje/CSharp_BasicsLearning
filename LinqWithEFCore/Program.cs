// See https://aka.ms/new-console-template for more information
using LinqWithEFCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static System.Console;

//FilteredAndSort();
//GroupJoinCategoriesAndProducts();
//AggregateProducts();

//OutputProductsAsXml();

ProcessSettings();

// формирование XML с помощью Linq 
static void OutputProductsAsXml()
{
    using (LinqDB db = new())
    {
        Product[] products = db.Products.ToArray();
        XElement xml = new("products", from p in products
                                       select new XElement("product",
                                       new XAttribute("id", p.ProductId),
                                       new XAttribute("price", p.UnitPrice),
                                       new XElement("name", p.ProductName)));
        WriteLine(xml);
    }
}

static void ProcessSettings()
{
    XDocument doc = XDocument.Load("settings.xml");

    var appSettings = doc.Descendants("add")
                         .Select(node => new
                         {
                             Key = node.Attribute("key")?.Value,
                             Value = node.Attribute("value")?.Value
                         }).ToArray();

    foreach (var item in appSettings)
    {
        WriteLine($"{item.Key}: {item.Value}");
    }
}

static void FilteredAndSort()
{
    using (LinqDB db = new())
    {
        DbSet<Product> allProducts = db.Products;

        if (allProducts is null)
        {
            WriteLine("No products found.");
            return;
        }

        //IQueryable<Product> filteredProducts = allProducts.Where(product => product.UnitPrice < 10M);
        IQueryable<Product> processedProducts = allProducts.ProcessSequence();


        IOrderedQueryable<Product> sortedAndFilteredProducts = processedProducts.OrderByDescending(product => product.UnitPrice);
      

        var list = db.Products.Where(product => product.UnitPrice < 10M).OrderByDescending(desc => desc.UnitPrice).Select(product => new // анонимный тип
        {
            product.ProductId,
            product.ProductName,
            product.UnitPrice
        });       

        foreach (var p in list)
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00}",
            p.ProductId, p.ProductName, p.UnitPrice);
        }
        WriteLine();
        //sortedAndFilteredProducts.ForEachAsync(product => { WriteLine($"{product.ProductId} costs {product.ProductName} cost {product.UnitPrice}"); });

    }
}

static void GroupJoinCategoriesAndProducts()
{
    using (LinqDB db = new())
    {
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, matchingProducts) => new
            {
                c.CategoryName,
                Products = matchingProducts.OrderBy(p => p.ProductName)
            });

        foreach (var category in queryGroup)
        {
            WriteLine("{0} has {1} products.",
            arg0: category.CategoryName,
            arg1: category.Products.Count());

            foreach (var product in category.Products)
            {
                WriteLine($" {product.ProductName}");
            }
        }
    }
}

static void AggregateProducts()
{
    using(LinqDB db = new())
    {
        WriteLine(db.Products.Count());
        WriteLine(db.Products.Max(p => p.UnitPrice));
        WriteLine(db.Products.Sum(p => p.UnitsInStock));
        WriteLine(db.Products.Sum(p => p.UnitsOnOrder));
        WriteLine(db.Products.Average(p => p.UnitPrice));
        WriteLine(db.Products.Sum(p => p.UnitPrice * p.UnitsInStock));
    }
}

static void JoinCategoriesAndProducts()
{
    using (LinqDB db = new())
    {
        //Join принимает четыре параметра: последовательность, с которой требуется объединиться; свойство или свойства левой последовательности, по которым
        //нужно найти соответствие; свойство или свойства правой последовательности,по которым нужно найти соответствие; и проекцию;
        var queryJoin = db.Categories.Join(
            inner: db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) => new { c.CategoryName, p.ProductName, p.ProductId }).OrderBy(u => u.CategoryName);

        foreach (var item in queryJoin)
        {
            WriteLine("{0}: {1} is in {2}.",
            arg0: item.ProductId,
            arg1: item.ProductName,
            arg2: item.CategoryName);
        }
    }
}

static void CustomExtensionMethods()
{
    using (LinqDB db = new())
    {
        WriteLine("Mean units in stock: {0:N0}",db.Products.Average(p => p.UnitsInStock));
        WriteLine("Mean unit price: {0:$#,##0.00}",db.Products.Average(p => p.UnitPrice));

        WriteLine("Median units in stock: {0:N0}",db.Products.Median(p => p.UnitsInStock));
        WriteLine("Median unit price: {0:$#,##0.00}",db.Products.Median(p => p.UnitPrice));

        WriteLine("Mode units in stock: {0:N0}",db.Products.Mode(p => p.UnitsInStock));
        WriteLine("Mode unit price: {0:$#,##0.00}",db.Products.Mode(p => p.UnitPrice));
    }
}