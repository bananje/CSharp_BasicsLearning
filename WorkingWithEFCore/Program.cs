// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore;
using static System.Console;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;

WriteLine($"Using {ProjectConstants.DatabaseProvider} database provider.");
//FilteredIncludes();
//QueryingCategories();
//QueryingWithLike();
if (AddProduct(categoryId: 6,
 productName: "Bob's Burgers", price: 500M))
{
    WriteLine("Add product successful.");
}
ListProducts();

static void QueryingCategories()
{
    using (Northwind db = new())
    {
        ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        WriteLine("Categories and how many products they have:");
        // запрос на получение всех категорий и связанных с ними продуктов
        IQueryable<Category>? categories = db.Categories?.TagWith("Категории с продуктами"); // код TagWith добавит комментарий SQL в журнал
        // .Include(c => c.Products); // жадная загрузка

        db.ChangeTracker.LazyLoadingEnabled = false;

        Write("Enable eager loading? (Y/N): ");
        bool eagerloading = (ReadKey().Key == ConsoleKey.Y);
        bool explicitloading = false;

        WriteLine();

        if (eagerloading)
        {
            categories = db.Categories?.Include(c => c.Products);
        }
        else
        {
            categories = db.Categories;
            Write("Enable explicit loading? (Y/N): ");
            explicitloading = (ReadKey().Key == ConsoleKey.Y);
            WriteLine();
        }

        if (categories is null)
        {
            WriteLine("No categories found.");
            return;
        }

        // выполнение запроса и перечисление результатов
        foreach (Category c in categories)
        {
            if (explicitloading) // пример явной загрузки
            {
                Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                ConsoleKeyInfo key = ReadKey();
                WriteLine();
                if (key.Key == ConsoleKey.Y)
                {
                    CollectionEntry<Category, Product> products =
                    db.Entry(c).Collection(c2 => c2.Products);
                    if (!products.IsLoaded) products.Load(); // загузить сущность продуктов
                }
            }

            WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
        }
    }
}

// фильтрация
static void FilteredIncludes()
{
    using (Northwind db = new())
    {
        ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        Write("Enter a minimum for units in stock: ");
        string unitsInStock = ReadLine() ?? "10";
        int stock = int.Parse(unitsInStock);

        IQueryable<Category>? categories = db.Categories?
        .Include(c => c.Products.Where(p => p.Stock >= stock));

        WriteLine($"ToQuery : {categories.ToQueryString()}");
        if (categories is null)
        {
            WriteLine("No categories found.");
            return;
        }

        foreach (Category c in categories)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of { stock} units in stock.");
            foreach (Product p in c.Products)
            {
                WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
            }
        }
    }
}

// сортировка
static void QueryingProducts()
{
    using (Northwind db = new())
    {
        WriteLine("Products that cost more than a price, highest at top.");
        string? input;
        decimal price;

        do
        {
            Write("Enter a product price: ");
            input = ReadLine();
        } while (!decimal.TryParse(input, out price));

        IQueryable<Product>? products = db.Products?.Where(product => product.Cost > price).OrderByDescending(product => product.Cost);

        if (products is null)
        {
            WriteLine("No products found.");
            return;
        }

        foreach (Product p in products)
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.", p.ProductId, p.ProductName, p.Cost, p.Stock);
        }
    }
}

// Сопоставление с образцом
static void QueryingWithLike()
{
    using (Northwind db = new())
    {
        ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        Write("Enter part of a product name: ");
        string? input = ReadLine();

        IQueryable<Product>? products = db.Products?.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
        if (products is null)
        {
            WriteLine("No products found.");
            return;
        }

        foreach (Product p in products)
        {
            WriteLine("{0} has {1} units in stock. Discontinued? {2}", p.ProductName, p.Stock, p.Discontinued);
        }
    }
}

static bool AddProduct(int categoryId, string productName, decimal? price)
{
    using (Northwind db = new())
    {
        int id = db.Products.OrderBy(p => p.ProductId).Last().ProductId + 1;
        Product p = new()
        {
            ProductId = id,
            CategoryId = categoryId,
            ProductName = productName,
            Cost = price
        };
        // помечаем продукт как добавленный к отслеживанию изменений
        db.Products.Add(p);
        // сохранение отслеживаемых изменений в базе данных
        int affected = db.SaveChanges();

        return (affected == 1);
    }
}

static void ListProducts()
{
    using (Northwind db = new())
    {
        foreach (Product p in db.Products.OrderByDescending(p => p.Cost))
        {
            WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);
        }
    }
}

static int DeleteProducts(string productName)
{
    using (Northwind db = new())
    {
        IQueryable<Product>? products = db.Products?.Where(p => p.ProductName.StartsWith(productName));

        if (products != null) db.Products.RemoveRange(products);
        else
        {
            WriteLine("No products found to delete.");
            return 0;
        }

        int affected = db.SaveChanges();
        return affected;
    }
}