using System;
using System.Collections.Generic;

namespace SE.Models;

public partial class Goal
{
    public int GoalId { get; set; }

    public int? UserId { get; set; }

    public string? GoalName { get; set; }

    public string? Deadlline { get; set; }

    public string? CreatedDate { get; set; }

    public string? UpdatedDate { get; set; }

    public string? DeletedDate { get; set; }

    public bool? DeleteTransaction { get; set; }
}
