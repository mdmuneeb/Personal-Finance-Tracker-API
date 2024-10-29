using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SE.Models;
using SE.Services.Inteface;
using System.Runtime.InteropServices;

namespace SE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;
        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("AddCategoryIncome")]
        public async Task<IActionResult> AddCategoryIncome([FromBody] CategoriesTypeExpense categoryData)
        {
            try
            {
                var data = await _categoryService.AddIncomeCategory(categoryData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCategoryIncome")]
        public async Task<IActionResult> getIncomeCategory()
        {
            var data = await _categoryService.GetIncomeCategory();
            return Ok(data);
        }
        
        
        
        [HttpGet]
        [Route("GetCategoryExpense")]
        public async Task<IActionResult> getExpenseCategory()
        {
            var data = await _categoryService.GetExpenseCategory();
            return Ok(data);
        }

        [HttpPost]
        [Route("AddCategoryExpense")]
        public async Task<IActionResult> AddCategoryExpense([FromBody] CategoryTypeExpense categoryData)
        {
            try
            {
                var data = await _categoryService.AddExpenseCategory(categoryData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
