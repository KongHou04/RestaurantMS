using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class RMContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Demo connection - Hard Code
        //    // Config connStr in DI Container/ appsettings.json
        //    //const string connectSqlString = "Server=LAPTOP-BV87SDMM\\HAU;Database=DemoMyRM;Integrated Security=True; TrustServerCertificate=True;";
        //    const string connectNpgsqlString = "Host=localhost;Database=RM-Project1;Username=postgres;Password=123";
        //    optionsBuilder.UseNpgsql(connectNpgsqlString);
        //}

        public RMContext(DbContextOptions<RMContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(e =>
            {
                e.HasOne(a => a.Employee)
                    .WithOne(e => e.Account)
                    .HasForeignKey<Account>(a => a.EmployeeID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Bill>(e =>
            {
                e.HasOne(b => b.Order)
                    .WithOne(o => o.Bill)
                    .HasForeignKey<Bill>(b => b.OrderID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.HasMany(c => c.Products)
                    .WithOne(c => c.Category)
                    .HasForeignKey(c => c.CategoryID)
                    .OnDelete(DeleteBehavior.SetNull);
                e.HasIndex(c => c.Name);
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.HasMany(od => od.OrderDetails)
                    .WithOne(o => o.Order)
                    .HasForeignKey(od => od.OrderID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<OrderDetail>(e =>
            {


            });
            modelBuilder.Entity<Product>(e =>
            {
                e.HasMany(p => p.OrderDetails)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.ProductID)
                    .OnDelete(DeleteBehavior.SetNull);
                e.HasIndex(p => p.Name);
            });
            modelBuilder.Entity<Area>(e =>
            {
                e.HasMany(r => r.Tables)
                    .WithOne(t => t.Area)
                    .HasForeignKey(t => t.AreaID)
                    .OnDelete(DeleteBehavior.SetNull);
                e.HasIndex(r => r.Name);
            });
            modelBuilder.Entity<Role>(e =>
            {
                e.HasMany(ro => ro.Employees)
                    .WithOne(u => u.Role)
                    .HasForeignKey(t => t.RoleID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Table>(e =>
            {
                e.HasMany(t => t.Orders)
                    .WithOne(o => o.Table)
                    .HasForeignKey(t => t.TableID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Employee>(e =>
            {
                e.HasMany(u => u.Orders)
                    .WithOne(o => o.Employee)
                    .HasForeignKey(o => o.EmployeeID)
                    .OnDelete(DeleteBehavior.SetNull);
                e.HasIndex(u => u.Phone);
                e.HasIndex(u => u.Email);
            });
        }
    }
}
