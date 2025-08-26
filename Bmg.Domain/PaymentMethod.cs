using System.ComponentModel;

namespace Bmg.Domain;

public enum PaymentMethod
{
    [Description("Cartão de Crédito")]
    CreditCard,
    [Description("PIX")]
    Pix,
}