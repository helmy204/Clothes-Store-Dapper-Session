using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Dapper.Data
{
    public class CategoryRepositoryAdvanced
    {
        private readonly IDbConnection db;

        public CategoryRepositoryAdvanced(string connectionString)
        {
            this.db = new SqlConnection(connectionString);
        }

        public List<Category> GetCategoriesByIds(params int[] ids)
        {
            return this.db.Query<Category>("SELECT * FROM Category WHERE Id in @Ids", new { Ids = ids }).ToList();
        }

        public List<dynamic> GetTop10DynamicCategories()
        {
            return this.db.Query("SELECT TOP 10 * FROM Category").ToList();
        }

        public List<Category> GetAllCategoriesWithProducts()
        {
            var sql = @"SELECT *
                        FROM Category AS C
                        INNER JOIN Product AS P
                            ON C.Id = P.CategoryId";

            var categories = this.db.Query<Category, Product, Category>(sql,
                (category, product) =>
                {
                    category.Products.Add(product);
                    return category;
                });

            return categories.ToList();
        }

        public List<Category> GetAllCategoriesWithProducts_Fixed()
        {
            var sql = @"SELECT *
                        FROM Category AS C
                        INNER JOIN Product AS P
                            ON C.Id = P.CategoryId";

            var categoryDictionary = new Dictionary<int, Category>();

            var categories = this.db.Query<Category, Product, Category>(sql,
                (category, product) =>
                {
                    if (!categoryDictionary.TryGetValue(category.Id, out var currentCategory))
                    {
                        currentCategory = category;
                        categoryDictionary.Add(currentCategory.Id, currentCategory);
                    }

                    currentCategory.Products.Add(product);
                    return currentCategory;
                });

            return categories.Distinct().ToList();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await this.db.QueryAsync<Category>("SELECT * FROM Category");
            return categories.ToList();
        }
    }
}
