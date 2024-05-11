using Microsoft.EntityFrameworkCore;

namespace dotnettodolist.Data
{
    public class TodosContext : DbContext
    {
        public TodosContext(DbContextOptions<TodosContext> options) 
            : base(options)
        {
        }

        public DbSet<dotnettodolist.Models.Todos> Todos { get; set; } = default!;
    }
}
