using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class BuyerService
    {
        public static List<Buyer> GetAllBuyers()
        {
            using (var repo = new BuyerRepository())
            {
                return repo.GetAll().ToList();
            }
        }

        public static Buyer GetBuyerById(int id)
        {
            using (var repo = new BuyerRepository())
            {
                return repo.GetBuyerById(id).FirstOrDefault();
            }
        }

        public void AddNewBuyer(Buyer newBuyer)
        {
            using (var repo = new BuyerRepository())
            {
                repo.Add(newBuyer);
            }
        }

        public void RemoveBuyer(Buyer buyer)
        {
            using (var repo = new BuyerRepository())
            {
                repo.Remove(buyer);
            }
        }

        public void UpdateBuyer(Buyer newBuyer)
        {
            using (var repo = new BuyerRepository())
            {
                repo.Update(newBuyer);
            }
        }
    }
}
