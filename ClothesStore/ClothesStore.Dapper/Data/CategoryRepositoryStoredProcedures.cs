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
    public class CategoryRepositoryStoredProcedures : ICategoryRepository
    {
        private readonly IDbConnection db;

        public CategoryRepositoryStoredProcedures(string connectionString)
        {
            this.db = new SqlConnection(connectionString);
        }

        public Category Add(Category category)
        {
            throw new NotImplementedException();
        }

        public Category Find(int id)
        {
            return this.db.Query<Category>("GetCategory", new { Id = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetFullCategory(int id)
        {
            using (var multipleResults = this.db.QueryMultiple("GetCategory", new { Id = id }, commandType: CommandType.StoredProcedure))
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

        public void Remove(int id)
        {
            this.db.Execute("DeleteCategory", new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public void Save(Category category)
        {
            using (var transaction = new TransactionScope())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", value: category.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                parameters.Add("@Name", category.Name);

                this.db.Execute("SaveCategory", parameters, commandType: CommandType.StoredProcedure);
                category.Id = parameters.Get<int>("@Id");

                foreach (var product in category.Products.Where(p => !p.IsDeleted))
                {
                    product.CategoryId = category.Id;

                    var productParameters = new DynamicParameters(new
                    {
                        CategoryId = product.CategoryId,
                        Name = product.Name
                    });
                    productParameters.Add("@Id", product.Id, DbType.Int32, ParameterDirection.InputOutput);
                    this.db.Execute("SaveProduct", productParameters, commandType: CommandType.StoredProcedure);
                    product.Id = productParameters.Get<int>("@Id");
                }

                foreach (var product in category.Products.Where(p => p.IsDeleted))
                {
                    this.db.Execute("DeleteProduct", new { Id = product.Id }, commandType: CommandType.StoredProcedure);
                }

                transaction.Complete();
            }
        }

        public Category Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
