using Microsoft.EntityFrameworkCore;
using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Services.Jobs
{
    public class TransactionJob: BaseService<Transaction>
    {
        public TransactionJob(PersonalFinanceTrackerContext context):base(context)
        {
            
        }

        public async Task TransferRepeatedTransactions()
        {
            var transaction = await BeginTransaction();
            try
            {
                var currentDate = DateTime.Now.Date;
                var repeatedTransactions = await _context.RepeatedTransactions
                    .Where(rt => DateTime.ParseExact(rt.TransactionDate, "dd/MM/yyyy", null).AddDays(30) == currentDate && rt.DeleteTransaction != true)
                    .ToListAsync();

                foreach (var repeatedTransaction in repeatedTransactions)
                {
                    var newTransaction = new Transaction
                    {
                        Amount = repeatedTransaction.Amount,
                        CategoryId = repeatedTransaction.CategoryId,
                        Description = repeatedTransaction.Description,
                        TransactionDate = currentDate.ToString(),
                        TransactionTypeId = repeatedTransaction.TransactionTypeId,
                        UpdatedDate = repeatedTransaction.UpdatedDate,
                        DeletedDate = repeatedTransaction.DeletedDate,
                        DeleteTransaction = repeatedTransaction.DeleteTransaction,
                        UserId = repeatedTransaction.UserId,
                    };
                    _context.Transactions.Add(newTransaction);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
