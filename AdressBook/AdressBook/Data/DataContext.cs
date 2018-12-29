using System;
using AdressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AdressBook.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        // Specify DbSet properties etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add your own confguration here
            // series of statements
            modelBuilder.Entity<Contact>().Property(c => c.FirstName).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.LastName).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.TelephoneNumber).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.Address).IsRequired();

            modelBuilder.Entity<Contact>().Property(c => c.FirstName).HasMaxLength(40);
            modelBuilder.Entity<Contact>().Property(c => c.LastName).HasMaxLength(40);
            modelBuilder.Entity<Contact>().Property(c => c.TelephoneNumber).HasMaxLength(40);
            modelBuilder.Entity<Contact>().Property(c => c.Address).HasMaxLength(255);

            modelBuilder.Entity<Contact>().HasIndex(c => c.TelephoneNumber).IsUnique();

        }
    }
}
