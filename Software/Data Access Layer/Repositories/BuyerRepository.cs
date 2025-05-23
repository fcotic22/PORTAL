using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class BuyerRepository : Repository<Buyer>
    {
        public BuyerRepository() : base(new PORTAL_DBContext()) { }

        public IQueryable<Buyer> GetBuyerById(int id)
        {
            var query = from buyer in Entities
                        where buyer.id == id
                        select buyer;
            return query;
        }

        public override int Update(Buyer entity, bool saveChanges = true)
        {
            var buyer = GetBuyerById(entity.id).FirstOrDefault();
            buyer.name = entity.name;
            buyer.adress = entity.adress;
            buyer.city = entity.city;
            buyer.country = entity.country;
            buyer.zipCode = entity.zipCode;
            buyer.company = entity.company;
            buyer.oib = entity.oib;
            buyer.email = entity.email;
            buyer.phone = entity.phone;
            buyer.bankAccountNumber = entity.bankAccountNumber;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
