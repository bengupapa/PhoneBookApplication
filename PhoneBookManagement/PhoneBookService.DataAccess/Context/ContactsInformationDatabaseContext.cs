using Microsoft.EntityFrameworkCore;
using PhoneBookService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBookService.DataAccess.Context
{
    public class ContactsInformationDatabaseContext: DbContext
    {
        private readonly string _connectionString;

        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumber { get; set; }

        public ContactsInformationDatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserProfileId);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
                entity.HasMany(e => e.PhoneNumbers);
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberId);
                entity.Property(e => e.SubscriberNumber).IsRequired();
                entity.Property(e => e.DialingCode).IsRequired(false);
                entity.Property(e => e.IsPrimary).IsRequired();
                entity.Property(e => e.Description).IsRequired();
            });
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public void SetEntityState<TEntity>(TEntity entity, int state) 
            where TEntity : class => Attach(entity).State = (EntityState)state;

        public async Task AddAsync<TEntity>(TEntity entity) => await AddAsync(entity);

        public async Task<TEntity> FindByIdAsync<TEntity>(object id) 
            where TEntity: class => await Set<TEntity>().FindAsync(id);
    }
}
