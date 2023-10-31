namespace MVC5KurumsalWebSitesi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC5KurumsalWebSitesi.Models.Context.KurumsalContext>
    {
        public Configuration()
        {
            /* bunu false býraksaydýk her seferinde add-migration diyecektik true dediðimiz için add-migration demeden sadace update-database dememiz yeterli olacak */
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MVC5KurumsalWebSitesi.Models.Context.KurumsalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
