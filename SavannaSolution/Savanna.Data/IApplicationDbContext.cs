using Microsoft.EntityFrameworkCore;
using Savanna.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savanna.Data
{
    public interface IApplicationDbContext 
    {
        DbSet<User> User { get; set; }

        Task SaveChanges();
        Task Update<T>(T entity) where T : class;
    }
}
