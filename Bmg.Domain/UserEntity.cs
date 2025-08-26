namespace Bmg.Domain;

public class UserEntity : AuditableEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public virtual ICollection<CartEntity> Carts { get; set; } = [];
}
