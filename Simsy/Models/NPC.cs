using Simsy.Interface;
using Simsy.Models;

public enum NPCEmotion
{
    Neutral,
    Happy,
    Angry,
    Surprised,
    Sad
}
public class NPC : INPC
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int RelationshipLevel { get; set; } = 0;
    public bool Met { get; set; } = false;
    public string Gender { get; set; }
    public NPCEmotion CurrentEmotion { get; private set; } = NPCEmotion.Neutral;
    public List<string> Interests { get; set; }
    public string PersonalityType { get; set; } = "Neutral";  
    private Random rng = new Random();

    public NPC(string name, int age, string gender, List<string> interests)
    {
        Name = name;
        Age = age;
        Gender = gender;
        Interests = interests;
    }

    public void Greet(Character character)
    {
        Met = true;
        CurrentEmotion = (NPCEmotion)rng.Next(0, 5);
        Console.WriteLine($"{Name} ({Gender}) mowi:");

        switch (CurrentEmotion)
        {
            case NPCEmotion.Happy:
                Console.WriteLine("\"Hej! Ale sie ciesze ze cie widze!\"");
                RelationshipLevel += 10;
                break;
            case NPCEmotion.Angry:
                Console.WriteLine("\"Ojej... znowu ty...\"");
                RelationshipLevel -= 5;
                break;
            case NPCEmotion.Surprised:
                Console.WriteLine("\"O! Nie spodziewalem sie ciebie.\"");
                RelationshipLevel += 2;
                break;
            case NPCEmotion.Sad:
                Console.WriteLine("\"Hej... mam dzis slaby dzien.\"");
                RelationshipLevel -= 2;
                break;
            default:
                Console.WriteLine("\"Czesc.\"");
                break;
        }
    }

    public void Talk(Character character)
    {
        Console.WriteLine($"{Name}: \"Fajnie bylo pogadac.\"");
        RelationshipLevel += 5;
        character.Statistics.Happiness += 3;
        CurrentEmotion = NPCEmotion.Happy;
    }

    public void GiveGift(Character character, Gift gift)
    {
        Console.WriteLine($"{Name} otrzymuje prezent: {gift.Name}");

        if (Interests.Contains(gift.Type))
        {
            Console.WriteLine($"{Name} uwielbia takie rzeczy!");
            RelationshipLevel += 10;
            CurrentEmotion = NPCEmotion.Happy;
        }
        else
        {
            Console.WriteLine($"{Name} przyjmuje prezent, ale nie jest zachwycony.");
            RelationshipLevel += 3;
            CurrentEmotion = NPCEmotion.Neutral;
        }
    }

    public void Kiss(Character character)
    {
        if (RelationshipLevel < 60 || Gender != "Kobieta")
        {
            Console.WriteLine("Nie mozesz sprobowac pocalunku z ta osoba.");
            return;
        }

        Console.WriteLine($"Probujesz pocalowac {Name}...");

        int reaction = rng.Next(0, 3);
        switch (reaction)
        {
            case 0:
                Console.WriteLine($"{Name} odwzajemnia pocalunek.");
                RelationshipLevel += 15;
                CurrentEmotion = NPCEmotion.Happy;
                break;
            case 1:
                Console.WriteLine($"{Name} mowi: \"To za szybko...\"");
                RelationshipLevel -= 10;
                CurrentEmotion = NPCEmotion.Sad;
                break;
            case 2:
                Console.WriteLine($"{Name} odsuwa sie i jest zla.");
                RelationshipLevel -= 20;
                CurrentEmotion = NPCEmotion.Angry;
                break;
        }
    }
}
