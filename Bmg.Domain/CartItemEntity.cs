namespace Bmg.Domain;

public class CartItemEntity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual CartEntity? Cart { get; set; }
    public virtual ProductEntity? Product { get; set; }
}
