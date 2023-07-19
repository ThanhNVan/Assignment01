﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assignment01.EntityProviders;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Discount { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
