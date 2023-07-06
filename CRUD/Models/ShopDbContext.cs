using Microsoft.EntityFrameworkCore;



namespace CRUD.Models
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) 
        {
        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6SNBLBF\\SQLEXPRESS;Initial Catalog=Test;Encrypt=False;Integrated Security=True");
        }
       /* protected override void OnModelCreating(ModelBuilder modelBuilder)//multiple primary keys
        {
            modelBuilder.Entity<User>()
                .HasKey(u => new { u.Id, u.NIC });
        }*/
    }
}