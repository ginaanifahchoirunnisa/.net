using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.customer_id); 


            modelBuilder.Entity<Food>()
              .HasKey(f => f.food_id);


            modelBuilder.Entity<Transaction>()
                .HasKey(t => new { t.transaction_id });

            modelBuilder.Entity<Transaction>()
                .HasOne(c => c.Customer)
                .WithMany(t => t.Transactions)
                .HasForeignKey(tr => tr.customer_id);

            modelBuilder.Entity<Transaction>()
                .HasOne(f => f.Food)
                .WithMany(t => t.Transactions)
                .HasForeignKey(tr => tr.food_id);

               
        }





    }
}
