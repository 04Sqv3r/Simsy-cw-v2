using Simsy.Interface;
using Simsy.Models;

namespace Simsy.Service
{
    public class Shop : shop
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public bool Buy(Character character, Product product)
        {
            if (character.Wallet.Balance >= product.Price)
            {
                character.Wallet.Spend(product.Price);
                return true;
            }
            return false;
        }
    }
}
