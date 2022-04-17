using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
    public class ShoppingCardRepository : Repository<ShoppingCard>, IShoppingCardRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(ShoppingCard shoppingCard, int count)
        {
            shoppingCard.Count -=count;
            _db.SaveChanges();
            return shoppingCard.Count;  
        }

        public int IncrementCount(ShoppingCard shoppingCard, int count)
        {
            shoppingCard.Count += count;
            _db.SaveChanges();
            return shoppingCard.Count;
        }
    }
}
