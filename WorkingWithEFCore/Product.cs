using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithEFCore
{
    public class Product
    {
        public int ProductId { get; set; } // первичный ключ

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; } = null!; // null ! - значение не допускающее null
        [Column("UnitPrice", TypeName = "money")]
        public decimal? Cost { get; set; } // имя свойства != имя столбца
        [Column("UnitsInStock")]
        public short? Stock { get; set; }
        public bool Discontinued { get; set; }
        // эти два параметра определяют отношение внешнего ключа
        // к таблице Categories
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

    }
}
