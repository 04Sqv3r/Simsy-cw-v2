namespace Simsy.Interface
{
    public interface IFin
    {
        decimal Balance { get; set; }
        void Earn(decimal amount);
        void Spend(decimal amount);
    }
}
