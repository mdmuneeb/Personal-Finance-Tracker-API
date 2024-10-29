using Microsoft.EntityFrameworkCore;
using SE.Models;
using SE.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Services
{
    public class categoryService: BaseService<CategoriesTypeExpense>, ICategory
    {
        public categoryService(PersonalFinanceTrackerContext context): base(context)
        {
            
        }

        public async Task<CategoriesTypeExpense> AddIncomeCategory(CategoriesTypeExpense category)
        {
            var transaction = await BeginTransaction();

            try
            {
                _context.CategoriesTypes.Add(category);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return category;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }


        public async Task<CategoryTypeExpense> AddExpenseCategory(CategoryTypeExpense category)
        {
            var transaction = await BeginTransaction();

            try
            {
                //_context.CategoriesTypesExpense.Add(category);
                _context.CategoryTypeExpenses.Add(category);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return category;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<List<CategoriesTypeExpense>> GetIncomeCategory()
        {
            var data = await _context.CategoriesTypes.ToListAsync();
            return data;
        }

        public async Task<List<CategoryTypeExpense>> GetExpenseCategory()
        {
            var data = await _context.CategoryTypeExpenses.ToListAsync();
            return data;
        }

        public async Task<CategoriesTypeExpense> GetCategoryByIdIncome(int id)
        {
            var data = await _context.CategoriesTypes.Where(x=> x.CategoryId  == id).FirstOrDefaultAsync();
            return data;
        }        
        
        public async Task<CategoryTypeExpense> GetCategoryByIdExpense(int id)
        {
            var data = await _context.CategoryTypeExpenses.Where(x=> x.CategoryId  == id).FirstOrDefaultAsync();
            return data;
        }
    }
}
