using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ClothesStore.Dapper.Data
{
    public class CategoryRepositoryContrib : ICategoryRepository
    {
        private readonly IDbConnection db;

        public CategoryRepositoryContrib(string connectionString)
        {
            this.db = new SqlConnection(connectionString);

            SqlMapperExtensions.TableNameMapper = entityType =>
            {
                if (entityType == typeof(Category))
                {
                    return "Category";
                }
                throw new Exception($"Not supported entity type {entityType}");
            };
        }

        public Category Add(Category category)
        {
            var id = this.db.Insert(category);
            category.Id = (int)id;
            return category;
        }

        public Category Find(int id)
        {
            return this.db.Get<Category>(id);
        }

        public Category GetFullCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return this.db.GetAll<Category>().ToList();
        }

        public void Remove(int id)
        {
            this.db.Delete(new Category { Id = id });
        }

        public Category Update(Category category)
        {
            this.db.Update(category);
            return category;
        }

        public void Save(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
