using System.Text.Json.Serialization;
using Bmg.Domain;

namespace Bmg.Application.Services.Payments.Models;

public abstract class PayRequest
{
    [JsonIgnore]
    public abstract PaymentMethod Method { get; }
}