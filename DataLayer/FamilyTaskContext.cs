using Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FamilyTaskContext : DbContext
    {

        public FamilyTaskContext(DbContextOptions<FamilyTaskContext> options):base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Member> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>(entity => {
                entity.HasKey(k => k.Id);
                entity.ToTable("Member");
            });

            modelBuilder.Entity<Task>(entity => {
                entity.HasKey(k => k.Id);
                entity.ToTable("Task");
                entity.HasOne(x => x.AssignMember)
                    .WithMany(x => x.Tasks)
                    .HasForeignKey(x=>x.AssignedMemberId);
            });
        }
    }
}