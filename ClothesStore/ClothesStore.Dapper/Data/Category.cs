using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesStore.Dapper.Data
{
    //[Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Computed]
        public bool IsNew => this.Id == default(int);

        [Write(false)]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
