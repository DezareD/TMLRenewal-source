using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Data;
using RenewalTML.Data.Model;
using Volo.Abp.MultiTenancy;

namespace RenewalTML.EFCore
{
    [ConnectionStringName("Default")]
    public class ApplicationContext : AbpDbContext<ApplicationContext>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<CroppedImageFile> CroppedImageFiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Award> Awards { get; set; }
        public DbSet<ClientAward> ClientAwards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AdminAction> AdminActions { get; set; }
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
