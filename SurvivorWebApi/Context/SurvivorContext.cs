using Microsoft.EntityFrameworkCore;
using SurvivorWebApi.Models;

namespace SurvivorWebApi.Context
{
    public class SurvivorContext:DbContext
    {
        public SurvivorContext(DbContextOptions<SurvivorContext> options):base(options) { }
       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Competitor> Competitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relation tables
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Competitor>().HasOne(e => e.Category)
                .WithMany(e => e.Competitors)
                .HasForeignKey(e => e.CategoryId);

            // data seeding
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1,Name="Ünlüler",IsDeleted=false,CreateDate=Convert.ToDateTime("2024-01-01"), ModifiedDate=Convert.ToDateTime("2024-01-01")},
                new Category { Id=2,Name="Gönüllüler",IsDeleted=false, CreateDate = Convert.ToDateTime("2024-01-01"), ModifiedDate = Convert.ToDateTime("2024-01-01") }
                );

            modelBuilder.Entity<Competitor>().HasData(
                new Competitor { Id = 1 ,FirstName="Kıvanç", LastName="Tatlıtuğ",IsDeleted=false, CreateDate = Convert.ToDateTime("2024-01-01"), ModifiedDate = Convert.ToDateTime("2024-01-01") ,CategoryId =1},
                 new Competitor { Id = 2, FirstName = "Tuğba", LastName = "Büyüküstün", IsDeleted = false, CreateDate = Convert.ToDateTime("2024-01-01"), ModifiedDate = Convert.ToDateTime("2024-01-01"), CategoryId = 1 },
                  new Competitor { Id = 3, FirstName = "Ahmet", LastName = "Yılmaz", IsDeleted = false, CreateDate = Convert.ToDateTime("2024-01-01"), ModifiedDate = Convert.ToDateTime("2024-01-01"), CategoryId = 2 },
                   new Competitor { Id = 4, FirstName = "Melike", LastName = "Göz", IsDeleted = false, CreateDate = Convert.ToDateTime("2024-01-01"), ModifiedDate = Convert.ToDateTime("2024-01-01"), CategoryId = 2}
                );

        }





    }
}
