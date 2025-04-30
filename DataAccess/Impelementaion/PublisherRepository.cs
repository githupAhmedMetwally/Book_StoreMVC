using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Repositiries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Impelementaion
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext context;

        public PublisherRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        
        
        public void Update(Publisher publisher)
        {
            var PublisherInDb = context.Publishers.FirstOrDefault(x => x.Id == publisher.Id);
            if (PublisherInDb != null)
            {
                PublisherInDb.Name = publisher.Name;
                PublisherInDb.Phone=publisher.Phone;
                PublisherInDb.Address=publisher.Address;
            }
    }
}
}
