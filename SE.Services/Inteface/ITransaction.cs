using SE.Models;
using SE.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace SE.Services.Inteface
{
    public interface ITransaction
    {
        Task<TransactionDTO> AddTransaction(TransactionDTO transactionData);
        Task<List<TransactionDtoList>> GetAllTransaction(int id);
        Task<SE.Models.Transaction> UpdateTransaction(SE.Models.Transaction transactioData);
        Task<SE.Models.Transaction?> GetTransactionById(int? userId, int transactionId);
    }
}
