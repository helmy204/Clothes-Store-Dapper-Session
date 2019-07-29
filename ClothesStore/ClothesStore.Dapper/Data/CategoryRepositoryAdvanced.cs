using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
    }
}
