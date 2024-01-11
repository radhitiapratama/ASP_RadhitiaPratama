using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASP_RadhitiaPratama.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
