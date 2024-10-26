using SE.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Inteface
{
    public interface ITransaction
    {
        Task<TransactionDTO> AddTransaction(TransactionDTO transactionData);
    }
}
