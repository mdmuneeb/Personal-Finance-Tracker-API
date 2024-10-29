using SE.Models;
using SE.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE.Models.DTOS;
using Microsoft.VisualBasic;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

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
                    Description = transactionData.Description,
                };
                _context.Transactions.Add(data);
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

        //public async Task<List<Transaction>> GetAllTransaction(int id)
        //{
        //    var data =  await _context.Transactions.Where(x => x.UserId == id).ToListAsync();

        //    if (data.Count > 0)
        //    {
        //        foreach (var item in data)
        //        {
        //            if(item.TransactionTypeId == 1)
        //            {
        //                var innerJoinQuery =
        //                from transaction in _context.Transactions
        //                join category in _context.CategoriesTypes on transaction.CategoryId equals category.CategoryId
        //                where transaction.UserId == id
        //            }
        //        }
        //    }
        //}

        public async Task<List<TransactionDtoList>> GetAllTransaction(int id)
        {
            var transactions = await _context.Transactions
                .Where(x => x.UserId == id)
                .Select(transaction => new TransactionDtoList
                {
                    TransactionId = transaction.TransactionId,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    CategoryName = transaction.TransactionTypeId == 1
                        ? _context.CategoriesTypes.FirstOrDefault(c => c.CategoryId == transaction.CategoryId).CategoryName
                        : _context.CategoryTypeExpenses.FirstOrDefault(c => c.CategoryId == transaction.CategoryId).CategoryName,
                    TransactionName = _context.TransactionTypes.FirstOrDefault(c => c.TransactionTypeId == transaction.TransactionTypeId).TransactionName,
                    TransactionTypeId = transaction.TransactionTypeId,
                    UserId = transaction.UserId,
                    TransactionDate = transaction.TransactionDate,
                    CategoryId = transaction.CategoryId,
                    UpdatedDate = transaction.UpdatedDate,
                    DeletedDate = transaction.DeletedDate,
                    DeleteTransaction = transaction.DeleteTransaction
                })
                .ToListAsync();

            return transactions;
        }


        public async Task<Transaction?> GetTransactionById(int? userId, int transactionId)
        {
            return await _context.Transactions.SingleOrDefaultAsync(x => x.TransactionId == transactionId && x.UserId == userId);
        }

        public async Task<Transaction?> GetTransactionByIdUserId(int? userId, int transactionId)
        {
            return await _context.Transactions.SingleOrDefaultAsync(x => x.TransactionId == transactionId && x.UserId == userId);
        }

        public async Task<Transaction> UpdateTransaction(Transaction transactioData)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingTransaction = await GetTransactionById(transactioData.UserId, transactioData.TransactionId);

                if (existingTransaction != null)
                {
                    
                    _context.Entry(existingTransaction).CurrentValues.SetValues(transactioData);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return existingTransaction;
                }

                return null; 
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the exception if necessary
                return null;
            }
        }


    }
}
