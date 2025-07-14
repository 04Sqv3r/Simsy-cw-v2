namespace Simsy.Interface
{
    public interface IStat
    {
        int Life { get; set; }
        int Hunger { get; set; }
        int Sleep { get; set; }
        int Communication { get; set; }
        int Happiness { get; set; }
        bool Sadness { get; set; }

        void Update();
    }
}
