using Simsy.Interface;

namespace Simsy.Models
{
    public class Statistics : IStat
    {
        public int Life { get; set; } = 100;
        public int Hunger { get; set; } = 0;
        public int Sleep { get; set; } = 100;
        public int Communication { get; set; } = 50;
        public int Happiness { get; set; } = 50;
        public bool Sadness { get; set; } = false;

        public void Update()
        {
            Hunger += 2;
            Sleep -= 2;
            Communication -= 1;
            Life = 100 - Hunger + Sleep / 2 + Communication / 2 + Happiness / 4;
        }
    }
}
