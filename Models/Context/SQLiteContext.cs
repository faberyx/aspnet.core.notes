using Microsoft.Data.Entity;
using notes_manager.Models.Entities;

namespace notes_manager.Models.Context
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Note> Note { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             
        }
    }
}
