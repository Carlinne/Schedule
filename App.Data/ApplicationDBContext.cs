using App.Models;
using App.Models.Schedule;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
           : base(options)
        {

        }

        //Schedule
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configuration
            builder.Entity<Status>().ToTable("Statuses", "tb");

            //Schedule
            builder.Entity<Property>().ToTable("Properties", "tr");
            builder.Entity<Activity>().ToTable("Activities", "tr");
            builder.Entity<Survey>().ToTable("Surveys", "tr");

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }
    }
}
