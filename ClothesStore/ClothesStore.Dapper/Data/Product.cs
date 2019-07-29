using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesStore.Dapper.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Computed]
        public bool IsNew => this.Id == default(int);
        public bool IsDeleted { get; set; }
    }
}
