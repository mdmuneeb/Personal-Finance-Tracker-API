using SE.Models;
using SE.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE.Models.DTOS;
using Microsoft.VisualBasic;

namespace SE.Services.Services
{
    public class transactionService: BaseService<Transaction>, ITransaction
    {
        public transactionService(PersonalFinanceTrackerContext context): base(context)
        {
            
        }


        public async Task<TransactionDTO> AddTransaction(TransactionDTO transactionData)
        {
            var transaction = await BeginTransaction();
            try
            {
                Transaction data = new Transaction
                {
                    UserId = transactionData.UserId,
                    CategoryId = transactionData.CategoryId,
                    Amount = transactionData.Amount,
                    TransactionTypeId = transactionData.TransactionTypeId,
                    TransactionDate = transactionData.TransactionDate,
                };
                _context.Add(data);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return transactionData;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
    }
}
