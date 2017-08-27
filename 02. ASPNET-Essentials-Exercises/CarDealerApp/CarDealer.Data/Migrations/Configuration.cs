using System.Data.Entity.Migrations;

namespace CarDealer.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CarDealer.Data.CarDealerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CarDealer.Data.CarDealerContext context)
        { 
        }
    }
}
