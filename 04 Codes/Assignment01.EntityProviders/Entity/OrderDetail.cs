using System.Text.Json.Serialization;

namespace Assignment01.EntityProviders;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Discount { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public virtual Order? Order { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public virtual Product? Product { get; set; }
}
