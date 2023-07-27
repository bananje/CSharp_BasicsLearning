using Microsoft.EntityFrameworkCore;

namespace LinqWithEFCore
{
    public class LinqDB : DbContext
    {
        // эти свойства сопоставляются с таблицами в базе данных
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-33G772A\SQLEXPRESS;Initial Catalog=
            NorthWind; Integrated Security = true; MultipleActiveResultSets = true; ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(product => product.UnitPrice)
            .HasConversion<double>();
        }
    }
}
