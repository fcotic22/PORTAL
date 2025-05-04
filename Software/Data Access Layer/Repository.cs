using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public abstract class Repository<T> : IDisposable where T : class
        {
            public PORTAL_DBContext Context { get; set; }
            public DbSet<T> Entities { get; set; }
            protected Repository(PORTAL_DBContext context)
            {
                Context = context;
                Entities = Context.Set<T>();
            }

            public virtual IQueryable<T> GetAll()
            {
                var query = from s in Entities
                            select s;
                return query;
            }

            public virtual int Add(T entity, bool saveChanges = true)
            {
                Entities.Add(entity);
                if (saveChanges)
                {
                    return SaveChanges();
                }
                else
                {
                    return 0;
                }
            }

            public abstract int Update(T entity, bool saveChanges = true);

            public virtual int Remove(T entity, bool saveChanges = true)
            {
                Entities.Attach(entity);
                Entities.Remove(entity);
                if (saveChanges)
                {
                    return SaveChanges();
                }
                else
                {
                    return 0;
                }
            }

            public virtual void Dispose()
            {
                Context.Dispose();
                GC.SuppressFinalize(this);
            }

        public virtual int SaveChanges()
            {
                return Context.SaveChanges();
            }
    }
}

