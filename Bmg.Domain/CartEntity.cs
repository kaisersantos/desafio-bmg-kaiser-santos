namespace Bmg.Domain;

public class CartEntity : AuditableEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual PaymentEntity? Payment { get; set; }
    public virtual ICollection<CartItemEntity> Items { get; set; } = [];
}
