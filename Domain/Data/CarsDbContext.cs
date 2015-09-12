using System.Data.Entity;
using Domain.Model;

namespace Domain.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext()
        {
            // Disabling any Entity Framework DB initialization/auto-migration
            Database.SetInitializer<CarsDbContext>(null);
        }

        public IDbSet<Car> Cars { get; set; } 
    }
}
