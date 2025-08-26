using System.ComponentModel;

namespace Bmg.Domain;

public enum PaymentStatus
{
    [Description("Aprovado")]
    Approved,
    [Description("Recusado")]
    Rejected,
}