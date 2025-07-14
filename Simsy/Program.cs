using System;
using System.Collections.Generic;
using System.Linq;
using Simsy.Models;
using Simsy.Service;

namespace Simsy
{
    class Program
    {
        static void ShowStatistics(Character sim)
        {
            Console.WriteLine($"Imie: {sim.Name} | Zycie: {sim.Statistics.Life} | Sen: {sim.Statistics.Sleep} | Glod: {sim.Statistics.Hunger}");
            Console.WriteLine($"Komunikacja: {sim.Statistics.Communication} | Szczescie: {sim.Statistics.Happiness} | Saldo: {sim.Wallet.Balance} zl");
        }

        static int ChooseNumber(string question, int max)
        {
            Console.Write(question);
            if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= max)
                return number - 1;
            Console.WriteLine("Bledny wybor.");
            return -1;
        }

        static void TalkToCharacter(List<NPC> acquaintances, Character sim)
        {
            Console.WriteLine("Z kim chcesz rozmawiac?");
            for (int i = 0; i < acquaintances.Count; i++)
                Console.WriteLine($"{i + 1}. {acquaintances[i].Name} ({acquaintances[i].Gender}) — Relacja: {acquaintances[i].RelationshipLevel}");

            int index = ChooseNumber("Wybierz numer: ", acquaintances.Count);
            if (index >= 0)
            {
                var npc = acquaintances[index];
                npc.Talk(sim);

                if (npc.Gender == "Kobieta" && npc.RelationshipLevel >= 60)
                {
                    Console.Write("Czy chcesz pocalowac ja? (T/N): ");
                    if (Console.ReadLine()?.ToUpper() == "T")
                        npc.Kiss(sim);
                }
            }
        }

        static void Main()
        {
            Console.Write("Podaj imie postaci: ");
            string name = Console.ReadLine();
            var rng = new Random();

            var sim = new Character(name, 25);
            var shop = new Shop();
            shop.Products.Add(new Product("Pizza", 100));
            shop.Products.Add(new Product("Motocykl", 1500));
            shop.Products.Add(new Product("Laptop", 900));

            var allNpc = new List<NPC>
            {
                new NPC("Anna", 24, "Kobieta", new List<string> { "Baking pies", "Listening to sea shanties", "Shell collecting", "Telling ghost stories"}),
                new NPC("Jacek", 30, "Mezczyzna", new List < string > { "Trading", "Storytelling", "Fishing" }),
                new NPC("Daria", 22, "Kobieta", new List<string> { "Gadget building", "Steam engines", "Sketching blueprints", "Puzzle solving", "Tinkering with tools" }),
                new NPC("Henry", 27, "Mezczyzna", new List<string> { "Tracking animals", "Archery", "Herbal medicine", "Campfire cooking", "Wood carving" }
)
            };

            bool game = true;
            while (game)
            {
                Console.Clear();
                ShowStatistics(sim);

                Console.WriteLine("\nWybierz akcje:");
                Console.WriteLine("1. Pracuj");
                Console.WriteLine("2. Spij");
                Console.WriteLine("3. Jedz");
                Console.WriteLine("4. Rozmowa z NPC");
                Console.WriteLine("5. Sklep");
                Console.WriteLine("6. Poznaj nowa osobe");
                Console.WriteLine("7. Zakoncz gre");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    sim.Wallet.Earn(300);
                    sim.Statistics.Sleep -= 10;
                    Console.WriteLine("Zarobiles 300 zl. Sen -10.");
                }
                else if (choice == "2")
                {
                    sim.Sleep();
                    Console.WriteLine("Postac pospala.");
                }
                else if (choice == "3")
                {
                    sim.Eat();
                    Console.WriteLine("Postac zjadla.");
                }
                else if (choice == "4")
                {
                    var met = allNpc.Where(n => n.Met).ToList();
                    if (met.Count == 0)
                        Console.WriteLine("Nie znasz jeszcze zadnych osob.");
                    else
                        TalkToCharacter(met, sim);
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Dostepne produkty:");
                    for (int i = 0; i < shop.Products.Count; i++)
                        Console.WriteLine($"{i + 1}. {shop.Products[i].Name} – {shop.Products[i].Price} zl");

                    int number = ChooseNumber("Wybierz numer produktu: ", shop.Products.Count);
                    if (number >= 0)
                    {
                        var product = shop.Products[number];
                        bool purchased = shop.Buy(sim, product);
                        Console.WriteLine(purchased ? $"Kupiono: {product.Name}" : $"Nie stac cie na: {product.Name}");
                    }
                }
                else if (choice == "6")
                {
                    var newOnes = allNpc.Where(n => !n.Met).ToList();
                    if (newOnes.Count == 0)
                        Console.WriteLine("Znasz juz wszystkich.");
                    else
                    {
                        var person = newOnes[rng.Next(newOnes.Count)];
                        Console.WriteLine($"Spotykasz nowa osobe: {person.Name} ({person.Gender})");
                        person.Greet(sim);
                    }
                }
                else if (choice == "7")
                {
                    game = false;
                    Console.WriteLine("Gra zakonczona.");
                }
                else
                {
                    Console.WriteLine("Nieznana komenda.");
                }

                sim.Statistics.Update();
                Console.WriteLine("\nNacisnij Enter, aby kontynuowac...");
                Console.ReadLine();
            }
        }
    }
}
