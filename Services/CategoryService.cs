using Microsoft.Extensions.Azure;
using Project.Controllers;

namespace Project.Services;

public class CategoryService
{
    public List<Category> GetAllExpenseCategories()
    {
        var categories = new List<Category>();
        
        categories.Add(new Category()
        {
            Id = 1,
            Name = "Храна"
        });
        
        categories.Add(new Category()
        {
            Id = 2,
            Name = "Превоз"
        });
        
        categories.Add(new Category()
        {
            Id = 3,
            Name = "Сметки"
        });
        
        categories.Add(new Category()
        {
            Id = 4,
            Name = "Останато"
        });

        return categories;
    }
    
    public List<Category> GetAllIncomeCategories()
    {
        var categories = new List<Category>();
        
        categories.Add(new Category()
        {
            Id = 1,
            Name = "Плата"
        });
        
        categories.Add(new Category()
        {
            Id = 2,
            Name = "Патни трошоци"
        });
        
        categories.Add(new Category()
        {
            Id = 3,
            Name = "Останато"
        });

        return categories;
    }
}