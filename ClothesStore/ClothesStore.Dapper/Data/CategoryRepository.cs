using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ClothesStore.Dapper.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection db;

        public CategoryRepository(string connectionString)
        {
            this.db = new SqlConnection(connectionString);
        }

        public Category Add(Category category)
        {
            var sql =
                "INSERT INTO Category (Name) VALUES (@Name) " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = this.db.Query<int>(sql, category).Single();
            category.Id = id;
            return category;
        }

        public Category Find(int id)
        {
            return this.db.Query<Category>("SELECT * FROM Category WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Category> GetAll()
        {
            return this.db.Query<Category>("SELECT * FROM Category").ToList();
        }

        public void Remove(int id)
        {
            this.db.Execute("DELETE FROM Category WHERE Id = @Id", new { id });
        }

        public Category Update(Category category)
        {
            var sql = "UPDATE Category " +
                "SET Name = @Name " +
                "WHERE Id = @Id";

            this.db.Execute(sql, category);
            return category;
        }

        public Category GetFullCategory(int id)
        {
            var sql = "SELECT * FROM Category WHERE Id = @Id; " +
                      "SELECT * FROM Product WHERE CategoryId = @Id";

            using (var multipleResults = this.db.QueryMultiple(sql, new { Id = id }))
            {
                var category = multipleResults.Read<Category>().SingleOrDefault();

                var products = multipleResults.Read<Product>().ToList();

                if (category != null && products != null)
                {
                    category.Products.AddRange(products);
                }

                return category;
            }
        }

        public void Save(Category category)
        {
            using (var transaction = new TransactionScope())
            {
                if (category.IsNew)
                {
                    this.Add(category);
                }
                else
                {
                    this.Update(category);
                }

                foreach (var product in category.Products.Where(p => !p.IsDeleted))
                {
                    product.CategoryId = category.Id;

                    if (product.IsNew)
                    {
                        this.Add(product);
                    }
                    else
                    {
                        this.Update(product);
                    }
                }

                foreach (var product in category.Products.Where(p => p.IsDeleted))
                {
                    this.db.Execute("DELETE FROM Product WHERE Id = @Id", new { product.Id });
                }
                transaction.Complete();
            }
        }

        public Product Add(Product product)
        {
            var sql =
                "INSERT INTO Product (Name,CategoryId) VALUES (@Name,@CategoryId) " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var id = this.db.Query<int>(sql, product).Single();
            product.Id = id;
            return product;
        }

        public Product Update(Product product)
        {
            var sql = "UPDATE Product " +
                "SET Name = @Name " +
                "WHERE Id = @Id";

            this.db.Execute(sql, product);
            return product;
        }
    }
}
