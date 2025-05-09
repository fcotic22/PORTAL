using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class ImageService
    {
        public Image GetLogo()
        {
            using (var repo = new ImageRepository())
            {
                return repo.GetLogo().First();
            }
        }
    }
}
