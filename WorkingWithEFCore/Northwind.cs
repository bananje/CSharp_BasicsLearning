using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace WorkingWithEFCore
{
    public class Northwind : DbContext
    {
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ProjectConstants.DatabaseProvider == "SQLite")
            {
                string path = Path.Combine(
                Environment.CurrentDirectory, "Northwind.db");
                WriteLine($"Using {path} database file.");
                optionsBuilder.UseSqlite($"Filename={path}");
            }
            else
            {
                string connection = "Data Source=DESKTOP-33G772A\\SQLEXPRESS;Initial Catalog=NorthWind;Integrated Security=true;MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(connection);
                optionsBuilder.UseLazyLoadingProxies(); // подключение ленивой загрузки с помощью пакета Microsoft.EntityFrameworkCore.Proxies
            }
        }

        // определение соглашений
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(50); // все свойства string по умолчанию должны иметь максимальную длину 50 символов
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // пример использования Fluent API вместо атрибутов,
            // чтобы ограничить длину имени категории 15 символами
            modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired() // NOT NULL
            .HasMaxLength(15);

            modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued); // глобальный фильтр для удаления снятых с производства товаров

            if (ProjectConstants.DatabaseProvider == "SQLite")
            {
                // добавлен патч для десятичной поддержки в SQLite
                modelBuilder.Entity<Product>()
                .Property(product => product.Cost)
                .HasConversion<double>();
            }
        }
    }
}
