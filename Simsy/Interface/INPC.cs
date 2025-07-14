using Simsy.Models;

namespace Simsy.Interface
{
    public interface INPC
    {
        string Name { get; set; }
        int Age { get; set; }
        int RelationshipLevel { get; set; }

        void Greet(Character character);
        void Talk(Character character);
        void GiveGift(Character character, Gift gift); 
        void Kiss(Character character);
    }
}
