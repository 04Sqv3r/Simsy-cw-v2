using Simsy.Interface;

namespace Simsy.Models
{
    public class Finance : IFin
    {
        public decimal Balance { get; set; } = 1000m;
        public void Earn(decimal amount) => Balance += amount;
        public void Spend(decimal amount) => Balance -= amount;
    }
}
