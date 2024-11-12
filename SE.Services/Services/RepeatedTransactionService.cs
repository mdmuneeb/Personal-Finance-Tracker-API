using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SE.Models;
using SE.Models.DTOS;
using SE.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Services
{
    public class RepeatedTransactionService: BaseService<RepeatedTransaction>, IRepeatedTransaction
    {
        public RepeatedTransactionService(PersonalFinanceTrackerContext context):base(context)
        {
            
        }

        public async Task<RTransactionDTO> AddGoal(RepeatedTransaction Rdata)
        {
            var transaction = await BeginTransaction();

            try
            {
                if (Rdata != null)
                {
                    _context.RepeatedTransactions.Add(Rdata);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new RTransactionDTO
                    {
                        Status = true,
                        Message = "Successfully Entered the Trasnaction",
                        transactionData = Rdata
                    };
                }
                return new RTransactionDTO
                {
                    Status = false,
                    Message = "The Data Received is Empty",
                    transactionData = Rdata
                };
            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                return new RTransactionDTO
                {
                    Status = false,
                    Message = $"{ex.Message}",
                    transactionData = Rdata
                };
            }
        }

        public async Task<List<TransactionDtoList>> GetAllRepeatedTransactionList(int userId)
        {
            var data = await _context.RepeatedTransactions.Where(x=>x.UserId == userId)
                .Select(transaction => new TransactionDtoList
                {
                    TransactionId = transaction.Id,
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

            return data;
        } 

        public async Task<RepeatedTransaction> GetASingleTransaction(int userID, int transactionID)
        {
            var data = await _context.RepeatedTransactions.Where(x=>x.UserId==userID && x.Id == transactionID).FirstOrDefaultAsync();
            return data;
        }


        public async Task<RTransactionDTO> UpdateATransaction(RepeatedTransaction newData)
        {
            var transaction = await BeginTransaction();
            try
            {
                var exisitingData = await GetASingleTransaction((int)newData.UserId, newData.Id);
                if (exisitingData != null)
                {
                    //_context.RepeatedTransactions.Update(newData);
                    _context.Entry(exisitingData).CurrentValues.SetValues(newData);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new RTransactionDTO
                    {
                        Status = true,
                        Message = "Successfully Updated the Trasnaction",
                        transactionData = newData
                    };
                }
                return new RTransactionDTO
                {
                    Status = false,
                    Message = "This transaction doesnot exist",
                    transactionData = newData
                };
            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                return new RTransactionDTO
                {
                    Status = false,
                    Message = $"{ex.Message}",
                    transactionData = newData
                };

            }
        }

        public async Task<RTransactionDTO> DeleteATransaction(RepeatedTransaction RData)
        {
            var transaction = await BeginTransaction();
            try
            {
                var existingData = await GetASingleTransaction((int)RData.UserId, RData.Id);
                if (existingData != null)
                {
                    _context.RepeatedTransactions.Remove(existingData);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new RTransactionDTO
                    {
                        Status = true,
                        Message = "Successfully Deleted Transaction",
                        transactionData = existingData
                    };
                }
                return new RTransactionDTO
                {
                    Status = false,
                    Message = "This transaction doesnot exist",
                    transactionData = RData
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new RTransactionDTO
                {
                    Status = false,
                    Message = $"{ex.Message}",
                    transactionData = RData
                };
            }

        }
    }
}
