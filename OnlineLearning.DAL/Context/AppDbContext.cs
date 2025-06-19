using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CafeMenuItem>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<PaidBook>()
               .Property(p => p.Price)
               .HasPrecision(18, 2); // 18 ümumi rəqəm, 2 onluqdan sonra
            modelBuilder.Entity<PurchasedBook>()
        .HasOne(p => p.Book)
        .WithMany(b => b.PurchasedBooks)
        .HasForeignKey(p => p.BookId)
        .OnDelete(DeleteBehavior.Cascade);

        }
        

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<NewsEvent> NewsEvents { get; set; }

        public   DbSet<EmployeeComment> EmployeeComments { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<CafeMenuItem> CafeMenuItems { get; set; }

        public DbSet<PaidBook> PaidBooks { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public  DbSet<CartItem> CartItems { get; set; }

        public DbSet<PurchasedBook> PurchasedBooks { get; set; }


    }
}
