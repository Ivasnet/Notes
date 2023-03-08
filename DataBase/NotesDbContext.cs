using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataBase
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=NotesDataBase", option => 
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("NotesTable", "NotesSchema");
            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                //entity.HasIndex(e => e.Name).IsUnique;
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}