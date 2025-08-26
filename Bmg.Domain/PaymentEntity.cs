namespace Bmg.Domain;

public class PaymentEntity : AuditableEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CartId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public Guid TransactionId { get; set; }
    public virtual CartEntity? Cart { get; set; }
}
