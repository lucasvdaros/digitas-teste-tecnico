using Digitas.Quotes.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Digitas.Quotes.Domain.ValueObjects;

public class Simulation
{   
    public string hashIdentificacao { get; set; }
    public decimal RequestAmount { get; set; }
    public OperationChoice Operation { get; set; }
    public Coin Coin { get; set; }
    public decimal Result { get; set; }
    public List<Quote>? Quotes { get; set; }

    public static string GenerateSHA256Hash(string input)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));

        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}
