using Microsoft.EntityFrameworkCore;
using PyggApi.Models;

namespace PyggApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    
        public DbSet<User> Users { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberAccount> MemberAccounts { get; set; }
        public DbSet<MemberRole> MemberRoles { get; set; }
        public DbSet<MemberTransaction> MemberTransactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<GroupFineType> GroupFineTypes { get; set; }
        public DbSet<GroupAccountType> GroupAccountTypes { get; set; }
        public DbSet<GroupAccount> GroupAccounts { get; set; }
        public DbSet<GroupContribution> GroupContributions { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<GroupDepositInstallment> GroupDepositInstallments { get; set; }
        public DbSet<DefaultDetail> DefaultDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SQL8005.site4now.net;Initial Catalog=db_a99d70_pygg;User Id=db_a99d70_pygg_admin;Password=MyXamrin2016*");
            //optionsBuilder.UseSqlServer(@"Server=OR001;Database=db_a99d70_pygg;Persist Security Info = False; User Id=sa;Password=Myxamarin2017*; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = True; Connection Timeout = 30;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:orrealestateserver.database.windows.net,1433; Initial Catalog = Pygg; Persist Security Info = False; User ID = mwakai; Password =MyXamrin2016*; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 2000;");
            //optionsBuilder.UseSqlServer(@"Server=OR001;Database=db_a99d70_pygg;Persist Security Info = False; User Id=sa;Password=Myxamarin2017*; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = True; Connection Timeout = 30;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:OR001, Initial Catalog = PYGG; Persist Security Info = False; User ID = sa; Password =Myxamarin2017*; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");

            //optionsBuilder.UseSqlServer(@"Server=tcp:orrealestateserver.database.windows.net,1433;Initial Catalog=RealEstateDb;Persist Security Info=False;User ID=mwakai;Password=MyXamrin2016*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ReaEstateDb01;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberRole>()
                .HasKey(e => e.RoleId);

            modelBuilder.Entity<MemberAccount>()
               .HasKey(e => new { e.AccountNumber, e.AccountGroupId, e.AccountMemberId });

            base.OnModelCreating(modelBuilder);

        }
    }
}
