using System;
using System.Collections.Generic;

namespace SE.Models;

public partial class RepeatedTransaction
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public int? Amount { get; set; }

    public int? TransactionTypeId { get; set; }

    public string? TransactionDate { get; set; }

    public string? Description { get; set; }

    public string? UpdatedDate { get; set; }

    public string? Frequency { get; set; }

    public string? DeletedDate { get; set; }

    public bool? DeleteTransaction { get; set; }
}
