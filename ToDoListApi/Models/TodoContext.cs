using System;
using Microsoft.EntityFrameworkCore;
namespace TareaApi.Models
{

        public class TodoContext : DbContext
        {
            // Crear context para la base de datos.
            public TodoContext(DbContextOptions<TodoContext> options)
                : base(options)
            {
            }

            public DbSet<TodoItem> TodoItems { get; set; }
        }

}
