using Simsy.Models;

public class Item
{
    public string Name { get; set; }
    public string Type { get; set; }  
    public int Value { get; set; }    

    public Item(string name, string type, int value)
    {
        Name = name;
        Type = type;
        Value = value;
    }

    public void ApplyEffect(Character person)
    {
        switch (Type)
        {
            case "Jedzenie":
                person.Statistics.Hunger = Math.Max(0, person.Statistics.Hunger - Value);
                Console.WriteLine($"{person.Name} spozywa {Name}. Glod zmniejsza sie.");
                break;
            case "Prezent":
                person.Statistics.Happiness += Value;
                Console.WriteLine($"{person.Name} ucieszyl sie z {Name}. Szczescie wzroslo.");
                break;
            case "Paliwo":
                Console.WriteLine($"{Name} moze byc uzyte w pojezdzie.");
                break;
            default:
                Console.WriteLine($"{Name} nie ma efektu.");
                break;
        }
    }
}
