using SE.Models.DTOS;
using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Inteface
{
    public interface IRepeatedTransaction
    {
        Task<RTransactionDTO> AddGoal(RepeatedTransaction Rdata);
        Task<List<RepeatedTransaction>> GetAllRepeatedTransactionList(int userId);
        Task<RepeatedTransaction> GetASingleTransaction(int userID, int transactionID);
        Task<RTransactionDTO> UpdateATransaction(RepeatedTransaction newData);
        Task<RTransactionDTO> DeleteATransaction(RepeatedTransaction RData);
    }
}
