using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASP_RadhitiaPratama.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Brand { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public double Price { get; set; }

    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
