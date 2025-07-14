using Simsy.Interface;

namespace Simsy.Models
{
    public class Character : IAKCharacter
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public IStat Statistics { get; set; }
        public IFin Wallet { get; set; }

        public Character(string name, int age)
        {
            Name = name;
            Age = age;
            Statistics = new Statistics();
            Wallet = new Finance();
        }

        public void Eat() => Statistics.Hunger = Math.Max(0, Statistics.Hunger - 30);
        public void Sleep() => Statistics.Sleep = Math.Min(100, Statistics.Sleep + 40);
        public void Talk()
        {
            Statistics.Communication = Math.Min(100, Statistics.Communication + 30);
            Statistics.Sadness = false;
        }
    }
}
