using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assignment01.EntityProviders;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
