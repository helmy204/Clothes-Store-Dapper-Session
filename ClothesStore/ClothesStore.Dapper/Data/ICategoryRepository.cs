using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesStore.Dapper.Data
{
    public interface ICategoryRepository
    {
        Category Find(int id);
        List<Category> GetAll();
        Category Add(Category category);
        Category Update(Category category);
        void Remove(int id);

        Category GetFullCategory(int id);
        void Save(Category category);
    }
}
