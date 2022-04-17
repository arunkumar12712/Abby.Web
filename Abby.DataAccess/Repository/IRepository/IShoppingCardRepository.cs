using Abby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository.IRepository
{
    public interface IShoppingCardRepository : IRepository<ShoppingCard>
    {
        int IncrementCount(ShoppingCard shoppingCard, int count);
        int DecrementCount(ShoppingCard shoppingCard, int count);
    }
}
