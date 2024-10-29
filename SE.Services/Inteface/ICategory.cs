using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Inteface
{
    public interface ICategory
    {
        Task<CategoriesTypeExpense> AddIncomeCategory(CategoriesTypeExpense category);
        Task<List<CategoriesTypeExpense>> GetIncomeCategory();
        Task<CategoryTypeExpense> AddExpenseCategory(CategoryTypeExpense category);
        Task<List<CategoryTypeExpense>> GetExpenseCategory();
    }
}
