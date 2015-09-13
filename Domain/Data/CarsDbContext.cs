using System.Data.Entity;
using Domain.Model;

namespace Domain.Data
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext()
        {
           Initialize();
        }

        public CarsDbContext(string connectionString)
            : base(connectionString)
        {
           Initialize();
        }

        private void Initialize()
        {
             // Disabling any Entity Framework DB initialization/auto-migration
            Database.SetInitializer<CarsDbContext>(null);
        }

        public IDbSet<Car> Cars { get; set; } 
    }
}
