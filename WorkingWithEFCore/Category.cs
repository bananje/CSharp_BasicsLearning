﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace WorkingWithEFCore
{
    public class Category
    {
        // эти свойства сопоставляются со столбцами в базе данных
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            // чтобы разработчики могли добавлять продукты в категорию,
            // мы должны инициализировать свойство навигации в пустую коллекцию
            Products = new HashSet<Product>();
        }
    }
}
