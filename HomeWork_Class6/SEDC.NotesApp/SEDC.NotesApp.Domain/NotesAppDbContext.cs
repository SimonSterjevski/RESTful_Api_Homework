using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.Domain
{
    public class NotesAppDbContext:DbContext
    {
        public NotesAppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Note>()
                .Property(x => x.Text)
                .HasMaxLength(100)
                .IsRequired(); // not null
            modelBuilder.Entity<Note>()
                .Property(x => x.Color)
                .HasMaxLength(30);
            //relation
            modelBuilder.Entity<Note>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Note>()
                .HasOne(x => x.Tag);

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Ignore(x => x.Age); //don't make a column for Age prop
            modelBuilder.Entity<User>()
                .Property(x => x.Address)
                .HasMaxLength(150);
            //SEED

            modelBuilder.Entity<Tag>()
                .HasData(new Tag
                {
                    Id = 1,
                    Type = "Health"
                }, new Tag
                {
                    Id = 2,
                    Type = "Work"
                }
                );

            modelBuilder.Entity<Note>()
                .HasData(new Note
                {
                    Id = 1,
                    Text = "Stop drinking",
                    Color = "Red",
                    TagId = 1,
                    UserId = 2
                }, new Note
                {
                    Id = 2,
                    Text = "Be focused",
                    Color = "Yellow",
                    TagId = 2,
                    UserId = 1
                }, new Note
                {
                    Id = 3,
                    Text = "Drink lemon juice",
                    Color = "Yellow",
                    TagId = 1,
                    UserId = 2
                }
                );

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "Mark",
                    LastName = "Smith",
                    UserName = "MSmith",
                    Address = "Adress1",
                    Age = 43
                }, new User
                {
                    Id = 2,
                    FirstName = "Ema",
                    LastName = "Smith",
                    UserName = "ESmith",
                    Address = "Adress1",
                    Age = 38
                }
                );
        }
    }
}
