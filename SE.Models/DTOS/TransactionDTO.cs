using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Models.DTOS
{
    public class TransactionDTO
    {
        public int? UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? Amount { get; set; }

        public int? TransactionTypeId { get; set; }

        public string? Description { get; set; }
        public string? TransactionDate { get; set; }

    }
}
