using System.ComponentModel;

namespace Bmg.Domain;

public enum UserRole
{
    [Description("Administrador")]
    Admin,
    [Description("Cliente")]
    Customer
}
