using ClothesStore.Dapper.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Dapper
{
    class Program
    {
        private static IConfigurationRoot config;

        static void Main(string[] args)
        {
            Initialize();

            #region CRUD Operations

            //Get_all_Should_return_10_categories();

            //Insert_Should_assign_id_to_new_category();
            //ICategoryRepository repository = CreateRepository();
            //int entityId = repository.GetAll().LastOrDefault().Id;
            //Find_Should_retrieve_existing_entity(entityId);

            //Modify_Should_update_existing_entity(entityId);
            //Find_Should_retrieve_existing_entity(entityId);

            //Delete_Should_remove_entity(entityId);
            //Find_Should_retrieve_existing_entity(entityId);

            #endregion CRUD Operations

            /* Check with Dapper.Contrib */
            //Get_all_Should_return_10_results();

            #region CRUD Operations Complex

            //// Parent-Child
            //var repository = CreateRepository();
            //var fullCategory = repository.GetFullCategory(1);
            //fullCategory.Output();


            //Insert_Should_assign_id_to_new_category_Complex();
            //ICategoryRepository repository = CreateRepository();
            //int entityId = repository.GetAll().LastOrDefault().Id;
            //Find_Should_retrieve_existing_category_Complex(entityId);

            //Modify_Should_update_existing_category_Complex(entityId);
            //Find_Should_retrieve_existing_category_Complex(entityId);

            //Delete_Should_remove_entity(entityId);
            //Find_Should_retrieve_existing_category_Complex(entityId);

            #endregion CRUD Operations Complex

            #region CRUD Operations Stored Procedures

            //Insert_Should_assign_id_to_new_category_Complex();

            //ICategoryRepository repository = CreateRepository();
            //int entityId = 1010;
            //Find_Should_retrieve_existing_category_Complex(entityId);

            //Modify_Should_update_existing_category_Complex(entityId);
            //Find_Should_retrieve_existing_category_Complex(entityId);

            //Delete_Should_remove_entity(entityId);
            //Find_Should_retrieve_existing_category_Complex(entityId);

            #endregion CRUD Operations Stored Procedures

            #region Advanced

            //GetCategoriesByIds_should_return_correct_categories();
            //GetTop10DynamicCategories_should_return_dynamic_categories();

            // Multi Mapping
            GetAllCategoriesWithProducts_should_return_categories_with_products();

            #endregion Advanced
        }

        static async Task xMain(string[] args)
        {
            Initialize();

            await Get_all_Should_return_10_categories_async();
        }

        #region CRUD Operations
        static void Delete_Should_remove_entity(int id)
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();

            // Act
            repository.Remove(id);

            // Assert
            Console.WriteLine("*** Category Deleted ***");
        }

        static void Modify_Should_update_existing_entity(int id)
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();

            // Act
            var category = repository.Find(id);
            category.Name = "Modified Test";
            repository.Update(category);

            // Assert
            Console.WriteLine("*** Category Modified ***");
        }

        static void Find_Should_retrieve_existing_entity(int id)
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();

            // Act
            var category = repository.Find(id);

            // Assert
            Console.WriteLine("*** Get Category ***");
            category.Output();
            //Debug.Assert(category.Name == "Test");
        }

        static int Insert_Should_assign_id_to_new_category()
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();
            Category category = new Category
            {
                Name = "Test"
            };

            // Act
            repository.Add(category);

            // Assert
            Debug.Assert(category.Id != 0);
            Console.WriteLine("*** Category Inserted ***");
            Console.WriteLine($"New Id: {category.Id}");
            return category.Id;
        }

        static void Get_all_Should_return_10_categories()
        {
            // Arrange
            var repository = CreateRepository();

            // Act
            var catalogs = repository.GetAll();

            // Assert
            Console.WriteLine($"Count: {catalogs.Count}");
            Debug.Assert(catalogs.Count == 10);
            catalogs.Output();
        }

        #endregion CRUD Operations

        #region CRUD Operations Complex
        static void Delete_Should_remove_entity_Complex(int id)
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();

            // Act
            repository.Remove(id);

            // Assert
            Console.WriteLine("*** Category Deleted ***");
        }

        static void Modify_Should_update_existing_category_Complex(int id)
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();

            // Act
            //var category = repository.Find(id);
            var category = repository.GetFullCategory(id);
            category.Name = "Modified Test Category";
            category.Products[0].Name = "Modified Test Product";
            //repository.Update(category);
            repository.Save(category);

            // Assert
            Console.WriteLine("*** Category Modified ***");
        }

        static void Find_Should_retrieve_existing_category_Complex(int id)
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();

            // Act
            //var category = repository.Find(id);
            var category = repository.GetFullCategory(id);

            // Assert
            Console.WriteLine("*** Get Category ***");
            category.Output();
        }

        static int Insert_Should_assign_id_to_new_category_Complex()
        {
            // Arrange
            ICategoryRepository repository = CreateRepository();
            Category category = new Category
            {
                Name = "Test Category"
            };
            Product product = new Product
            {
                Name = "Test Product"
            };
            category.Products.Add(product);

            // Act
            //repository.Add(category);
            repository.Save(category);

            // Assert
            Debug.Assert(category.Id != 0);
            Console.WriteLine("*** Category Inserted ***");
            Console.WriteLine($"New Id: {category.Id}");
            return category.Id;
        }

        static void Get_all_Should_return_10_categories_Complex()
        {
            // Arrange
            var repository = CreateRepository();

            // Act
            var catalogs = repository.GetAll();

            // Assert
            Console.WriteLine($"Count: {catalogs.Count}");
            Debug.Assert(catalogs.Count == 10);
            catalogs.Output();
        }

        #endregion CRUD Operations Complex

        #region Advanced Operations

        static void GetCategoriesByIds_should_return_correct_categories()
        {
            // Arrange
            var repository = CreateRepositoryAdvanced();

            // Act
            var categories = repository.GetCategoriesByIds(1, 2, 5, 6);

            // Assert
            Debug.Assert(categories.Count == 4);
            categories.Output();
        }

        static void GetTop10DynamicCategories_should_return_dynamic_categories()
        {
            // Arrange
            var repository = CreateRepositoryAdvanced();

            // Act
            var categories = repository.GetTop10DynamicCategories();

            // Assert
            Debug.Assert(categories.Count > 0);
            categories.Output();
        }

        static void GetAllCategoriesWithProducts_should_return_categories_with_products()
        {
            // Arrange
            var repository = CreateRepositoryAdvanced();

            // Act
            var categories = repository.GetAllCategoriesWithProducts();
            //var categories = repository.GetAllCategoriesWithProducts_Fixed();

            // Assert
            Debug.Assert(categories.Count > 0);
            categories.Output();
        }

        static async Task Get_all_Should_return_10_categories_async()
        {
            // Arrange
            var repository = CreateRepositoryAdvanced();

            // Act
            var catalogs = await repository.GetAllAsync();

            // Assert
            Console.WriteLine($"Count: {catalogs.Count}");
            Debug.Assert(catalogs.Count == 10);
            catalogs.Output();
        }

        #endregion Advanced Operations

        #region Helpers
        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            config = builder.Build();
        }

        private static ICategoryRepository CreateRepository()
        {
            //return new CategoryRepository(config.GetConnectionString("ClothesStoreConnection"));
            //return new CategoryRepositoryContrib(config.GetConnectionString("ClothesStoreConnection"));
            return new CategoryRepositoryStoredProcedures(config.GetConnectionString("ClothesStoreConnection"));
        }

        private static CategoryRepositoryAdvanced CreateRepositoryAdvanced()
        {
            return new CategoryRepositoryAdvanced(config.GetConnectionString("ClothesStoreConnection"));
        }

        #endregion
    }
}
