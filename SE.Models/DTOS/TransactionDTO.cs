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

    public class TransactionDtoList
    {
        public int TransactionId { get; set; }

        public int? UserId { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public int? Amount { get; set; }

        public int? TransactionTypeId { get; set; }
        public string? TransactionName { get; set; } 

        public string? TransactionDate { get; set; }

        public string? Description { get; set; }

        public string? UpdatedDate { get; set; }

        public string? DeletedDate { get; set; }

        public bool? DeleteTransaction { get; set; }

    }





}
